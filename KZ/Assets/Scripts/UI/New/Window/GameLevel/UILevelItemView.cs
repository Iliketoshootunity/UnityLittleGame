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
        private Text m_LevelText;
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
            //EazySoundManager.PlayUISound(Global.Instance.UISound, 1, true);
        }

        public void SetUI(DataTransfer data)
        {


            isInit = true;
            int gamLevel = data.GetData<int>(ConstDefine.Item_Level);
            m_GameLevel = gamLevel;
            m_IsLock = data.GetData<bool>(ConstDefine.Item_IsLock);
            if (m_IsLock)
            {
                m_LockImage.sprite = m_LockSprite;
            }
            else
            {
                m_LockImage.sprite = m_UnLockSprite;

            }
            m_LevelText.text = m_GameLevel.ToString();
        }

    }
}
