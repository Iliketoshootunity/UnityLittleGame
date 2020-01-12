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
        [SerializeField]
        private Text m_Level;
        [SerializeField]
        private Text m_Step;
        [SerializeField]
        private int m_StepFontSize1 = 125;
        [SerializeField]
        private int m_StepFontSize2 = 100;
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnAudio":
                    UIDispatcher.Instance.Dispatc(ConstDefine.MusicSetting, null);
                    SetMusic();
                    break;
                case "btnRestart":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelViewClickRestartBtn, null);
                    break;
                case "btnReturn":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelViewClickReturnBtn, null);
                    break;
            }
            //EazySoundManager.PlayUISound(Global.Instance.UISound);
        }

        public void SetUI(int level)
        {
            SetMusic();
            m_Level.text = level.ToString();
        }

        public void SetStepCount(bool isShow, int stepCount)
        {
            if (isShow)
            {
                m_Step.gameObject.SetActive(true);
                if (stepCount == 0)
                {
                    m_Step.fontSize = m_StepFontSize2;
                    m_Step.text = "无法移动";
                }
                else
                {
                    m_Step.text = stepCount.ToString(); ;
                    m_Step.fontSize = m_StepFontSize1;
                }
            }
            else
            {
                m_Step.gameObject.SetActive(false);

            }

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
