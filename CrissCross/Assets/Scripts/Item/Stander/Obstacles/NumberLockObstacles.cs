using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 数字锁障碍物
    /// </summary>
    public class NumberLockObstacles : Obstacles
    {
        private SpriteRenderer m_BodyRenderer;
        [SerializeField]
        private SpriteRenderer m_Number1Renderer;
        [SerializeField]
        private SpriteRenderer[] m_Number2RendererArr;
        [SerializeField]
        private SpriteRenderer m_Number1ShadowRenderer;
        [SerializeField]
        private SpriteRenderer[] m_Number2ShadowRendererArr;
        [SerializeField]
        private Transform Number1;
        [SerializeField]
        private Transform Number2;
        private Transform m_Body;

        private void Start()
        {

        }

        public override ObstaclesType ObstaclesType
        {
            get
            {
                return ObstaclesType.NumberLock;
            }
        }

        public void Init(int number)
        {
            m_Body = transform.Find("Body");
            m_BodyRenderer = m_Body.GetComponent<SpriteRenderer>();
            if (number >= 10)
            {
                int n1 = number % 10;
                int n2 = number / 10;
                m_Number2RendererArr[0].sprite = GameLevelSceneCtrl.Instance.Number[n2];
                m_Number2RendererArr[1].sprite = GameLevelSceneCtrl.Instance.Number[n1];
                m_Number2ShadowRendererArr[0].sprite = GameLevelSceneCtrl.Instance.Number[n2];
                m_Number2ShadowRendererArr[1].sprite = GameLevelSceneCtrl.Instance.Number[n1];
                Number1.gameObject.SetActive(false);
                Number2.gameObject.SetActive(true);
            }
            else
            {
                m_Number1Renderer.sprite = GameLevelSceneCtrl.Instance.Number[number];
                m_Number1ShadowRenderer.sprite = GameLevelSceneCtrl.Instance.Number[number];
                Number1.gameObject.SetActive(true);
                Number2.gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            IsShow = false;
            gameObject.SetActive(false);
        }
    }
}
