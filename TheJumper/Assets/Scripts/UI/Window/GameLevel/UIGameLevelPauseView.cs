using UnityEngine;
using System.Collections;
using System;
using LFrameWork.Sound;

public class UIGameLevelPauseView : UIWindowViewBase
{
    public Action OnClickReturnButton;
    public Action OnClickContineButton;
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnReturn":
                if (OnClickReturnButton != null)
                {
                    OnClickReturnButton();
                }

                break;
            case "btnContinue":
                if (OnClickContineButton != null)
                {
                    OnClickContineButton();
                }
                break;
        }
    }
}
