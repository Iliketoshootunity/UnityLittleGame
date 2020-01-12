using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class InitSceneCtrl : GameSceneCtrlBase
    {
        [SerializeField]
        private AudioClip m_Music;
        protected override void OnAwake()
        {
            base.OnAwake();
            UISceneCtrl.Instance.Load(UISceneType.Init);
            UIDispatcher.Instance.AddEventListen(ConstDefine.InitScene_StartGame, OnClickInitSceneStartGameBtn);
            UIDispatcher.Instance.AddEventListen(ConstDefine.InitScene_Help, OnClickInitSceneHelpBtn);
            UIDispatcher.Instance.AddEventListen(ConstDefine.InitScene_Shop, OnClickInitSceneShopBtn);
            UIDispatcher.Instance.AddEventListen(ConstDefine.InitScene_Music, OnClickInitSceneMusicBtn);
            object[] obj = new object[1];
            obj[0] = Global.Instance.IsPlaySound;
            OnClickInitSceneMusicBtn(obj);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        private void OnClickInitSceneShopBtn(object[] p)
        {
            UIViewMgr.Instance.OpenView(UIViewType.Shop);
        }
        private void OnClickInitSceneStartGameBtn(object[] p)
        {
            SceneManager.LoadScene("GameLevel");
        }
        private void OnClickInitSceneHelpBtn(object[] p)
        {
            UIViewUtil.Instance.OpenWindow(UIViewType.Help);
        }
        private void OnClickInitSceneMusicBtn(object[] p)
        {
            bool isPlay = (bool)p[0];
            Global.Instance.SetMusic(isPlay, m_Music);
        }
    }
}
