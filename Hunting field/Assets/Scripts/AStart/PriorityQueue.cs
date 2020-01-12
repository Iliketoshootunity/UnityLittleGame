using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LFramework.Plugins.Astart
{
    //优先级队列
    //对OpenList 和 closeList 做排序
    public class PriorityQueue
    {
        private ArrayList nodes = new ArrayList();

        public int Length
        {
            get
            {
                return nodes.Count;
            }
        }

        public void Push(Node node)
        {
            nodes.Add(node);
            nodes.Sort();
        }

        public void Remove(Node node)
        {
            nodes.Remove(node);
            nodes.Sort();
        }

        public Node First()
        {
            if (this.nodes.Count <= 0) return null;
            return (Node)nodes[0];
        }
        public bool Contain(Node node)
        {
            return nodes.Contains(node);
        }
    }
}