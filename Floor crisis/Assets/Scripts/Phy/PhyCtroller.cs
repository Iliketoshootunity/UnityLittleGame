using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class PhyCtroller : MonoBehaviour
    {


        [Header("Raycasting 射线")]
        /// <summary>
        /// 水平方向射线数量
        /// </summary>       
        public int NumberOfHorizontalRays = 8;
        /// <summary>
        /// 垂直方向射线数量
        /// </summary>
        public int NumberOfVerticalRays = 8;

        public float RayOffset = 0.05f;

        public bool IsShootForwardRay = true;

        public bool IsShootBackwardRay = false;

        public bool IsShootBelowRay = true;

        public bool IsShootAboveRay = true;

        public LayerMask SidesLayerMask;

        public LayerMask BelowLayerMask;

        public LayerMask AboveLayerMask;

        /// <summary>
        /// 射线矩形
        /// </summary>
        protected Rect m_RayBoundsRectangel;
        /// <summary>
        /// 碰撞体
        /// </summary>
        protected BoxCollider2D m_BoxCollider;

        /// <summary>
        /// 刚体
        /// </summary>
        protected Rigidbody2D m_Rigibody;

        /// <summary>
        /// 侧面碰撞集合
        /// </summary>
        protected RaycastHit2D[] m_ForwardHitsStorage;
        /// <summary>
        /// 侧面碰撞集合
        /// </summary>
        protected RaycastHit2D[] m_BackwardHitsStorage;
        /// <summary>
        /// 底部碰撞集合
        /// </summary>
        protected RaycastHit2D[] m_BelowHitsStorge;
        /// <summary>
        /// 顶部碰撞集合
        /// </summary>
        protected RaycastHit2D[] m_AboveHitsStorge;

        /// <summary>
        /// 计算水平射线的分割的底部值
        /// </summary>
        protected Vector2 m_HorizontalRayCastFromBottom;
        /// <summary>
        /// 计算水平射线的分割的顶部值
        /// </summary>
        protected Vector2 m_HorizontalRayCastToTop;
        /// <summary>
        /// 计算垂直射线的分割的左值
        /// </summary>
        protected Vector2 m_VerticalRayCastFormLeft;
        /// <summary>
        /// 计算垂直射线的分割的右值
        /// </summary>
        protected Vector2 m_VerticalRayCastToRight;

        private Vector3 m_Speed;
        private Vector3 m_PrePos;
        public RaycastHit2D[] ForwardHitsStorage { get { return m_ForwardHitsStorage; } }
        public RaycastHit2D[] BackwardHitsStorage { get { return m_BackwardHitsStorage; } }
        public RaycastHit2D[] BelowHitsStorge { get { return m_BelowHitsStorge; } }
        public RaycastHit2D[] AboveHitsStorge { get { return m_AboveHitsStorge; } }

        private void Awake()
        {
            m_ForwardHitsStorage = new RaycastHit2D[NumberOfHorizontalRays];
            m_BackwardHitsStorage = new RaycastHit2D[NumberOfHorizontalRays];
            m_BelowHitsStorge = new RaycastHit2D[NumberOfVerticalRays];
            m_AboveHitsStorge = new RaycastHit2D[NumberOfVerticalRays];
            m_Rigibody = GetComponentInChildren<Rigidbody2D>();
            m_RayBoundsRectangel = new Rect();
            m_BoxCollider = GetComponentInChildren<BoxCollider2D>();
            m_PrePos = transform.position;
        }

        public void OnUpdate()
        {
            SetRaysParameters();
            FrameInitialzation();
            CastRayToTheFordward();
            CastRayToTheBackward();
            CastRayBelow();
            CastRaysAbove();
            SetRaysParameters();
            m_Speed = (transform.position - m_PrePos) / Time.deltaTime;
            m_PrePos = transform.position;
        }

        public float GetHeight()
        {
            return m_RayBoundsRectangel.height;
        }
        public float GetWight()
        {
            return m_RayBoundsRectangel.width;
        }

        /// <summary>
        /// 每帧初始化
        /// </summary>
        protected virtual void FrameInitialzation()
        {

            for (int i = 0; i < m_ForwardHitsStorage.Length; i++)
            {
                m_ForwardHitsStorage[i] = new RaycastHit2D();
            }
            for (int i = 0; i < m_BackwardHitsStorage.Length; i++)
            {
                m_BackwardHitsStorage[i] = new RaycastHit2D();
            }
            for (int i = 0; i < m_BelowHitsStorge.Length; i++)
            {
                m_BelowHitsStorge[i] = new RaycastHit2D();
            }
            for (int i = 0; i < m_AboveHitsStorge.Length; i++)
            {
                m_AboveHitsStorge[i] = new RaycastHit2D();
            }
        }

        /// <summary>
        /// 设置射线参数
        /// </summary>
        public virtual void SetRaysParameters()
        {
            m_RayBoundsRectangel.xMin = m_BoxCollider.bounds.min.x;
            m_RayBoundsRectangel.yMin = m_BoxCollider.bounds.min.y;
            m_RayBoundsRectangel.xMax = m_RayBoundsRectangel.xMin + m_BoxCollider.bounds.size.x;
            m_RayBoundsRectangel.yMax = m_RayBoundsRectangel.yMin + m_BoxCollider.bounds.size.y;
        }

        /// <summary>
        /// 从中心轴向侧面发射射线
        /// 如果检测到墙或者斜坡，我们会检查它的角度，根据检查结果，判断是否移动
        /// </summary>
        protected virtual void CastRayToTheFordward()
        {
            if (!IsShootForwardRay) return;
            //if (m_Speed == Vector3.zero) return;
            //向左
            float movementDirection = -1;
            if (m_Rigibody.velocity.x > 0)
            {
                //向右
                movementDirection = 1;
            }
            //水平方向射线长度
            float horizontalRayLength = m_RayBoundsRectangel.width / 2 + RayOffset;
            //计算水平射线分割低部值
            m_HorizontalRayCastFromBottom.x = m_RayBoundsRectangel.center.x;
            m_HorizontalRayCastFromBottom.y = m_RayBoundsRectangel.yMin;

            //计算水平射线分割顶部值
            m_HorizontalRayCastToTop.x = m_RayBoundsRectangel.center.x;
            m_HorizontalRayCastToTop.y = m_RayBoundsRectangel.yMax;
            //设置侧边碰撞信息集合
            if (m_ForwardHitsStorage.Length != NumberOfHorizontalRays)
            {
                m_ForwardHitsStorage = new RaycastHit2D[NumberOfHorizontalRays];
            }

            //发射NumberOfHorizontalRays 射线
            for (int i = 0; i < NumberOfHorizontalRays; i++)
            {
                Vector2 rayOriginPoint = Vector2.Lerp(m_HorizontalRayCastFromBottom, m_HorizontalRayCastToTop, (float)(i / (float)(NumberOfHorizontalRays - 1)));
                m_ForwardHitsStorage[i] = PhyDebug.Raycast(rayOriginPoint, movementDirection * (Vector2.right), horizontalRayLength, SidesLayerMask, Color.red, true);
            }
        }

        /// <summary>
        /// 从中心轴向侧面发射射线
        /// 如果检测到墙或者斜坡，我们会检查它的角度，根据检查结果，判断是否移动
        /// </summary>
        protected virtual void CastRayToTheBackward()
        {
            if (!IsShootBackwardRay) return;
            //if (m_Speed == Vector3.zero) return;
            //向左
            float movementDirection = 1;
            if (m_Rigibody.velocity.x > 0)
            {
                //向右
                movementDirection = -1;
            }
            //水平方向射线长度
            float horizontalRayLength = m_RayBoundsRectangel.width / 2 + RayOffset;

            //计算水平射线分割低部值
            m_HorizontalRayCastFromBottom.x = m_RayBoundsRectangel.center.x;
            m_HorizontalRayCastFromBottom.y = m_RayBoundsRectangel.yMin;

            //计算水平射线分割顶部值
            m_HorizontalRayCastToTop.x = m_RayBoundsRectangel.center.x;
            m_HorizontalRayCastToTop.y = m_RayBoundsRectangel.yMax;
            //设置侧边碰撞信息集合
            if (m_BackwardHitsStorage.Length != NumberOfHorizontalRays)
            {
                m_BackwardHitsStorage = new RaycastHit2D[NumberOfHorizontalRays];
            }

            //发射NumberOfHorizontalRays 射线
            for (int i = 0; i < NumberOfHorizontalRays; i++)
            {
                Vector2 rayOriginPoint = Vector2.Lerp(m_HorizontalRayCastFromBottom, m_HorizontalRayCastToTop, (float)(i / (float)(NumberOfHorizontalRays - 1)));
                m_BackwardHitsStorage[i] = PhyDebug.Raycast(rayOriginPoint, movementDirection * (Vector2.right), horizontalRayLength, SidesLayerMask, Color.blue, true);
            }
        }
        /// <summary>
        /// 从角色的中心向下方发射射线
        /// </summary>
        protected virtual void CastRayBelow()
        {
            if (!IsShootBelowRay) return;
            float rayLength = m_RayBoundsRectangel.height / 2 + RayOffset;

            m_VerticalRayCastFormLeft.x = m_RayBoundsRectangel.xMin;
            m_VerticalRayCastFormLeft.y = m_RayBoundsRectangel.center.y;

            m_VerticalRayCastToRight.x = m_RayBoundsRectangel.xMax;
            m_VerticalRayCastToRight.y = m_RayBoundsRectangel.center.y;

            if (m_BelowHitsStorge.Length != NumberOfVerticalRays)
            {
                m_BelowHitsStorge = new RaycastHit2D[NumberOfVerticalRays];
            }
            for (int i = 0; i < NumberOfVerticalRays; i++)
            {
                Vector2 rayOriginPoint = Vector2.Lerp(m_VerticalRayCastFormLeft, m_VerticalRayCastToRight, (float)(i / ((NumberOfVerticalRays - (float)1))));
                m_BelowHitsStorge[i] = PhyDebug.Raycast(rayOriginPoint, Vector2.down, rayLength, BelowLayerMask, Color.black, true);

            }
        }

        protected virtual void CastRaysAbove()
        {
            if (!IsShootAboveRay) return;
            float rayLength = m_RayBoundsRectangel.height / 2 + RayOffset;

            m_VerticalRayCastFormLeft.x = m_RayBoundsRectangel.xMin;
            m_VerticalRayCastFormLeft.y = m_RayBoundsRectangel.center.y;

            m_VerticalRayCastToRight.x = m_RayBoundsRectangel.xMax;
            m_VerticalRayCastToRight.y = m_RayBoundsRectangel.center.y;

            if (m_AboveHitsStorge.Length != NumberOfVerticalRays)
            {
                m_AboveHitsStorge = new RaycastHit2D[NumberOfVerticalRays];
            }

            for (int i = 0; i < NumberOfVerticalRays; i++)
            {
                Vector2 rayOriginPoint = Vector2.Lerp(m_VerticalRayCastFormLeft, m_VerticalRayCastToRight, (float)(i / ((NumberOfVerticalRays - (float)1))));
                m_AboveHitsStorge[i] = PhyDebug.Raycast(rayOriginPoint, Vector2.up, rayLength, AboveLayerMask, Color.green, true);
            }
        }
    }
}
