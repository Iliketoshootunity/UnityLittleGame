using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;


namespace EasyFrameWork
{
    public class UILevelItemView : UISubViewBase
    {
        [SerializeField]
        private Sprite m_UnLockSprite;
        [SerializeField]
        private Sprite m_LockSprite;
        [SerializeField]
        private Image m_LockImage;
        [SerializeField]
        private Image m_StarImagge;
        [SerializeField]
        private Sprite m_Star1Sprite;
        [SerializeField]
        private Sprite m_Star2Sprite;
        [SerializeField]
        private Text m_LevelText;
        [SerializeField]
        private Font m_Font1;
        [SerializeField]
        private Font m_Font2;
        private bool m_IsLock;
        private int m_GameLevel;

        public Action<int> OnClickBtn;
        private bool isInit;
        protected override void OnStart()
        {
            base.OnStart();
            Button btn = GetComponentInChildren<Button>();
            btn.onClick.AddListener(ClikBtn);
            float weightScale = (Screen.width * (1136f / Screen.height)) / 852f;
            m_LevelText.transform.parent.localScale *= weightScale;
        }

        private void Update()
        {
            if (isInit)
            {
                EventTriggerListener listener = GetComponent<EventTriggerListener>();
                Destroy(listener);
                isInit = false;
            }
        }
        private void ClikBtn()
        {
            if (m_IsLock) return;
            if (OnClickBtn != null)
            {
                OnClickBtn(m_GameLevel);
            }
            Global.Instance.PlayBtnMusic();
            //EazySoundManager.PlayUISound(Global.Instance.UISound, 1, true);
        }

        public void SetUI(DataTransfer data)
        {
            m_LevelText.transform.parent.localScale = Vector3.one;
            isInit = true;
            int gamLevel = data.GetData<int>(ConstDefine.Item_Level);
            m_GameLevel = gamLevel;
            m_IsLock = data.GetData<bool>(ConstDefine.Item_IsLock);
            int hasStar = data.GetData<int>(ConstDefine.Item_HasStar);
            if (hasStar > 0)
            {
                m_StarImagge.sprite = m_Star1Sprite;
            }
            else
            {
                m_StarImagge.sprite = m_Star2Sprite;
            }
            if (m_IsLock)
            {
                m_LockImage.sprite = m_LockSprite;
                m_LevelText.font = m_Font1;
            }
            else
            {
                m_LockImage.sprite = m_UnLockSprite;
                m_LevelText.font = m_Font2;

            }
            m_LevelText.text = m_GameLevel.ToString();
        }

    }
}
