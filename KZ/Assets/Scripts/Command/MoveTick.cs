using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class MoveTick : PlotTick
    {
        public float MoveSpeed;
        public Vector3 MoveEndPos;
        public Transform MoveActor;
        public override void OnUpdate()
        {
            MoveActor.localPosition = Vector3.MoveTowards(MoveActor.localPosition, MoveEndPos, MoveSpeed * Time.deltaTime);
            if (MoveActor.localPosition == MoveEndPos)
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
