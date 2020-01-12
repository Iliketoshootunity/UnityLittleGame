using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{

    public class Star : MonoBehaviour, IWeak, ICollect
    {
        public Action CollectEvent;
        private bool m_IsCollect;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IWeak weak = collision.GetComponent<IWeak>();
            if (weak != null) return;
            Collect();
        }
        public void Collect()
        {
            if (m_IsCollect)
            {
                m_IsCollect = true;
                return;
            }
            if (CollectEvent != null)
            {
                CollectEvent();
            }
            Destroy(this.gameObject);
        }

    }
}
