using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System.IO;
using LitJson;
using LFrameWork.Sound;


public enum EnumFloorType
{
    Sky,
    Snow,
    Desert,
    Forest,
    Sea

}

[System.Serializable]
public class FloorBGInfo
{
    public EnumFloorType FloorBGType;
    public Sprite BGSprite;
    public Sprite LayerSprite;
    public List<Sprite> LayerNumberList;
    private List<Sprite> m_LayerNumber;
    public List<Sprite> GetLayerNumber(int number)
    {
        if (m_LayerNumber == null)
        {
            m_LayerNumber = new List<Sprite>();
        }
        m_LayerNumber.Clear();

        if (number > 10 && number < 20)
        {
            int n1 = number / 10;
            int n2 = number % 10;
            m_LayerNumber.Add(LayerNumberList[0]);
            if (n2 != 0)
            {
                m_LayerNumber.Add(LayerNumberList[n2]);
            }
        }
        else if (number > 20)
        {
            int n1 = number / 10;
            int n2 = number % 10;
            m_LayerNumber.Add(LayerNumberList[n1]);
            m_LayerNumber.Add(LayerNumberList[0]);
            if (n2 != 0)
            {
                m_LayerNumber.Add(LayerNumberList[n2]);
            }
        }
        else if (number == 10)
        {
            m_LayerNumber.Add(LayerNumberList[0]);
        }
        else if (number == 20)
        {
            m_LayerNumber.Add(LayerNumberList[2]);
            m_LayerNumber.Add(LayerNumberList[0]);
        }
        else
        {
            m_LayerNumber.Add(LayerNumberList[number]);
        }
        return m_LayerNumber;
    }

}

namespace EasyFrameWork
{
    public class Global : MonoSingleton<Global>
    {
        public List<FloorBGInfo> FloorBGInfoList;
        public AudioClip BtnClip;
        public UserInfo UserInfo;
        public bool IsPlaySound = true;
        private void Awake()
        {
            UserInfo = UserInfo.ToUserInfo();
            DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                UserInfo.Coin = 10000;
            }
        }
        private void OnApplicationQuit()
        {
            UserInfo.ToSava();
        }

        private void OnApplicationFocus(bool focus)
        {
            UserInfo.ToSava();
        }
        private void OnApplicationPause(bool pause)
        {
            UserInfo.ToSava();
        }
        public void SetMusic(bool isPlay, AudioClip musicClip)
        {
            if (isPlay)
            {
                EazySoundManager.GlobalSoundsVolume = 1;
                EazySoundManager.GlobalUISoundsVolume = 1;
                EazySoundManager.PlayMusic(musicClip, 1, true, false, 1, 1);
            }
            else
            {
                EazySoundManager.GlobalSoundsVolume = 0;
                EazySoundManager.GlobalUISoundsVolume = 0;
                EazySoundManager.StopAll(1);
            }

            Global.Instance.IsPlaySound = isPlay;
        }
    }
}
