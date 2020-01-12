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
        public int NumOfRow = 8;                    //行
        public int NumOfColumn = 14;                 //列
        public float ColumnInterval = 1;
        public float RowInterval = 1;
        public List<List<Node>> m_Nodes;
        private List<List<Cell>> m_MapCell;
        private List<Cell> m_SideCell;
        public static GridManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void CreateNode(int level)
        {
            m_MapCell = new List<List<Cell>>();
            m_Nodes = new List<List<Node>>();
            GameObject go = null;
            Cell cell = null;
            //地面
            //行
            for (int i = 0; i < NumOfRow; i++)
            {
                List<Cell> cellList = new List<Cell>();
                List<Node> nodeList = new List<Node>();
                //列
                for (int j = 0; j < NumOfColumn; j++)
                {
                    //Cell 构建方块
                    Vector2 pos = new Vector2(j * ColumnInterval + ColumnInterval / 2f, -(i * RowInterval + RowInterval / 2f));

                    Node n = new Node(pos, i, j);
                    nodeList.Add(n);

                    go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/Ground", isCache: true);
                    go.transform.position = pos;
                    go.transform.rotation = Quaternion.identity;
                    go.transform.localScale = new Vector3(1, 1, 1);
                    cell = go.GetComponent<Cell>();
                    cellList.Add(cell);
                    cell.Init(n);
                }
                m_MapCell.Add(cellList);
                m_Nodes.Add(nodeList);
            }

            //要塞
            List<StrongholdInfo> strongholdInfoList = GameLevelDBModel.Instance.GetStrongholdInfo(level);
            for (int i = 0; i < strongholdInfoList.Count; i++)
            {
                cell = m_MapCell[strongholdInfoList[i].Row - 1][strongholdInfoList[i].Column - 1];

                go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/Stronghold", isCache: true);
                go.transform.position = cell.transform.position;
                go.transform.rotation = Quaternion.identity;
                go.transform.localScale = new Vector3(1, 1, 1);

                Stronghold s = go.GetComponent<Stronghold>();
                s.RefreshCell(cell);
                s.Init(strongholdInfoList[i].Range);
                s.Cell.Node.IsObstacle = true;
            }
            //怪物
            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Role, "Monster", isCache: true);
            Vector2 monsterPos = GameLevelDBModel.Instance.GetMonsterRowAndColunm(level);
            cell = m_MapCell[(int)monsterPos.x - 1][(int)monsterPos.y - 1];
            go.transform.position = cell.transform.position;
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = new Vector3(1, 1, 1);
            RoleCtrl roleCtrl = go.GetComponent<RoleCtrl>(); ;
            roleCtrl.RefreshCell(cell);
            GameLevelSceneCtrl.Instance.SetMonster(roleCtrl);

        }
        /// <summary>
        /// 重置所有节点
        /// </summary>
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

        public Cell GetCellByNode(Node n)
        {
            return m_MapCell[n.Row][n.Column];
        }

        public Cell GetCell(int row,int column)
        {
            return m_MapCell[row][column];
        }
        /// <summary>
        /// 获取边路的网格
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetSideCell()
        {
            if (m_SideCell == null)
            {
                m_SideCell = new List<Cell>();
                for (int i = 0; i < NumOfRow; i++)
                {
                    for (int j = 0; j < NumOfColumn; j++)
                    {
                        if (i == 0 || i == NumOfRow - 1 || j == 0 || j == NumOfColumn - 1)
                        {
                            Cell c = m_MapCell[i][j];
                            if (!m_SideCell.Contains(c))
                            {
                                m_SideCell.Add(c);
                            }
                        }
                    }
                }
            }
            return m_SideCell;
        }

        /// <summary>
        /// 获得相邻的4个节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="neighbours"></param>
        public void GetNeighbours(Node node, ArrayList neighbours, bool ignoreObstacle = false)
        {
            Vector3 originPos = node.Position;
            int orginRow = node.Row;
            int orginCol = node.Column;
            //上
            int neighbourRow = orginRow - 1;
            int neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours, ignoreObstacle);

            //下
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours, ignoreObstacle);

            //左
            neighbourRow = orginRow;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours, ignoreObstacle);
            //右
            neighbourRow = orginRow;
            neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours, ignoreObstacle);

        }
        /// <summary>
        /// 是否是四周的点
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsNeighbours(Node n1, Node n2)
        {
            //原点
            if (n1 == n2)
            {
                return false;
            }
            ArrayList list = new ArrayList();
            GetNeighbours(n1, list, true);
            foreach (var item in list)
            {
                Node n = (Node)item;
                if (n == n2)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 分配节点
        /// </summary>
        private void AssignedNeighbour(int row, int col, ArrayList neighbours, bool ignoreObstacle = false)
        {
            if (row >= 0 && row < NumOfRow && col >= 0 && col < NumOfColumn)
            {
                if (ignoreObstacle)
                {
                    neighbours.Add(m_Nodes[row][col]);
                }
                else
                {
                    if (!m_Nodes[row][col].IsObstacle)
                    {
                        neighbours.Add(m_Nodes[row][col]);
                    }
                }

            }
        }

        private void OnDrawGizmos()
        {
            if (m_Nodes == null || m_Nodes.Count == 0) return;
            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {
                    if (m_Nodes[i][j].IsObstacle)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(m_Nodes[i][j].Position, 0.3f);
                    }
                }
            }
        }

    }
}