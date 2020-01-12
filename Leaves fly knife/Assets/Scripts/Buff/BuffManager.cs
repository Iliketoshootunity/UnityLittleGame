using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EasyFrameWork;
using System;

namespace EasyFrameWork.Buff
{
    public class BuffManager : MonoSingleton<BuffManager>
    {

        public List<Buff> BuffList = new List<Buff>();

        void Update()
        {

        }

        public void AddBuff(Buff buff)
        {
            BuffList.Add(buff);
            buff.BuffFinished += OnBuffFinished;
            buff.BuffEnter();
        }

        private void OnBuffFinished(Buff buff)
        {
            BuffList.Remove(buff);
        }
    }
}



