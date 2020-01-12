using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class RoleStateAttack : RoleStateAbstract
    {

        public override RoleStateType RoleStateType
        {
            get
            {
                return RoleStateType.Attack;
            }
        }

        public override void HandleInput()
        {

        }

        public override void OnEnter()
        {
            //攻击
            if (m_Role.StateMachine.IsNormalAtk)
            {
            
            }
            else
            {
                if (m_Role.IsRage)
                {
                    m_Ani.SetTrigger("IsSkill1");
                }
                else
                {
                    m_Ani.SetTrigger("IsSkill2");
                }
          
            }
        }

        public override void OnLevel()
        {
            m_Role.RoundEnd();
        }

        public override void OnUpdate()
        {
            AnimatorStateInfo info = m_Ani.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] clips = m_Ani.GetCurrentAnimatorClipInfo(0);
            //Debug.Log(m_Ani.IsInTransition(0));
            if (!m_Ani.IsInTransition(0) && (info.IsName("Skill1") || info.IsName("Skill2")) && info.normalizedTime > 1 && clips.Length == 1)
            {
                m_Role.ToIdle();
            }
        }
    }
}
