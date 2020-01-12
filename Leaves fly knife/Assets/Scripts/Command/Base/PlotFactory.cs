using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 命令工厂
    /// </summary>
	public class PlotFactory
    {

        public static PlotCommand CreatePlotCommand(string name)
        {
            PlotCommand command = null;
            switch (name)
            {
                case "DialogUICommand":
                    command = new DialogUICommand(true);
                    break;

            }
            return command;
        }

    }
}
