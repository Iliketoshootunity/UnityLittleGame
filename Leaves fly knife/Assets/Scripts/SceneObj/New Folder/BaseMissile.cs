using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class BaseMissile : MonoBehaviour
    {
        public enum MissileType
        {
            Fixed,
            Normal
        }
        public enum Status
        {
            None,
            Fixed,
            Ready,
            Shoot,
            Over
        }
        public MissileType Type;
        [HideInInspector]
        public BaseMissile ConnectMissile;
        [SerializeField]
        protected Status m_Status;
        [SerializeField]
        protected Status m_NextStatus;
        [SerializeField]
        protected AudioClip m_ShootClip;
        [SerializeField]
        protected AudioClip m_OverClip;

        protected Rigidbody2D m_Rigi;
        protected Collider2D m_Collider;
        protected TrailRenderer m_TrailRenderer;
        protected Vector2 m_CurSpeed;

        public Status GetStatus()
        {
            return m_Status;
        }

        private void Start()
        {
            OnStart();
        }
        protected virtual void OnStart()
        {
            m_Rigi = GetComponent<Rigidbody2D>();
            m_Collider = GetComponent<Collider2D>();
            m_TrailRenderer = GetComponentInChildren<TrailRenderer>();
            if (m_TrailRenderer != null)
            {
                m_TrailRenderer.sortingOrder = 2;
            }
        }
        private void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            if (m_NextStatus == Status.Fixed)
            {
                FixedEnter();
            }
            else if (m_NextStatus == Status.Ready)
            {
                ReadyEnter();
            }
            else if (m_NextStatus == Status.Shoot)
            {
                ShootEnter();
            }
            else if (m_NextStatus == Status.Over)
            {
                OverBegin();
            }
            if (m_NextStatus != Status.None)
            {
                m_Status = m_NextStatus;
                m_NextStatus = Status.None;
            }
            if (m_Status == Status.Fixed)
            {
                Fixedupdate();
            }
            else if (m_Status == Status.Ready)
            {
                ReadyUpdate();
            }
            else if (m_Status == Status.Shoot)
            {
                ShootUpdate();
                Vector2 viewPos = Camera.main.WorldToViewportPoint(transform.position);
                if (viewPos.x < -0.2f || viewPos.y < -0.2f || viewPos.x > 1.2f || viewPos.y > 1.2f)
                {
                    GameSceneCtrl.Instance.EmitterFixed();
                    m_NextStatus = Status.Over;
                }
            }

        }

        public virtual void ToFixed()
        {
            m_NextStatus = Status.Fixed;
            if (m_TrailRenderer != null)
            {
                m_TrailRenderer.enabled = false;
            }
        }
        public virtual void ToReady()
        {
            m_NextStatus = Status.Ready;
        }
        protected virtual void FixedEnter()
        {
        }
        protected virtual void Fixedupdate()
        {

        }
        protected virtual void ReadyEnter()
        {

        }
        protected virtual void ReadyUpdate()
        {

        }
        public virtual void ToShoot()
        {
            m_NextStatus = Status.Shoot;
        }
        protected virtual void ShootEnter()
        {
            EazySoundManager.PlayUISound(m_ShootClip);
        }
        protected virtual void ShootUpdate()
        {

        }
        protected virtual void OverBegin()
        {
            Destroy(this.gameObject);
        }
        public virtual void ToOver()
        {
            m_NextStatus = Status.Over;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollision(collision.gameObject);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision(collision.gameObject);
        }
        protected virtual void OnCollision(GameObject collision)
        {
            if (m_NextStatus != Status.None) return;
            if (m_Status == Status.Shoot)
            {
                Rigidbody2D rigi = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rigi != null)
                {
                    Vector2 f = m_Rigi.mass * m_CurSpeed / Time.deltaTime;
                    rigi.AddForce(f);
                }
                IHealth health = collision.gameObject.GetComponent<IHealth>();
                if (health != null)
                {
                    health.OnHit(this.gameObject);
                }
                IWeak weak = collision.gameObject.GetComponent<IWeak>();
                Missile m = collision.gameObject.GetComponent<Missile>();
                if (weak == null && m == null)
                {
                    m_NextStatus = Status.Over;
                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, "Explode", isCache: true);
                    go.transform.position = transform.position;
                    EazySoundManager.PlayUISound(m_OverClip);
                }
            }
        }
    }
}
