using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelVictoryView : UIWindowViewBase
    {

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnReturn":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelScene_VictoryWindow_Return, null);
                    break;
                case "btnContinue":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelScene_VictoryWindow_Continue, null);
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.BtnClip);
        }
    }
}
