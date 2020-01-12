using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    public static class TweenSettingsExtensions
    {
        public static T SetDelay<T>(this T t, float delay) where T : Tween
        {
            t.Delay = delay;
            return t;
        }
        public static T SetIsSpeed<T>(this T t, bool isSpeed) where T : Tween
        {
            Tween t1 = t;
            IIsSpeed rt = (IIsSpeed)t1;
            if (rt != null)
            {
                rt.IsSpeed = isSpeed;
            }
            return t;
        }
        public static T SetIsRelative<T>(this T t, bool isRelative) where T : Tween
        {
            Tween t1 = t;
            IIsRelative rt = (IIsRelative)t1;
            if (rt != null)
            {
                rt.IsRelative = isRelative;
            }
            return t;
        }
        public static T SetLoopType<T>(this T t, TweenLoopType loopType) where T : Tween
        {
            t.LoopType = loopType;
            return t;
        }
        public static T SetIsFrom<T>(this T t, bool isFrom) where T : Tween
        {
            t.IsFrom = isFrom;
            return t;
        }
        public static T SetIsLocal<T>(this T t, bool isLocal) where T : Tween
        {
            Tween t1 = t;
            TransformTweener rt = (TransformTweener)t1;
            if (rt != null)
            {
                rt.IsRelative = isLocal;
            }
            return t;
        }
        public static T SetAutoPlay<T>(this T t, bool isAutoPlay) where T : Tween
        {
            t.IsAutoPlay = isAutoPlay;
            return t;
        }
        public static T SetLoopCount<T>(this T t, int loopCount) where T : Tween
        {
            t.LoopMaxCount = loopCount;
            return t;
        }
        public static T SetAnimatorCurve<T>(this T t, AnimationCurve curve) where T : Tween
        {
            t.Curve = curve;
            return t;
        }
        public static T SetAnimatorCurve<T>(this T t, EaseType ease) where T : Tween
        {
            t.Curve = Ease.GetAnimationCurveByCurve(ease);
            return t;
        }
        public static T SetStartAction<T>(this T t, Action startAction) where T : Tween
        {
            t.StartAction = startAction;
            return t;
        }
        public static T SetEndAction<T>(this T t, Action endAction) where T : Tween
        {
            t.EndAction = endAction;
            return t;
        }
        public static T SetUpdateAction<T>(this T t, Action<float> updateAction) where T : Tween
        {
            t.UpdateAction = updateAction;
            return t;
        }
        public static void DoKill<T>(this T t) where T : Tween
        {
            TweenManager.Instance.StopTween(t);
        }

    }
}
