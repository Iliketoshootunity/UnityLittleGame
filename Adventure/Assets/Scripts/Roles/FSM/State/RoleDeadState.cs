using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RoleDeadState : RoleStateAbstract
    {
        public override RoleStateType RoleStateType
        {
            get
            {
                return RoleStateType.Die;
            }
        }

        public override void HandleInput()
        {

        }

        public override void OnEnter()
        {
            m_Ani.SetTrigger("IsDead");

        }

        public override void OnLevel()
        {

        }

        public override void OnUpdate()
        {
            AnimatorStateInfo info = m_Ani.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] clips = m_Ani.GetCurrentAnimatorClipInfo(0);
            //Debug.Log(m_Ani.IsInTransition(0));
            if (!m_Ani.IsInTransition(0) && info.IsName("Dead") && info.normalizedTime > 1 && clips.Length == 1)
            {
                Destroy(m_Role.gameObject);
            }
        }


    }
}
