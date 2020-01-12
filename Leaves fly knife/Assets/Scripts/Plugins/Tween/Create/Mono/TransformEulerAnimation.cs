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
        private void OnDestroy()
        {
            TransformEulerTweener.DoKill();
        }

        public override void Play()
        {
            base.Play();
            TransformEulerTweener.Init();
            TweenManager.Instance.AddTween(TransformEulerTweener);
        }
        protected override void Init()
        {
            base.Init();
            TransformEulerTweener.SetTransform(this.transform);
        }
    }
}
