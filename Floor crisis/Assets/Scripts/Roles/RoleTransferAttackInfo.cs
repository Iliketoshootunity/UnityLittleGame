using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// 角色攻击信息传输类
    /// </summary>
    public class RoleTransferAttackInfo
    {
        /// <summary>
        /// 攻击者
        /// </summary>
        public RoleCtrl AttackRoleCtrl;
        /// <summary>
        /// 被攻击者id
        /// </summary>
        public RoleCtrl BeAttackRoleCtrl;

        /// <summary>
        /// 技能信息
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

