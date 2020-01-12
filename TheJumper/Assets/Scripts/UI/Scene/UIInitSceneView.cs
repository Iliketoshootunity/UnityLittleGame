using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LFrameWork.Sound;

public class UIInitSceneView : UISceneViewBase
{
    public Action OnClickStartGameBtn;
    public Action<bool> OnClickAudioBtn;
    private bool isPlay;
    [SerializeField]
    private Sprite[] m_AudioSprites;

    [SerializeField]
    private Image m_AudioImage;

    protected override void OnBtnClick(GameObject go)
    {

        if (go.name == "btnStartGame")
        {
            if (OnClickStartGameBtn != null)
            {
                OnClickStartGameBtn();
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
