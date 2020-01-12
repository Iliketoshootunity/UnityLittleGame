using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// 角色状态机管理器
    /// </summary>
    public class RoleFSMMgr
    {

        /// <summary>
        /// 角色控制器
        /// </summary>
        public RoleCtrl CurRoleCtrl { get; private set; }

        /// <summary>
        /// 状态类型到状态的映射
        /// </summary>
        private Dictionary<RoleState, RoleStateAbstract> states;

        /// <summary>
        /// 
        /// </summary>
        public RoleState CurrentRoleStateEnum { get; private set; }

        public RoleStateAbstract CurrentRoleState = null;

        public IdleType NextIdleType;

        public IdleType CurIdleType;
        public RoleFSMMgr(RoleCtrl roleCtrl, Action onDestory, Action onDie)
        {
            this.CurRoleCtrl = roleCtrl;
            states = new Dictionary<RoleState, RoleStateAbstract>();
            //装载状态
            //states.Add(RoleState.Idle, new RoleStateIdle(this));
            //states.Add(RoleState.Run, new RoleStateRun(this));
            //states.Add(RoleState.Attack, new RoleStateAttack(this));
            //states.Add(RoleState.Hurt, new RoleStateHurt(this));
            //states.Add(RoleState.Die, new RoleStateDie(this));
            //states.Add(RoleState.Select, new RoleStateSelect(this));
            if (states.ContainsKey(CurrentRoleStateEnum))
            {
                CurrentRoleState = states[CurrentRoleStateEnum];
            }
        }

        public RoleStateAbstract GetState(RoleState stateType)
        {
            if (states.ContainsKey(stateType))
            {
                return states[stateType];
            }
            return null;
        }
        public void ChangeState(RoleState nextState)
        {
            //如果在当前状态则返回，但是当前状态是Idle Attack 可以进来
            //攻击的限制条件 是否僵值， Idle 的限制条件是当前的Idle状态
            if (CurrentRoleStateEnum == nextState && CurrentRoleStateEnum != RoleState.Idle && CurrentRoleStateEnum != RoleState.Attack) return;

            //当前状态退出
            if (CurrentRoleState != null)
            {
                CurrentRoleState.OnLevel();
            }
            //替换当前状态
            CurrentRoleStateEnum = nextState;
            CurrentRoleState = states[CurrentRoleStateEnum];
            if (CurrentRoleStateEnum == RoleState.Idle)
            {
                CurIdleType = NextIdleType;
            }
            //当前状态进入
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

