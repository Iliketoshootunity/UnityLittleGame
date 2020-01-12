using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{

    public class Exploder : MonoBehaviour, ICooling
    {
        private enum Status
        {
            Idle,
            CountDown,
            Explode
        }
        [SerializeField]
        private float m_CountDown;
        [SerializeField]
        private float m_Range;
        [SerializeField]
        private bool m_Fixed;
        [SerializeField]
        private SpriteRenderer m_CountDonwSprite;
        [SerializeField]
        private List<Sprite> m_NumberList;

        private Status m_Status;
        private float m_CountDownr;

        public float CountDown
        {
            get
            {
                return m_CountDown;
            }

        }

        void Start()
        {
            if (m_Fixed)
            {
                Rigidbody2D rigi = GetComponent<Rigidbody2D>();
            }
        }

        private void Update()
        {
            if (m_Status == Status.CountDown)
            {
                HandleCountDown();
            }
            else if (m_Status == Status.Explode)
            {
                Explode();
            }
        }

        private void HandleCountDown()
        {
            m_CountDownr += Time.deltaTime;
            if (m_CountDownr > m_CountDown)
            {
                m_Status = Status.Explode;
            }
            int index = (int)(m_CountDown - m_CountDownr) + 1;
            m_CountDonwSprite.sprite = m_NumberList[index];
        }

        private void Explode()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, m_Range, Vector2.zero);
            for (int i = 0; i < hits.Length; i++)
            {
                IHealth health = hits[i].collider.GetComponent<IHealth>();
                if (health != null)
                {
                    health.OnHit(this.gameObject);
                }
                ICollect collect = hits[i].collider.GetComponent<ICollect>();
                if (collect != null)
                {
                    collect.Collect();
                }
            }
            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, "Explode", isCache: true);
            go.transform.position = transform.position;
            Destroy(this.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_Range);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.EndsWith("Ground")) return;
            m_Status = Status.CountDown;
            GameSceneCtrl.Instance.SetCanOverRime(5);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.EndsWith("Ground")) return;
            m_Status = Status.CountDown;
        }
    }
}
