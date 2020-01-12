using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RoleHurtState : RoleStateAbstract
    {
        public override RoleStateType RoleStateType
        {
            get
            {
                return RoleStateType.Hurt;
            }
        }

        public override void HandleInput()
        {

        }

        public override void OnEnter()
        {
            m_Role.ToIdle();
        }

        public override void OnLevel()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
