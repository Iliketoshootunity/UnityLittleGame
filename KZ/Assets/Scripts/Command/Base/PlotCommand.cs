using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public abstract class PlotCommand
    {
        /// <summary>
        /// 是否是自动播放
        /// </summary>
        public bool IsAuto;
        /// <summary>
        /// 上一条命令
        /// </summary>
        public PlotCommand PreCommand { get; set; }
        /// <summary>
        /// 下一条命令
        /// </summary>
        public PlotCommand NextCommand { get; set; }

        public PlotCommand(bool isAuto)
        {
            if (isAuto)
            {
                IsAuto = isAuto;
            }
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <returns></returns>
        public bool OnParse(string command)
        {
            return Parse(command);
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected abstract bool Parse(string command);

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public virtual bool DoAction()
        {
            PlotMgr.Instance.PI.CurCommand = this;
            return true;
        }

        /// <summary>
        /// 命令结束
        /// </summary>
        public void OnComandFinsh()
        {
            if (NextCommand != null)
            {
                NextCommand.DoAction();
            }
        }

    }
}
