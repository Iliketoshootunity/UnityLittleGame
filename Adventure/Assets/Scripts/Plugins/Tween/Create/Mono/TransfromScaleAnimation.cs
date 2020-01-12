using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class TransfromScaleAnimation : DotweenAnimation
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

        private Transform m_Trans;


        private void Start()
        {
            m_Trans = GetComponent<Transform>();
            if (IsAutoPlay)
            {
                Play();
            }


        }
        public override void Play()
        {
            base.Play();
            m_Trans.DoScale(TargetPos, Variable).SetDelay(Dealy).SetLoopCount(LoopMaxCount).SetLoopType(LoopType).SetAnimatorCurve(Curve).SetIsFrom(IsFrom)
              .SetIsLocal(IsLocal).SetIsSpeed(IsSpeed).SetIsRelative(IsRelative);
        }
        protected override void Init()
        {

            base.Init();
        }

    }
}
