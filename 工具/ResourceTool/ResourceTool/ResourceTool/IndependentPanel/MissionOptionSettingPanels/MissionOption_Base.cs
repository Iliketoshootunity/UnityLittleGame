using System;
using System.Windows.Forms;

namespace CheckResourceTool.IndependentPanel.MissionOptionSettingPanels
{
    public partial class MissionOption_Base : UserControl
    {
        private int componentLocationY = 0;

        public MissionOption_Base()
        {
            InitializeComponent();
            InitializeIndependentComponents();
        }

        /// <summary>
        /// 添加独立组件
        /// </summary>
        protected virtual void InitializeIndependentComponents() { }

        public virtual void CreateNewMission()
        {

        }

        protected virtual string GetBriefInformation()
        {
            return string.Format("任务创建时间: {0}", Global.Instance.GetCurrentTime());
        }

        #region 在任务配置面板添加自定义组件
        protected virtual T AddComponentInArea<T>(int posx, int width, params AnchorStyles[] anchorstyles)
            where T : UserControl
        {
            T temp = AddComponentInArea<T>(posx, anchorstyles);
            temp.Size = new System.Drawing.Size(width, temp.Height);
            return temp;
        }

        protected virtual T AddComponentInArea<T>(int posx, System.Drawing.Size size, params AnchorStyles[] anchorstyles)
            where T : UserControl
        {
            T temp = AddComponentInArea<T>(posx, anchorstyles);
            temp.Size = size;
            componentLocationY += Math.Max(0, (size.Height - temp.Size.Height));
            return temp;
        }

        protected virtual T AddComponentInArea<T>(int posx, params AnchorStyles[] anchorstyles)
            where T : UserControl
        {
            T tempUC = Global.Instance.CreateUserControlInControl<T>(this, anchorstyles);
            tempUC.Location = new System.Drawing.Point(posx, componentLocationY);
            componentLocationY += tempUC.Size.Height;
            return tempUC;
        }
        #endregion
    }
}
