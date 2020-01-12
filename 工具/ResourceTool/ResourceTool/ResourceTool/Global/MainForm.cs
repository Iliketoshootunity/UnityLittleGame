using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckResourceTool.IndependentPanel.MainFormAreas;
using CheckResourceTool.IndependentPanel.MissionOptionSettingPanels;
using CheckResourceTool.IndependentPanel.MissionPanels;
using System.IO;

namespace CheckResourceTool
{
    public partial class MainForm : Form
    {
        public Action<string> writeLogDel;

        public CreateMission_Area createMissionArea;
        private Dictionary<MissionType, Type> typeToOptionDic;

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Global.Instance.MainForm = this;
            RegisterUIAndType();
            InitializeArea();
            FormClosed += delegate (object obj, FormClosedEventArgs e)
            {
                try
                {
                    if (e.CloseReason == CloseReason.UserClosing)
                    {

                        if (Directory.Exists(Global.Instance.TempDirectory))
                        {
                            Directory.Delete(Global.Instance.TempDirectory, true);
                        }
                    }
                }
                catch (Exception) { }
            };
        }

        #region UI
        /// <summary>
        /// 注册UI和类型
        /// </summary>
        private void RegisterUIAndType()
        {
            typeToOptionDic = new Dictionary<MissionType, Type>();
            typeToOptionDic.Add(MissionType.资源自动打包, typeof(MissionOption_AutoGenerateResourceZIP));
            typeToOptionDic.Add(MissionType.检验资源列表, typeof(MissionOption_CheckResourceList));
            typeToOptionDic.Add(MissionType.下载列表资源, typeof(MissionOption_DownloadResourceInList));
            typeToOptionDic.Add(MissionType.检验配置文件, typeof(MissionOption_CheckConfigFiles));
        }

        /// <summary>
        /// 初始化区域
        /// </summary>
        private void InitializeArea()
        {
            createMissionArea = Global.Instance.CreateUserControlInControl<CreateMission_Area>(panel_CreateMission, panel_CreateMission.Size, AnchorStyles.Bottom, AnchorStyles.Left, AnchorStyles.Right, AnchorStyles.Top);
            createMissionArea.onSelectedMissionChangedEvent += OnMissionSelectionChanged;
            createMissionArea.createNewMissionButtonClickedEvent += OnCreateNewMissionButtonClicked;
            writeLogDel += AppendGlobalLog;
        }

        private void OnMissionSelectionChanged(MissionType mt)
        {
            Type type;
            if (typeToOptionDic.TryGetValue(mt, out type))
            {
                createMissionArea.ShowMissionOption(type);
            }
        }

        private void OnCreateNewMissionButtonClicked(CreateMission_Area area)
        {
            area.CurrentMissionOptionUC?.CreateNewMission();
            if (flowLayoutPanel_MissionList.Controls.Count > 0)
            {
                flowLayoutPanel_MissionList.ScrollControlIntoView(flowLayoutPanel_MissionList.Controls[flowLayoutPanel_MissionList.Controls.Count - 1]);
            }
        }

        private void button_ClearGlobalLog_Click(object sender, EventArgs e)
        {
            textBox_GlobalLog.Clear();
        }

        private void AppendGlobalLog(string log)
        {
            textBox_GlobalLog.AppendText(log);
            textBox_GlobalLog.AppendText("\n");
        }

        /// <summary>
        /// 新建任务面板
        /// </summary>
        /// <typeparam name="T">新建的任务面板</typeparam>
        /// <returns></returns>
        public T CreateNewMissionPanel<T>() where T : MissionPanel_Base
        {
            return Global.Instance.CreateUserControlInControl<T>(flowLayoutPanel_MissionList);
        }
        #endregion
    }
}
