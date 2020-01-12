using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.SceneManagement;
using EasyFrameWork.UI;
using System;
using LFrameWork.Sound;

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
        public bool IsFirstEnterGame = true;
        public float RowInterval = 0.3f;
        public float ColumnInterval = 0.69f;
        public int Test = -1;
        private Audio m_Audio;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            SceneManager.LoadScene("Init");
            if (Test > 0)
            {
                MaxPassLevel = Test;
            }
            UIDispatcher.Instance.AddEventListen(ConstDefine.MusicSetting, OnMusicSetting);
            int id = EazySoundManager.PlayMusic(BGM, 1, true, false, 1, 1);
            m_Audio = EazySoundManager.GetMusicAudio(id);
            m_Audio.Persist = true;
        }

        private void OnDestroy()
        {
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.MusicSetting, OnMusicSetting);
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
                m_Audio.Play();
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
