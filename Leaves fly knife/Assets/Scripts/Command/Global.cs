using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;
using System;

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
        public AudioClip BtnClip;
        public static int TestLevel = 15;
        private void Awake()
        {
            //PlayerPrefs.SetString("GameLevelInfo", null);
            string info = PlayerPrefs.GetString("GameLevelInfo");
;
            if (string.IsNullOrEmpty(info))
            {
                GameLevelInfo = GameLevelInfo.GetEmptyGameLevelInfo(MaxLevel);
            }
            else
            {
                GameLevelInfo = GameLevelInfo.GetGameLevelInfo();
            }
            IsPlaySound = true;
            SetMusic();
        }
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            //UISceneCtrl.Instance.Load(UISceneType.Level);
            //ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            SceneManager.LoadScene("Init");
            UIDispatcher.Instance.AddEventListen(ConstDefine.Music, OnMusic);

        }

        private void OnMusic(object[] p)
        {
            IsPlaySound = !IsPlaySound;
            SetMusic();
        }

        public void PlayBtnMusic()
        {
            EazySoundManager.PlayUISound(BtnClip, 1, true);
        }

        public void SetMusic()
        {
            if (IsPlaySound)
            {
                EazySoundManager.GlobalSoundsVolume = 1;
                EazySoundManager.GlobalUISoundsVolume = 1;
                if (m_BGAudio == null)
                {
                    int id = EazySoundManager.PlayMusic(BGClip, 1, true, true);
                    m_BGAudio = EazySoundManager.GetAudio(id);
                    return;
                }

                m_BGAudio.Play();
            }
            else
            {
                EazySoundManager.GlobalSoundsVolume = 0;
                EazySoundManager.GlobalUISoundsVolume = 0;
                EazySoundManager.StopAll(1);
            }
        }
        public static bool OnWin(int starCount)
        {
            int level = CurLevel;
            if (GameLevelInfo.StarList[level - 1] == -1)
            {
                GameLevelInfo.StarList[level - 1] = starCount;
            }
            level++;
            int maxLevel = Global.Instance.MaxLevel;
            if (level > maxLevel)
            {
                GameLevelInfo.SetGameLevelInfo();
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
            GameLevelInfo.SetGameLevelInfo();
            return false;
        }
    }
}
