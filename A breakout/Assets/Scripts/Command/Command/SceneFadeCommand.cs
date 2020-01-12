using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    /// <summary>
    /// 场景淡入淡出
    /// </summary>
    public class SceneFadeCommand : PlotCommand
    {
        /// <summary>
        /// 是否淡入
        /// </summary>
        public bool IsFadeIn;
        /// <summary>
        /// 事件
        /// </summary>
        public float Time;

        public SceneFadeCommand(bool isAuto) : base(isAuto)
        {

        }

        public SceneFadeCommand(bool isAuto, bool isFadeIn, float time) : base(isAuto)
        {
            IsFadeIn = isFadeIn;
            Time = time;
        }
        public override bool DoAction()
        {
            bool isOk = base.DoAction();
            //UIViewMgr.Instance.OpenView(UIViewType.SceneFade);
            PlotMgr.Instance.StartCoroutine(OnComandFinshIE());
            return isOk;
        }

        private IEnumerator OnComandFinshIE()
        {
            yield return new WaitForSeconds(Time);
            OnComandFinsh();
        }

        protected override bool Parse(string command)
        {
            string[] param = command.Split(',');
            return true;
        }

    }
}
