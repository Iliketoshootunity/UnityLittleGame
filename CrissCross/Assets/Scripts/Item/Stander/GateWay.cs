using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class GateWay : Stander
    {
        [SerializeField]
        private GameObject m_Light;
        public override StanderType StanderType
        {
            get
            {
                return StanderType.GateWay;
            }
        }
        public void ToAwake()
        {
            m_Light.SetActive(true);
        }

    }
}
