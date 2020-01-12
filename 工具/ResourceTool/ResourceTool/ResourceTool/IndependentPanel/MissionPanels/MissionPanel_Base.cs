using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace CheckResourceTool.IndependentPanel.MissionPanels
{
    public partial class MissionPanel_Base : UserControl
    {
        public static int missionNumber = 0;
        private static Color[] backgroundColors = new Color[] { Color.PaleGreen, Color.LightYellow, Color.LightSteelBlue, Color.Plum };

        public enum MissionState
        {
            UnStarted,//尚未开始
            Operating,//正在操作
            Finished,//已结束
            Paused,//已暂停
        }

        #region 公开属性
        private string _MissionType;
        private string _MissionBriefInformation;
        private float _ProgressValue;
        private int missionOrder;
        private MissionState currentMissionState;

        /// <summary>
        /// 任务类型
        /// </summary>
        public string MissionType
        {
            get { return _MissionType; }
            set { _MissionType = value; label_MissionType.Text = _MissionType; }
        }

        /// <summary>
        /// 任务简略信息
        /// </summary>
        public string MissionBriefInformation
        {
            get { return _MissionBriefInformation; }
            set { _MissionBriefInformation = value; label_MissionBriefInformation.Text = _MissionBriefInformation; }
        }

        /// <summary>
        /// 进度值(小数)
        /// </summary>
        public float ProgressValue
        {
            get { return _ProgressValue; }
        }

        /// <summary>
        /// 任务编号
        /// </summary>
        public int MissionOrder
        {
            get { return missionOrder; }
            set { missionOrder = value; label_MissionOrder.Text = "任务" + value; }
        }

        public MissionState CurrentMissionState
        {
            get { return currentMissionState; }
            set { currentMissionState = value; this?.Invoke(missionStateDel, value); }
        }
        #endregion

        public Action<string> missionLogDel;
        private Action<float> progressDel;
        protected Action<MissionState> missionStateDel;
        protected Thread missionThread;
        protected Dictionary<TabPage, int> tabPageYPositionDic;
        protected DateTime missionBeginTime;
        protected DateTime missionEndTime;

        public MissionPanel_Base()
        {
            InitializeComponent();
            Initialize();
            Global.Instance.MissionList.Add(this);
        }

        ~MissionPanel_Base()
        {
            StopMissionThread();
        }

        /// <summary>
        /// 初始化,在基类的控件初始化之后执行
        /// </summary>
        protected virtual void Initialize()
        {
            missionNumber++;
            if (backgroundColors != null && backgroundColors.Length > 0) { BackColor = backgroundColors[missionNumber % backgroundColors.Length]; }
            MissionOrder = missionNumber;

            missionLogDel = delegate (string log)
            {
                if (!string.IsNullOrEmpty(log))
                {
                    textBox_OperationLog.AppendText(log);
                    textBox_OperationLog.AppendText("\r\n");
                }
            };
            progressDel = delegate (float progressValue)
            {
                _ProgressValue = Math.Min(Math.Max(progressValue, 0), 1);
                label_ProgressPercentage.Text = (progressValue == 0) ? string.Empty : string.Format("{0:N2}%", _ProgressValue * 100);
                progressBar_MissionPercentage.Maximum = 1000;
                progressBar_MissionPercentage.Value = (int)(_ProgressValue * 1000);
            };
            missionStateDel = RefreshMissionState;

            SetMissionProgress(0);
        }

        public virtual void InitializeMission(params object[] input)
        {
            Global.Instance.WriteGlobalLog(string.Format("新建{0} {1} {2}", label_MissionOrder.Text, MissionType, MissionBriefInformation));
            CurrentMissionState = MissionState.UnStarted;
        }

        protected virtual void RefreshMissionState(MissionState missionState)
        {
            switch (missionState)
            {
                case MissionState.UnStarted:
                    button_StartMission.Enabled = true;
                    button_FinishMission.Enabled = false;
                    button_PauseMission.Enabled = false;
                    button_PauseMission.Text = "暂停";
                    break;
                case MissionState.Operating:
                    button_StartMission.Enabled = false;
                    button_FinishMission.Enabled = true;
                    button_PauseMission.Enabled = true;
                    button_PauseMission.Text = "暂停";
                    break;
                case MissionState.Finished:
                    button_StartMission.Enabled = false;
                    button_FinishMission.Enabled = false;
                    button_PauseMission.Enabled = false;
                    button_PauseMission.Text = "暂停";
                    break;
                case MissionState.Paused:
                    button_StartMission.Enabled = false;
                    button_FinishMission.Enabled = true;
                    button_PauseMission.Enabled = true;
                    button_PauseMission.Text = "继续";
                    break;
                default:
                    break;
            }
        }

        #region 工具方法
        /// <summary>
        /// 设置任务进度
        /// </summary>
        /// <param name="progressValue"></param>
        public void SetMissionProgress(float progressValue)
        {
            try
            {
                Invoke(progressDel, progressValue);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 追加日志
        /// </summary>
        /// <param name="log"></param>
        public void AppendMissionLog(string log)
        {
            try
            {
                Invoke(missionLogDel, log);
            }
            catch (Exception) { }
        }

        public void AppendMissionLog(object obj)
        {
            if (obj == null)
            {
                AppendMissionLog("null");
            }
            else
            {
                AppendMissionLog(obj.ToString());
            }
        }

        /// <summary>
        /// 在信息标签页中添加组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="posX">组件X位置</param>
        /// <param name="anchorStyles">锚点</param>
        /// <returns></returns>
        public T AddComponentInInfoTabpage<T>(int posX, params AnchorStyles[] anchorStyles)
            where T : UserControl
        {
            return AddComponentInTabpage<T>(tabPage_Info, posX, anchorStyles);
        }

        public virtual T AddComponentInTabpage<T>(TabPage tabpage, int posX, params AnchorStyles[] anchorStyles)
            where T : UserControl
        {
            if (tabpage == null)
            {
                return null;
            }
            if (tabPageYPositionDic == null)
            {
                tabPageYPositionDic = new Dictionary<TabPage, int>();
            }
            int posY = 0;
            if (!tabPageYPositionDic.TryGetValue(tabpage, out posY))
            {
                tabPageYPositionDic.Add(tabpage, posY);
            }
            T tempUC = Global.Instance.CreateUserControlInControl<T>(tabpage, anchorStyles);
            tempUC.Location = new Point(posX, posY);
            tabPageYPositionDic[tabpage] += tempUC.Height;
            return tempUC;
        }

        /// <summary>
        /// 从外部开启任务
        /// </summary>
        public void StartMissionOuter()
        {
            if (button_StartMission.Enabled == true)
            {
                button_StartMission.PerformClick();
            }
        }

        /// <summary>
        /// 从外部暂停任务
        /// </summary>
        public void PauseMissionOuter()
        {
            if (button_PauseMission.Enabled == true)
            {
                button_PauseMission.PerformClick();
            }
        }

        /// <summary>
        /// 从外部结束任务
        /// </summary>
        public void FinishMissionOuter()
        {
            if (button_FinishMission.Enabled == true)
            {
                button_FinishMission.PerformClick();
            }
        }

        /// <summary>
        /// 从外部关闭任务
        /// </summary>
        public void CloseMissionOuter()
        {
            if (button_CloseMission.Enabled == true)
            {
                button_CloseMission.PerformClick();
            }
        }
        #endregion

        #region 控件事件
        /// <summary>
        /// 导出日志按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LogOutput_Click(object sender, EventArgs e)
        {
            Global.Instance.SaveFileByDialog(textBox_OperationLog.Text, string.Format("选择{0}日志保存目录", MissionType));
        }

        /// <summary>
        /// 开始按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_StartMission_Click(object sender, EventArgs e)
        {
            StartMissionThread();
        }

        /// <summary>
        /// 关闭按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_FinishMission_Click(object sender, EventArgs e)
        {
            StopMissionThread();
        }

        /// <summary>
        /// 暂停按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_PauseMission_Click(object sender, EventArgs e)
        {
            Button pauseBtn = (Button)sender;
            if (pauseBtn.Text == "暂停")
            {
                PauseMissionThread();
                pauseBtn.Text = "继续";
            }
            else
            {
                ContinueMissionThread();
                pauseBtn.Text = "暂停";
            }
        }

        /// <summary>
        /// 关闭按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void button_CloseMission_Click(object sender, EventArgs e)
        {
            if (missionThread != null)
            {
                missionThread.Abort();
                missionThread = null;
            }
            Global.Instance.WriteGlobalLog(string.Format("{0}--- 任务{1}  {2}", Global.Instance.STRING_ClosePanelMission, MissionOrder, MissionType));
            Global.Instance.MissionList.Remove(this);
            Dispose(true);
        }

        /// <summary>
        /// 导出记录按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ExportLog_Click(object sender, EventArgs e)
        {
            Global.Instance.SaveFileByDialog(textBox_OperationLog.Text, string.Format("选择路径保存 任务{0} 执行记录", MissionOrder));
        }

        /// <summary>
        /// 清空记录按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearLog_Click(object sender, EventArgs e)
        {
            textBox_OperationLog.Clear();
        }
        #endregion

        #region 任务

        #region 线程
        private bool isPaused;
        private object threadPausedLock = new object();
        private bool isEnd;
        private object threadEndLock = new object();

        protected void StartMissionThread()
        {
            if (missionThread != null)
            {
                return;
            }
            missionThread = new Thread(ThreadMethod);
            missionThread.IsBackground = true;
            ContinueMissionThread();
            isEnd = false;
            missionThread.Start();
        }

        protected void StopMissionThread()
        {
            if (missionThread != null)
            {
                try
                {
                    missionThread.Abort();
                    missionThread = null;
                    CurrentMissionState = MissionState.Finished;
                    AppendMissionLog(Global.Instance.STRING_CloseMissionThread);
                    missionEndTime = DateTime.Now;
                    TimeSpan throughTime = missionBeginTime - missionEndTime;
                    AppendMissionLog("任务用时:  " + throughTime);
                }
                catch (Exception ex)
                {
                    Global.Instance.WriteGlobalLog(ex.Message);
                }
            }
        }

        protected void PauseMissionThread()
        {
            lock (threadPausedLock)
            {
                isPaused = true;
            }
        }

        protected void ContinueMissionThread()
        {
            lock (threadPausedLock)
            {
                isPaused = false;
            }
        }

        protected void EndMissionThread()
        {
            lock (threadEndLock)
            {
                isEnd = true;
            }
        }

        private void ThreadMethod()
        {
            CurrentMissionState = MissionState.Operating;
            Thread_Starting();
            while (!isEnd)
            {
                if (isPaused)
                {
                    AppendMissionLog(Global.Instance.STRING_PauseMissionThread);
                    CurrentMissionState = MissionState.Paused;
                    while (isPaused)
                    {
                        Thread.Sleep(10);
                    }
                    CurrentMissionState = MissionState.Operating;
                    AppendMissionLog(Global.Instance.STRING_ContinueMissionThread);
                }
                Thread_Looping();
            }
            Thread_Finishing();
            CurrentMissionState = MissionState.Finished;
        }

        /// <summary>
        /// 线程初始化
        /// </summary>
        protected virtual void Thread_Starting()
        {
            AppendMissionLog(Global.Instance.STRING_StartMissionThread);
            missionBeginTime = DateTime.Now;
        }

        /// <summary>
        /// 线程循环部分
        /// </summary>
        protected virtual void Thread_Looping()
        {

        }

        /// <summary>
        /// 线程结束部分
        /// </summary>
        protected virtual void Thread_Finishing()
        {
            AppendMissionLog(Global.Instance.STRING_FinishMissionThread);
            missionEndTime = DateTime.Now;
            TimeSpan throughTime = missionEndTime - missionBeginTime;
            AppendMissionLog("任务开始时间:" + missionBeginTime + "  任务结束时间:" + missionEndTime);
            AppendMissionLog("任务用时:  " + Global.Instance.TransferTimeSpanToSTR(throughTime));
        }
        #endregion

        #endregion
    }
}
