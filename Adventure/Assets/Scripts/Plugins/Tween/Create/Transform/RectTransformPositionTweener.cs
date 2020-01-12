using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    [System.Serializable]
    public class RectTransformPositionTweener : TransformTweener
    {
        public RectTransformPositionTweener(Transform transform, Vector3 target, float variable) : base(transform, target, variable)
        {
            SetTransformTweenerType(TransformTweenerType.RectPosotion);
        }
        public RectTransformPositionTweener() : base()
        {
            SetTransformTweenerType(TransformTweenerType.RectPosotion);
        }
    }
}
