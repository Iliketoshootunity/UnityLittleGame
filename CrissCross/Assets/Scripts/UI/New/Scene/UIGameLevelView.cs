using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelView : UISceneViewBase
    {
        [SerializeField]
        private Sprite m_MusicOnSprite;
        [SerializeField]
        private Sprite m_MusicOffSprite;
        [SerializeField]
        private Image m_MusicImage;
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnAudio":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSceneViewClickAudioBtn, null);
                    SetMusic(Global.Instance.IsPlaySound);
                    break;
                case "btnPause":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSceneViewClickPauseBtn, null);
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound);
        }

        public void SetUI()
        {
            SetMusic(Global.Instance.IsPlaySound);
        }

        private void SetMusic(bool isPlaySound)
        {
            if(isPlaySound)
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
