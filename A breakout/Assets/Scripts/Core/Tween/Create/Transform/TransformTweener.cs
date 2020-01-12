using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public abstract class TransformTweener : Tweener
    {
        public enum TransformTweenerType
        {
            Position,
            RectPosotion,
            Scale,
            Euler
        }
        public bool IsSpeed;
        public bool IsLocal;
        public bool IsRelative;
        public Vector3 TargetPos;
        protected Vector3 m_StartPos;
        protected Vector3 m_EndPos;
        protected Vector3 m_FixedStartPos;
        protected Vector3 m_FixedEndPos;
        protected Transform m_Transform;
        protected TransformTweenerType m_TransformTweenerType;
        public TransformTweener(Transform transform, Vector3 target, float variable) : base(variable)
        {
            Variable = variable;
            m_Transform = transform;
            TargetPos = target;
            m_IsFirstDelay = true;
        }
        public TransformTweener()
        {

        }
        public void SetTransform(Transform transform)
        {
            m_Transform = transform;
            m_IsFirstDelay = true;
        }

        protected void SetTransformTweenerType(TransformTweenerType transformTweenerType)
        {
            m_TransformTweenerType = transformTweenerType;
        }
        protected override void InitPrepare()
        {
            switch (m_TransformTweenerType)
            {
                case TransformTweenerType.RectPosotion:
                    m_FixedStartPos = ((RectTransform)m_Transform).anchoredPosition3D;
                    break;
                case TransformTweenerType.Position:
                    if (IsLocal)
                    {
                        m_FixedStartPos = m_Transform.localPosition;
                    }
                    else
                    {
                        m_FixedStartPos = m_Transform.position;
                    }
                    break;
                case TransformTweenerType.Scale:
                    m_FixedStartPos = m_Transform.localScale;
                    break;
                case TransformTweenerType.Euler:
                    if (IsLocal)
                    {
                        m_FixedStartPos = m_Transform.localEulerAngles;
                    }
                    else
                    {
                        m_FixedStartPos = m_Transform.eulerAngles;
                    }
                    break;
                default:
                    break;
            }
            if (IsRelative)
            {
                m_FixedEndPos = m_FixedStartPos + TargetPos;
            }
            else
            {
                m_FixedEndPos = TargetPos;
            }
            if (IsSpeed)
            {
                float dis = Vector3.Distance(m_FixedStartPos, m_FixedEndPos);
                Variable = dis / Variable;
            }
        }
        protected override void LoopPrepare()
        {
            if (IsFrom)
            {
                m_EndPos = m_FixedStartPos;
                m_StartPos = m_FixedEndPos;
            }
            else
            {
                m_StartPos = m_FixedStartPos;
                m_EndPos = m_FixedEndPos;
            }
            switch (m_TransformTweenerType)
            {
                case TransformTweenerType.RectPosotion:
                    ((RectTransform)m_Transform).anchoredPosition3D = m_StartPos;
                    break;
                case TransformTweenerType.Position:
                    if (IsLocal)
                    {
                        m_Transform.localPosition = m_StartPos;
                    }
                    else
                    {
                        m_Transform.position = m_StartPos;
                    }
                    break;
                case TransformTweenerType.Scale:
                    m_Transform.localScale = m_StartPos;
                    break;
                case TransformTweenerType.Euler:
                    if (IsLocal)
                    {
                        m_Transform.localEulerAngles = m_StartPos;
                    }
                    else
                    {
                        m_Transform.eulerAngles = m_StartPos;
                    }
                    break;
                default:
                    break;
            }
        }
        protected override void UpdateTween(float process)
        {
            Vector3 v = Vector3.LerpUnclamped(m_StartPos, m_EndPos, process);
            switch (m_TransformTweenerType)
            {
                case TransformTweenerType.RectPosotion:
                    ((RectTransform)m_Transform).anchoredPosition3D = v;
                    break;
                case TransformTweenerType.Position:
                    if (IsLocal)
                    {
                        m_Transform.localPosition = v;
                    }
                    else
                    {
                        m_Transform.position = v;
                    }

                    break;
                case TransformTweenerType.Scale:
                    m_Transform.localScale = v;
                    break;
                case TransformTweenerType.Euler:
                    if (IsLocal)
                    {
                        m_Transform.localEulerAngles = v;
                    }
                    else
                    {
                        m_Transform.eulerAngles = v;
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
