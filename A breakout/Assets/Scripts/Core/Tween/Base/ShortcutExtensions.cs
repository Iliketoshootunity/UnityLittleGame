using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public static class ShortcutExtensions
    {
        public static Tweener DoMove(this Transform target, Vector3 endValue, float variable)
        {
            Tweener t = new TransformPositionTweener(target, endValue, variable);
            t.DoTween();
            return t;
        }
        public static Tweener DoMove(this RectTransform target, Vector3 endValue, float variable)
        {
            Tweener t = new RectTransformPositionTweener(target, endValue, variable);
            t.DoTween();
            return t;
        }
        public static Tweener DoScale(this Transform target, Vector3 endValue, float variable)
        {
            Tweener t = new TransformScaleTweener(target, endValue, variable);
            t.DoTween();
            return t;
        }
        public static Tweener DoColor(this SpriteRenderer target, Color endColor, float variable)
        {
            Tweener t = new SpriteColorTweener(target, endColor, variable);
            t.DoTween();
            return t;
        }


    }

}
