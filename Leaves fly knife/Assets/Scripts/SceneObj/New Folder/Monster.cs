using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    /// <summary>
    /// 攻击除Monster类型的所有
    /// </summary>
    public class Monster : MonoBehaviour, IHealth
    {
        public enum Status
        {
            Normal,
            Hit
        }
        public Action DeadEvent;
        private Status m_Status;
        private Animator m_Animator;
        [SerializeField]
        private SpriteRenderer m_Ice;
        void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Animator.speed = 0;
        }

        public void OnHit(GameObject attacker)
        {
            if (m_Status == Status.Hit) return;
            m_Status = Status.Hit;
            m_Animator.speed = 1;
            m_Ice.DoColor(new Color(0, 0, 0, 0), 0.3f);
            if (DeadEvent != null)
            {
                DeadEvent();
            }
        }
    }
}
