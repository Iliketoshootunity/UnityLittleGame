using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LFramework;
using EasyFrameWork;

namespace LFramework.Plugins.Astart
{
    public class GridManager : MonoSingleton<GridManager>
    {
        [Header("网格")]
        public int NumOfRow;                    //行
        public int NumOfColumn;                 //列
        public float m_CellHeight;              //格子高
        public float m_CellWidth;              //格子宽
        public GameObject CellPrefab;
        [Header("Debug")]
        public bool IsShowGrid;                 //是否显示网格   

        public GameObject Map;



        private Node[,] nodes;
        public Vector3 Origin
        {
            get
            {
                return transform.position;
            }
        }

        public float CellWidth
        {
            get
            {
                return m_CellWidth;
            }
        }

        public float CellHeight
        {
            get
            {
                return m_CellHeight;
            }
        }

        private void Start()
        {
            //InitializationNodes();
        }

        /// <summary>
        /// 初始化
        ///
        /// </summary>

        public void InitializationNodes()
        {
            nodes = new Node[NumOfRow, NumOfColumn];
            for (int i = 0; i < NumOfRow; i++)
            {
                for (int j = 0; j < NumOfColumn; j++)
                {
                    Vector2 pos = new Vector2(Origin.x + j * CellWidth + CellWidth / 2, Origin.y + i * CellHeight + CellHeight / 2);
                    nodes[i, j] = new Node(pos, i, j);
                    //GameObject go = GameObject.Instantiate(CellPrefab);
                    //go.transform.position = pos;
                    //go.transform.SetParent(Map.transform);

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
            return new Vector3(column * CellWidth + CellWidth / 2, 0, row * CellHeight + CellHeight / 2) + Origin;
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
            int row = (int)(Mathf.Abs(position.z - Origin.z) / CellHeight);
            int column = (int)(Mathf.Abs(position.x - Origin.x) / CellWidth);
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
            int orginRow = node.Row;
            int orginCol = node.Col;
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
            ////左上
            //neighbourRow = orginRow + 1;
            //neighbourCol = orginCol + 1;
            //AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            ////右上
            //neighbourRow = orginRow + 1;
            //neighbourCol = orginCol - 1;
            //AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            ////左下
            //neighbourRow = orginRow - 1;
            //neighbourCol = orginCol + 1;
            //AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
            ////右下
            //neighbourRow = orginRow - 1;
            //neighbourCol = orginCol - 1;
            //AssignedNeighbour(neighbourRow, neighbourCol, neighbours);
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
            int row = (int)(Mathf.Abs(position.y - Origin.y) / CellHeight);
            int column = (int)(Mathf.Abs(position.x - Origin.x) / CellWidth);
            if (row < 0 || column < 0 || row >= NumOfRow || column >= NumOfColumn)
            {
                return null;
            }
            return nodes[row, column];
        }
        /// <summary>
        /// 根据位置得到节点
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Node GetNode(int row, int col)
        {
            if (row < NumOfRow && col < NumOfColumn)
            {
                return nodes[row, col];
            }
            return null;

        }



        private void OnDrawGizmos()
        {
            if (IsShowGrid)
            {

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
                            Gizmos.DrawSphere(nodes[i, j].Position, CellHeight / 10);
                            Gizmos.DrawWireCube(nodes[i, j].Position, new Vector3(CellWidth, CellHeight, 0));
                        }
                    }

                }

            }
        }





    }
}