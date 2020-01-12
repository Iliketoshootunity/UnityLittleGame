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
        public static T SetBaseSpeed<T>(this T t, bool baseSpeed) where T : Tween
        {
            Tween t1 = t;
            TransformTweener rt = (TransformTweener)t1;
            if (rt != null)
            {
                rt.IsSpeed = baseSpeed;
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
        public static T SetEndAction<T>(this T t, Action endAction) where T : Tween
        {
            t.EndAction = endAction;
            return t;
        }
        public static void DoKill<T>(this T t) where T : Tween
        {
            TweenManager.Instance.StopCoroutineByIndex(t.Index);

        }
    }
}
