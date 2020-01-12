using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RoleTransferAttackInfo
    {
        /// <summary>
        /// 攻击者ID
        /// </summary>
        public long AttackRoleID;
        /// <summary>
        /// 被攻击者id
        /// </summary>
        public long BeAttaclRoleID;
        /// <summary>
        /// 攻击技能Id
        /// </summary>
        public int SkillId;
        /// <summary>
        /// 攻击技能编号
        /// </summary>
        public int SkillLevel;
        /// <summary>
        /// 是否暴击
        /// </summary>
        public bool IsCri;
        /// <summary>
        /// 伤害数值
        /// </summary>
        public int HurtValue;
        /// <summary>
        /// 附加的Buff
        /// </summary>
        public int BuffId = -1;
    }
}
