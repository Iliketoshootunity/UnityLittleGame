using UnityEngine;
using System.Collections;
using System.Reflection;

namespace EasyFrameWork.Buff
{

    /// <summary>
    /// Buff的类型
    /// </summary>
    public enum BuffType
    {
        None,

        /// <summary>
        /// 回复HP
        /// </summary>
        AddHp,
        /// <summary>
        /// 回复到最大血量
        /// </summary>
        AddMaxHp,
        /// <summary>
        /// 减血
        /// </summary>
        ReduceHp,
        /// <summary>
        /// 减最大生命值
        /// </summary>
        ReduceMaxpHp,

        /// <summary>
        /// 眩晕
        /// </summary>
        AddVertigo,
        /// <summary>
        /// 浮空
        /// </summary>
        AddFloated,
        /// <summary>
        /// 击退
        /// </summary>
        AddRepel,
        /// <summary>
        /// 冲刺
        /// </summary>
        AddSprint,
        /// <summary>
        /// 被击浮空
        /// </summary>
        AddDamageFloated,
        /// <summary>
        /// 忽视重力
        /// </summary>
        AddIsIgnoreGravity,
    }

    /// <summary>
    /// 叠加类型
    /// </summary>
    public enum BuffOverlap
    {
        None,
        /// <summary>
        /// 增加时间
        /// </summary>
        StackedTime,
        /// <summary>
        /// 叠加层数
        /// </summary>
        StackedLayer,
        /// <summary>
        /// 重置时间
        /// </summary>
        ResterTime

    }

    /// <summary>
    /// 关闭类型，这个枚举的作用是控制倒计时结束后，应该是减一层，还是直接清空buff
    /// </summary>
    public enum BuffShutDownType
    {
        /// <summary>
        /// 关闭所有
        /// </summary>
        All,
        /// <summary>
        /// 单层关闭
        /// </summary>
        Layer
    }

    /// <summary>
    /// 执行类型
    /// </summary>
    public enum BuffCalculateType
    {
        /// <summary>
        /// 一次
        /// </summary>
        Once,
        /// <summary>
        /// m每次
        /// </summary>
        Loop
    }

    [System.Serializable]
    public class BuffBase
    {

        /// <summary>
        /// BuffId
        /// </summary>
        public int BuffID;

        /// <summary>
        /// Buff类型
        /// </summary>
        public BuffType BuffType;

        /// <summary>
        /// 执行类型
        /// </summary>
        public BuffCalculateType BuffCalculateType = BuffCalculateType.Loop;

        /// <summary>
        /// 叠加类型
        /// </summary>
        public BuffOverlap BuffOverlap = BuffOverlap.StackedLayer;

        /// <summary>
        /// 清除类型
        /// </summary>
        public BuffShutDownType BuffShutDownType = BuffShutDownType.All;

        /// <summary>
        /// 如果是叠加层数，表示最大层数，如果是时间，表示最大时间
        /// </summary>
        public int MaxLimit = 0;

        /// <summary>
        /// 执行时间
        /// </summary>
        public float Time;

        /// <summary>
        /// 间隔时间
        /// </summary>
        public float CallFrequency = 1;

        /// <summary>
        /// 执行数值，比如加血就是每次加多少
        /// </summary>
        public float Num;
    }
}

