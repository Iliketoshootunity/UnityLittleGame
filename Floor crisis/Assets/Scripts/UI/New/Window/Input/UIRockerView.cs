using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public enum RockerStyle
    {
        Free,
        EightDirection,
        FourDirection,
        LeftAndRight,
        UpAndDown
    }
    public class UIRockerView : MonoBehaviour
    {

        public event Action<Vector2, float> onRockerDragged;
        public Camera UICamera;
        public Canvas MainCanvas;
        private Image backgroundImage;
        protected Image BackgroundImage
        {
            get
            {
                if (backgroundImage == null)
                {
                    Transform trans = transform.Find("Background");
                    if (trans != null)
                    {
                        backgroundImage = trans.GetComponent<Image>();
                    }
                }
                return backgroundImage;
            }
        }

        private Image centerImage;
        protected Image CenterImage
        {
            get
            {
                if (centerImage == null)
                {
                    Transform trans = transform.Find("Center");
                    if (trans != null)
                    {
                        centerImage = trans.GetComponent<Image>();
                    }
                }
                return centerImage;
            }
        }

        [Header("摇杆类型")]
        [SerializeField]
        private RockerStyle rockerStyle = RockerStyle.Free;
        public RockerStyle RockerStyle { get { return rockerStyle; } set { rockerStyle = value; } }

        [Header("摇杆移动范围")]
        [SerializeField]
        private float range = 10f;
        public float Range
        {
            get
            {
                return range;
            }
            set
            {
                range = Mathf.Max(0, value);
            }
        }

        [Header("摇杆静止范围")]
        [SerializeField]
        private float minRange = 5f;
        public float MinRange
        {
            get { return minRange; }
            set { minRange = Mathf.Max(0, value); }
        }

        public static UIRockerView Instance;
        private void Awake()
        {
            Instance = this;
            Range = Range;
            MinRange = MinRange;
            Initialize();
        }
        private void Initialize()
        {
            if (CenterImage != null)
            {
                Mini_Rocker_Event centerRockerEvent = CenterImage.GetComponent<Mini_Rocker_Event>();
                if (centerRockerEvent != null)
                {
                    centerRockerEvent.onRockerDraggedEvent += OnRockerCenterDragged;
                    centerRockerEvent.onRockerBeginDraggedEvent += OnRockerCenterBeginDragged;
                    centerRockerEvent.onRockerEndDraggedEvent += OnRockerCenterEndDragged;
                }
                Mini_Rocker_Event bgRockerEvent = BackgroundImage.GetComponent<Mini_Rocker_Event>();
                if (bgRockerEvent != null)
                {
                    bgRockerEvent.onRockerBeginDraggedEvent += OnRockerCenterBeginDragged;
                    bgRockerEvent.onRockerDraggedEvent += OnRockerCenterDragged;
                    bgRockerEvent.onRockerEndDraggedEvent += OnRockerCenterEndDragged;
                }
            }
        }

        private void OnRockerCenterDragged(Mini_Rocker_Event e, PointerEventData data)
        {
            SetCenterPosition(TransferScreenToWorldPosition(data.position) - transform.position);
        }

        private void OnRockerCenterBeginDragged(Mini_Rocker_Event e, PointerEventData data)
        {
            SetCenterPosition(TransferScreenToWorldPosition(data.position) - transform.position);
        }

        public Vector3 TransferScreenToWorldPosition(Vector2 screenPosition)
        {
            if (UICamera.orthographic)
            {
                return UICamera.ScreenToWorldPoint(screenPosition);
            }
            else
            {
                Vector3 viewPos = UICamera.ScreenToViewportPoint(screenPosition);
                Vector3 worldPos = MainCanvas.transform.position;
                float worldScreenHeight = Mathf.Tan(UICamera.fieldOfView * Mathf.PI * 0.002777778f) * MainCanvas.planeDistance * 2f;
                float rightDis = (viewPos.x - 0.5f) * worldScreenHeight * Mini_Helper.Instance.ScreenWHRadio;
                float upDis = (viewPos.y - 0.5f) * worldScreenHeight;
                worldPos += UICamera.transform.up * upDis + UICamera.transform.right * rightDis;
                return worldPos;
            }
        }

        public Vector2 TransferWorldToScreenPosition(Vector3 worldPosition)
        {
            if (UICamera.orthographic)
            {
                return UICamera.WorldToScreenPoint(worldPosition);
            }
            else
            {
                Vector3 posVector = worldPosition - MainCanvas.transform.position;
                float widthRadio = Vector3.Dot(posVector, UICamera.transform.right) / (UICamera.transform.right.magnitude * Screen.width * MainCanvas.transform.lossyScale.x) + 0.5f;
                float heightRadio = Vector3.Dot(posVector, UICamera.transform.up) / (UICamera.transform.up.magnitude * Screen.height * MainCanvas.transform.lossyScale.y) + 0.5f;
                Vector3 viewPos = new Vector3(widthRadio, heightRadio, 0);
                return UICamera.ViewportToScreenPoint(viewPos);
            }
        }
        private void OnRockerCenterEndDragged(Mini_Rocker_Event e, PointerEventData data)
        {
            ResetCenterPosition();
        }

        private void ResetCenterPosition()
        {
            if (CenterImage != null)
            {
                SetCenterPosition(Vector2.zero);
            }
        }

        private void SetCenterPosition(Vector2 relateDraggedPos)
        {
            if (CenterImage != null)
            {
                Vector2 relatePos = (relateDraggedPos.magnitude >= MinRange) ? relateDraggedPos : Vector2.zero;
                relatePos = (relatePos.magnitude <= Range) ? relatePos : relatePos.normalized * Range;
                switch (RockerStyle)
                {
                    case RockerStyle.Free:
                        break;
                    case RockerStyle.EightDirection:
                        if (relatePos.magnitude > 0)
                        {
                            float angle = Vector3.Angle(Vector3.right, relatePos);
                            angle = (angle > 90) ? (180 - angle) : angle;
                            int count = (angle == 90) ? 4 : (int)((angle % 90f) / 22.5f);
                            relatePos = new Vector2(((count < 3) ? 1 : 0) * ((relatePos.x > 0) ? 1 : ((relatePos.x < 0) ? -1 : 0)), ((count > 1) ? 1 : 0) * ((relatePos.y > 0) ? 1 : ((relatePos.y < 0) ? -1 : 0))).normalized * Range;
                        }
                        break;
                    case RockerStyle.FourDirection:
                        if (relatePos.magnitude > 0)
                        {
                            relatePos = new Vector2((relatePos.x > 0 ? 1 : -1) * (Mathf.Abs(relatePos.x) >= Mathf.Abs(relatePos.y) ? 1 : 0), (relatePos.y > 0 ? 1 : -1) * (Mathf.Abs(relatePos.x) < Mathf.Abs(relatePos.y) ? 1 : 0)).normalized * Range;
                        }
                        break;
                    case RockerStyle.LeftAndRight:
                        if (relatePos.magnitude > 0)
                        {
                            relatePos = new Vector2(relatePos.x > 0 ? 1 : (relatePos.x < 0 ? -1 : 0), 0).normalized * Range;
                        }
                        break;
                    case RockerStyle.UpAndDown:
                        if (relatePos.magnitude > 0)
                        {
                            relatePos = new Vector2(0, relatePos.y > 0 ? 1 : (relatePos.y < 0 ? -1 : 0)).normalized * Range;
                        }
                        break;
                    default:
                        break;
                }
                if (onRockerDragged != null)
                {
                    onRockerDragged(relatePos.normalized, ((Range - MinRange) > 0) ? (Mathf.Max(0, (relatePos.magnitude - MinRange)) / (Range - MinRange)) : ((relatePos.magnitude > 0) ? 1 : 0));
                }
                CenterImage.transform.localPosition = new Vector3(relatePos.x / transform.lossyScale.x, relatePos.y / transform.lossyScale.y, 0);
            }
        }


    }
}

