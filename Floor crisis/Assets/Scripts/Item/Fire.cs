using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class Fire : Attacker
    {
        private bool m_IsAttack;
        [SerializeField]
        private float m_MoveSpeed;

        private bool m_IsMove;

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
        public void Init(Vector3 startPos, float moveDelay)
        {
            StartCoroutine(InitIE(startPos, moveDelay));
        }

        private IEnumerator InitIE(Vector3 startPos, float moveDelay)
        {
            m_AttackTimer = m_AttackInterval;
            m_IsMove = false;
            yield return new WaitForSeconds(moveDelay);
            transform.position = startPos;
            m_IsMove = true;

        }
        protected override void Move()
        {
            if (m_IsMove)
            {
                base.Move();
                transform.Translate(Vector3.up * m_MoveSpeed * Time.deltaTime);

            }

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            m_AttackTimer -= Time.deltaTime;
            if (m_AttackTimer < 0)
            {
                m_IsAttack = true;
            }
            if (m_IsAttack)
            {
                RoleCtrl ctrl = collision.GetComponent<RoleCtrl>();
                if (ctrl != null)
                {
                    ctrl.ToHurt(m_AttackImpuse);
                    m_IsAttack = false;
                    m_AttackTimer = m_AttackInterval;
                }
            }
        }

        public void StopMove()
        {
            m_IsMove = false;
        }

    }
}
