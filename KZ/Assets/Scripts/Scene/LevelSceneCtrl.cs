using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;

namespace EasyFrameWork
{
    public class LevelSceneCtrl : GameSceneCtrlBase
    {
        protected override void OnStart()
        {
            base.OnStart();
            UISceneCtrl.Instance.Load(UISceneType.Level);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            UIDispatcher.Instance.AddEventListen(ConstDefine.NextScene, OnNextScene);
        }

        private void OnNextScene(object[] p)
        {
            string nextName = p[0].ToString();
            if (nextName == "Init")
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, ()=> { SceneManager.LoadScene(nextName); });
            }       
        }
    }
}
