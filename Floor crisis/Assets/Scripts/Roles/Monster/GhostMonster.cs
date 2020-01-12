using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class GhostMonster : Attacker, ITopWeakness, IBottomWeakness
    {

        /// <summary>
        /// 垂直移动速度
        /// </summary>
        [SerializeField]
        private float m_VerticalVelocity = 3;

        [SerializeField]
        private float m_StartY;
        [SerializeField]
        private float m_EndY;
        [SerializeField]


        private bool isForward;

        protected override void OnStart()
        {
            base.OnStart();
            isForward = true;
        }
        public void Init(Vector3 startPos, float endY)
        {
            transform.position = startPos;
            m_StartY = startPos.y;
            m_EndY = m_StartY + endY;
        }

        protected override void Move()
        {
            transform.Translate(new Vector3(0, m_VerticalVelocity, 0) * Time.deltaTime);
            if (isForward)
            {
                if (Mathf.Abs(transform.position.y - m_StartY) < 0.03f)
                {
                    isForward = false;
                    m_VerticalVelocity = -m_VerticalVelocity;
                }
            }
            else
            {
                if (Mathf.Abs(transform.position.y - m_EndY) < 0.03f)
                {
                    isForward = true;
                    m_VerticalVelocity = -m_VerticalVelocity;
                }
            }
        }

        protected override void Attack()
        {
            m_AttackTimer -= Time.deltaTime;
            if (m_AttackTimer < 0)
            {
                for (int i = 0; i < m_PhyCtroller.ForwardHitsStorage.Length; i++)
                {
                    if (m_PhyCtroller.ForwardHitsStorage[i].distance > 0)
                    {
                        RoleCtrl ctrl = m_PhyCtroller.ForwardHitsStorage[i].collider.GetComponent<RoleCtrl>();
                        if (ctrl != null)
                        {
                            ctrl.ToHurt(m_AttackImpuse, true);
                            m_AttackTimer = m_AttackInterval;
                            return;
                        }
                    }
                }
                for (int i = 0; i < m_PhyCtroller.BackwardHitsStorage.Length; i++)
                {
                    if (m_PhyCtroller.BackwardHitsStorage[i].distance > 0)
                    {
                        RoleCtrl ctrl = m_PhyCtroller.BackwardHitsStorage[i].collider.GetComponent<RoleCtrl>();
                        if (ctrl != null)
                        {
                            ctrl.ToHurt(m_AttackImpuse, true);
                            m_AttackTimer = m_AttackInterval;
                            return;
                        }
                    }

                }
            }
        }

        public bool Break()
        {
            if (DelegateDefine.Instance.OnGetCoin != null)
            {
                DelegateDefine.Instance.OnGetCoin();
            }
            Destroy(this.gameObject);
            return true;
        }
    }
}
