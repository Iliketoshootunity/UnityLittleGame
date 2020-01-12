using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;
using UnityEngine.UI;

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
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnStartGame":
                    UIDispatcher.Instance.Dispatc(ConstDefine.StartGame, null);
                    break;
                case "btnMusic":
                    UIDispatcher.Instance.Dispatc(ConstDefine.MusicSetting, null);
                    SetMusic();
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound);
        }

        public void SetUI()
        {
            SetMusic();
        }

        private void SetMusic()
        {
            if (Global.Instance.IsPlaySound)
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
