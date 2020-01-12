using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{

    public class TransformPositionAnimation : DotweenAnimation
    {
        public TransformPositionTweener TransformPositionTweener;

        private void Start()
        {
            if (TransformPositionTweener.IsAutoPlay)
            {
                Play();
            }
        }

        public override void Play()
        {
            base.Play();
            TransformPositionTweener.DoTween();
        }
        protected override void Init()
        {
            base.Init();
            TransformPositionTweener.SetTransform(this.transform);
        }

    }
}
