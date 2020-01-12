using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelPauseView : UIWindowViewBase
    {
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnReturnGame":
                    UIDispatcher.Instance.Dispatc(ConstDefine.PauseViewClickReturnGameBtn, null);
                    Close();
                    break;
                case "btnRestartGame":
                    UIDispatcher.Instance.Dispatc(ConstDefine.PauseViewClickRestartGameBtn, null);
                    Close();
                    break;
                case "btnReturnGameLevelSelect":
                    UIDispatcher.Instance.Dispatc(ConstDefine.PauseViewClickReturnGameLevelSelectBtn, null);
                    Close();
                    break;
            }
        }

    }
}
