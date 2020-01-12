using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LFramework;
using  EasyFrameWork;

//24 23 22 21 20
//18 17 16 15 14
//14 13 12 14 10
//9   8  7  6  5
//4   3  2  1  0
//序列号排列规则如上所示
//暂时只支持行列相等的正方形地图
namespace LFramework.Plugins.Astart
{
    public class GridManager : MonoSingleton<GridManager>
    {
        [Header("网格")]
        public int NumOfRow;                    //行
        public int NumOfColumn;                 //列
        public float GridCellSize;              //网格单元大小

        [Header("Debug")]
        public bool IsShowGrid;                 //是否显示网格   
        public string GroundMask;
        public string ObstacleMask;

        public AstarBakeInfo info;
        private Node[,] nodes;
        public Vector3 Origin
        {
            get
            {
                return transform.position;
            }
        }

        public float Width
        {
            get
            {
                return NumOfColumn * GridCellSize;
            }
        }

        public float Height
        {
            get
            {
                return NumOfRow * GridCellSize;
            }
        }

        private void Awake()
        {
            InitializationNodes();
        }
        /// <summary>
        /// 初始化，后期改烘培
        ///
        /// </summary>

        public void InitializationNodes()
        {
            nodes = new Node[NumOfRow, NumOfColumn];
            int index = 0;
            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {
                    nodes[i, j] = new Node();
                    Vector3 rayOrigin = GetPosition(index) + Vector3.up * 100;
                    RaycastHit hit;
                    float lastY = 0;
                    if (Physics.Raycast(rayOrigin, rayOrigin - Vector3.up * 2000, out hit, 2000, 1 << LayerMask.NameToLayer(ObstacleMask) | 1 << LayerMask.NameToLayer(GroundMask)))
                    {
                        nodes[i, j].Position = hit.point;
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer(ObstacleMask))
                        {
                            nodes[i, j].MaskAsObstacle();
                            lastY = hit.point.y;
                        }
                        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(GroundMask))
                        {
                            lastY = hit.point.y;
                        }
                        else
                        {
                            nodes[i, j].Position = new Vector3(nodes[i, j].Position.x, lastY, nodes[i, j].Position.z);
                        }
                    }
                    index++;
                }
            }

        }

        public void RestGrid()
        {
            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {

                    nodes[i, j].TotalCost = 0;
                    nodes[i, j].EstimatedCost = 0;
                    nodes[i, j].Parent = null;
                }
            }
        }
        /// <summary>
        /// 根据系列号得到坐标
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Vector3 GetPosition(int index)
        {
            int row = GetRow(index);
            int column = GetColumn(index);
            return new Vector3(column * GridCellSize + GridCellSize / 2, 0, row * GridCellSize + GridCellSize / 2) + Origin;
        }
        /// <summary>
        /// 获得行
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetRow(int index)
        {
            return (int)(index / NumOfRow);
        }
        /// <summary>
        /// 获得列
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetColumn(int index)
        {
            return index % NumOfColumn;
        }
        /// <summary>
        /// 根据位置得到Index
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private int GetIndex(Vector3 position)
        {
            int row = (int)(Mathf.Abs(position.z - Origin.z) / GridCellSize);
            int column = (int)(Mathf.Abs(position.x - Origin.x) / GridCellSize);
            //推到公式
            return row * NumOfColumn + column;

        }

        /// <summary>
        /// 获得相邻的8个节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="neighbours"></param>
        public void GetNeighbours(Node node, ArrayList neighbours)
        {
            Vector3 originPos = node.Position;
            int originIndex = GetIndex(originPos);
            int orginRow = GetRow(originIndex);
            int orginCol = GetColumn(originIndex);
            //上方
            int neighbourRow = orginRow + 1;
            int neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //下方
            neighbourRow = orginRow - 1;
            neighbourCol = orginCol;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //左方
            neighbourRow = orginRow;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右方
            neighbourRow = orginRow;
            neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //左上
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右上
            neighbourRow = orginRow + 1;
            neighbourCol = orginCol - 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //左下
            neighbourRow = orginRow - 1;
            neighbourCol = orginCol + 1;
            AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            //右下
            neighbourRow = orginRow - 1;
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
                if (!nodes[row, col].IsObstacle)
                {
                    neighbours.Add(nodes[row, col]);
                }

            }
        }

        /// <summary>
        /// 根据位置得到节点
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Node GetNode(Vector3 position)
        {
            int row = (int)(Mathf.Abs(position.z - Origin.z) / GridCellSize);
            int column = (int)(Mathf.Abs(position.x - Origin.x) / GridCellSize);
            return nodes[row, column];
        }

        private void OnDrawGizmos()
        {
            if (IsShowGrid)
            {
                //计算行网格
                for (int i = 0; i < NumOfRow + 1; i++)
                {
                    Vector3 startPos = Origin + new Vector3(0.0f, 0.0f, i * GridCellSize);
                    Vector3 endPos = startPos + new Vector3(Width, 0, 0);
                    DebugDrawLine(startPos, endPos, Color.blue);
                }
                //计算列网格
                for (int i = 0; i < NumOfColumn + 1; i++)
                {
                    Vector3 startPos = Origin + new Vector3(i * GridCellSize, 0.0f, 0);
                    Vector3 endPos = startPos + new Vector3(0, 0, Height);
                    DebugDrawLine(startPos, endPos, Color.blue);
                }

                if (nodes != null)
                {
                    for (int i = 0; i < NumOfRow; i++)
                    {
                        for (int j = 0; j < NumOfColumn; j++)
                        {
                            if (nodes[i, j].IsObstacle)
                            {
                                Gizmos.color = Color.red;
                            }
                            else
                            {
                                Gizmos.color = Color.green;
                            }
                            Gizmos.DrawSphere(nodes[i, j].Position, GridCellSize / 10);
                        }
                    }
                    //Gizmos.color = Color.yellow;
                    //ArrayList nodeList = new ArrayList();
                    //GetNeighbours(nodes[1, 1], nodeList);
                    //foreach (Node item in nodeList)
                    //{
                    //    Gizmos.DrawSphere(item.Position, GridCellSize / 5);
                    //}
                }

            }
        }

        private void DebugDrawLine(Vector3 startPos, Vector3 endPos, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(startPos, endPos);
        }



    }
}