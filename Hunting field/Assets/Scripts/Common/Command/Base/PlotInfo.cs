using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 剧情数据
    /// </summary>
    public class PlotInfo
    {
        /// <summary>
        /// 所有的命令
        /// </summary>
        private List<PlotCommand> m_PlotCommands = new List<PlotCommand>();

        public PlotMgr PlotMgr { get; set; }
        /// <summary>
        /// 当前剧情
        /// </summary>
        public PlotCommand CurCommand;
        public PlotInfo() { }

        public PlotInfo(PlotMgr mgr)
        {
            PlotMgr = mgr;
        }

        public int Count
        {
            get { return m_PlotCommands.Count; }
        }

        /// <summary>
        /// 开始剧情
        /// </summary>
        public void Play()
        {
            //清除所有角色
            PlotMgr.ClearPlotActor();
            //第一个剧情
            if (Count > 0)
            {
                m_PlotCommands[0].DoAction();
            }
        }

        /// <summary>
        /// 停止剧情
        /// </summary>
        public void Stop()
        {

        }

        /// <summary>
        /// 关闭剧情
        /// </summary>
        public void Close()
        {

        }

        public void AddCommand(PlotCommand plotCommand)
        {
            if (Count > 0)
            {
                plotCommand.PreCommand = m_PlotCommands[Count - 1];
                m_PlotCommands[Count - 1].NextCommand = plotCommand;
            }
            m_PlotCommands.Add(plotCommand);
        }
    }
}
