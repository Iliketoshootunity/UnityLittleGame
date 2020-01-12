using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class TransfromScaleAnimation : DotweenAnimation
    {
        public TransformScaleTweener TransformScaleTweener;
        private void Start()
        {
            if (TransformScaleTweener.IsAutoPlay)
            {
                Play();
            }
        }

        public override void Play()
        {
            base.Play();
            TransformScaleTweener.DoTween();
        }
        protected override void Init()
        {
            base.Init();
            TransformScaleTweener.SetTransform(this.transform);
        }

    }
}
