using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckResourceTool.IndependentPanel.MissionOptionSettingPanels;
using System.IO;

namespace CheckResourceTool.IndependentPanel.MainFormAreas
{
    public partial class CreateMission_Area : UserControl
    {
        public event Action<CreateMission_Area> createNewMissionButtonClickedEvent;
        public event Action<MissionType> onSelectedMissionChangedEvent;

        public List<object> MissionOptions { get { return missionOptionDic.Values.ToList(); } }
        private Dictionary<Type, object> missionOptionDic;

        private MissionOption_Base currentMissionOptionUC;
        /// <summary>
        /// 当前人物选项控件
        /// </summary>
        public MissionOption_Base CurrentMissionOptionUC { get { return currentMissionOptionUC; } }

        public CreateMission_Area()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            comboBox_MissionType.Items.AddRange(Global.Instance.MissionTypes);
            missionOptionDic = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 显示任务选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ShowMissionOption(Type type)
        {
            object ob;
            MissionOption_Base mo;
            if (!(missionOptionDic != null && missionOptionDic.ContainsKey(type) && missionOptionDic.TryGetValue(type, out ob)))
            {
                ob = Global.Instance.CreateUserControlInControl(type, panel_MissionOptionSetting.Size, panel_MissionOptionSetting, AnchorStyles.Bottom, AnchorStyles.Left, AnchorStyles.Right, AnchorStyles.Top);
                if (ob is MissionOption_Base)
                {
                    missionOptionDic.Add(type, ob as MissionOption_Base);
                }
            }
            mo = ob as MissionOption_Base;
            if (mo != null)
            {
                ShowMissionOption(mo);
            }
        }

        /// <summary>
        /// 展示任务选项
        /// </summary>
        /// <param name="targetMO"></param>
        private void ShowMissionOption(MissionOption_Base targetMO)
        {
            if (currentMissionOptionUC == targetMO || targetMO == null)
            {
                return;
            }
            if (currentMissionOptionUC != null)
            {
                currentMissionOptionUC.Hide();
            }
            targetMO.Show();
            currentMissionOptionUC = targetMO;
        }

        private void button_CreateNewMission_Click(object sender, EventArgs e)
        {
            createNewMissionButtonClickedEvent?.Invoke(this);
        }

        private void comboBox_MissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            MissionType mt;
            if (Enum.TryParse(cb.Text, out mt))
            {
                onSelectedMissionChangedEvent?.Invoke(mt);
            }
        }

        private void button_OpenTempDirectory_Click(object sender, EventArgs e)
        {
            Global.Instance.OpenURL(Global.Instance.TempDirectory);
        }

        private void button_OpenFixedDirectory_Click(object sender, EventArgs e)
        {
            Global.Instance.OpenURL(Global.Instance.FixedDirectory);
        }

        private void button_StartAllMissions_Click(object sender, EventArgs e)
        {
            List<MissionPanels.MissionPanel_Base> missions = new List<MissionPanels.MissionPanel_Base>(Global.Instance.MissionList);
            for (int i = 0; i < missions.Count; i++)
            {
                if (missions[i] != null)
                {
                    missions[i].StartMissionOuter();
                }
            }
        }

        private void button_PauseAllMissions_Click(object sender, EventArgs e)
        {
            List<MissionPanels.MissionPanel_Base> missions = new List<MissionPanels.MissionPanel_Base>(Global.Instance.MissionList);
            for (int i = 0; i < missions.Count; i++)
            {
                if (missions[i] != null)
                {
                    missions[i].PauseMissionOuter();
                }
            }
        }

        private void button_FinishAllMissions_Click(object sender, EventArgs e)
        {
            List<MissionPanels.MissionPanel_Base> missions = new List<MissionPanels.MissionPanel_Base>(Global.Instance.MissionList);
            for (int i = 0; i < missions.Count; i++)
            {
                if (missions[i] != null)
                {
                    missions[i].FinishMissionOuter();
                }
            }
        }

        private void button_CloseAllMissions_Click(object sender, EventArgs e)
        {
            List<MissionPanels.MissionPanel_Base> missions = new List<MissionPanels.MissionPanel_Base>(Global.Instance.MissionList);
            for (int i = 0; i < missions.Count; i++)
            {
                if (missions[i] != null)
                {
                    missions[i].CloseMissionOuter();
                }
            }
        }
    }
}
