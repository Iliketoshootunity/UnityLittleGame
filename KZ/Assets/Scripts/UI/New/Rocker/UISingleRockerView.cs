using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EasyFrameWork
{
    /// <summary>
    /// 单个摇杆UI
    /// </summary>
    public class UISingleRockerView : UISubViewBase
    {
        public enum RockerType
        {
            Free,
            EightDirection,
            FourDirection,
            LeftAndRight,
            UpAndDown,
        }

        public event Action<string, Vector2, float, bool> OnRockerDrag;

        private Image m_BackgroundImage;
        protected Image BackgroundImage { get { if (m_BackgroundImage == null) { m_BackgroundImage = transform.Find("Background").GetComponent<Image>(); } return m_BackgroundImage; } }

        protected Image m_CenterImage;
        protected Image CenterImage { get { if (m_CenterImage == null) { m_CenterImage = transform.Find("Center").GetComponent<Image>(); } return m_CenterImage; } }
        [SerializeField]
        private RockerType m_RockerStyle = RockerType.Free;
        public RockerType RockerStyle { get { return m_RockerStyle; } set { m_RockerStyle = value; } }
        [SerializeField]
        private float m_MinRange;
        public float MinRange { get { return m_MinRange; } set { m_MinRange = Mathf.Max(0, value); } }
        [SerializeField]
        private float m_Range;
        public float Range { get { return m_Range; } set { m_Range = Mathf.Max(0, value); } }

        protected override void OnAwake()
        {
            Range = Range;
            Range = Range;
            Initialized();
        }

        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, MinRange);
        }

        private void Initialized()
        {
            //绑定事件
            if (BackgroundImage != null)
            {
                UIRockerEventView view = BackgroundImage.GetComponent<UIRockerEventView>();
                if (view != null)
                {
                    view.OnRockerBeginDragEvent += OnRoeckerBeginDragEvent;
                    view.OnRockerDragEvent += OnRoeckerDragEvent;
                    view.OnRockerEndDragEvent += OnRoeckerEndDragEvent;
                    view.OnRockerPointDownEvent += OnRoeckerBeginDragEvent;
                    view.OnRockerPointUpEvent += OnRoeckerEndDragEvent;
                }
            }
            if (CenterImage != null)
            {
                UIRockerEventView view = CenterImage.GetComponent<UIRockerEventView>();
                if (view != null)
                {
                    view.OnRockerBeginDragEvent += OnRoeckerBeginDragEvent;
                    view.OnRockerDragEvent += OnRoeckerDragEvent;
                    view.OnRockerEndDragEvent += OnRoeckerEndDragEvent;
                    view.OnRockerPointDownEvent += OnRoeckerBeginDragEvent;
                    view.OnRockerPointUpEvent += OnRoeckerEndDragEvent;
                }
            }
        }
        private void OnRoeckerBeginDragEvent(UIRockerEventView e, PointerEventData data)
        {
            SetCenterPosition(UISceneCtrl.Instance.TransferScreenToWorldPosition(data.position) - transform.position);
        }
        private void OnRoeckerEndDragEvent(UIRockerEventView e, PointerEventData data)
        {
            ResetCenterPosition();
        }
        private void OnRoeckerDragEvent(UIRockerEventView e, PointerEventData data)
        {
            SetCenterPosition(UISceneCtrl.Instance.TransferScreenToWorldPosition(data.position) - transform.position);
        }

        private void ResetCenterPosition()
        {
            if (CenterImage != null)
            {
                SetCenterPosition(Vector2.zero, isEnd: true);
            }
        }
        private void SetCenterPosition(Vector2 relateDraggedPos, bool isEnd = false)
        {
            if (CenterImage != null)
            {
                Vector2 relatePos = (relateDraggedPos.magnitude >= MinRange) ? relateDraggedPos : Vector2.zero;
                relatePos = (relatePos.magnitude <= Range) ? relatePos : relatePos.normalized * Range;
                switch (RockerStyle)
                {
                    case RockerType.Free:
                        break;
                    case RockerType.EightDirection:
                        if (relatePos.magnitude > 0)
                        {
                            float angle = Vector3.Angle(Vector3.right, relatePos);
                            angle = (angle > 90) ? (180 - angle) : angle;
                            int count = (angle == 90) ? 4 : (int)((angle % 90f) / 22.5f);
                            relatePos = new Vector2(((count < 3) ? 1 : 0) * ((relatePos.x > 0) ? 1 : ((relatePos.x < 0) ? -1 : 0)), ((count > 1) ? 1 : 0) * ((relatePos.y > 0) ? 1 : ((relatePos.y < 0) ? -1 : 0))).normalized * Range;
                        }
                        break;
                    case RockerType.FourDirection:
                        if (relatePos.magnitude > 0)
                        {
                            relatePos = new Vector2((relatePos.x > 0 ? 1 : -1) * (Mathf.Abs(relatePos.x) >= Mathf.Abs(relatePos.y) ? 1 : 0), (relatePos.y > 0 ? 1 : -1) * (Mathf.Abs(relatePos.x) < Mathf.Abs(relatePos.y) ? 1 : 0)).normalized * Range;
                        }
                        break;
                    case RockerType.LeftAndRight:
                        if (relatePos.magnitude > 0)
                        {
                            relatePos = new Vector2(relatePos.x > 0 ? 1 : (relatePos.x < 0 ? -1 : 0), 0).normalized * Range;
                        }
                        break;
                    case RockerType.UpAndDown:
                        if (relatePos.magnitude > 0)
                        {
                            relatePos = new Vector2(0, relatePos.y > 0 ? 1 : (relatePos.y < 0 ? -1 : 0)).normalized * Range;
                        }
                        break;
                    default:
                        break;
                }
                if (OnRockerDrag != null)
                {
                    OnRockerDrag(transform.name, relatePos.normalized, ((Range - MinRange) > 0) ? (Mathf.Max(0, (relatePos.magnitude - MinRange)) / (Range - MinRange)) : ((relatePos.magnitude > 0) ? 1 : 0), isEnd);
                }
                CenterImage.transform.localPosition = new Vector3(relatePos.x / transform.lossyScale.x, relatePos.y / transform.lossyScale.y, 0);
            }
        }
    }
}
