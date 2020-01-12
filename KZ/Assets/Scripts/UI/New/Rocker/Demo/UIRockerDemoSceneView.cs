using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class UIRockerDemoSceneView : UISceneViewBase
    {
        protected override void OnStart()
        {
            base.OnStart();
            UISceneCtrl.Instance.CurrentUIScene = this;
        }
    }
}
