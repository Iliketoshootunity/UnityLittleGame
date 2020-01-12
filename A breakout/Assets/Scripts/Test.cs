using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;


namespace EasyFrameWork
{
    public class Test : MonoBehaviour
    {

        public List<float> m_AngleList;
        public List<Vector2> m_VV;

        private float m_Angle;
        private float timer;
        public SpriteRenderer s;
        public LineRenderer L;
        public PolygonCollider2D c;
        // Use this for initialization
        void Start()
        {
            CameraCtrl tt = new CameraCtrl();
        }

        // Update is called once per frame
        void Update()
        {

        }
        Vector3 lastPos = Vector3.zero;
        private void OnDrawGizmos()
        {
            if (m_AngleList != null)
            {
                for (int i = 0; i < m_AngleList.Count; i++)
                {
                    float a = Mathf.Deg2Rad * m_AngleList[i];
                    float p = 10 * (1 - Mathf.Cos(a));
                    float x = p * Mathf.Cos(a);
                    float y = p * Mathf.Sin(a);
                    Gizmos.DrawLine(lastPos, new Vector2(x, y));
                    lastPos = new Vector2(x, y);
                }
            }

        }


    }
}
