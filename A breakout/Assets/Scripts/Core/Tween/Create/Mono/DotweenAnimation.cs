using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public abstract class DotweenAnimation : MonoBehaviour
    {

        private bool m_Init;
        public virtual void Play()
        {
            if (!m_Init)
            {
                Init();
                m_Init = true;
            }
        }
        protected virtual void Init()
        {

        }

    }
}
