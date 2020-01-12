using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    //格子类
    public class Cell : MonoBehaviour
    {
        //public Cell LeftCell { get; set; }
        //public Cell RightCell { get; set; }

        //public Cell UpCell { get; set; }
        //public Cell DownCell { get; set; }

        public Cell LeftCell;
        public Cell RightCell;

        public Cell UpCell;
        public Cell DownCell;

        public int Row;

        public int Column;


        /// <summary>
        /// 是否是障碍物
        /// </summary>
        //public bool IsObstacle;
        //public Stander Stander { get; set; }

        /// <summary>
        /// 可能不止站立了一个武平
        /// </summary>
        public List<Stander> StanderList;

        public void Init(Cell leftCell, Cell rightCell, Cell upCell, Cell downCell)
        {
            LeftCell = leftCell;
            RightCell = rightCell;
            UpCell = upCell;
            DownCell = downCell;

        }

        public void AddRefreshStander(Stander stander)
        {
            if (StanderList == null)
            {
                StanderList = new List<Stander>();
            }
            StanderList.Add(stander);
            stander.OnDestoryAction += OnStanderDestory;
        }

        public void RemoveRefreshStander(Stander stander)
        {
            if (StanderList == null)
            {
                StanderList = new List<Stander>();
            }
            StanderList.Remove(stander);
            stander.OnDestoryAction -= OnStanderDestory;
        }


        public void OnStanderDestory(Stander stander)
        {
            StanderList.Remove(stander);
        }

        public Cell GetCell(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return UpCell;
                case Direction.Down:
                    return DownCell;
                case Direction.Left:
                    return LeftCell;
                case Direction.Right:
                    return RightCell;
            }
            return null;

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
