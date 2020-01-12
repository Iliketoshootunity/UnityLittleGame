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

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnReturn")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSelectViewClickReturnBtn, null);
            }
        }
    }
}
