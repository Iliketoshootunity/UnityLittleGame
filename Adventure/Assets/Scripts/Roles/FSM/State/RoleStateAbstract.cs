using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public abstract class RoleStateAbstract : MonoBehaviour
    {
        protected RoleCtrl m_Role { get; set; }
        protected RoleStateMachine m_StateMechine { get; set; }

        protected Animator m_Ani;

        public abstract RoleStateType RoleStateType { get; }

        protected virtual void Awake()
        {
            m_Role = GetComponent<RoleCtrl>();
            m_Ani = GetComponentInChildren<Animator>();
            m_StateMechine = m_Role.StateMachine;
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public abstract void OnEnter();
        /// <summary>
        /// 更新状态
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// 离开状态
        /// </summary>
        public abstract void OnLevel();

        public abstract void HandleInput();


    }
}

