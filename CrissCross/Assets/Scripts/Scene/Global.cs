using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.SceneManagement;
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

        public float RowInterval = 0.3f;
        public float ColumnInterval = 0.69f;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            SceneManager.LoadScene("Init");
        }

        public void SetMusic()
        {
            if (IsPlaySound)
            {
                EazySoundManager.GlobalSoundsVolume = 1;
                EazySoundManager.GlobalUISoundsVolume = 1;
                EazySoundManager.PlayMusic(BGM, 1, true, false, 1, 1);
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
