using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
namespace EasyFrameWork
{
    public class Global : MonoSingleton<Global>
    {
        public static bool IsFirstOpen = true;
        public int MaxLevel;
        public static int CurLevel;
        public static GameLevelInfo GameLevelInfo;
        public static bool IsPlaySound;
        public static Audio m_BGAudio;
        public AudioClip BGClip;
        public bool TestNextDay;

        /// <summary>
        /// 拥有的所有的角色的信息
        /// </summary>
        public static List<HeroInfo> HeroInfoList
        {
            get
            {
                //每次调用都是满状态
                return SimulatedDatabase.Instance.GetHeroInfoList();
            }
        }

        /// <summary>
        /// 配置中的英雄 roleId
        /// </summary>
        public static List<long> FightHeroList
        {
            get
            {
                return SimulatedDatabase.Instance.GetFightHeroList();
            }
        }

        private void Awake()
        {
            PlayerCtrl t = PlayerCtrl.Instance;
        }
        private void Start()
        {
            //与过去时间对比，如果是下一天

            if (TestNextDay)
            {
                //重置所有的每日任务
                SimulatedDatabase.Instance.ResetEveryDayTask();
                //发布每日任务任务
                List<PlayerInfo.SimpleTaskInfo> allTaskList = SimulatedDatabase.Instance.GetAllEveryDayTask();
                for (int i = 0; i < allTaskList.Count; i++)
                {
                    TaskManager.Instance.TakeTheTask(allTaskList[i].TaskId);
                }
            }
            else
            {
                //重新接任务
                List<PlayerInfo.SimpleTaskInfo> allTaskList = SimulatedDatabase.Instance.GetAllEveryDayTask();
                for (int i = 0; i < allTaskList.Count; i++)
                {
                    if (!allTaskList[i].IsFinish)
                    {
                        Task t = TaskManager.Instance.TakeTheTask(allTaskList[i].TaskId);
                        //重新执行任务
                        for (int j = 0; j < allTaskList[i].ConditionInfoList.Count; j++)
                        {
                            int conditionID = allTaskList[i].ConditionInfoList[j].Id;
                            int curCount = allTaskList[i].ConditionInfoList[j].CurCount;
                            TaskManager.Instance.ExecuteTheTask(conditionID, curCount);
                        }
                        if (allTaskList[i].GetReward)
                        {
                            t.TaskStatus = TaskStatus.GetReward;
                        }

                    }
                }

            }
            DontDestroyOnLoad(this.gameObject);
            //UISceneCtrl.Instance.Load(UISceneType.Level);
            //ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            //SceneManager.LoadScene("Init");

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                bool has = TaskManager.Instance.TryMathchCondition(1);
                if (has)
                {
                    //完成任务
                    TaskManager.Instance.ExecuteTheTask(1001, 1);
                }
            }
        }
        public void SetMusic()
        {
            if (IsPlaySound)
            {
                if (m_BGAudio == null)
                {
                    int id = EazySoundManager.PlayMusic(BGClip);
                    m_BGAudio = EazySoundManager.GetAudio(id);
                }
                EazySoundManager.GlobalSoundsVolume = 1;
                EazySoundManager.GlobalUISoundsVolume = 1;
                m_BGAudio.Play();
            }
            else
            {
                EazySoundManager.GlobalSoundsVolume = 0;
                EazySoundManager.GlobalUISoundsVolume = 0;
                EazySoundManager.StopAll(1);
            }
        }
        public static bool OnWin()
        {
            int level = CurLevel;
            level++;
            int maxLevel = Global.Instance.MaxLevel;
            if (level > maxLevel)
            {
                return true;
            }
            if (level >= maxLevel)
            {
                maxLevel = level;
            }
            CurLevel = level;
            if (CurLevel >= GameLevelInfo.MaxCanPlay)
            {
                GameLevelInfo.MaxCanPlay = CurLevel;
            }
            return false;
        }

        public string GetSkillIconByRanggeAndType(int attackRange, int attackType)
        {
            return "";
        }
    }
}
