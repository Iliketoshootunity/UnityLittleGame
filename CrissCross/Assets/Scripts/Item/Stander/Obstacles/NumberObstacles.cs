using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class NumberObstacles : Obstacles
    {
        public int Number;
        [SerializeField]
        private SpriteRenderer m_BodyRenderer;
        [SerializeField]
        private SpriteRenderer m_NumberRenderer;
        [SerializeField]
        private SpriteRenderer m_NumberShadowRenderer;
        [SerializeField]
        private Color m_TriggerColor = Color.blue;
        [SerializeField]
        private Color m_TriggerFailColor = Color.red;



        private void Start()
        {

        }
        public override ObstaclesType ObstaclesType
        {
            get
            {
                return ObstaclesType.Number;
            }
        }

        public void Init(int number)
        {
            Number = number;
            m_NumberRenderer.sprite = GameLevelSceneCtrl.Instance.Number[number];
            m_NumberShadowRenderer.sprite = GameLevelSceneCtrl.Instance.Number[number];
        }

        /// <summary>
        /// 被触发
        /// </summary>
        public void Trigger()
        {
            m_BodyRenderer.color = Color.blue;
            m_NumberRenderer.color = Color.blue;
        }

        /// <summary>
        /// 触发失败
        /// </summary>
        public void TriggerFail()
        {
            m_BodyRenderer.color = Color.red;
            m_NumberRenderer.color = Color.red;
            Invoke("Normal", 0.3f);
        }

        /// <summary>
        /// 触发陈宫
        /// </summary>
        public void TriggerSucess()
        {
            Normal();
        }
        /// <summary>
        /// 正常状态
        /// </summary>
        public void Normal()
        {
            m_BodyRenderer.color = Color.white;
            m_NumberRenderer.color = Color.white;
        }
    }
}
