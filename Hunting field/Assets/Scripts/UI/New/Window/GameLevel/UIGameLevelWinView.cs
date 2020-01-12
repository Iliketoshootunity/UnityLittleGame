using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelWinView : UIWindowViewBase
    {
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnNextGameLevel":
                    UIDispatcher.Instance.Dispatc(ConstDefine.ClickNextGameLevelBtn, null);
                    break;
                case "btnGameLeveSelect":
                    UIDispatcher.Instance.Dispatc(ConstDefine.ClickGameLeveSelectBtn, null);
                    break;
                case "btnRestart":
                    UIDispatcher.Instance.Dispatc(ConstDefine.ClickRestartBtn, null);
                    break;
            }
        }

    }
}
