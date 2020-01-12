using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// 角色状态机管理器
    /// </summary>
    public class RoleStateMachine : StateMachine<RoleStateType>
    {
        public RoleCtrl m_CurRoleCtrl { get; protected set; }

        private Dictionary<RoleStateType, RoleStateAbstract> m_StateDic;
        public RoleStateType CurrentRoleStateType { get; protected set; }

        public RoleStateAbstract CurrentRoleState = null;

        public bool IsNormalAtk;

        public int SkillIndex;

        public int RoleType
        {
            get
            {
                if (m_CurRoleCtrl.CurRoleType == EasyFrameWork.RoleType.Hero)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }
        public RoleStateMachine(GameObject target, bool triggerEvents) : base(target, triggerEvents)
        {
            Target = target;
            TriggerEvents = triggerEvents;
            m_StateDic = new Dictionary<RoleStateType, RoleStateAbstract>();
            m_CurRoleCtrl = target.GetComponent<RoleCtrl>();

        }

        public void AddState(RoleStateAbstract state)
        {
            m_StateDic.Add(state.RoleStateType, state);
        }

        public RoleStateAbstract GetState(RoleStateType stateType)
        {
            if (m_StateDic.ContainsKey(stateType))
            {
                return m_StateDic[stateType];
            }
            return null;
        }
        public override void ChangeState(RoleStateType nextState)
        {
            if (CurrentRoleStateType == nextState) return;
            if (CurrentRoleState != null)
            {
                CurrentRoleState.OnLevel();
            }
            CurrentRoleStateType = nextState;
            CurrentRoleState = m_StateDic[CurrentRoleStateType];
            CurrentRoleState.OnEnter();
        }

        public void OnUpdate()
        {
            if (CurrentRoleState != null)
            {
                CurrentRoleState.OnUpdate();
            }
        }


    }
}

