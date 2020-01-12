using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 要塞
    /// </summary>
	public class Stronghold : Stander
    {
        /// <summary>
        /// 连接的堡垒
        /// </summary>
        public Stronghold ConnectStronghold;
        [SerializeField]
        private GameObject m_Sign;
        [SerializeField]
        private GameObject m_Double;
        [SerializeField]
        private SpriteRenderer m_NumberSprite1;
        [SerializeField]
        private SpriteRenderer m_NumberSprite2;
        [SerializeField]
        private SpriteRenderer m_NumberSprite3;
        [SerializeField]
        private GameObject m_Flag;
        public int Range;


        public void Init(int range)
        {
            Range = range;
            List<Sprite> list = GameLevelSceneCtrl.Instance.GetNumber(range);
            if (list.Count > 1)
            {
                m_Double.gameObject.SetActive(true);
                m_Sign.gameObject.SetActive(false);
                m_NumberSprite2.sprite = list[0];
                m_NumberSprite3.sprite = list[1];
            }
            else
            {
                m_Double.gameObject.SetActive(false);
                m_Sign.gameObject.SetActive(true);
                m_NumberSprite1.sprite = list[0];
            }
        }

        public bool HasConnectStronghold()
        {
            if (ConnectStronghold != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowOrHideFlag(bool isShow)
        {
            m_Flag.SetActive(isShow);
        }
    }
}
