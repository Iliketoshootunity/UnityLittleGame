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
            UIInitSceneView view = UISceneCtrl.Instance.Load(UISceneType.Init).GetComponent<UIInitSceneView>();
            view.SetUI();
            UIDispatcher.Instance.AddEventListen(ConstDefine.StartGame, OnStartGame);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, Global.Instance.ChangeSceneTime);
        }

        private void OnStartGame(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => SceneManager.LoadScene("GameLevelSelect"));
        }


        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.StartGame, OnStartGame);
        }

    }
}
