using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;
using LitJson;
using System.IO;
using System.Text;

namespace EasyFrameWork
{
    /// <summary>
    /// 模拟数据库
    /// </summary>
	public class SimulatedDatabase : Singleton<SimulatedDatabase>
    {
        //英雄信息 HeroLevel（基础数值）* Hero(系数)*Job（系数）*HeroStar(系数)
        private PlayerInfo m_PlayerInfo;
        protected PlayerInfo PlayerInfo
        {
            get
            {
                if (m_PlayerInfo == null)
                {
                    m_PlayerInfo = PlayerInfo.JosnToPlayerInfo();
                }
                return m_PlayerInfo;
            }
        }
        #region 关卡

        public int GetMaxCanPlayGameLevel(int grade)
        {
            if (grade < PlayerInfo.MaxCanPlayGameLevelGrade)
            {
                //返回最大关卡
                return GameLevelDBModel.Instance.GetList()[GameLevelDBModel.Instance.GetList().Count - 1].Id;
            }
            else if (grade > PlayerInfo.MaxCanPlayGameLevelGrade)
            {
                return -1001;// -1001 表示还没有到达的等级 0表示第一关
            }
            //返回当前
            if (PlayerInfo.MaxCanPlayGameLevelId == 0)//第一次打开
            {
                PlayerInfo.MaxCanPlayGameLevelId = GameLevelDBModel.Instance.GetList()[0].Id;
                PlayerInfo.ToJson();
            }
            return PlayerInfo.MaxCanPlayGameLevelId;
        }
        public void SetMaxCanPlayGameLevel(int grade, int gameLevelid)
        {
            if (grade < PlayerInfo.MaxCanPlayGameLevelGrade) return;
            else if (grade > PlayerInfo.MaxCanPlayGameLevelGrade)
            {
                int lastLevelId = GameLevelDBModel.Instance.GetList()[GameLevelDBModel.Instance.GetList().Count - 1].Id;
                if (gameLevelid != lastLevelId)
                {
                    Debug.LogError("错误；低级副本还未通关");
                    return;
                }
            }
            PlayerInfo.MaxCanPlayGameLevelId = gameLevelid;
            PlayerInfo.MaxCanPlayGameLevelGrade = grade;
            PlayerInfo.ToJson();
        }
        #endregion
        #region 货币
        public int GetCoin()
        {
            return PlayerInfo.Coin;
        }

        public void AddCoin(int coin)
        {
            PlayerInfo.Coin += coin;
            PlayerInfo.ToJson();
        }

        public void ReduceCoin(int coin)
        {
            PlayerInfo.Coin -= coin;
            if (m_PlayerInfo.Coin <= 0)
            {
                m_PlayerInfo.Coin = 0;
            }
            PlayerInfo.ToJson();
        }
        public int GetDebris()
        {
            return PlayerInfo.Debris;
        }
        public void AddDebris(int debris)
        {
            PlayerInfo.Debris += debris;
            PlayerInfo.ToJson();
        }
        public void ReduceDebris(int debris)
        {
            PlayerInfo.Debris -= debris;
            if (PlayerInfo.Debris <= 0)
            {
                PlayerInfo.Debris = 0;
            }
            PlayerInfo.ToJson();
        }
        #endregion
        #region 英雄编辑
        public List<long> GetFightHeroList()
        {
            return PlayerInfo.FightHeroList;
        }
        public bool AddFightHero(long roleID)
        {
            if (PlayerInfo.HeroList.Find(x => x.RoleId == roleID).RoleId != 0)
            {
                if (PlayerInfo.FightHeroList.Contains(roleID))
                {
                    return false;
                }
                PlayerInfo.FightHeroList.Add(roleID);
                return true;
            }
            return false;
        }
        public void ReplaceFightHero(long sourceRoleId, long targetRoleId)
        {
            RemoveFightHero(sourceRoleId);
            AddFightHero(targetRoleId);
        }


        public bool RemoveFightHero(long roleID)
        {
            if (PlayerInfo.HeroList.Find(x => x.RoleId == roleID).RoleId != 0)
            {
                if (PlayerInfo.FightHeroList.Contains(roleID))
                {
                    PlayerInfo.FightHeroList.Remove(roleID);
                    return true;
                }
                return false;
            }
            return false;
        }

        public List<HeroInfo> GetHeroInfoList()
        {
            if (PlayerInfo == null) { }
            List<HeroInfo> lst = new List<HeroInfo>();
            for (int i = 0; i < PlayerInfo.HeroList.Count; i++)
            {
                HeroInfo info = PrepareHeroInfo(PlayerInfo.HeroList[i]);
                if (info != null)
                {
                    lst.Add(info);
                }

            }
            return lst;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="heroId"></param>
        /// <param name="heroStar"></param>
        /// <returns></returns>
        public HeroInfo CreateHero(int heroId, int heroStar = 1)
        {
            PlayerInfo.SimpleHeroInfo heroInfo = new PlayerInfo.SimpleHeroInfo();
            heroInfo.RoleId = DateTime.Now.Ticks;
            heroInfo.HeroId = heroId;
            heroInfo.HeroLevel = PlayerInfo.Level;
            heroInfo.HeroStar = heroStar;
            HeroEntity heroEntity = HeroDBModel.Instance.Get(heroId);

            heroInfo.SkillInfoList = new List<PlayerInfo.SimpleSkillInfo>();
            string[] skillArr = heroEntity.UesSkill.Split('|');
            for (int i = 0; i < skillArr.Length; i++)
            {
                PlayerInfo.SimpleSkillInfo skillInfo = new PlayerInfo.SimpleSkillInfo();
                skillInfo.SkillId = skillArr[i].ToInt();
                skillInfo.SkillLevel = 1;
                heroInfo.SkillInfoList.Add(skillInfo);
            }
            HeroInfo info = PrepareHeroInfo(heroInfo);
            info.RoleId = heroInfo.RoleId;
            PlayerInfo.HeroList.Add(heroInfo);
            PlayerInfo.ToJson();
            return info;
        }


        private HeroInfo PrepareHeroInfo(PlayerInfo.SimpleHeroInfo simpInfo)
        {
            HeroEntity heroEntity = HeroDBModel.Instance.Get(simpInfo.HeroId);
            HeroLevelEntity heroLevelEntity = HeroLevelDBModel.Instance.GetList().Find(x => x.Level == simpInfo.HeroLevel);
            JobEntity jobEntity = JobDBModel.Instance.Get(heroEntity.Job);
            HeroStarEntity heroStarEntity = HeroStarDBModel.Instance.GetList().Find(x => x.Star == simpInfo.HeroStar);
            if (heroLevelEntity != null || heroLevelEntity != null || jobEntity != null || heroStarEntity != null)
            {
                HeroInfo info = new HeroInfo();
                info.RoleId = simpInfo.RoleId;
                info.RoleNickName = heroEntity.Name;
                info.Level = heroLevelEntity.Level;
                info.MaxHP = (int)(heroLevelEntity.Hp * heroEntity.Hp * jobEntity.Hp * heroStarEntity.Hp);
                info.CurrentHP = info.MaxHP;
                info.PhyAtk = (int)(heroLevelEntity.PhyAtk * heroEntity.PhyAtk * jobEntity.PhyAtk * heroStarEntity.PhyAtk);
                info.MgicAtk = (int)(heroLevelEntity.MgicAtk * heroEntity.MgicAtk * jobEntity.MgicAtk * heroStarEntity.MgicAtk);
                info.Cri = (int)(heroLevelEntity.Cri * heroEntity.Cri * jobEntity.Cri * heroStarEntity.Cri);
                info.CriValue = (int)(heroLevelEntity.CriValue * heroEntity.CriValue * jobEntity.CriValue * heroStarEntity.CriValue);
                info.PhyDef = (int)(heroLevelEntity.PhyDef * heroEntity.PhyDef * jobEntity.PhyDef * heroStarEntity.PhyDef);
                info.MgicDef = (int)((heroLevelEntity.MgicDef * heroEntity.MgicDef * jobEntity.MgicDef * heroStarEntity.MgicDef));
                info.HeroID = heroEntity.Id;
                info.JobID = jobEntity.Id;
                info.HeroStar = simpInfo.HeroStar;
                for (int j = 0; j < simpInfo.SkillInfoList.Count; j++)
                {
                    RoleInfoSkill skillInfo = new RoleInfoSkill();
                    skillInfo.SkillId = simpInfo.SkillInfoList[j].SkillId;
                    skillInfo.SKillLevel = simpInfo.SkillInfoList[j].SkillLevel;
                    info.SkillList.Add(skillInfo);
                }
                return info;
            }
            return null;
        }
        #endregion
        #region 角色属性
        /// <summary>
        /// 玩家升级
        /// </summary>
        public void UpgradePlayer()
        {
            int maxLevel = HeroLevelDBModel.Instance.GetList().Count;
            if (PlayerInfo.Level >= maxLevel)
            {
                //提示
                //TODO
                return;
            }
            PlayerInfo.Level++;
        }
        public int GetLevel()
        {
            return PlayerInfo.Level;
        }
        /// <summary>
        /// 英雄升星
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public HeroInfo UpgradeHeroStart(long roleId)
        {
            PlayerInfo.SimpleHeroInfo simpleHeroInfo = PlayerInfo.HeroList.Find(x => x.RoleId == roleId);
            PlayerInfo.HeroList.Remove(simpleHeroInfo);
            if (simpleHeroInfo.HeroStar < 5)
            {
                simpleHeroInfo.HeroStar++;
            }
            PlayerInfo.HeroList.Add(simpleHeroInfo);
            HeroInfo heroInfo = PrepareHeroInfo(simpleHeroInfo);
            PlayerInfo.ToJson();
            return heroInfo;
        }
        #endregion
        #region 技能
        /// <summary>
        /// 英雄升技能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="skillLevel"></param>
        /// <returns></returns>
        public HeroInfo UpgradeRoleSkill(int roleId, int skillLevel)
        {
            PlayerInfo.ToJson();
            return null;
        }
        #endregion
        #region 任务相关

        //获取每日任务
        public List<PlayerInfo.SimpleTaskInfo> GetAllEveryDayTask()
        {
            return PlayerInfo.TaskList;
        }
        //重置每日任务
        public void ResetEveryDayTask()
        {
            PlayerInfo.TaskList.Clear();
            List<TaskEntity> taskEntityList = TaskDBModel.Instance.GetList();
            for (int i = 0; i < taskEntityList.Count; i++)
            {
                PlayerInfo.SimpleTaskInfo info = new PlayerInfo.SimpleTaskInfo();
                info.TaskId = taskEntityList[i].Id;
                info.IsFinish = false;
                info.GetReward = false;
                info.ConditionInfoList = new List<PlayerInfo.SimpleTaskInfo.SimpleConditionInfo>();
                string[] conditionIdArr = taskEntityList[i].Conditions.Split('|');
                for (int j = 0; j < conditionIdArr.Length; j++)
                {
                    PlayerInfo.SimpleTaskInfo.SimpleConditionInfo conditionInfo = new PlayerInfo.SimpleTaskInfo.SimpleConditionInfo();
                    int conditionId = conditionIdArr[j].ToInt();
                    TaskConditionEntity conditionEntity = TaskConditionDBModel.Instance.Get(conditionId);
                    if (conditionEntity == null)
                    {
                        Debug.LogError("错误：任务条件实体找不到");
                        continue;
                    }
                    conditionInfo.Id = conditionId;
                    conditionInfo.CurCount = 0;
                    info.ConditionInfoList.Add(conditionInfo);
                }
                PlayerInfo.TaskList.Add(info);

            }
            PlayerInfo.ToJson();
        }
        //更新每日任务
        public void UpdateEveryDayTask(int taskId, int conditionId, int count)
        {
            //
            PlayerInfo.SimpleTaskInfo info = PlayerInfo.TaskList.Find(x => x.TaskId == taskId);
            PlayerInfo.SimpleTaskInfo.SimpleConditionInfo condiftion = info.ConditionInfoList.Find(x => x.Id == conditionId);
            info.ConditionInfoList.RemoveAll(x => x.Id == conditionId);
            condiftion.CurCount = count;
            info.ConditionInfoList.Add(condiftion);
            PlayerInfo.TaskList.RemoveAll(x => x.TaskId == taskId);
            PlayerInfo.TaskList.Add(info);
            PlayerInfo.ToJson();
        }

        public void GetReward(int taskId)
        {
            PlayerInfo.SimpleTaskInfo info = PlayerInfo.TaskList.Find(x => x.TaskId == taskId);
            info.GetReward = true;
            PlayerInfo.TaskList.RemoveAll(x => x.TaskId == taskId);
            PlayerInfo.TaskList.Add(info);
            PlayerInfo.ToJson();

        }
        #endregion
    }
    /// <summary>
    /// 玩家信息
    /// </summary>
    [SerializeField]
    public class PlayerInfo
    {
        [NonSerialized]
        public static string Path = Application.streamingAssetsPath + "/PlayerInfo.txt";
        public int Coin = 0;
        public int Debris = 0;
        public int MaxCanPlayGameLevelGrade = 0;
        public int MaxCanPlayGameLevelId = 0;
        private int m_Level = 1;


        [SerializeField]
        public struct SimpleHeroInfo
        {
            public long RoleId;//角色ID 唯一的Key，由时间赋值
            public int HeroId;
            public int HeroLevel; //英雄等级跟着玩家走，玩家多少级，英雄就多少级
            public int HeroStar;
            public List<SimpleSkillInfo> SkillInfoList;

            public static SimpleHeroInfo CopyForm(SimpleHeroInfo source)
            {
                SimpleHeroInfo taret = new SimpleHeroInfo();
                taret.RoleId = source.RoleId;
                taret.HeroId = source.HeroId;
                taret.HeroLevel = source.HeroLevel;
                taret.HeroStar = source.HeroStar;
                taret.SkillInfoList = source.SkillInfoList;
                return taret;
            }
        }
        [SerializeField]
        public struct SimpleSkillInfo
        {
            public int SkillId;
            public int SkillLevel;
        }
        [SerializeField]
        public struct SimpleTaskInfo
        {
            [SerializeField]
            public struct SimpleConditionInfo
            {
                public int Id;
                public int CurCount;
            }

            public int TaskId;
            public List<SimpleConditionInfo> ConditionInfoList;
            public bool IsFinish;
            public bool GetReward;
        }
        /// <summary>
        /// 所有的英雄
        /// </summary>
        public List<SimpleHeroInfo> HeroList = new List<SimpleHeroInfo>();
        /// <summary>
        /// 出战的英雄
        /// </summary>
        public List<long> FightHeroList = new List<long>();
        /// <summary>
        /// 任务完成情况
        /// </summary>
        public List<SimpleTaskInfo> TaskList = new List<SimpleTaskInfo>();

        public PlayerInfo()
        {

        }

        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                m_Level = value;
                for (int i = 0; i < HeroList.Count; i++)
                {
                    SimpleHeroInfo target = SimpleHeroInfo.CopyForm(HeroList[i]);
                    target.HeroLevel = m_Level;
                    HeroList[i] = target;
                }
            }
        }

        public void ToJson()
        {
            string str = JsonMapper.ToJson(this);
            FileStream fs = null;
            if (File.Exists(Path))
            {
                fs = File.Create(Path);
                fs.Close();
            }
            fs = File.OpenWrite(Path);
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        public static PlayerInfo JosnToPlayerInfo()
        {
            if (!File.Exists(Path))
            {
                return new PlayerInfo();
            }
            FileStream fs = File.OpenRead(Path);
            byte[] buffer = new Byte[fs.Length];
            int t = fs.Read(buffer, 0, buffer.Length);
            string str = Encoding.UTF8.GetString(buffer);
            PlayerInfo info = JsonMapper.ToObject<PlayerInfo>(str);
            if (info == null)
            {
                Debug.LogError("错误：文件读取错误");
            }
            fs.Close();
            return info;
        }


    }
}
