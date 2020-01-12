using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LFramework.Plugins.Astart
{
    [Serializable]
    public class Node : IComparable
    {
        public float TotalCost;             //目前为止的总成本
        public float EstimatedCost;         //预估成本=  目前为止的总成本+自身节点到终点的成本  
        public Node Parent;                 //父节点
        public bool IsObstacle;             //是否是障碍物 
        public Vector3 Position;            //位置

        public Node()
        {
            TotalCost = 0;
            EstimatedCost = 0;
            Parent = null;
            IsObstacle = false;
            Position = Vector3.zero;
        }

        public Node(Vector3 pos)
        {
            TotalCost = 0;
            EstimatedCost = 0;
            Parent = null;
            IsObstacle = false;
            Position = pos;
        }
        /// <summary>
        /// 设置成障碍物
        /// </summary>
        public void MaskAsObstacle()
        {
            IsObstacle = true;
        }

        public int CompareTo(object obj)
        {
            Node node = (Node)obj;
            if (this.EstimatedCost < node.EstimatedCost)
            {
                return -1;
            }
            if (this.EstimatedCost > node.EstimatedCost)
            {
                return 1;
            }
            return 0;
        }
    }
}