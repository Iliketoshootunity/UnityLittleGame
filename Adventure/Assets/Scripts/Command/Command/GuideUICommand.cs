using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    /// <summary>
    /// 引导UI命令
    /// </summary>
	public class GuideUICommand : PlotCommand
    {
        public RectTransform Target;
        public GuideUICommand(RectTransform target, bool isAuto) : base(isAuto)
        {
            Target = target;
        }
        protected override bool Parse(string command)
        {
            return true;
        }

        public override bool DoAction()
        {
            bool isOk = base.DoAction();
            //UIViewMgr.Instance.OpenView(UIViewType.GameLevelGuide);
            return isOk;
        }
    }
}
