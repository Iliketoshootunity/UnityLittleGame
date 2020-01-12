using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    //格子类
    public class Cell : MonoBehaviour
    {
        public Node Node;

        public void Init(Node n)
        {
            Node = n;
        }

        public Node GetNode()
        {
            return Node;
        }

        public Vector2 GetRowAndColumn()
        {
            return new Vector2(Node.Row, Node.Column);
        }

        /// <summary>
        /// 可能不止站立了一个武平
        /// </summary>
        public List<Stander> StanderList;

        public Stronghold GetStronghold()
        {
            for (int i = 0; i < StanderList.Count; i++)
            {
                if (StanderList[i].GetType() == typeof(Stronghold))
                {
                    return (Stronghold)StanderList[i];
                }
            }
            return null;
        }

        public void AddRefreshStander(Stander stander)
        {
            if (StanderList == null)
            {
                StanderList = new List<Stander>();
            }
            StanderList.Add(stander);
            stander.OnDestoryAction += OnStanderDestory;
            Node.IsObstacle = true;
        }

        public void RemoveRefreshStander(Stander stander)
        {
            if (StanderList == null)
            {
                StanderList = new List<Stander>();
            }
            StanderList.Remove(stander);
            stander.OnDestoryAction -= OnStanderDestory;
            if (StanderList.Count == 0)
            {
                Node.IsObstacle = false;
            }
        }


        public void OnStanderDestory(Stander stander)
        {
            RemoveRefreshStander(stander);
        }

        public bool HasStander()
        {
            if (StanderList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < StanderList.Count; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(StanderList[i].transform.position + new Vector3(0, i * 0.07f, 0), 0.15f);
            }
        }
    }
}
