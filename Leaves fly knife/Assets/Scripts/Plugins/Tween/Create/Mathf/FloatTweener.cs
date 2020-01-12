using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    /// <summary>
    ///浮点数渐变
    /// </summary>
	public class FloatTweener : Tweener, IIsSpeed, IIsRelative
    {
        private float m_Value;
        [SerializeField]
        private bool m_IsSpeed;
        [SerializeField]
        private bool m_IsRelative;

        public bool IsSpeed
        {
            get
            {
                return m_IsSpeed;
            }
            set
            {
                m_IsSpeed = value;
            }
        }

        public bool IsRelative
        {
            get
            {
                return m_IsRelative;
            }
            set
            {
                m_IsRelative = value;
            }
        }

        public float TargetValue;
        protected float m_StartValue;
        protected float m_EndValue;
        protected float m_FixedStartValue;
        protected float m_FixedEndValue;

        private DoSetter<float> m_Setter;
        public FloatTweener(DoGetter<float> getter, DoSetter<float> setter, float target, float variable) : base(variable)
        {
            Variable = variable;
            m_Value = getter();
            m_Setter += setter;
            m_FixedStartValue = m_Value;
            m_FixedEndValue = target;
            TargetValue = target;
            m_IsFirstDelay = true;
        }

        public FloatTweener() : base()
        {

        }
        protected override void InitPrepare()
        {
            if (IsRelative)
            {
                m_FixedEndValue = m_FixedStartValue + TargetValue;
            }
            else
            {
                m_FixedEndValue = TargetValue;
            }
            if (IsSpeed)
            {
                float dis = Mathf.Abs(m_FixedEndValue - m_StartValue);
                Variable = dis / Variable;
            }
            m_StartValue = m_FixedStartValue;
            m_EndValue = m_FixedEndValue;
        }

        protected override void UpdateTween(float process)
        {
            m_Value = Mathf.LerpUnclamped(m_StartValue, m_EndValue, process);
            m_Setter(m_Value);
        }
    }
}
