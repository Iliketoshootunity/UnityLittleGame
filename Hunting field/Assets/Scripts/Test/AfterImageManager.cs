using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 残影管理
    /// </summary>
	public class AfterImageManager : MonoBehaviour
    {
        [SerializeField]
        private Color m_StartColor;
        /// <summary>
        /// 残影启动标识
        /// </summary>
        public bool AfterImageActive;

        /// <summary>
        /// 残影生存时间
        /// </summary>
        [SerializeField]
        private float m_LifeTime;
        /// <summary>
        /// 残影生成间隔
        /// </summary>
        [SerializeField]
        private float m_CreateInterval;
        /// <summary>
        /// 残影计时器
        /// </summary>
        private float m_CreateTimer;

        private SpriteRenderer m_Renderer;
        private Transform m_Body;
        // Use this for initialization
        void Start()
        {
            m_Body = transform.Find("Body");
            m_Renderer = m_Body.GetComponent<SpriteRenderer>();

        }

        // Update is called once per frame
        void Update()
        {
            if (AfterImageActive)
            {
                m_CreateTimer -= Time.deltaTime;
                if (m_CreateTimer <= 0)
                {
                    CreateAfterImage();
                    m_CreateTimer = m_CreateInterval;
                }
            }

        }

        /// <summary>
        /// 生成残影
        /// </summary>
        private void CreateAfterImage()
        {
            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, "RoleAfterEffect", isCache: true);
            go.transform.position = m_Body.transform.position;
            go.transform.rotation = m_Body.transform.rotation;
            go.transform.localScale = m_Body.transform.localScale;
            AfterImage ai = go.GetComponent<AfterImage>();
            ai.Init(m_LifeTime, m_Renderer.sprite, m_StartColor);
        }

    }
}
