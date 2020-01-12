using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;

namespace EasyFrameWork
{
    public class UIGameLevelGuideView : UIWindowViewBase
    {
        public Action OnClose;
        private string[] m_ContentArr;

        [SerializeField]
        private Button m_CloseButton;
        [SerializeField]
        private Image m_Finger;
        [SerializeField]
        private Text m_Content;
        private int m_index;
        private RectTransform m_FingerRect;
        protected override void OnStart()
        {
            base.OnStart();
            //m_FingerRect.sizeDelta = new Vector2(m_FingerRect.sizeDelta.x * m_XSalce, m_FingerRect.sizeDelta.y * m_YScale);
        }

        private void OnClick(GameObject go)
        {
            if (m_index >= m_ContentArr.Length)
            {
                Close();
                if (OnClose != null)
                {
                    OnClose();
                }
                return;
            }
            string[] arr = m_ContentArr[m_index].Split('_');
            //锚点
            string[] anchorArr = arr[0].Split(',');
            m_FingerRect.anchorMax = new Vector2(anchorArr[0].ToFloat(), anchorArr[1].ToFloat());
            m_FingerRect.anchorMin = new Vector2(anchorArr[2].ToFloat(), anchorArr[3].ToFloat());
            //坐标
            string[] posArr = arr[1].Split(',');
            Vector3 pos = new Vector3(posArr[0].ToFloat(), posArr[1].ToFloat(), posArr[2].ToFloat());
            pos = new Vector3(pos.x, pos.y, 0);
            m_FingerRect.anchoredPosition3D = pos;

            string[] rotArr = arr[2].Split(',');
            m_FingerRect.localEulerAngles = new Vector3(rotArr[0].ToFloat(), rotArr[1].ToFloat(), rotArr[2].ToFloat());
            m_Content.text = arr[3];
            m_index++;
        }

        public void SetUI(string content)
        {
            m_ContentArr = content.Split('|');
            m_index = 0;
            EventTriggerListener.Get(m_CloseButton.gameObject).onClick = OnClick;
            m_FingerRect = m_Finger.GetComponent<RectTransform>();
            //模拟点击
            OnClick(this.gameObject);

        }

    }
}
