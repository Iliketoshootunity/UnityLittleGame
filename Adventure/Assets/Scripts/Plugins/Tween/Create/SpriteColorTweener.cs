using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    [System.Serializable]
    public class SpriteColorTweener : Tweener
    {

        public Color TargetColor;
        private Color m_StartColor;
        private Color m_EndColor;
        private Color m_FixedStartColor;
        private Color m_FixedEndColor;

        private SpriteRenderer m_Renderer;
        public SpriteColorTweener(SpriteRenderer renderer, Color targetColor, float variable) : base(variable)
        {
            m_Renderer = renderer;
            TargetColor = targetColor;
            Variable = variable;
        }

        public SpriteColorTweener() : base()
        {

        }
        public void SetRenderer(SpriteRenderer renderer)
        {
            m_Renderer = renderer;
            m_IsFirstDelay = true;
        }
        protected override void InitPrepare()
        {
            m_FixedStartColor = m_Renderer.color;
            m_FixedEndColor = TargetColor;
            m_Renderer.color = m_StartColor;
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
            m_Renderer.color = m_StartColor;
        }
        protected override void UpdateTween(float process)
        {
            m_Renderer.color = Color.LerpUnclamped(m_StartColor, m_EndColor, process);
        }


    }
}
