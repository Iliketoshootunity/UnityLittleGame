using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using LFrameWork.Sound;

public class UIGameLevelSceneView : UISceneViewBase
{
    public Action OnClickPauseBtn;
    public Action OnClickInputBtn;
    public Action<bool> OnClickAudioBtn;
    [SerializeField]
    private Text m_ScoreText;
    [SerializeField]
    private Sprite[] m_AudioSprites;
    [SerializeField]
    private Image m_AudioImage;
    private bool isPlay;

    protected override void OnStart()
    {
        GameLevelCtrl.Instance.OpenView(UIViewType.GameLevelHelp);
    }

    protected override void OnBtnClick(GameObject go)
    {
        if (go.name == "btnPause")
        {
            if (OnClickPauseBtn != null)
            {
                OnClickPauseBtn();
            }
        }
        else if (go.name == "btnAudio")
        {

            if (OnClickAudioBtn != null)
            {
                isPlay = !isPlay;
                SetSoundBG();
                OnClickAudioBtn(isPlay);
            }
        }

    }

    public void SetScore(int score)
    {
        m_ScoreText.text = score.ToString();
    }

    public void SetUI(bool isPlaySound)
    {
        isPlay = isPlaySound;
        SetSoundBG();
    }

    private void SetSoundBG()
    {
        if (isPlay)
        {
            m_AudioImage.sprite = m_AudioSprites[0];
        }
        else
        {
            m_AudioImage.sprite = m_AudioSprites[1];
        }
    }
}
