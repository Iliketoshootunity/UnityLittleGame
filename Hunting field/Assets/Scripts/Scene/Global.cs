using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;
using EasyFrameWork.UI;
using System;

namespace EasyFrameWork
{
    public class Global : MonoSingleton<Global>
    {
        public AudioClip BGM;

        public AudioClip UISound;
        /// <summary>
        /// 最高通过的关卡
        /// </summary>
        public int MaxPassLevel
        {
            get
            {
                return PlayerPrefs.GetInt("MaxPassLevel", 1);
            }
            set
            {
                PlayerPrefs.SetInt("MaxPassLevel", value);
            }
        }


        /// <summary>
        /// 当前关卡
        /// </summary>
        public int CurLevel;

        public bool IsPlaySound = true;

        public float ChangeSceneTime = 1f;
        public int Test;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            UIDispatcher.Instance.AddEventListen(ConstDefine.MusicSetting, OnMusicSetting);
            if (Test > 0)
            {
                MaxPassLevel = Test;
            }
            SetMusic();
            TweenManager t = TweenManager.Instance;
            SceneManager.LoadScene("Init");

        }

        private void OnMusicSetting(object[] p)
        {
            IsPlaySound = !IsPlaySound;
            SetMusic();
        }
        public void SetMusic()
        {
            if (IsPlaySound)
            {
                EazySoundManager.GlobalSoundsVolume = 1;
                EazySoundManager.GlobalUISoundsVolume = 1;
                EazySoundManager.PlayMusic(BGM, 1, true, true, 1, 1);
            }
            else
            {
                EazySoundManager.GlobalSoundsVolume = 0;
                EazySoundManager.GlobalUISoundsVolume = 0;
                EazySoundManager.StopAll(1);
            }
        }
    }
}
