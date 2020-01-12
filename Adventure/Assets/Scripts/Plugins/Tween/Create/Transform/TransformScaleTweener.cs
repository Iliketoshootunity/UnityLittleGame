using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    [System.Serializable]
    public class TransformScaleTweener : TransformTweener
    {
        public TransformScaleTweener(Transform transform, Vector3 target, float variable) : base(transform, target, variable)
        {
            SetTransformTweenerType(TransformTweenerType.Scale);
        }
        public TransformScaleTweener() : base()
        {
            SetTransformTweenerType(TransformTweenerType.Scale);
        }
    }
}
