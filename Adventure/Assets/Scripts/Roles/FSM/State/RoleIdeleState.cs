using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RoleIdeleState : RoleStateAbstract
    {
        public override RoleStateType RoleStateType
        {
            get
            {
                return RoleStateType.Idle;
            }

        }

        public override void HandleInput()
        {

        }

        public override void OnEnter()
        {
            if (m_Ani == null)
            {
                m_Ani = GetComponentInChildren<Animator>();
            }
            if (m_Ani != null)
            {
                m_Ani.SetTrigger("IsIdle");
            }

        }

        public override void OnLevel()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
