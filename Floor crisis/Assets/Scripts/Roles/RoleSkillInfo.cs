using System.Collections;
using System.Collections.Generic;
using EasyFrameWork.Buff;
using UnityEngine;

/// <summary>
/// 角色技能信息
/// </summary>
[System.Serializable]
public class RoleSkillInfo
{
    /// <summary>
    /// 编号
    /// </summary>
    public int Index;
    /// <summary>
    /// 是否是近战攻击
    /// </summary>
    public bool IsMeleeAttack;
    /// <summary>
    /// 攻击伤害
    /// </summary>
    public float AttackValue;

    /// <summary>
    /// 攻击数量
    /// </summary>
    public int AttackCount;

    /// <summary>
    ///如果是近战攻击 攻击范围
    /// </summary>
    public float AttackRange;

    /// <summary>
    ///如果是远程攻击 子弹名字
    /// </summary>
    public string BulletName;

    /// <summary>
    /// 攻击计算延迟
    /// </summary>
    public float HurtDelay;

    /// <summary>
    /// 是否屏幕震动
    /// </summary>
    public bool IsCameraShake;

    /// <summary>
    /// 特效名字
    /// </summary>
    public string EffectName;

    /// <summary>
    /// 特效存在时间
    /// </summary>
    public float EffectLifeTime;

    /// <summary>
    /// 给自己附加异常状态
    /// </summary>
    public BuffType BuffToMe;

    /// <summary>
    /// 给敌人附加异常状态
    /// </summary>
    public BuffType BuffToEnemy;
}
