using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckResourceTool.IndependentPanel.MissionPanels;

namespace CheckResourceTool.IndependentPanel.MissionOptionSettingPanels
{
    class MissionOption_CheckConfigFiles : MissionOption_Base
    {
        #region 控件
        private Button button_SelectLocalConfigFolder;
        private ListBox listBox_ConfigList;
        private TextBox textBox_RemoteConfigFileURL;
        private Button button_AddRemoteURLTolist;
        private CheckBox checkBox_CheckResListAtSameTime;
        private CheckBox checkBox_CheckFullResourceList;
        private Button button_ClearConfigList;
        private Button button_SelectLocalConfigFile;

        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            this.checkBox_CheckFullResourceList = new System.Windows.Forms.CheckBox();
            this.checkBox_CheckResListAtSameTime = new System.Windows.Forms.CheckBox();
            this.button_AddRemoteURLTolist = new System.Windows.Forms.Button();
            this.textBox_RemoteConfigFileURL = new System.Windows.Forms.TextBox();
            this.listBox_ConfigList = new System.Windows.Forms.ListBox();
            this.button_SelectLocalConfigFolder = new System.Windows.Forms.Button();
            this.button_SelectLocalConfigFile = new System.Windows.Forms.Button();
            this.button_ClearConfigList = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(23, 185);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(101, 12);
            label1.TabIndex = 5;
            label1.Text = "远程配置文件地址";
            // 
            // checkBox_CheckFullResourceList
            // 
            this.checkBox_CheckFullResourceList.AutoSize = true;
            this.checkBox_CheckFullResourceList.Location = new System.Drawing.Point(4, 87);
            this.checkBox_CheckFullResourceList.Name = "checkBox_CheckFullResourceList";
            this.checkBox_CheckFullResourceList.Size = new System.Drawing.Size(120, 16);
            this.checkBox_CheckFullResourceList.TabIndex = 8;
            this.checkBox_CheckFullResourceList.Text = "检验完整资源列表";
            this.checkBox_CheckFullResourceList.UseVisualStyleBackColor = true;
            // 
            // checkBox_CheckResListAtSameTime
            // 
            this.checkBox_CheckResListAtSameTime.AutoSize = true;
            this.checkBox_CheckResListAtSameTime.Location = new System.Drawing.Point(4, 65);
            this.checkBox_CheckResListAtSameTime.Name = "checkBox_CheckResListAtSameTime";
            this.checkBox_CheckResListAtSameTime.Size = new System.Drawing.Size(96, 16);
            this.checkBox_CheckResListAtSameTime.TabIndex = 7;
            this.checkBox_CheckResListAtSameTime.Text = "检验更新列表";
            this.checkBox_CheckResListAtSameTime.UseVisualStyleBackColor = true;
            // 
            // button_AddRemoteURLTolist
            // 
            this.button_AddRemoteURLTolist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AddRemoteURLTolist.BackgroundImage = global::CheckResourceTool.Properties.Resources.Addbutton;
            this.button_AddRemoteURLTolist.Location = new System.Drawing.Point(1084, 182);
            this.button_AddRemoteURLTolist.Name = "button_AddRemoteURLTolist";
            this.button_AddRemoteURLTolist.Size = new System.Drawing.Size(20, 18);
            this.button_AddRemoteURLTolist.TabIndex = 6;
            this.button_AddRemoteURLTolist.UseVisualStyleBackColor = true;
            this.button_AddRemoteURLTolist.Click += new System.EventHandler(this.button_AddRemoteURLToList_Click);
            // 
            // textBox_RemoteConfigFileURL
            // 
            this.textBox_RemoteConfigFileURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RemoteConfigFileURL.Location = new System.Drawing.Point(130, 182);
            this.textBox_RemoteConfigFileURL.Name = "textBox_RemoteConfigFileURL";
            this.textBox_RemoteConfigFileURL.Size = new System.Drawing.Size(945, 21);
            this.textBox_RemoteConfigFileURL.TabIndex = 4;
            // 
            // listBox_ConfigList
            // 
            this.listBox_ConfigList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_ConfigList.FormattingEnabled = true;
            this.listBox_ConfigList.HorizontalScrollbar = true;
            this.listBox_ConfigList.ItemHeight = 12;
            this.listBox_ConfigList.Location = new System.Drawing.Point(130, 4);
            this.listBox_ConfigList.Name = "listBox_ConfigList";
            this.listBox_ConfigList.Size = new System.Drawing.Size(974, 172);
            this.listBox_ConfigList.TabIndex = 3;
            // 
            // button_SelectLocalConfigFolder
            // 
            this.button_SelectLocalConfigFolder.Location = new System.Drawing.Point(4, 35);
            this.button_SelectLocalConfigFolder.Name = "button_SelectLocalConfigFolder";
            this.button_SelectLocalConfigFolder.Size = new System.Drawing.Size(120, 24);
            this.button_SelectLocalConfigFolder.TabIndex = 1;
            this.button_SelectLocalConfigFolder.Text = "本地配置文件夹";
            this.button_SelectLocalConfigFolder.UseVisualStyleBackColor = true;
            this.button_SelectLocalConfigFolder.Click += new System.EventHandler(this.button_SelectLocalConfigFolder_Click);
            // 
            // button_SelectLocalConfigFile
            // 
            this.button_SelectLocalConfigFile.Location = new System.Drawing.Point(4, 4);
            this.button_SelectLocalConfigFile.Name = "button_SelectLocalConfigFile";
            this.button_SelectLocalConfigFile.Size = new System.Drawing.Size(120, 24);
            this.button_SelectLocalConfigFile.TabIndex = 0;
            this.button_SelectLocalConfigFile.Text = "本地配置文件";
            this.button_SelectLocalConfigFile.UseVisualStyleBackColor = true;
            this.button_SelectLocalConfigFile.Click += new System.EventHandler(this.button_SelectLocalConfigFile_Click);
            // 
            // button_ClearConfigList
            // 
            this.button_ClearConfigList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ClearConfigList.Location = new System.Drawing.Point(4, 153);
            this.button_ClearConfigList.Name = "button_ClearConfigList";
            this.button_ClearConfigList.Size = new System.Drawing.Size(120, 24);
            this.button_ClearConfigList.TabIndex = 9;
            this.button_ClearConfigList.Text = "清空";
            this.button_ClearConfigList.UseVisualStyleBackColor = true;
            this.button_ClearConfigList.Click += new System.EventHandler(this.button_ClearConfigList_Click);
            // 
            // MissionOption_CheckConfigFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.button_ClearConfigList);
            this.Controls.Add(this.checkBox_CheckFullResourceList);
            this.Controls.Add(this.checkBox_CheckResListAtSameTime);
            this.Controls.Add(this.button_AddRemoteURLTolist);
            this.Controls.Add(label1);
            this.Controls.Add(this.textBox_RemoteConfigFileURL);
            this.Controls.Add(this.listBox_ConfigList);
            this.Controls.Add(this.button_SelectLocalConfigFolder);
            this.Controls.Add(this.button_SelectLocalConfigFile);
            this.Name = "MissionOption_CheckConfigFiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public MissionOption_CheckConfigFiles() : base()
        {
            InitializeComponent();
            listBox_ConfigList.ContextMenuStrip = new ContextMenuStrip();
            ToolStripItem linkStrip = listBox_ConfigList.ContextMenuStrip.Items.Add("打开");
            ToolStripItem deleteStrip = listBox_ConfigList.ContextMenuStrip.Items.Add("删除");
            linkStrip.Click += delegate (object obj, EventArgs e)
              {
                  if (listBox_ConfigList.SelectedItem != null)
                  {
                      string selectedItem = listBox_ConfigList.SelectedItem.ToString();
                      Global.Instance.OpenURL(selectedItem);
                  }
              };
            deleteStrip.Click += delegate (object obj, EventArgs e)
              {
                  if (listBox_ConfigList.SelectedItem != null)
                  {
                      ConfigFileList.Remove(listBox_ConfigList.SelectedItem.ToString());
                  }
                  RefreshList();
              };
            listBox_ConfigList.Click += delegate (object obj, EventArgs e)
              {
                  int posY = listBox_ConfigList.PointToClient(MousePosition).Y;
                  if (posY < 0 || posY > (listBox_ConfigList.Items.Count * listBox_ConfigList.ItemHeight))
                  {
                      listBox_ConfigList.ClearSelected();
                  }
              };
        }

        private List<string> configFileList;
        /// <summary>
        /// 配置文件地址列表
        /// </summary>
        public List<string> ConfigFileList { get { configFileList = configFileList ?? new List<string>(); return configFileList; } }

        public override void CreateNewMission()
        {
            base.CreateNewMission();
            MissionPanel_CheckConfigFiles mission = Global.Instance.MainForm.CreateNewMissionPanel<MissionPanel_CheckConfigFiles>();
            mission.MissionType = "检查配置文件";
            mission.MissionBriefInformation = GetBriefInformation();
            mission.InitializeMission(checkBox_CheckResListAtSameTime.CheckState == CheckState.Checked, checkBox_CheckFullResourceList.CheckState == CheckState.Checked, ConfigFileList.ToArray());
        }

        /// <summary>
        /// 刷新地址列表
        /// </summary>
        private void RefreshList()
        {
            listBox_ConfigList.Items.Clear();
            for (int i = 0; i < ConfigFileList.Count; i++)
            {
                listBox_ConfigList.Items.Add(ConfigFileList[i]);
            }
        }

        private void button_AddRemoteURLToList_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_RemoteConfigFileURL.Text) && !ConfigFileList.Contains(textBox_RemoteConfigFileURL.Text))
            {
                ConfigFileList.Add(textBox_RemoteConfigFileURL.Text);
            }
            RefreshList();
        }

        private void button_SelectLocalConfigFile_Click(object sender, EventArgs e)
        {
            string temp = Global.Instance.SelectFileByDialog("选择本地配置文件");
            if (!string.IsNullOrEmpty(temp))
            {
                if (!ConfigFileList.Contains(temp))
                {
                    ConfigFileList.Add(temp);
                }
            }
            RefreshList();
        }

        private void button_SelectLocalConfigFolder_Click(object sender, EventArgs e)
        {
            string temp = Global.Instance.SelectFolderByDialog("选择本地配置文件夹");
            if (!string.IsNullOrEmpty(temp))
            {
                string[] paths = Directory.GetFiles(temp, "*.json", SearchOption.AllDirectories);
                for (int i = 0; i < paths.Length; i++)
                {
                    if (!ConfigFileList.Contains(paths[i]))
                    {
                        ConfigFileList.Add(paths[i]);
                    }
                }
            }
            RefreshList();
        }

        private void button_ClearConfigList_Click(object sender, EventArgs e)
        {
            listBox_ConfigList.Items.Clear();
            ConfigFileList.Clear();
        }
    }
}
