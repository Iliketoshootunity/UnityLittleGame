using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LFramework;
using EasyFrameWork;

//序列号排列规则如上所示
//暂时只支持行列相等的正方形地图
namespace LFramework.Plugins.Astart
{
    public class GridManager : MonoBehaviour
    {
        [Header("网格")]
        public int NumOfRow = 10;                    //行
        public int NumOfColumn = 10;                 //列
        public List<List<Node>> m_Nodes;
        private List<Hex> m_Hexs;
        [SerializeField]
        public float YScale = 0.5682f;
        [SerializeField]
        private float m_GridSize = 0.9f;
        public static GridManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void CreateNode()
        {
            m_Nodes = new List<List<Node>>();
            m_Hexs = new List<Hex>();
            Hex[] hexArr = GetComponentsInChildren<Hex>();
            for (int i = 0; i < hexArr.Length; i++)
            {
                m_Hexs.Add(hexArr[i]);
            }
            //生成隐形的网格 为寻路做准备
            for (int i = 0; i < NumOfRow; i++)
            {
                List<Node> ns = new List<Node>();
                for (int j = 0; j < NumOfColumn; j++)
                {
                    Vector2 pos = GetShearPos(new Vector2(j * m_GridSize, i * m_GridSize));
                    //pos = GetScalePos(pos, m_YScale);
                    Node n = new Node(pos, i, j);
                    ns.Add(n);
                }
                m_Nodes.Add(ns);
            }

            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {
                    Hex h = m_Hexs.Find(x => x.Row == i && x.Column == j);
                    if (h == null)
                    {
                        m_Nodes[i][j].IsObstacle = true;
                    }
                }

            }
        }
        public Vector2 GetShearPos(Vector2 pos)
        {
            float x = pos.x * 1 + pos.y * GetAxisY(60).x;
            float y = pos.x * 0 + pos.y * GetAxisY(60).y;
            return new Vector2(x, y);
        }
        public Vector2 GetScalePos(Vector2 pos, float ySclae)
        {
            float x = pos.x * 1 + pos.y * 0;
            float y = pos.x * 0 + pos.y * ySclae;
            return new Vector2(x, y);
        }
        public Vector2 GetAxisY(float angle)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = Mathf.Sin(Mathf.Deg2Rad * angle);
            return new Vector2(x, y);
        }
        /// <summary>
        /// 获得相邻的6个节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="neighbours"></param>
        public void GetNeighbours(Node node, ArrayList neighbours)
        {
            Vector3 originPos = node.Position;
            int orginRow = node.Row;
            int orginCol = node.Column;
            int neighbourRow = 0;
            int neighbourCol = 0;

            //下
            neighbourRow = orginRow - 1;
            neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //左
            neighbourRow = orginRow;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //左上
            neighbourRow = orginRow - 1;
            neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //上
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右
            neighbourRow = orginRow;
            neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右下
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);






        }

        /// <summary>
        /// 分配节点
        /// </summary>
        private void AssignedNeighbour(int row, int col, ArrayList neighbours)
        {
            if (row >= 0 && row < NumOfRow && col >= 0 && col < NumOfColumn)
            {
                if (!m_Nodes[row][col].IsObstacle)
                {
                    neighbours.Add(m_Nodes[row][col]);
                }
            }
        }
        /// <summary>
        /// 根据位置获取节点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Node GetNodeByPos(Vector3 pos)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.up, 0.05f);
            if (hit.collider == null) return null;
            Hex hex = hit.collider.GetComponent<Hex>();
            if (hex != null)
            {
                return m_Nodes[hex.Row][hex.Column];
            }
            return null;
        }

        public Hex GetHex(int row, int column)
        {
            return m_Hexs.Find(x => x.Row == row && x.Column == column);
        }
        public void RestGrid()
        {
            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {

                    m_Nodes[i][j].TotalCost = 0;
                    m_Nodes[i][j].EstimatedCost = 0;
                    m_Nodes[i][j].Parent = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (m_Nodes == null) return;
            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {
                    if (m_Nodes[i][j].IsObstacle)
                    {
                        Vector3 pos = GetScalePos(
     m_Nodes[i][j].Position, YScale);
                        Gizmos.color = Color.gray;
                        Gizmos.DrawWireSphere(pos, 0.3f);
                    }

                }
            }
        }


    }
}