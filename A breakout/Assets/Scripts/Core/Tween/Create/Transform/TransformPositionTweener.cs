using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    [System.Serializable]
    public class TransformPositionTweener : TransformTweener
    {
        public TransformPositionTweener(Transform transform, Vector3 target, float variable) : base(transform, target, variable)
        {
            SetTransformTweenerType(TransformTweenerType.Position);
        }
        public TransformPositionTweener()
        {
            SetTransformTweenerType(TransformTweenerType.Position);
        }
    }
}
