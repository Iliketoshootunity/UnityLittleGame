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
            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.MusicStatus, Global.Instance.IsPlaySound);
            view.SetUI(data);
            if (Global.Instance.IsFirstEnterGame)
            {
                Global.Instance.IsFirstEnterGame = false;
            }
            else
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            }
            UIDispatcher.Instance.AddEventListen(ConstDefine.SceneInitSceneViewClickStartGameBtn, OnInitSceneViewClickStartGameBtn);
            //Global.Instance.SetMusic();
        }

        private void OnInitSceneViewClickStartGameBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("GameLevelSelect"); });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, null);
            }
        }
        private void OnDestroy()
        {
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.SceneInitSceneViewClickStartGameBtn, OnInitSceneViewClickStartGameBtn);
        }
    }
}
