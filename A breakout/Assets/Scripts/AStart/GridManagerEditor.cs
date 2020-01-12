using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LFramework;
using EasyFrameWork;

//序列号排列规则如上所示
//暂时只支持行列相等的正方形地图
namespace LFramework.Plugins.Astart
{
    public class GridManagerEditor : MonoBehaviour
    {
        [Header("网格")]
        public int NumOfRow = 10;                    //行
        public int NumOfColumn = 10;                 //列
        private List<List<Node>> m_Nodes;
        private List<List<GameObject>> m_TransparentCells;
        [SerializeField]
        private Transform m_Cell;
        [SerializeField]
        private float m_YScale = 0.5682f;
        [SerializeField]
        private float m_GridSize = 0.9f;
        public static GridManagerEditor Instance;
        private void Awake()
        {
            Instance = this;

        }
        private void Start()
        {
            CreateNode();
        }
        public void CreateNode()
        {
            m_Nodes = new List<List<Node>>();
            m_TransparentCells = new List<List<GameObject>>();
            //生成隐形的网格 为寻路做准备
            for (int i = 0; i < NumOfRow; i++)
            {
                List<GameObject> gos = new List<GameObject>();
                for (int j = 0; j < NumOfColumn; j++)
                {
                    GameObject go = Instantiate<GameObject>(m_Cell.gameObject);
                    go.name = i + "  " + j;
                    Vector2 pos = GetShearPos(new Vector2(j * m_GridSize, i * m_GridSize));
                    pos = GetScalePos(pos, m_YScale);
                    go.transform.position = new Vector3(pos.x, pos.y);
                    go.transform.SetParent(transform);
                    gos.Add(go);
                }
                m_TransparentCells.Add(gos);
            }
            //y轴缩放
            //transform.localScale = new Vector3(1, 50 / 88f, 1);
            transform.localScale = new Vector3(1, 1, 1);
            //生成寻路节点
            for (int i = 0; i < NumOfRow; i++)
            {
                List<Node> ns = new List<Node>();
                for (int j = 0; j < NumOfColumn; j++)
                {
                    GameObject go = m_TransparentCells[i][j];
                    Hex c = go.GetComponent<Hex>();
                    Node n = new Node(go.transform.position, i, j);
                    c.Row = i;
                    c.Column = j;
                    ns.Add(n);
                }
                m_Nodes.Add(ns);
            }
            //生成障碍物
            //List<Vector3> oobstaclePos = GameLevelDBModel.Instance.GetObstaclesVectorList(1);
            //for (int i = 0; i < oobstaclePos.Count; i++)
            //{
            //    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacle", isCache: true);
            //    go.transform.position = oobstaclePos[i];
            //    //将此位置设为障碍物
            //    Node n = GetNodeByPos(oobstaclePos[i]);
            //    if (n != null)
            //    {
            //        n.IsObstacle = true;
            //    }
            //}
        }
        public Vector2 GetScalePos(Vector2 pos, float ySclae)
        {
            float x = pos.x * 1 + pos.y * 0;
            float y = pos.x * 0 + pos.y * ySclae;
            return new Vector2(x, y);
        }
        public Vector2 GetShearPos(Vector2 pos)
        {
            float x = pos.x * 1 + pos.y * GetAxisY(60).x;
            float y = pos.x * 0 + pos.y * GetAxisY(60).y;
            return new Vector2(x, y);
        }
        private void Update()
        {

        }
        public Vector2 GetPos(Vector2 pos)
        {
            float x = pos.x * 1 + pos.y * GetAxisY(60).x;
            float y = pos.x * 0 + pos.y * GetAxisY(60).y;
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
            //左上
            int neighbourRow = orginRow - 1;
            int neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右下
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //上
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //下
            neighbourRow = orginRow - 1;
            neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //左
            neighbourRow = orginRow;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右
            neighbourRow = orginRow;
            neighbourCol = orginCol + 1;
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




    }
}