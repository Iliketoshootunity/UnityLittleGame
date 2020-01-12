using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;

namespace EasyFrameWork
{
    public class GameLevelSelectSceneCtrl : GameSceneCtrlBase
    {
        protected override void OnStart()
        {
            UIGameLevelSelectSceneView view = UISceneCtrl.Instance.Load(UISceneType.GameLevelSelect).GetComponent<UIGameLevelSelectSceneView>();
            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.MusicStatus, Global.Instance.IsPlaySound);
            view.SetUI(data);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            //Global.Instance.SetMusic();
            UIDispatcher.Instance.AddEventListen(ConstDefine.SceneGameLevelSelectViewClickReturnBtn, OnClickReturnBtn);
        }

        private void OnClickReturnBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("Init"); });
        }

        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.SceneGameLevelSelectViewClickReturnBtn, OnClickReturnBtn);
        }
    }
}
