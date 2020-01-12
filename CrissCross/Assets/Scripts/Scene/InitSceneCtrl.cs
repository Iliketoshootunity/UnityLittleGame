using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;

namespace EasyFrameWork
{
    public class InitSceneCtrl : GameSceneCtrlBase
    {
        protected override void OnStart()
        {
            base.OnStart();
            UISceneCtrl.Instance.Load(UISceneType.Init);
            UIDispatcher.Instance.AddEventListen(ConstDefine.InitSceneViewClickStartGameBtn, OnInitSceneViewClickStartGameBtn);
            UIDispatcher.Instance.AddEventListen(ConstDefine.InitSceneViewClickHelpBtn, OnInitSceneViewClickHelpBtn);
            Global.Instance.SetMusic();
        }

        private void OnInitSceneViewClickHelpBtn(object[] p)
        {
            UIViewUtil.Instance.OpenWindow(UIViewType.Help);
        }

        private void OnInitSceneViewClickStartGameBtn(object[] p)
        {
            SceneManager.LoadScene("GameLevelSelect");
        }

        private void Update()
        {
            int count = FromText.ToInt();
            if (count > 0)
            {
                Global.Instance.MaxPassLevel = count;
            }

        }
        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.InitSceneViewClickStartGameBtn, OnInitSceneViewClickStartGameBtn);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.InitSceneViewClickHelpBtn, OnInitSceneViewClickHelpBtn);
        }

        public string FromText = "";
        private void OnGUI()
        {
            FromText = GUI.TextField(new Rect(0, 0, 100, 100), FromText);
        }
    }
}
