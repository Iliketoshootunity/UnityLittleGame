using UnityEngine;
using System.Collections;
//using DG.Tweening;
using EasyFrameWork;
//using EasyFrameWork;
namespace AA
{
    public class Test : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public AnimationCurve Curve;
        void Start()
        {
            //((RectTransform)transform).DOMove(Vector3.one, 1).SetBaseSpeed(false).SetAnimatorCurve(EaseType.Linner).SetIsLocal(true).SetLoopCount(-1).SetLoopType(TweenLoopType.YoYo);
            //transform.DOMove().SetDelay
            //SpriteRenderer.DOColor
            float number = 0;
            //DOTween.To(() => number, (x) => number = x, 1, 1);

            transform.DoMove(new Vector3(0, 10, 0), 1).SetIsSpeed(true).SetIsRelative(true).SetIsFrom(true).SetDelay(0.7f).SetAnimatorCurve(Curve).SetLoopType(TweenLoopType.YoYo).SetLoopCount(-1);
            //transform.DOMove(new Vector3(0, 10, 0), 3).SetRelative(true).From(true);

        }
        void Update()
        {

        }
    }
}
