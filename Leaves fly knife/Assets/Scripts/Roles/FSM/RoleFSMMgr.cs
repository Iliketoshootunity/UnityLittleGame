using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// ��ɫ״̬��������
    /// </summary>
    public class RoleFSMMgr
    {

        /// <summary>
        /// ��ɫ������
        /// </summary>
        public RoleCtrl CurRoleCtrl { get; private set; }

        /// <summary>
        /// ״̬���͵�״̬��ӳ��
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
            //װ��״̬
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
            //����ڵ�ǰ״̬�򷵻أ����ǵ�ǰ״̬��Idle Attack ���Խ���
            //�������������� �Ƿ�ֵ�� Idle �����������ǵ�ǰ��Idle״̬
            if (CurrentRoleStateEnum == nextState && CurrentRoleStateEnum != RoleState.Idle && CurrentRoleStateEnum != RoleState.Attack) return;

            //��ǰ״̬�˳�
            if (CurrentRoleState != null)
            {
                CurrentRoleState.OnLevel();
            }
            //�滻��ǰ״̬
            CurrentRoleStateEnum = nextState;
            CurrentRoleState = states[CurrentRoleStateEnum];
            if (CurrentRoleStateEnum == RoleState.Idle)
            {
                CurIdleType = NextIdleType;
            }
            //��ǰ״̬����
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

