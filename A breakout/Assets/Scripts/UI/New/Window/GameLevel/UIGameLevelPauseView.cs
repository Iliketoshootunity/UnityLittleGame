using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelPauseView : UIWindowViewBase
    {

        [SerializeField]
        private Image m_MusicImage;
        [SerializeField]
        private Sprite m_MusicOnSprite;
        [SerializeField]
        private Sprite m_MusicOffSprite;
        private bool m_IsPlayMusic;


        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnAudio":
                    UIDispatcher.Instance.Dispatc(ConstDefine.MusicSetting, null);
                    m_IsPlayMusic = !m_IsPlayMusic;
                    SetMusic();
                    break;
                case "btnReturn":
                    UIDispatcher.Instance.Dispatc(ConstDefine.PauseOrFailViewClickReturnBtn, null);
                    break;
                case "btnContinue":
                    UIDispatcher.Instance.Dispatc(ConstDefine.PauseOrFailViewClickContinueBtn, null);
                    Close();
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound, 1, true);
        }


        public void SetUI(DataTransfer data)
        {
            m_IsPlayMusic = data.GetData<bool>(ConstDefine.MusicStatus);
            SetMusic();
        }

        private void SetMusic()
        {
   
            if (m_IsPlayMusic)
            {
                m_MusicImage.sprite = m_MusicOnSprite;
            }
            else
            {
                m_MusicImage.sprite = m_MusicOffSprite;
            }
        }
    }
}
