using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RotTick : PlotTick
    {
        public float RotSpeed;
        public Vector3 EndAngle;
        public Transform RotActor;
        public override void OnUpdate()
        {
            RotActor.localEulerAngles = Vector3.Lerp(RotActor.localEulerAngles, EndAngle, RotSpeed * Time.deltaTime);
            if (Vector3.Distance(RotActor.localEulerAngles, EndAngle) < 0.02f)
            {
                IsFinsh = true;
                if (OnFinsh != null)
                {
                    OnFinsh();
                }
            }
        }


    }
}
