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
            UILevelSceneView view = UISceneCtrl.Instance.Load(UISceneType.Level).GetComponent<UILevelSceneView>();
            if (view != null)
            {
                int maxStarCount = Global.GameLevelInfo.MaxLevel;
                int getStarCount = Global.GameLevelInfo.StarList.FindAll(x => x == 1).Count;
                view.SetUI(maxStarCount, getStarCount);
            }
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            UIDispatcher.Instance.AddEventListen(ConstDefine.NextScene, OnNextScene);
        }

        private void OnNextScene(object[] p)
        {
            string nextName = p[0].ToString();
            if (nextName == "Init")
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene(nextName); });
            }
        }
    }
}
