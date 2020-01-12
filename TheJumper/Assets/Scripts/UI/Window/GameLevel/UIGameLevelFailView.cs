using LFrameWork.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameLevelFailView : UIWindowViewBase
{
    [SerializeField]
    private Text m_CurScoreText;
    [SerializeField]
    private Text m_MaxScoreText;
    public Action OnClickReturnButton;
    public Action OnClickRestartGameButton;
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
            case "btnRestart":
                if (OnClickRestartGameButton != null)
                {
                    OnClickRestartGameButton();
                }
                break;
        }
    }

    public void SetUI(int curScore, int maxScore)
    {
        m_CurScoreText.text = curScore.ToString();
        m_MaxScoreText.text = maxScore.ToString();
    }

}
