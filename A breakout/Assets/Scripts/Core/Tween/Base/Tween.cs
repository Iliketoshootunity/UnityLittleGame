using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    public enum TweenLoopType
    {
        None,
        Restart,
        YoYo
    }
    [System.Serializable]
    /// <summary>
    /// 补间动画
    /// </summary>
    public abstract class Tween
    {
        [HideInInspector]
        public int Index;

        public float Delay;
        public float Variable;
        public int LoopMaxCount;
        public TweenLoopType LoopType;
        public AnimationCurve Curve;
        public Action EndAction;
        public bool IsFrom;
        public bool IsAutoPlay;

        protected Coroutine m_TweenCoroutine;
        protected float m_Timer;
        protected int m_LoopCount;
        protected bool m_IsFirstDelay = true;

    }
}
