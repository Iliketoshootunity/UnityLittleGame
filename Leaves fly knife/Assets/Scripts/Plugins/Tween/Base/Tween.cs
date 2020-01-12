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
        [HideInInspector]
        public bool IsAutoRelease = true;
        [HideInInspector]
        public bool IsPlay;

        public float Delay;
        public float Variable;
        public int LoopMaxCount;
        public TweenLoopType LoopType;
        public AnimationCurve Curve;
        public Action StartAction;
        public Action<float> UpdateAction;
        public Action EndAction;
        public bool IsFrom;
        public bool IsAutoPlay;

    }
}
