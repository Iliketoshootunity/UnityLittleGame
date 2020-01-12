using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class TransformEulerTweener : TransformTweener
    {
        public TransformEulerTweener(Transform transform, Vector3 target, float variable) : base(transform, target, variable)
        {
            SetTransformTweenerType(TransformTweenerType.Euler);
        }
        public TransformEulerTweener()
        {
            SetTransformTweenerType(TransformTweenerType.Euler);
        }
    }
}
