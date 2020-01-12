using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    public abstract class Faction : MonoBehaviour
    {
        public Action OnRoundEnd;

        public abstract void RoundStart();

        public abstract void RoundUpdate();
        public virtual void RoundEnd()
        {
            if (OnRoundEnd != null)
            {
                OnRoundEnd();
            }
        }


    }
}
