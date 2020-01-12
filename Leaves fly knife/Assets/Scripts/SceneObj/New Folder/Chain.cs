using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    /// <summary>
    /// 链条
    /// </summary>
	public class Chain : MonoBehaviour, IWeak
    {
        [SerializeField]
        private Rigidbody2D m_ConnectRigi;
        [SerializeField]
        private ChainLine m_LinePrefab;
        private Rigidbody2D m_Rigi;
        private ChainLine m_Line;
        private AnchoredJoint2D m_Joint;
        // Use this for initialization
        void Start()
        {
            m_Joint = GetComponent<AnchoredJoint2D>();
            m_Rigi = GetComponent<Rigidbody2D>();
            m_Joint.connectedBody = m_ConnectRigi;
            m_Line = GameObject.Instantiate(m_LinePrefab.gameObject).GetComponent<ChainLine>();
            m_Line.transform.SetParent(transform);
            m_Line.transform.position = Vector3.zero;
            m_Line.SetChainLine(transform, m_Joint.connectedBody.gameObject.transform);
            m_ConnectRigi.freezeRotation = true;
        }

        public void Break(Vector3 pos)
        {
            m_ConnectRigi.freezeRotation = false;
            Transform t = m_Joint.connectedBody.transform;
            m_Joint.connectedBody = null;
            Destroy(m_Line.gameObject);

            m_Line = GameObject.Instantiate(m_LinePrefab.gameObject).GetComponent<ChainLine>();
            m_Line.transform.SetParent(transform);
            m_Line.SetChainLine(transform, null);
            float dis = Vector2.Distance(pos, transform.position);
            m_Line.Break(dis, (pos - transform.position).normalized);

            m_Line = GameObject.Instantiate(m_LinePrefab.gameObject).GetComponent<ChainLine>();
            m_Line.transform.SetParent(t);
            m_Line.SetChainLine(t, null);
            dis = Vector2.Distance(pos, t.position);
            m_Line.Break(dis, (pos - t.transform.position).normalized);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BaseMissile m = collision.gameObject.GetComponent<BaseMissile>();
            if (m != null)
            {
                RaycastHit2D[] rs = null;
                collision.Raycast(collision.transform.right, rs, 100);
                Vector3 dir = collision.transform.position - m_ConnectRigi.transform.position;
                m_ConnectRigi.AddForce(dir * 10);
                RaycastHit2D[] hits = Physics2D.RaycastAll(collision.transform.position, collision.transform.right, 100);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.transform != collision.transform)
                    {
                        Break(hits[i].point);
                        break;
                    }
                }



            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {

        }


    }
}
