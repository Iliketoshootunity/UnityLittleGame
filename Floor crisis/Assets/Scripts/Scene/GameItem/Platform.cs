using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public enum PlatformStatus
    {
        Normal,
        Break
    }
    public class Platform : MonoBehaviour, IBottomWeakness
    {
        [SerializeField]
        private BoxCollider2D m_TriggerCollider;
        [SerializeField]
        private AudioClip m_BreakClip;

        private bool m_HasBody;

        public Action OnRoleStand;

        private PlatformStatus m_PlatformStatus;

        public PlatformStatus PlatformStatus
        {
            get
            {
                return m_PlatformStatus;
            }
        }

        // Use this for initialization
        void Start()
        {
            m_PlatformStatus = PlatformStatus.Normal;
            m_HasBody = true;
            m_TriggerCollider.GetComponent<Rigidbody>();
        }

        public void OnStand()
        {
            if (OnRoleStand != null)
            {
                OnRoleStand();
            }
        }

        public bool Break()
        {
            if (m_BreakClip != null)
            {
                EazySoundManager.PlaySound(m_BreakClip);
            }
            Destroy(this.gameObject);
            m_PlatformStatus = PlatformStatus.Break;
            //m_TriggerCollider.enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            return GetComponentInChildren<SpriteRenderer>().enabled;
        }


    }
}
