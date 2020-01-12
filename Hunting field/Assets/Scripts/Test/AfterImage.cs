using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 残影
    /// </summary>
	public class AfterImage : MonoBehaviour
    {

        /// <summary>
        /// 透明曲线
        /// </summary>
        [SerializeField]
        private AnimationCurve m_LucencyCurve;
        /// <summary>
        /// 透明时间
        /// </summary>
        private float m_LucencyTime;
        private float m_LucencyTimer;
        /// <summary>
        /// 透明标识
        /// </summary>
        private bool m_IsLucency;

        private SpriteRenderer m_Renderer;

        // Update is called once per frame
        void Update()
        {
            if (m_IsLucency)
            {
                m_LucencyTimer += Time.deltaTime;
                float process = m_LucencyTimer / m_LucencyTime;
                if (process >= 1)
                {
                    Destroy(this.gameObject);
                }
                float a = m_LucencyCurve.Evaluate(process) * 0.75f;
                m_Renderer.color = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, a);
            }
        }

        public void Init(float lucencyTime, Sprite sprite, Color startColor)
        {
            m_Renderer = GetComponent<SpriteRenderer>();
            m_Renderer.color = startColor;
            m_IsLucency = true;
            m_Renderer.sprite = sprite;
            m_LucencyTime = lucencyTime;
        }
    }
}
