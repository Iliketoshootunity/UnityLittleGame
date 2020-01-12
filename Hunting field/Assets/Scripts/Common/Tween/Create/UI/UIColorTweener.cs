using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UIColorTweener : Tweener
    {

        public Color TargetColor;
        private Color m_StartColor;
        private Color m_EndColor;
        private Color m_FixedStartColor;
        private Color m_FixedEndColor;
        private Graphic m_Graphic;
        public UIColorTweener(Graphic graphic, Color targetColor, float variable) : base(variable)
        {
            m_Graphic = graphic;
            TargetColor = targetColor;
            Variable = variable;
        }

        public UIColorTweener() : base()
        {

        }
        public void SetGraphic(Graphic graphic)
        {
            m_Graphic = graphic;
            m_IsFirstDelay = true;
        }
        protected override void InitPrepare()
        {
            m_FixedStartColor = m_Graphic.color;
            m_FixedEndColor = TargetColor;
            m_Graphic.color = m_StartColor;
        }

        protected override void LoopPrepare()
        {
            if (IsFrom)
            {
                m_EndColor = m_FixedStartColor;
                m_StartColor = m_FixedEndColor;
            }
            else
            {
                m_StartColor = m_FixedStartColor;
                m_EndColor = m_FixedEndColor;
            }
            m_Graphic.color = m_StartColor;
        }
        protected override void UpdateTween(float process)
        {
            m_Graphic.color = Color.LerpUnclamped(m_StartColor, m_EndColor, process);
        }
    }
}
