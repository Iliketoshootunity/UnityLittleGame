using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RectTransformPositionAnimation : DotweenAnimation
    {
        public RectTransformPositionTweener RectTransformPositionTweener;
        private void Start()
        {
            if (RectTransformPositionTweener.IsAutoPlay)
            {
                Play();
            }
        }
        public override void Play()
        {
            base.Play();
            RectTransformPositionTweener.Init();
            TweenManager.Instance.AddTween(RectTransformPositionTweener);
        }
        protected override void Init()
        {
            RectTransformPositionTweener.SetTransform(this.transform);
            base.Init();
        }
    }
}
