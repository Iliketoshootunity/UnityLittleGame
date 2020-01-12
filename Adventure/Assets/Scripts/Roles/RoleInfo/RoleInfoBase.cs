using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using System.Collections.Generic;

namespace EasyFrameWork
{
    [System.Serializable]
    public class RoleInfoBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId;
        /// <summary>
        /// 昵称
        /// </summary>
        public string RoleNickName;
        /// <summary>
        /// 等级
        /// </summary>
        public int Level;
        /// <summary>
        /// 经验
        /// </summary>
        public int Exp;
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP;
        /// <summary>
        /// 当前生命值
        /// </summary>
        public int CurrentHP;
        /// <summary>
        /// 物攻
        /// </summary>
        public int PhyAtk;
        /// <summary>
        /// 魔攻
        /// </summary>
        public int MgicAtk;
        /// <summary>
        /// 物防
        /// </summary>
        public int PhyDef;
        /// <summary>
        /// 魔防防
        /// </summary>
        public int MgicDef;
        /// <summary>
        /// 暴击率
        /// </summary>
        public int Cri;
        /// <summary>
        /// 暴击值
        /// </summary>
        public float CriValue;

    }
}
