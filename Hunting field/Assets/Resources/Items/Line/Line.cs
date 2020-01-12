using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 线
    /// </summary>
    public class Line : MonoBehaviour
    {
        public Line Parent;

        public Transform Arrow;
        /// <summary>
        /// 线
        /// </summary>
        public LineRenderer LineRenderer;
        /// <summary>
        /// 起点方格
        /// </summary>
        public Cell Cell1;
        /// <summary>
        /// 终点方格
        /// </summary>
        public Cell Cell2;
        /// <summary>
        /// 空的站立者
        /// </summary>
        public GameObject EmptyStanderPrefab;
        /// <summary>
        /// 是否箭头
        /// </summary>
        public bool IsArrow;
        /// <summary>
        /// 身体长度
        /// </summary>
        public float BodyLenth;
        /// <summary>
        /// 箭头长度
        /// </summary>
        public float ArrowLength;


        private GameObject m_EmptyStander1;
        private GameObject m_EmptyStander2;

        private void Start()
        {
            Material m = new Material(LineRenderer.material);
            LineRenderer.material = m;

        }
        /// <summary>
        /// 添加方格
        /// </summary>
        /// <param name="cell"></param>
        public void AddCell(Cell cell)
        {
            if (Cell1 == null)
            {
                Arrow.gameObject.SetActive(false);
                Cell1 = cell;
                LineRenderer.SetPosition(0, new Vector3(cell.transform.position.x, cell.transform.position.y, -19f));
                LineRenderer.SetPosition(1, new Vector3(cell.transform.position.x, cell.transform.position.y, -19f));
                m_EmptyStander1 = GameObject.Instantiate(EmptyStanderPrefab.gameObject);
                EmptyStander es = m_EmptyStander1.GetComponent<EmptyStander>();
                es.RefreshCell(Cell1);
            }
            else if (Cell2 == null)
            {
                Cell2 = cell;
                if (!Cell2.HasStander())
                {
                    Arrow.gameObject.SetActive(true);
                }
                LineRenderer.SetPosition(1, new Vector3(cell.transform.position.x, cell.transform.position.y, -19f));
                Arrow.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -19f);
                m_EmptyStander2 = GameObject.Instantiate(EmptyStanderPrefab.gameObject);
                EmptyStander es = m_EmptyStander2.GetComponent<EmptyStander>();
                es.RefreshCell(Cell2);
                float dis = Vector3.Distance(new Vector3(Cell2.transform.position.x, Cell2.transform.position.y, -19f), new Vector3(Cell1.transform.position.x, Cell1.transform.position.y, -19f));
                float v = dis / 0.64f;
                LineRenderer.material.SetTextureScale("_MainTex", new Vector2(v, 1));
            }
        }

        private void OnDestroy()
        {
            Destroy(m_EmptyStander1);
            Destroy(m_EmptyStander2);
        }
        public void UpdateLine(Vector3 pos)
        {
            float dis = Vector3.Distance(pos, Cell1.transform.position);
            Vector3 localPos = pos - Cell1.transform.position;
            float linePosProcess = (dis - ArrowLength) * dis;
            Vector3 lineLocalPos = localPos * linePosProcess;
            LineRenderer.SetPosition(1, new Vector3(pos.x, pos.y, -19f));

        }

    }
}
