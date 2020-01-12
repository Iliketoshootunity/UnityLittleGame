using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    /// <summary>
    /// 角色血条
    /// </summary>
	public class RoleHeadBar : MonoBehaviour
    {
        [SerializeField]
        private Text m_NickName;
        [SerializeField]
        private Slider m_HPSlider;
        [SerializeField]
        private Slider m_RageSlider;

        private RectTransform m_Rect;
        /// <summary>
        /// 对齐的目标点
        /// </summary>
        private GameObject target;

        private bool m_IsHero;
        public void Init(bool isHero, GameObject target, string name, float hpSliderValue = 1)
        {
            m_IsHero = isHero;
            this.target = target;
            m_NickName.text = name;
            m_HPSlider.value = hpSliderValue;
            m_Rect = GetComponent<RectTransform>();
            if (!m_IsHero)
            {
                m_RageSlider.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_NickName == null || target == null) return;
            Vector3 scenePoint = Camera.main.WorldToScreenPoint(target.transform.position);
            Vector2 pos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RoleHeadBarCtrl.Instance.Rect, scenePoint, UISceneCtrl.Instance.CurrentUIScene.UICamera, out pos);
            m_Rect.anchoredPosition3D = pos;
        }
        public void SetHpSlider(float hpSliderValue)
        {
            m_HPSlider.value = hpSliderValue;
        }

        public void SetRageSlider(float rageSliderValue)
        {
            if (m_IsHero)
            {
                m_RageSlider.value = rageSliderValue;
            }

        }
        private void OnDestroy()
        {
            m_NickName = null;
            m_NickName = null;
            m_HPSlider = null;
            m_RageSlider = null;
        }
    }
}
