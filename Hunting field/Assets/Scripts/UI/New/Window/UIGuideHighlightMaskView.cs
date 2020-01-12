using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UIGuideHighlightMaskView : MaskableGraphic, ICanvasRaycastFilter
    {
        [SerializeField]
        private RectTransform m_Target;
        private Vector3 m_TargetMin = Vector3.zero;
        private Vector3 m_TargetMax = Vector3.zero;
        private bool m_CanRefresh = true;

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// 设置镂空的目标
        /// </summary>
        public void SetTarget(RectTransform target)
        {
            m_CanRefresh = true;
            m_Target = target;
            _RefreshView();
        }

        private void SetTarget(Vector3 tarMin, Vector3 tarMax)
        {
            if (tarMin == m_TargetMin && tarMax == m_TargetMax)
                return;
            m_TargetMin = tarMin;
            m_TargetMax = tarMax;
            SetAllDirty();
        }

        private void _RefreshView()
        {
            if (!m_CanRefresh) return;
            m_CanRefresh = false;

            if (null == m_Target)
            {
                SetTarget(Vector3.zero, Vector3.zero);
                SetAllDirty();
            }
            else
            {
                Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform, m_Target);
                SetTarget(bounds.min, bounds.max);
            }
        }
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (null == m_Target) return true;
            // 将目标对象范围内的事件镂空（使其穿过）
            return !RectTransformUtility.RectangleContainsScreenPoint(m_Target, sp, eventCamera);
        }
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            if (m_TargetMin == Vector3.zero && m_TargetMax == Vector3.zero)
            {
                base.OnPopulateMesh(vh);
            }
            vh.Clear();

            //填充点点 顺时针填充顶点 法线看向摄像机 为 三角面的正面
            UIVertex vert = UIVertex.simpleVert;
            vert.color = color;

            Vector2 selfPiovt = rectTransform.pivot;
            Rect selfRect = rectTransform.rect;
            //外圈左值x
            float outerLx = -selfPiovt.x * selfRect.width;
            //外圈右值
            float outerRx = (1 - selfPiovt.x) * selfRect.width;
            //外圈顶值y
            float outerTy = (1 - selfPiovt.y) * selfRect.height;
            //外圈底值y
            float outerBy = -selfPiovt.y * selfRect.height;

            //外圈左上点
            Vector3 ltPos1 = new Vector3(outerLx, outerTy);
            vert.position = ltPos1;
            vh.AddVert(vert);
            //外圈右上点
            Vector3 rtPos1 = new Vector3(outerRx, outerTy);
            vert.position = rtPos1;
            vh.AddVert(vert);
            //外圈右下点
            Vector3 rbPos1 = new Vector3(outerRx, outerBy);
            vert.position = rbPos1;
            vh.AddVert(vert);
            //外圈左下点
            Vector3 lbPos1 = new Vector3(outerLx, outerBy);
            vert.position = lbPos1;
            vh.AddVert(vert);

            //内圈左上点
            Vector3 ltPos2 = new Vector3(m_TargetMin.x, m_TargetMax.y);
            vert.position = ltPos2;
            vh.AddVert(vert);
            //外圈右上点
            Vector3 rtPos2 = new Vector3(m_TargetMax.x, m_TargetMax.y);
            vert.position = rtPos2;
            vh.AddVert(vert);
            //外圈右下点
            Vector3 rbPos2 = new Vector3(m_TargetMax.x, m_TargetMin.y);
            vert.position = rbPos2;
            vh.AddVert(vert);
            //外圈左下点
            Vector3 lbPos2 = new Vector3(m_TargetMin.x, m_TargetMin.y);
            vert.position = lbPos2;
            vh.AddVert(vert);


            vh.AddTriangle(4, 0, 1);
            vh.AddTriangle(4, 1, 5);
            vh.AddTriangle(5, 1, 2);
            vh.AddTriangle(5, 2, 6);
            vh.AddTriangle(6, 2, 3);
            vh.AddTriangle(6, 3, 7);
            vh.AddTriangle(7, 3, 0);
            vh.AddTriangle(7, 0, 4);
        }

#if UNITY_EDITOR
        void Update()
        {
            m_CanRefresh = true;
            _RefreshView();
        }
#endif
    }
}
