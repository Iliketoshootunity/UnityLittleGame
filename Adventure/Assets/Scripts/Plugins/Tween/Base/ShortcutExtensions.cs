using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public static class ShortcutExtensions
    {
        /// <summary>
        /// Transform 移动
        /// </summary>
        /// <param name="target"></param>
        /// <param name="endValue"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener DoMove(this Transform target, Vector3 endValue, float variable)
        {
            Tweener t = new TransformPositionTweener(target, endValue, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }
        /// <summary>
        /// RectTransform 移动
        /// </summary>
        /// <param name="target"></param>
        /// <param name="endValue"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener DoMove(this RectTransform target, Vector3 endValue, float variable)
        {
            Tweener t = new RectTransformPositionTweener(target, endValue, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }
        /// <summary>
        /// Transform 缩放
        /// </summary>
        /// <param name="target"></param>
        /// <param name="endValue"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener DoScale(this Transform target, Vector3 endValue, float variable)
        {
            Tweener t = new TransformScaleTweener(target, endValue, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }
        /// <summary>
        /// Transform 欧拉
        /// </summary>
        /// <param name="target"></param>
        /// <param name="endValue"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener DoEuler(this Transform target, Vector3 endValue, float variable)
        {
            Tweener t = new TransformEulerTweener(target, endValue, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }
        /// <summary>
        /// SpriteRenderer 颜色
        /// </summary>
        /// <param name="target"></param>
        /// <param name="endColor"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener DoColor(this SpriteRenderer target, Color endColor, float variable)
        {
            Tweener t = new SpriteColorTweener(target, endColor, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }

        /// <summary>
        /// Graphic 颜色
        /// </summary>
        /// <param name="target"></param>
        /// <param name="endColor"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener DoColor(this Graphic target, Color endColor, float variable)
        {
            Tweener t = new UIColorTweener(target, endColor, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }
        /// <summary>
        /// float 数值
        /// </summary>
        /// <param name="number"></param>
        /// <param name="toNumber"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static Tweener To(DoGetter<float> getter, DoSetter<float> setter, float toNumber, float variable)
        {
            Tweener t = new FloatTweener(getter, setter, toNumber, variable);
            TweenManager.Instance.AddTween(t);
            return t;
        }


    }

}
