using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{

    public static class MissilePath
    {
        public static List<GameObject> ReferencePointList = new List<GameObject>();
        public static List<GameObject> PathPointList = new List<GameObject>();
        public static int ReferencePointCount = 30;

        public static void Init()
        {
            ReferencePointList.Clear();
            PathPointList.Clear();
            for (int i = 0; i < ReferencePointCount; i++)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Item2/ReferencePoint", isCache: true);
                go.SetActive(false);
                ReferencePointList.Add(go);
            }
        }
        public static void ShowOrHidePath(bool isShow)
        {
            for (int i = 0; i < PathPointList.Count; i++)
            {
                PathPointList[i].SetActive(isShow);
            }
        }
        public static void ShowOrHideReference(bool isShow)
        {
            for (int i = 0; i < ReferencePointList.Count; i++)
            {
                ReferencePointList[i].SetActive(isShow);
            }
        }
        public static GameObject GetReference(int index)
        {
            if (index < ReferencePointCount)
            {
                return ReferencePointList[index];
            }
            return null;

        }
        public static GameObject GetOrCreatePath()
        {
            GameObject go = null;
            if (PathPointList.Count > 0)
            {
                go = PathPointList[PathPointList.Count - 1];
            }
            if (go == null || go.activeSelf)
            {
                go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Item2/PathPoint", isCache: true);
                PathPointList.Add(go);
            }
            else
            {
                PathPointList.Insert(0, go);
                go.SetActive(true);
            }
            return go;
        }

    }

    public class Missile : BaseMissile
    {

        [SerializeField]
        private float m_Gravity = -3f;
        [SerializeField]
        private float m_MaxPower;
        private float m_Power;
        private float m_VerticalInitialVelocity;
        private float m_VerticalVelocity;
        private float m_VerticalEndVelocity;
        private float m_Hypotenuse;
        private float m_TrajectoryTimer;
        private Vector2 m_TrajectoryDir;

        [SerializeField]
        private float m_HorizontalVelocity;
        private Vector2 m_UniformMotionDir;


        [SerializeField]
        private float m_ReferenceTime = 0.02f;
        [SerializeField]
        private float m_ReferencePointMinSize = 0.2f;
        [SerializeField]
        private float m_PathTime = 0.02f;
        private float m_PathTimer = 0;

        private Vector2 m_PrePos;
        private bool IsTrajectory = true;
        public Action OnShoot;

        public void SetParent(Transform p)
        {
            transform.SetParent(p);
            if (p != null)
            {
                transform.rotation = p.rotation;
                transform.localPosition = Vector3.zero;
            }
        }

        private void Awake()
        {
            Physics2D.gravity = new Vector2(0, m_Gravity);
        }

        protected override void OnStart()
        {
            base.OnStart();
            IsTrajectory = true;
        }


        protected override void FixedEnter()
        {
            base.FixedEnter();
            m_Rigi.constraints = RigidbodyConstraints2D.FreezeAll;
            m_TrailRenderer.enabled = false;
        }
        protected override void ReadyEnter()
        {
            base.ReadyEnter();
            m_Power = 0;
            m_PrePos = Vector2.zero;
            transform.localPosition = Vector3.zero;
            transform.SetParent(null);
            MissilePath.ShowOrHidePath(false);
        }
        protected override void ReadyUpdate()
        {
            base.ReadyUpdate();
            UpdateReference();
        }
        protected override void ShootEnter()
        {
            base.ShootEnter();
            m_TrailRenderer.enabled = true;
            MissilePath.ShowOrHideReference(false);
            MissilePath.ShowOrHidePath(false);
            m_TrajectoryTimer = 0;
            m_PathTimer = 0;
            m_Rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
            m_Rigi.velocity = new Vector2(m_VerticalVelocity, m_VerticalEndVelocity);
            IsTrajectory = true;
            if (OnShoot != null)
            {
                OnShoot();
            }
        }
        protected override void ShootUpdate()
        {
            base.ShootUpdate();
            UpdatePath();
            if (IsTrajectory)
            {
                if (m_PrePos != Vector2.zero)
                {
                    Vector2 r = new Vector2(transform.position.x, transform.position.y) - m_PrePos;
                    if (r.x == 0) return;
                    float a1 = Mathf.Atan(r.y / r.x);
                    float a2 = Mathf.Rad2Deg * a1;
                    if (r.x > 0)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, a2);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, 0, a2 + 180);
                    }

                }
                m_PrePos = transform.position;
                m_CurSpeed = m_Rigi.velocity;
            }
            else
            {
                Vector2 pos = transform.right * m_HorizontalVelocity * Time.fixedDeltaTime;
                transform.position += new Vector3(pos.x, pos.y, 0);
                m_CurSpeed = m_HorizontalVelocity * transform.right;
            }
        }

        protected override void OverBegin()
        {
            base.OverBegin();
            Destroy(this.gameObject);
        }
        public void InputFireParameter(Vector2 dir, float pownr, bool isEnd)
        {
            if (!isEnd)
            {
                if (m_Status == Status.Fixed)
                {
                    ToReady(); ;
                }
            }

            if (m_Status == Status.Ready)
            {
                if (isEnd)
                {
                    m_NextStatus = Status.Shoot;
                }
                else
                {
                    m_TrajectoryDir = dir;
                    m_Power = m_MaxPower * pownr;
                    transform.right = dir;
                }
            }
        }


        public void SetUniformMotion()
        {
            if (m_Status == Status.Shoot && IsTrajectory)
            {
                IsTrajectory = false;
                m_UniformMotionDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
                m_Rigi.constraints = RigidbodyConstraints2D.FreezeAll;
                transform.right = m_UniformMotionDir;
                Debug.Log(IsTrajectory);
            }
        }


        private void UpdatePath()
        {
            if (m_Status == Status.Shoot)
            {
                m_PathTimer += Time.deltaTime;
                if (m_PathTimer > m_PathTime)
                {
                    m_PathTimer = 0;
                    GameObject go = null;
                    go = MissilePath.GetOrCreatePath();
                    go.transform.position = transform.position;
                }
            }
        }

        /// <summary>
        /// 显示path
        /// </summary>
        private void UpdateReference()
        {
            if (m_Power > 0.01f)
            {
                m_TrajectoryDir = m_TrajectoryDir.normalized;
                m_VerticalInitialVelocity = m_Power * Time.fixedDeltaTime * m_Rigi.mass;
                m_Hypotenuse = Mathf.Sqrt(m_TrajectoryDir.x * m_TrajectoryDir.x + m_TrajectoryDir.y * m_TrajectoryDir.y);
                //水平初速度
                m_VerticalVelocity = m_TrajectoryDir.x / m_Hypotenuse * m_VerticalInitialVelocity;
                //处置初速度
                m_VerticalEndVelocity = m_TrajectoryDir.y / m_Hypotenuse * m_VerticalInitialVelocity;
                float time = 0;
                for (int i = 0; i < MissilePath.ReferencePointCount; i++)
                {

                    time += m_ReferenceTime;
                    Vector2 pos = CalPos(time);
                    GameObject go = MissilePath.GetReference(i);
                    go.SetActive(true);
                    go.transform.position = new Vector3(pos.x, pos.y, 0) + transform.position;
                    go.transform.localScale = Vector3.one * ((1 - m_ReferencePointMinSize) * (1 - i / ((float)MissilePath.ReferencePointCount - 1)) + m_ReferencePointMinSize);
                }
            }
            else
            {
                MissilePath.ShowOrHideReference(false);
            }
        }
        private Vector2 CalPos(float time)
        {
            //水平位移
            float Sh = m_VerticalVelocity * time;
            //垂直位移
            float Vt = m_VerticalEndVelocity + Physics2D.gravity.y * time;
            float Sv = (Vt + m_VerticalEndVelocity) / 2 * time;
            return new Vector2(Sh, Sv);
        }

    }
}
