﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//解释说明
//https://blog.csdn.net/agroupofruffian/article/details/77702372

namespace LFramework.Plugins.Astart
{
    public class AStar
    {
        private static PriorityQueue openList;          //开启的可以计算的节点
        private static PriorityQueue closeList;         //关闭的不能计算的节点

        /// <summary>
        /// 节点的价值估算
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float NodeCost(Node a, Node b)
        {
            Vector3 cost = a.Position - b.Position;
            float value = cost.magnitude;
            return value;
        }

        public static ArrayList FindPath(Node start, Node end)
        {
            GridManager.Instance.RestGrid();
            ArrayList path = new ArrayList();
            openList = new PriorityQueue();
            closeList = new PriorityQueue();
            start.TotalCost = 0;
            start.EstimatedCost = NodeCost(start, end);
            openList.Push(start);
            //每次OpenList排序之后的最小预估值的节点，以下统称为新节点
            Node node = null;
            //A*每次更新OpenList后，选取离终点最近的点，这样的话就有一个趋势，往终点的趋势，所以可以不必把所有的点计算完毕
            while (openList.Length != 0)
            {
                node = openList.First();
                if (node.Position == end.Position)
                {
                    path = CalculatePath(node);
                    return path;
                }
                //获得新节点的已经排除障碍物最多8个的相邻的点
                ArrayList neighbours = new ArrayList();
                GridManager.Instance.GetNeighbours(node, neighbours);
                foreach (var item in neighbours)
                {
                    Node neighbour = (Node)item;
                    //确保相邻的节点不是关闭的节点
                    if (!closeList.Contain(neighbour))
                    {
                        //求出相邻两个节点的价值
                        float cost = NodeCost(node, neighbour);
                        float totalCost = node.TotalCost + cost;
                        float neighbourNodeEstCost = NodeCost(neighbour, end);
                        if (totalCost < neighbour.TotalCost || neighbour.TotalCost == 0)
                        {
                            neighbour.TotalCost = totalCost;
                            neighbour.EstimatedCost = totalCost + neighbourNodeEstCost;
                            neighbour.Parent = node;
                        }
                        //因为两个或多个的节点的相邻节点都是这个相邻节点的相邻的两个节点的估值是不一样的，所以有必要重新计算
                        if (!openList.Contain(neighbour))
                        {
                            openList.Push(neighbour);
                        }
                    }

                }
                //把已经计算过的节点移除打开队列
                openList.Remove(node);
                closeList.Push(node);
            }
            Debug.LogError("错误：没有路径");

            path = CalculatePath(node);
            return path;
        }

        public static ArrayList CalculatePath(Node node)
        {
            ArrayList path = new ArrayList();
            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();
            //删除自身的节点
            path.RemoveAt(0);
            return path;
        }

    }
}

