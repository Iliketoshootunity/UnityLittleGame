using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 忽隐忽现的障碍物
    /// </summary>
	public class FlickeringObstacles : Obstacles
    {
        public override ObstaclesType ObstaclesType
        {
            get
            {
                return ObstaclesType.Flickering;
            }
        }
        /// <summary>
        /// 忽隐忽现的间隔
        /// </summary>
        [SerializeField]
        private float m_Interval;
        /// <summary>
        ///  忽隐忽现的变换时间
        /// </summary>
        [SerializeField]
        private float m_TweenTime;

        private float m_IntervalTimer;
        private float m_TweenTimer;

        private SpriteRenderer m_Renderer;
        private void Start()
        {
            m_Renderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            m_IntervalTimer -= Time.deltaTime;
            if (m_IntervalTimer < 0)
            {
                m_IntervalTimer = m_Interval;
                IsShow = !IsShow;
                if (IsShow)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }
        public void Show()
        {
            Color startColor = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, m_Renderer.color.a);
            Color endColor = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, 1);
            StartCoroutine(ColorTween(startColor, endColor));
        }

        public void Hide()
        {
            Color startColor = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, m_Renderer.color.a);
            Color endColor = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, 0);
            StartCoroutine(ColorTween(startColor, endColor));
        }

        private IEnumerator ColorTween(Color startColor, Color endColor)
        {
            bool isRun = true;
            m_TweenTimer = 0;
            while (isRun)
            {
                m_TweenTimer += Time.deltaTime;
                float process = m_TweenTimer / m_TweenTime;
                if (process >= 1)
                {
                    process = 1;
                    isRun = false;
                }
                m_Renderer.color = Color.Lerp(startColor, endColor, process);
                yield return null;
            }
        }


    }
}
