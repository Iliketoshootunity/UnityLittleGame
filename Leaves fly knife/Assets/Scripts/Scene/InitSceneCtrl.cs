using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.SceneManagement;

namespace EasyFrameWork
{
    public class InitSceneCtrl : GameSceneCtrlBase
    {
        protected override void OnStart()
        {
            base.OnStart();
            UISceneCtrl.Instance.Load(UISceneType.Init);
            if (Global.IsFirstOpen)
            {
                Global.IsFirstOpen = false;
            }
            else
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            }
            UIDispatcher.Instance.AddEventListen(ConstDefine.Start, OnClickStart);

        }
        private void OnClickStart(object[] os)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("Level"); });
        }
        protected override void BeforeOnDestory()
        {
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.Start, OnClickStart);
        }
    }
}
