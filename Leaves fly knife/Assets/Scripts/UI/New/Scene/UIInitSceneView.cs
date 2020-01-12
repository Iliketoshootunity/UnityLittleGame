using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class UIInitSceneView : UISceneViewBase
    {

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name.EndsWith("btnStart"))
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.Start, null);
            }
        }

    }
}
