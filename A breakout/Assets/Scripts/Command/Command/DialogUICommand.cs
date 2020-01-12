using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    /// <summary>
    /// 对话UI剧情
    /// </summary>
    public class DialogUICommand : PlotCommand
    {
        /// <summary>
        /// UI头像名字
        /// </summary>
        public string HeadSpriteName;
        /// <summary>
        /// 内容
        /// </summary>
        public string DialogContent;

        public bool IsLeft;


        public override bool DoAction()
        {
            bool isOk = base.DoAction();
            //UIViewMgr.Instance.OpenView(UIViewType.DialogWindow);
            return isOk;
        }

        public DialogUICommand(bool isAuto):base(isAuto)
        {

        }

        public DialogUICommand(bool isAuto,string headSpriteName, string dialogContent, bool isLeft) : base(isAuto)
        {
            HeadSpriteName = headSpriteName;
            DialogContent = dialogContent;
            IsLeft = isLeft;
        }
        protected override bool Parse(string command)
        {
            string[] param = command.Split(',');
            IsLeft = param[0] == "left" ? true : false;
            HeadSpriteName = param[1];
            DialogContent = param[2];
            return true;
        }

    }
}
