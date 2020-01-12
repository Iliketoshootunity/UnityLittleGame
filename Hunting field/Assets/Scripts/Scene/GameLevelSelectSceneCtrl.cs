using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;

namespace EasyFrameWork
{
    public class GameLevelSelectSceneCtrl : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            UISceneCtrl.Instance.Load(UISceneType.GameLevelSelect);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, Global.Instance.ChangeSceneTime);
            UIDispatcher.Instance.AddEventListen(ConstDefine.GameLevelSelectViewClickReturnBtn, OnGameLevelSelectViewClickReturnBtn);
        }

        private void OnDestroy()
        {
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.GameLevelSelectViewClickReturnBtn, OnGameLevelSelectViewClickReturnBtn);
        }
        private void OnGameLevelSelectViewClickReturnBtn(object[] p)
        {

            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => { SceneManager.LoadScene("Init"); });
        }
    }
}
