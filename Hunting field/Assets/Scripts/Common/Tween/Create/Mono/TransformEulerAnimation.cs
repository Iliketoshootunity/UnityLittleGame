using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class TransformEulerAnimation : DotweenAnimation
    {

        public TransformEulerTweener TransformEulerTweener;
        private void Start()
        {
            if (TransformEulerTweener.IsAutoPlay)
            {
                Play();
            }
        }

        public override void Play()
        {
            base.Play();
            TweenManager.Instance.AddTween(TransformEulerTweener);
            TransformEulerTweener.DoTween();
        }
        protected override void Init()
        {
            base.Init();
            TransformEulerTweener.SetTransform(this.transform);
        }
    }
}
