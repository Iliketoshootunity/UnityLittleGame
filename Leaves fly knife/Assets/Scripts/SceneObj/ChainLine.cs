using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public class ChainLine : MonoBehaviour, IWeak
    {
        [SerializeField]
        private float m_CellSize;
        [SerializeField]
        private float m_BreakSpeed;
        private Transform m_StartTran;
        private Transform m_EndTran;
        private LineRenderer m_Line;
        private bool m_IsBreak;
        private float m_Dis;
        private float m_Dis2;
        private Vector2 m_Dir;
        private PolygonCollider2D m_Collider;

        public PolygonCollider2D Collider
        {
            get
            {
                if (m_Collider == null)
                {
                    m_Collider = GetComponent<PolygonCollider2D>();
                }
                return m_Collider;
            }
        }

        void Update()
        {
            if (m_IsBreak)
            {
                m_Dis = Mathf.Lerp(m_Dis, 0, m_BreakSpeed * Time.deltaTime);
                if (m_Dis < 0.05f)
                {
                    Destroy(this.gameObject);
                }
                Vector3 pos = Vector3.zero;
                pos = m_StartTran.TransformPoint(m_Dir * m_Dis);
                m_Line.SetPosition(0, m_StartTran.position);
                m_Line.SetPosition(1, pos);
                if (m_Collider.enabled)
                {
                    m_Collider.enabled = false;
                }
            }
            else
            {
                if (m_EndTran == null)
                {
                    Break(0, Vector2.up);
                    return;
                }
                m_Line.SetPosition(0, new Vector3(m_StartTran.position.x, m_StartTran.position.y, 0));
                m_Line.SetPosition(1, new Vector3(m_EndTran.position.x, m_EndTran.position.y, 0));
                List<Vector2> posList = new List<Vector2>();
                posList.Add(m_StartTran.position);
                posList.Add(m_EndTran.position);
                Collider.SetPath(0, GetColliderPath(posList).ToArray());
            }
        }
        public void SetChainLine(Transform startTran, Transform endTran)
        {
            m_Line = GetComponent<LineRenderer>();
            m_Line.sortingOrder = 1;
            m_StartTran = startTran;
            m_EndTran = endTran;
        }
        private List<Vector2> GetColliderPath(List<Vector2> posArr)
        {
            float colliderWidth = m_CellSize;
            List<Vector2> edgePointList = new List<Vector2>();
            for (int i = 1; i < posArr.Count; i++)
            {
                //向前的向量
                Vector2 forwardVector = posArr[i] - posArr[i - 1];
                //法线,垂直于向前的向量
                Vector2 crossVector = Vector3.Cross(forwardVector, Vector3.forward);
                crossVector = crossVector.normalized;
                //上方偏移
                Vector2 up = posArr[i - 1] + 0.5f * colliderWidth * crossVector;
                //下方偏移
                Vector2 down = posArr[i - 1] - 0.5f * colliderWidth * crossVector;
                edgePointList.Insert(0, down);
                edgePointList.Add(up);
                //加入最后一点
                if (i == posArr.Count - 1)
                {
                    up = posArr[i] + 0.5f * colliderWidth * crossVector;
                    //下方偏移
                    down = posArr[i] - 0.5f * colliderWidth * crossVector;
                    edgePointList.Insert(0, down);
                    edgePointList.Add(up);
                }
            }
            return edgePointList;
        }
        public void Break(float dis, Vector2 dir)
        {
            m_IsBreak = true;
            m_Dis = dis;
            m_Dir = dir;
            Collider.enabled = false;
        }
        //public void BeHit(GameObject go)
        //{
        //    if (m_IsBreak) return;
        //    if (HitAction != null)
        //    {
        //        HitAction(go.transform.position);
        //    }
        //}
    }
}
