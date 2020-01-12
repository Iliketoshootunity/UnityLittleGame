using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class SpriteColorAnimation : DotweenAnimation
    {

        public SpriteColorTweener SpriteColorTweener;
        public SpriteRenderer SpriteRenderer;

        public override void Play()
        {
            base.Play();
            TweenManager.Instance.AddTween(SpriteColorTweener);
            SpriteColorTweener.DoTween();

        }

        // Use this for initialization
        void Start()
        {
            if (SpriteColorTweener.IsAutoPlay && SpriteRenderer != null && gameObject.activeSelf)
            {
                Play();
            }
        }
        protected override void Init()
        {
            base.Init();
            if (SpriteRenderer == null)
            {
                SpriteRenderer = GetComponent<SpriteRenderer>();
            }
            SpriteColorTweener.SetRenderer(SpriteRenderer);
        }
    }
}
