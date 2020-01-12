using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    public class RoleHurt
    {

        private RoleStateMachine m_StateMachine;
        private RoleCtrl m_Role;
        public Action OnHurt;
        public RoleHurt(RoleStateMachine fsm, RoleCtrl role)
        {
            m_StateMachine = fsm;
            m_Role = role;

        }
        public IEnumerator ToHurt(RoleTransferAttackInfo attackInfo)
        {
            if (m_StateMachine == null) yield break;
            SkillEntity skillEntity = SkillDBModel.Instance.Get(attackInfo.SkillId);
            SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetSkillLevelBySkillIdAndLevel(attackInfo.SkillId, attackInfo.SkillLevel);
            if (skillEntity == null || skillLevelEntity == null) yield break;
            yield return new WaitForSeconds(skillEntity.ShowHurtEffectDelaySecond);
            //减血
            //m_Role.HeadBar.BloodTextAni(attackInfo.HurtValue);
            m_Role.CurRoleInfo.CurrentHP -= attackInfo.HurtValue;
            if (m_Role.CurRoleInfo.CurrentHP < 0)
            {
                m_Role.CurRoleInfo.CurrentHP = 0;
                m_Role.ToDie();
                if (OnHurt != null)
                {
                    OnHurt();
                }
                yield break;
            }
            if (OnHurt != null)
            {
                OnHurt();
            }
            //播放受伤特效
            int fontSize = 1;
            Color color = Color.red;
            if (attackInfo.IsCri)
            {
                fontSize = 2;
                color = Color.yellow;
            }
            if (!m_Role.IsRigibody)
            {
                //m_StateMachine.ChangeState(RoleStateType.Hurt);
            }

        }
    }
}
