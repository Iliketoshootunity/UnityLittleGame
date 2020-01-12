using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelItemView : UISubViewBase
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

        protected override void OnStart()
        {
            base.OnStart();
            Button btn = GetComponentInChildren<Button>();
            btn.onClick.AddListener(ClikBtn);
        }

        private void ClikBtn()
        {
            if (m_IsLock) return;
            if (OnClickBtn != null)
            {
                OnClickBtn(m_GameLevel);
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound);
        }

        public void SetUI(DataTransfer data)
        {
            int gamLevel = data.GetData<int>(ConstDefine.GameLevelItem_Level);
            m_GameLevel = gamLevel;
            m_IsLock = data.GetData<bool>(ConstDefine.GameLevelItem_IsLock);
            if (m_IsLock)
            {
                m_LockImage.sprite = m_LockSprite;
                m_LockImage.enabled = true;
            }
            else
            {
                m_LockImage.sprite = m_UnLockSprite;
                m_LockImage.enabled = false;
                m_LevelText.text = m_GameLevel.ToString();
            }
        }

    }
}
