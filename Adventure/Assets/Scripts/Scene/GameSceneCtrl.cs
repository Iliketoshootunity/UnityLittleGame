using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using EasyFrameWork.UI;
using System;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    /// <summary>
    ///关卡流程
    /// </summary>
    public enum LevelProcedure
    {
        None,
        OpenAni,        //开场动画
        LevelIntrol,    //副本介绍
        HeroEditor,     //阵容编辑
        Switch,         //切换
        Player,         //玩家回合
        Enemy,          //敌人回合
        Win,         //胜利或失败
        Fail

    }

    public class GameSceneCtrl : GameSceneCtrlBase
    {
        public int CurLevel;
        public int CurGrade;
        public EnemyFaction EnemyFaction;
        public HeroFaction HeroFaction;
        public bool PlayerRound;
        private int BormMonsterCount;

        public static GameSceneCtrl Instance;

        public List<RoleCtrl> MonsterList = new List<RoleCtrl>();
        public List<RoleCtrl> HeroList = new List<RoleCtrl>();
        public List<RoleCtrl> ForwardHeroList = new List<RoleCtrl>();
        public List<RoleCtrl> BackwardHeroList = new List<RoleCtrl>();
        public List<HeroInfo> HeroInfoList = new List<HeroInfo>();

        public List<HeroPlatform> ForwardHeroPlatformList;
        public List<HeroPlatform> BackwardHeroPlatformList;
        private UISceneViewBase m_View;

        private StateMachine<LevelProcedure> m_LevelProcedure;
        public StateMachine<LevelProcedure> GetLevelProcedure
        {
            get
            {
                return m_LevelProcedure;
            }
        }
        protected override void OnAwake()
        {
            base.OnAwake();
            Instance = this;
        }
        protected override void OnStart()
        {
            base.OnStart();
            m_LevelProcedure = new StateMachine<LevelProcedure>(this.gameObject, true, "LevelProcedureEvent");
            StateMachineDispatcher.Instance.AddEventListen("LevelProcedureEvent", OnLevelProcedureCallBack);
            m_LevelProcedure.ChangeState(LevelProcedure.OpenAni);

            GameLevelCtrl test = GameLevelCtrl.Instance;

            EnemyFaction.OnRoundEnd += OnRoundEndCallBack;
            HeroFaction.OnRoundEnd += OnRoundEndCallBack;

            m_View = UISceneCtrl.Instance.Load(UISceneType.Game).GetComponent<UISceneViewBase>();
            if (m_View != null)
            {
                m_View.OnLoadComplete += OnMainUIComplete;
                ((UIGameSceneView)m_View).ClickFight += OnClickFightCallBack;
            }
        }



        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            m_View.OnLoadComplete -= OnMainUIComplete;
            ((UIGameSceneView)m_View).ClickFight -= OnClickFightCallBack;
            EnemyFaction.OnRoundEnd -= OnRoundEndCallBack;
            HeroFaction.OnRoundEnd -= OnRoundEndCallBack;

        }

        private void OnApplicationQuit()
        {
            //public List<RoleCtrl> MonsterList = new List<RoleCtrl>();
            //public List<RoleCtrl> HeroList = new List<RoleCtrl>();
            //public List<RoleCtrl> ForwardHeroList = new List<RoleCtrl>();
            //public List<RoleCtrl> BackwardHeroList = new List<RoleCtrl>();
            //public List<HeroInfo> HeroInfoList = new List<HeroInfo>();
            for (int i = 0; i < MonsterList.Count; i++)
            {
                GameObject.Destroy(MonsterList[i].gameObject);
            }
            for (int i = 0; i < HeroList.Count; i++)
            {
                GameObject.Destroy(HeroList[i].gameObject);
            }
            GameObject.Destroy(GridManager.Instance.gameObject);
        }


        protected override void OnMainUIComplete()
        {
            GridManager.Instance.InitializationNodes();
            CreateMonster();
            PlayerCtrl.Instance.BindRoleCtrlEvent(HeroList);
            PlayerCtrl.Instance.UpdateAllHeroInfo();
        }

        #region 游戏流程

        public LevelProcedure GetCurLevelProcedure()
        {
            return m_LevelProcedure.CurState;
        }
        private void OnLevelProcedureCallBack(object p)
        {
            StateMachineEvents<LevelProcedure> events = (StateMachineEvents<LevelProcedure>)p;
            switch (events.CurState)
            {
                case LevelProcedure.OpenAni:
                    OpenAni();
                    break;
                case LevelProcedure.LevelIntrol:
                    LevelIntrol();
                    break;
                case LevelProcedure.HeroEditor:
                    HeroEditor();
                    break;
                case LevelProcedure.Switch:
                    Switch();
                    break;
                case LevelProcedure.Player:
                    Player();
                    break;
                case LevelProcedure.Enemy:
                    Enemy();
                    break;
                case LevelProcedure.Win:
                    Win();
                    break;
                case LevelProcedure.Fail:
                    Fail();
                    break;
            }
        }

        private void OpenAni()
        {
            Debug.Log("进入开场动画");
            m_LevelProcedure.ChangeState(LevelProcedure.LevelIntrol);
        }
        private void LevelIntrol()
        {
            Debug.Log("进入副本介绍");
            m_LevelProcedure.ChangeState(LevelProcedure.HeroEditor);
        }
        private void HeroEditor()
        {
            Debug.Log("进入英雄编辑");
        }

        private void Switch()
        {
            Debug.Log("切换中.....");
            PlayerRound = !PlayerRound;
            GameLevelCtrl.Instance.ShowSwitchView();
        }

        private void Player()
        {
            Debug.Log("进入玩家回合");
            //玩家回合开始
            HeroFaction.RoundStart();
        }
        private void Enemy()
        {
            Debug.Log("进入敌人回合");
            EnemyFaction.RoundStart();
        }

        private void Win()
        {
            Debug.Log("胜利......");
        }
        private void Fail()
        {
            Debug.Log("失败......");
        }
        #endregion

        #region 事件


        private void OnRoundEndCallBack()
        {
            //回合结束切到切换阵营流程
            if (HeroList.Count > 0 && MonsterList.Count > 0)
            {
                m_LevelProcedure.ChangeState(LevelProcedure.Switch);
            }
        }



        private void OnClickFightCallBack()
        {
            m_LevelProcedure.ChangeState(LevelProcedure.Switch);
        }

        public void OnSwitchEndCallBack()
        {
            if (HeroList.Count > 0 && MonsterList.Count > 0)
            {
                if (PlayerRound)
                {
                    m_LevelProcedure.ChangeState(LevelProcedure.Player);
                }
                else
                {
                    m_LevelProcedure.ChangeState(LevelProcedure.Enemy);
                }
            }

        }

        private void OnRoleDestoryCallBack(Transform obj)
        {
            if (m_LevelProcedure.CurState == LevelProcedure.HeroEditor)
            {
                RoleCtrl roleCtrl = obj.GetComponent<RoleCtrl>();
                if (roleCtrl.CurRoleType == RoleType.Hero)
                {
                    HeroPlatform platform = GetHeroPlatform(roleCtrl.CurRoleInfo.RoleId);
                    platform.RefreshRoleId(0);
                    RemoveHero(roleCtrl);
                }
            }

        }

        private void OnRoleDieCallBack(RoleCtrl ctrlTemp)
        {
            if (ctrlTemp.CurRoleType == RoleType.Hero)
            {
                HeroPlatform platform = GetHeroPlatform(ctrlTemp.CurRoleInfo.RoleId);
                platform.RefreshRoleId(0);
                RemoveHero(ctrlTemp);
                if (HeroList.Count <= 0)
                {
                    m_LevelProcedure.ChangeState(LevelProcedure.Fail);
                }
            }
            else
            {
                if (MonsterList.Contains(ctrlTemp))
                {
                    MonsterList.Remove(ctrlTemp);
                    if (MonsterList.Count <= 0)
                    {
                        m_LevelProcedure.ChangeState(LevelProcedure.Win);
                    }
                }
                else
                {
                    Debug.LogError("错误；角色被已经被移除了");
                }
            }
            ctrlTemp.OnRoleDie -= OnRoleDieCallBack;


        }

        #endregion

        private int m_TempIndex;
        //创建怪物
        private void CreateMonster()
        {
            //if (m_CurrentCreateCount > 0) return;
            List<GameLevelMonsterEntity> gameLevelMonsterEntityList = GameLevelMonsterDBModel.Instance.GetMonsterListByLevelAndGeade(CurLevel, CurGrade);
            for (int i = 0; i < gameLevelMonsterEntityList.Count; i++)
            {
                if (String.IsNullOrEmpty(gameLevelMonsterEntityList[i].SpriteRowAndCol)) continue;
                string[] rowAndColArr = gameLevelMonsterEntityList[i].SpriteRowAndCol.Split('|');
                for (int j = 0; j < rowAndColArr.Length; j++)
                {
                    SpriteEntity spriteEntity = SpriteDBModel.Instance.GetSpriteEntityById(gameLevelMonsterEntityList[i].SpriteId);
                    if (spriteEntity != null)
                    {
                        GameObject go = RoleMgr.Instance.LoadRole(RoleType.Monster, spriteEntity.PrefabName);
                        if (go != null)
                        {
                            RoleCtrl ctrl = go.GetComponent<RoleCtrl>();
                            if (ctrl != null)
                            {
                                ctrl.OnRoleDie = OnRoleDieCallBack;
                                ctrl.OnRoleDestory = OnRoleDestoryCallBack;

                                MonsterInfo info = new MonsterInfo(spriteEntity);
                                info.RoleId = ++m_TempIndex;
                                info.RoleNickName = spriteEntity.Name;
                                info.Level = spriteEntity.Level;
                                info.MaxHP = spriteEntity.Hp;
                                info.CurrentHP = info.MaxHP;
                                info.PhyAtk = spriteEntity.PhyAtk;
                                info.MgicAtk = spriteEntity.MgicAtk;
                                info.PhyDef = spriteEntity.PhyDef;
                                info.MgicDef = spriteEntity.MgicDef;
                                info.Cri = spriteEntity.Cri;
                                info.CriValue = spriteEntity.CriValue;
                                info.SpriteEntity = spriteEntity;
                                ctrl.AttackRange = spriteEntity.AttackRange;
                                ctrl.MoveStep = spriteEntity.MoveCellCount;
                                //位置赋值
                                string[] arr = rowAndColArr[j].Split('_');
                                int row = arr[0].ToInt();
                                int col = arr[1].ToInt();
                                Node node = GridManager.Instance.GetNode(row, col);
                                ctrl.transform.position = node.Position;
                                ctrl.Init(RoleType.Monster, info, new MonsterAI(ctrl, info));
                                BormMonsterCount++;
                                MonsterList.Add(ctrl);

                            }

                        }
                    }
                }
            }
        }

        //平台上是否有英雄
        public bool HasPlatform(Vector2 screenPos)
        {
            HeroPlatform platform = GetHeroPlatformByScreenPos(screenPos);
            if (platform != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //替换平台上的英雄
        public void ReplaceHeroOnPlatform(long sourceRoleId, long targetRole, Vector2 screenPos)
        {
            RoleCtrl ctrl = GetHero(sourceRoleId);

            DestroyImmediate(ctrl.gameObject);
            CreateHero(targetRole, screenPos);
        }
        //交换平台上的英雄
        public void SwapHeroOnPlatfrom(long sourceRoleId, long targetRoleId)
        {
            RoleCtrl sourcrRole = GetHero(sourceRoleId);
            RoleCtrl targetRole = GetHero(targetRoleId);

            HeroPlatform sourcrePlatform = GetHeroPlatform(sourceRoleId);
            HeroPlatform targetPlatform = GetHeroPlatform(targetRoleId);

            if (sourcrRole == null || targetRole == null || sourcrePlatform == null || targetPlatform == null)
            {
                Debug.LogError("错误");
                return;
            }


            ReplaceHero(sourcrRole, targetPlatform);
            ReplaceHero(targetRole, sourcrePlatform);

            sourcrePlatform.RefreshRoleId(targetRole.CurRoleInfo.RoleId);
            targetPlatform.RefreshRoleId(sourcrRole.CurRoleInfo.RoleId);

            sourcrRole.RefreshPlatfrom(targetPlatform);
            sourcrRole.StandOnPlatfrom();
            targetRole.RefreshPlatfrom(sourcrePlatform);
            targetRole.StandOnPlatfrom();



        }
        //刷新英雄平台
        public void RefreshHeroPlatform(long roleId, HeroPlatform platfrom)
        {
            RoleCtrl role = GetHero(roleId);
            HeroPlatform sourcePlatform = GetHeroPlatform(roleId);

            ReplaceHero(role, platfrom);

            sourcePlatform.RefreshRoleId(0);
            platfrom.RefreshRoleId(role.CurRoleInfo.RoleId);

            role.RefreshPlatfrom(platfrom);
            role.StandOnPlatfrom();
        }
        //在平台上创建英雄
        public void CreateHero(long roleId, Vector2 screenPos)
        {
            HeroInfo info = Global.HeroInfoList.Find(x => x.RoleId == roleId);
            if (info != null)
            {
                HeroEntity heroEntity = HeroDBModel.Instance.Get(info.HeroID);
                GameObject go = RoleMgr.Instance.LoadRole(RoleType.Hero, heroEntity.PrefabName);
                RoleCtrl ctrl = go.GetComponent<RoleCtrl>();
                ctrl.Init(RoleType.Hero, info, null);
                HeroPlatform platform = GetHeroPlatformByScreenPos(screenPos);
                platform.RefreshRoleId(ctrl.CurRoleInfo.RoleId);
                ctrl.RefreshPlatfrom(platform);
                ctrl.StandOnPlatfrom();
                go.transform.position = platform.transform.TransformPoint(platform.StandPos);
                bool inFoward = ForwardHeroPlatformList.Contains(platform);
                if (inFoward)
                {
                    AddHero(ctrl, true);
                }
                else
                {
                    AddHero(ctrl, false);
                }
                ctrl.OnRoleDestory += OnRoleDestoryCallBack;
                ctrl.OnRoleDie += OnRoleDieCallBack;
                return;
            }
            Debug.Log("错误：在模拟服务器上找不到持有的英雄信息");

        }
        #region 获取平台
        public HeroPlatform GetHeroPlatformByScreenPos(Vector2 screenPos)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.up, 0.01f);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null)
                {
                    HeroPlatform h = hits[i].collider.GetComponent<HeroPlatform>();
                    if (h != null)
                    {
                        return h;
                    }
                }
            }
            return null;
        }


        public HeroPlatform GetHeroPlatform(long roleId)
        {
            HeroPlatform platform = ForwardHeroPlatformList.Find(x => x.RoleId == roleId);
            if (platform == null)
            {
                platform = BackwardHeroPlatformList.Find(x => x.RoleId == roleId);
                if (platform == null)
                {
                    Debug.Log("错误：平台不存在");
                    return null;
                }
                else
                {
                    CheckHeroAndPlatformNoDifference(roleId, false);
                }
            }
            else
            {
                CheckHeroAndPlatformNoDifference(roleId, true);
            }
            return platform;
        }
        #endregion
        #region 增，删，替换,获取英雄
        public RoleCtrl GetHero(long roleId)
        {
            RoleCtrl ctrl = ForwardHeroList.Find(x => x.CurRoleInfo.RoleId == roleId);
            if (ctrl == null)
            {
                ctrl = BackwardHeroList.Find(x => x.CurRoleInfo.RoleId == roleId);
                if (ctrl == null)
                {
                    Debug.Log("错误：英雄不存在");
                    return null;
                }
                else
                {
                    CheckHeroAndPlatformNoDifference(roleId, false);
                }
            }
            else
            {
                CheckHeroAndPlatformNoDifference(roleId, true);
            }
            return ctrl;
        }

        public void AddHero(RoleCtrl ctrl, bool forward)
        {
            if (forward)
            {
                ForwardHeroList.Add(ctrl);
            }
            else
            {
                BackwardHeroList.Add(ctrl);
            }
            HeroList.Add(ctrl);
        }

        public void ReplaceHero(RoleCtrl source, HeroPlatform target)
        {
            if (ForwardHeroList.Contains(source))
            {
                if (ForwardHeroPlatformList.Contains(target)) return;
                ForwardHeroList.Remove(source);
                BackwardHeroList.Add(source);
            }
            else if (BackwardHeroList.Contains(source))

            {
                if (BackwardHeroPlatformList.Contains(target)) return;
                BackwardHeroList.Remove(source);
                ForwardHeroList.Add(source);
            }
            else
            {
                Debug.Log("错误：还没有加入");
            }
        }

        public void RemoveHero(RoleCtrl ctrl)
        {
            if (ForwardHeroList.Contains(ctrl))
            {
                ForwardHeroList.Remove(ctrl);
            }
            else
            {
                if (BackwardHeroList.Contains(ctrl))
                {
                    BackwardHeroList.Remove(ctrl);
                }
                else
                {
                    Debug.LogError("错误：找不到英雄");
                    return;
                }
            }
            HeroList.Remove(ctrl);
        }

        public void RemoveHero(long roleId)
        {
            RoleCtrl ctrl = HeroList.Find(x => x.CurRoleInfo.RoleId == roleId);
            if (ctrl == null)
            {
                Debug.LogError("错误：找不到英雄");
                return;
            }
            else
            {
                RemoveHero(ctrl);
            }
        }
        public RoleCtrl GetHeroByScreenPos(Vector2 screenPos)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.up, 0.01f);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null)
                {
                    RoleCtrl h = hits[i].collider.GetComponent<RoleCtrl>();
                    if (h != null)
                    {
                        return h;
                    }
                }
            }
            return null;
        }
        #endregion
        #region 检测异常
        private bool CheckHeroAndPlatformNoDifference(long roleId, bool forward)
        {
            RoleCtrl heroTemp = HeroList.Find(x => x.CurRoleInfo.RoleId == roleId);
            if (heroTemp == null)
            {
                Debug.Log("错误：没有这个英雄");
                return false;
            }
            if (forward)
            {
                HeroPlatform forwardPlatform = ForwardHeroPlatformList.Find(x => x.RoleId == roleId);
                RoleCtrl hero = ForwardHeroList.Find(x => x.CurRoleInfo.RoleId == roleId);
                if (hero == null || forwardPlatform == null)
                {
                    Debug.Log("错误：平台和英雄 不一致");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                HeroPlatform forwardPlatform = BackwardHeroPlatformList.Find(x => x.RoleId == roleId);
                RoleCtrl hero = BackwardHeroList.Find(x => x.CurRoleInfo.RoleId == roleId);
                if (hero == null || forwardPlatform == null)
                {
                    Debug.Log("错误：平台和英雄 不一致");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
        #region 技能位置
        //0 横轴 1 竖轴 2十字
        public List<Vector2> GetPositionsByShape(int shape, Vector2 origin, int range)
        {
            List<Vector2> pos = new List<Vector2>();
            switch (shape)
            {
                case 0:
                    pos = GetPositionsByHorizantal(origin, range);
                    break;
                case 1:
                    pos = GetPositionsByVerticall(origin, range);
                    break;
                case 2:
                    pos = GetPositionsByCross(origin, range);
                    break;
                default:
                    Debug.LogError("错误：没有这个形状");
                    break;
            }
            return pos;
        }

        private List<Vector2> GetPositionsByHorizantal(Vector2 origin, int range)
        {
            List<Vector2> posArr = new List<Vector2>();
            float length = GridManager.Instance.CellWidth * range;
            Vector2 tempOrigin = Vector2.zero;
            if (range % 2 == 0)
            {
                tempOrigin = origin - new Vector2((range / 2f - 1) * GridManager.Instance.CellWidth, 0);
            }
            else
            {
                tempOrigin = origin - new Vector2((range / 2f) * GridManager.Instance.CellWidth - GridManager.Instance.CellWidth / 2, 0);
            }
            //Vector2 tempOrigin = origin - new Vector2(length / 2 + GridManager.Instance.GridCellSize / 2, 0);
            for (int i = 0; i < range; i++)
            {
                Vector2 pos = tempOrigin + new Vector2(i * GridManager.Instance.CellWidth, 0);
                posArr.Add(pos);
            }
            return posArr;
        }

        private List<Vector2> GetPositionsByVerticall(Vector2 origin, int range)
        {
            List<Vector2> posArr = new List<Vector2>();
            float length = GridManager.Instance.CellHeight * range;
            Vector2 tempOrigin = Vector2.zero;
            //Vector2 tempOrigin = origin - new Vector2(0, length / 2 + GridManager.Instance.GridCellSize / 2);
            if (range % 2 == 0)
            {
                tempOrigin = origin - new Vector2(0, (range / 2f - 1) * GridManager.Instance.CellHeight);
            }
            else
            {
                tempOrigin = origin - new Vector2(0, (range / 2f) * GridManager.Instance.CellHeight - GridManager.Instance.CellHeight / 2);
            }
            for (int i = 0; i < range; i++)
            {
                Vector2 pos = tempOrigin + new Vector2(0, i * GridManager.Instance.CellHeight);
                posArr.Add(pos);
            }
            return posArr;
        }

        private List<Vector2> GetPositionsByCross(Vector2 origin, int range)
        {
            List<Vector2> posArr1 = GetPositionsByHorizantal(origin, range);
            List<Vector2> posArr2 = GetPositionsByVerticall(origin, range);
            List<Vector2> posArr3 = new List<Vector2>();
            for (int i = 0; i < posArr1.Count; i++)
            {
                //if (posArr3.Contains(posArr1[i])) continue;
                posArr3.Add(posArr1[i]);
            }
            for (int i = 0; i < posArr2.Count; i++)
            {
                //if (posArr3.Contains(posArr2[i])) continue;
                posArr3.Add(posArr2[i]);
            }
            return posArr3;
        }
        #endregion
        private void OnGUI()
        {
            if (GUI.Button(new Rect(30, 50, 50, 50), "胜利"))
            {
                UIViewMgr.Instance.OpenView(UIViewType.Win);
            }
            if (GUI.Button(new Rect(30, 100, 50, 50), "失败"))
            {
                UIViewMgr.Instance.OpenView(UIViewType.Fail);
            }
        }
    }
}
