using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIInitSceneView : UISceneViewBase
    {
        [SerializeField]
        private Image m_MusicImage;
        [SerializeField]
        private Sprite m_MusicOnSprite;
        [SerializeField]
        private Sprite m_MusicOffSprite;
        private bool m_IsPlayMusic;
        protected override void OnStart()
        {
            base.OnStart();

        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnStartGame")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.SceneInitSceneViewClickStartGameBtn, null);
            }
            else if (go.name == "btnMusic")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.MusicSetting, null);
                m_IsPlayMusic = !m_IsPlayMusic;
                SetMusic();
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
