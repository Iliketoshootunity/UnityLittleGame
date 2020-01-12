using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{

    public class TransformPositionAnimation : DotweenAnimation
    {
        public TransformPositionTweener TransformPositionTweener;

        private void Awake()
        {        
        }
        private void Start()
        {
            if (TransformPositionTweener.IsAutoPlay)
            {
                Play();
            }
        }
        private void OnDestroy()
        {
            TransformPositionTweener.DoKill();
        }
        public override void Play()
        {
            base.Play();
            TransformPositionTweener.Init();
            TweenManager.Instance.AddTween(TransformPositionTweener);      

        }
        protected override void Init()
        {
            base.Init();
            TransformPositionTweener.SetTransform(this.transform);
        }

    }
}
