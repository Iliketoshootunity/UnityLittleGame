using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class BloodText : UISubViewBase
    {
        [SerializeField]
        private Text m_BloodText;
        [SerializeField]
        private float m_BloodTextAniTime;
        [SerializeField]
        private Vector2 m_BloodTextAniXAxisTargetRange;
        [SerializeField]
        private Vector2 m_BloodTextAniYAxisTargetRange;
        [SerializeField]
        private Vector2 m_BloodTextAniScaleRange = new Vector2(1, 0.3f);
        [SerializeField]
        private float m_UIGravity = -980f;

        private RectTransform m_BloodTextRect;
        private float m_BloodTextAniTimer;
        private Vector2 m_BloodTextAniTargetPos;
        private Vector2 m_BloodTextAniInitPos;

        public void BloodTextAni(Vector2 worldPos, int hurtValue)
        {
            m_BloodTextRect = m_BloodText.GetComponent<RectTransform>();
            m_BloodTextAniInitPos = m_BloodTextRect.anchoredPosition;
            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            RectTransform mainCanvansRect = UISceneCtrl.Instance.CurrentUIScene.MainCanvans.GetComponent<RectTransform>();
            Vector2 pos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvansRect, screenPos, UISceneCtrl.Instance.CurrentUIScene.UICamera, out pos);
            StartCoroutine(BloodTextAniIE(hurtValue));
        }
        private IEnumerator BloodTextAniIE(int hurtValue)
        {
            m_BloodTextRect.anchoredPosition = Vector2.zero;
            m_BloodTextRect.localScale = Vector2.one;
            m_BloodText.color = Color.white;
            m_BloodText.text = hurtValue.ToString();
            float x = UnityEngine.Random.Range(m_BloodTextAniXAxisTargetRange.x, m_BloodTextAniXAxisTargetRange.y);
            float y = UnityEngine.Random.Range(m_BloodTextAniYAxisTargetRange.x, m_BloodTextAniYAxisTargetRange.y);
            m_BloodTextAniTargetPos = new Vector2(x, y);
            Vector2 relativePos = m_BloodTextAniTargetPos;
            float xVelocity = relativePos.x / m_BloodTextAniTime + m_BloodTextAniInitPos.x;
            float yVelocity = ((relativePos.y / (2 * m_BloodTextAniTime)) - m_UIGravity * m_BloodTextAniTime) / 2 + m_BloodTextAniInitPos.y;
            m_BloodTextAniTimer = 0;
            while (m_BloodTextAniTimer < m_BloodTextAniTime)
            {
                m_BloodTextAniTimer += Time.deltaTime;
                float vt_x = m_BloodTextAniTimer * xVelocity;
                float vt_y = (yVelocity + yVelocity + m_UIGravity * m_BloodTextAniTimer) / 2 * m_BloodTextAniTimer;
                m_BloodTextRect.anchoredPosition = new Vector2(vt_x, vt_y);
                Color c = Color.Lerp(Color.white, new Color(1, 1, 1, 0.1f), m_BloodTextAniTimer / m_BloodTextAniTime);
                m_BloodTextRect.localScale = Vector2.Lerp(Vector2.one * m_BloodTextAniScaleRange.y, Vector2.one * m_BloodTextAniScaleRange.x, m_BloodTextAniTimer / m_BloodTextAniTime);
                yield return null;
            }
            BloodTextCtrl.Instance.PushPool(this.gameObject);
        }
    }
}
