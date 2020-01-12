using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    /// <summary>
    /// 站立在格子上的物品
    /// </summary>
	public abstract class Stander : MonoBehaviour
    {
        public Action<Stander> OnDestoryAction;
        /// <summary>
        /// 所在的格子
        /// </summary>
        public Cell Cell;

        /// <summary>
        /// 刷新格子
        /// </summary>
        /// <param name="cell"></param>
        public void RefreshCell(Cell cell)
        {
            if (Cell != null)
            {
                Cell.RemoveRefreshStander(this);
            }
            Cell = cell;
            if (Cell != null)
            {
                Cell.AddRefreshStander(this);
            }
        }
        private void OnDestroy()
        {
            if (OnDestoryAction != null)
            {
                OnDestoryAction(this);
            }
        }
    }
}
