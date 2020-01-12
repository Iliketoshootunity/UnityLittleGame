using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public enum EaseType
    {
        Linner
    }

    public class Ease
    {
        public static AnimationCurve GetAnimationCurveByCurve(EaseType ease)
        {
            AnimationCurve c = new AnimationCurve();
            switch (ease)
            {
                case EaseType.Linner:
                    c.AddKey(0, 0);
                    c.AddKey(0.5f, 0.5f);
                    c.AddKey(1, 1);
                    break;
                default:
                    c.AddKey(0, 0);
                    c.AddKey(0.5f, 0.5f);
                    c.AddKey(1, 1);
                    break;
            }
            return c;
        }


    }
}
