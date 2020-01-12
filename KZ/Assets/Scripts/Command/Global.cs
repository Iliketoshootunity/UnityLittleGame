using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;

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
        private void Awake()
        {
            PlayerPrefs.SetString("GameLevelInfo", null);
            string info = PlayerPrefs.GetString("GameLevelInfo");
            if (string.IsNullOrEmpty(info))
            {
                GameLevelInfo = GameLevelInfo.GetEmptyGameLevelInfo(MaxLevel);
            }
            else
            {
                GameLevelInfo = GameLevelInfo.GetGameLevelInfo();
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            //UISceneCtrl.Instance.Load(UISceneType.Level);
            //ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            SceneManager.LoadScene("Init");

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
    }
}
