using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIInitSceneView : UISceneViewBase
    {

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnStartGame":
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitSceneViewClickStartGameBtn, null);
                    break;
                case "btnHelp":
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitSceneViewClickHelpBtn, null);
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound);
        }

    }
}
