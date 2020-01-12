using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RectTransformPositionAnimation : DotweenAnimation
    {
        public float Dealy;
        public float Variable;
        public int LoopMaxCount;
        public TweenLoopType LoopType;
        public AnimationCurve Curve;
        public bool IsFrom;
        public bool IsAutoPlay;
        public bool IsLocal;
        public bool IsSpeed;
        public bool IsRelative;
        public Vector3 TargetPos;

        private RectTransform m_Rect;


        private void Start()
        {
            m_Rect = GetComponent<RectTransform>();
            if (IsAutoPlay)
            {
                Play();
            }


        }
        public override void Play()
        {
            base.Play();
            m_Rect.DoMove(TargetPos, Variable).SetDelay(Dealy).SetLoopCount(LoopMaxCount).SetLoopType(LoopType).SetAnimatorCurve(Curve).SetIsFrom(IsFrom)
              .SetIsLocal(IsLocal).SetIsSpeed(IsSpeed).SetIsRelative(IsRelative);
        }
        protected override void Init()
        {

            base.Init();
        }
    }
}
