using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
namespace EasyFrameWork
{
    public class UILevelSceneView : UISceneViewBase
    {
        protected override void OnStart()
        {
            base.OnStart();
            UIViewMgr.Instance.OpenView(UIViewType.SelectLevel);
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnReturn")
            {
                object[] os = new object[1];
                os[0] = "Init";
                UIDispatcher.Instance.Dispatc(ConstDefine.NextScene, os);
            }


        }
    }
}
