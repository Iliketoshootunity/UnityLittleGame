using System;
using UnityEngine;
using System.Collections;

namespace EasyFrameWork.Buff
{
    public class Buff
    {

        public Action<Buff> BuffFinished;

        public virtual void BuffEnter()
        {

        }

        public virtual void BuffUpdate()
        {

        }

        public virtual void BuffExit()
        {
            if (BuffFinished != null)
            {
                BuffFinished(this);
            }
        }
    }

}
