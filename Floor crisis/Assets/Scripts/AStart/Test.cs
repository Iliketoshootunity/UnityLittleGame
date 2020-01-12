
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LFramework.Plugins.Astart
{
    public class Test : MonoBehaviour
    {
        public Transform end;
        public Transform start;
         ArrayList list;
        // Use this for initialization
        void Start()
        {
            //PriorityQueue pq = new PriorityQueue();
            //Node node1 = new Node();
            //node1.EstimatedCost = 3;
            //pq.Push(node1);
            //Node node2 = new Node();
            //node2.EstimatedCost = 5;
            //pq.Push(node2);
            //Node node3 = new Node();
            //node3.EstimatedCost = 2;
            //pq.Push(node3);
            //Node node4 = new Node();
            //node4.EstimatedCost = 7;
            //pq.Push(node4);

            //Debug.Log(pq.First().EstimatedCost);

            Node startNode = GridManager.Instance.GetNode(start.position);
            Node endNode = GridManager.Instance.GetNode(end.position);
            list = AStar.FindPath(startNode, endNode);

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    Gizmos.DrawLine(((Node)list[i]).Position, ((Node)list[i + 1]).Position);
                }
            }
        }
    }
}