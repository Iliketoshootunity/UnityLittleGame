using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;
using EasyFrameWork.UI;
using System;

namespace EasyFrameWork
{
    public class UIJobItemView : MonoBehaviour
    {

        public Action<UIJobItemView> OnClickButton;
        [SerializeField]
        private Button m_Button;
        [SerializeField]
        private int m_Index;
        [SerializeField]
        private int m_Coin;
        [SerializeField]
        private Text m_CoinText;
        [SerializeField]
        private Image m_FrameImage;
        [SerializeField]
        private Sprite m_SelectedSprite;
        [SerializeField]
        private Sprite m_NormalSprite;
        public int Index
        {
            get
            {
                return m_Index;
            }
        }

        public int Coin
        {
            get
            {
                return m_Coin;
            }
        }
        // Use this for initialization
        void Start()
        {
            EventTriggerListener.Get(m_Button.gameObject).onClick += OnClikButton;
            m_CoinText.text = m_Coin.ToString();
        }

        private void OnClikButton(GameObject go)
        {
            if (OnClickButton != null)
            {
                OnClickButton(this);
            }
            IsCurJob();
        }

        public void HasJob()
        {
            //HeadImage.color = Color.red;
        }
        public void IsCurJob()
        {
            m_FrameImage.sprite = m_SelectedSprite;
        }
        public void OnChange()
        {
            m_FrameImage.sprite = m_NormalSprite;
        }
    }
}
