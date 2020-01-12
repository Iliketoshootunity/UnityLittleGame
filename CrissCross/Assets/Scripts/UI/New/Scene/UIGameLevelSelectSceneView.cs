using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class UIGameLevelSelectSceneView : UISceneViewBase
    {
        protected override void OnStart()
        {
            base.OnStart();
            UIViewMgr.Instance.OpenView(UIViewType.GameLevelSelect);
        }

    }
}
