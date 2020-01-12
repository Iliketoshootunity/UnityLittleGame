using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class Attacker : MonoBehaviour
    {

        [SerializeField]
        protected float m_AttackInterval = 0.1f;
        [SerializeField]
        protected float m_AttackImpuse;
        [SerializeField]
        protected float m_AttackTimer;
        protected Rigidbody2D m_Rigibody;
        protected PhyCtroller m_PhyCtroller;

        // Use this for initialization
        void Start()
        {

            m_PhyCtroller = GetComponent<PhyCtroller>();
            m_Rigibody = GetComponent<Rigidbody2D>();
            OnStart();

        }

        protected virtual void OnStart()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OnUpdate();

        }

        protected virtual void OnUpdate()
        {
            if (m_PhyCtroller != null)
            {
                m_PhyCtroller.OnUpdate();
            }
            Attack();
            Move();
        }

        protected virtual void Move()
        {

        }
        protected virtual void Attack()
        {

        }
        public virtual void OnBreak()
        {
            Destroy(this.gameObject);
        }
    }
}
