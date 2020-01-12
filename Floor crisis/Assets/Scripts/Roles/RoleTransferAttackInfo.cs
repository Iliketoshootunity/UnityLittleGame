using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// ��ɫ������Ϣ������
    /// </summary>
    public class RoleTransferAttackInfo
    {
        /// <summary>
        /// ������
        /// </summary>
        public RoleCtrl AttackRoleCtrl;
        /// <summary>
        /// ��������id
        /// </summary>
        public RoleCtrl BeAttackRoleCtrl;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public RoleSkillInfo SkillInfo;

        public RoleTransferAttackInfo(RoleCtrl attackRole, RoleCtrl beAttackRole, RoleSkillInfo skillInfo)
        {
            AttackRoleCtrl = attackRole;
            BeAttackRoleCtrl = beAttackRole;
            SkillInfo = skillInfo;
        }
    }
}

