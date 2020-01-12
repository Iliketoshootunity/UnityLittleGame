using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;

namespace EasyFrameWork
{
    public enum StanderType
    {
        Player,
        GateWay,
        Prop,
        Obstacles
    }
    /// <summary>
    /// 站立在格子上的物品
    /// </summary>
	public abstract class Stander : MonoBehaviour
    {

        public abstract StanderType StanderType { get; }

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
            Cell = cell;
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
