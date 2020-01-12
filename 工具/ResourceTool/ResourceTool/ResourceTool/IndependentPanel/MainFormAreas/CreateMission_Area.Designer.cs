namespace CheckResourceTool.IndependentPanel.MainFormAreas
{
    partial class CreateMission_Area
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_MissionType = new System.Windows.Forms.ComboBox();
            this.button_CreateNewMission = new System.Windows.Forms.Button();
            this.panel_MissionOptionSetting = new System.Windows.Forms.Panel();
            this.button_OpenTempDirectory = new System.Windows.Forms.Button();
            this.button_OpenFixedDirectory = new System.Windows.Forms.Button();
            this.button_StartAllMissions = new System.Windows.Forms.Button();
            this.button_PauseAllMissions = new System.Windows.Forms.Button();
            this.button_FinishAllMissions = new System.Windows.Forms.Button();
            this.button_CloseAllMissions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_MissionType
            // 
            this.comboBox_MissionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_MissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MissionType.FormattingEnabled = true;
            this.comboBox_MissionType.Location = new System.Drawing.Point(1645, 10);
            this.comboBox_MissionType.Name = "comboBox_MissionType";
            this.comboBox_MissionType.Size = new System.Drawing.Size(100, 20);
            this.comboBox_MissionType.TabIndex = 0;
            this.comboBox_MissionType.SelectedIndexChanged += new System.EventHandler(this.comboBox_MissionType_SelectedIndexChanged);
            // 
            // button_CreateNewMission
            // 
            this.button_CreateNewMission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CreateNewMission.Location = new System.Drawing.Point(1645, 35);
            this.button_CreateNewMission.Name = "button_CreateNewMission";
            this.button_CreateNewMission.Size = new System.Drawing.Size(100, 20);
            this.button_CreateNewMission.TabIndex = 1;
            this.button_CreateNewMission.Text = "新建任务";
            this.button_CreateNewMission.UseVisualStyleBackColor = true;
            this.button_CreateNewMission.Click += new System.EventHandler(this.button_CreateNewMission_Click);
            // 
            // panel_MissionOptionSetting
            // 
            this.panel_MissionOptionSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_MissionOptionSetting.AutoScroll = true;
            this.panel_MissionOptionSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MissionOptionSetting.Location = new System.Drawing.Point(3, 3);
            this.panel_MissionOptionSetting.Name = "panel_MissionOptionSetting";
            this.panel_MissionOptionSetting.Size = new System.Drawing.Size(1636, 381);
            this.panel_MissionOptionSetting.TabIndex = 4;
            // 
            // button_OpenTempDirectory
            // 
            this.button_OpenTempDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OpenTempDirectory.Location = new System.Drawing.Point(1645, 85);
            this.button_OpenTempDirectory.Name = "button_OpenTempDirectory";
            this.button_OpenTempDirectory.Size = new System.Drawing.Size(100, 20);
            this.button_OpenTempDirectory.TabIndex = 5;
            this.button_OpenTempDirectory.Text = "打开临时目录";
            this.button_OpenTempDirectory.UseVisualStyleBackColor = true;
            this.button_OpenTempDirectory.Click += new System.EventHandler(this.button_OpenTempDirectory_Click);
            // 
            // button_OpenFixedDirectory
            // 
            this.button_OpenFixedDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OpenFixedDirectory.Location = new System.Drawing.Point(1646, 60);
            this.button_OpenFixedDirectory.Name = "button_OpenFixedDirectory";
            this.button_OpenFixedDirectory.Size = new System.Drawing.Size(100, 20);
            this.button_OpenFixedDirectory.TabIndex = 7;
            this.button_OpenFixedDirectory.Text = "打开永久目录";
            this.button_OpenFixedDirectory.UseVisualStyleBackColor = true;
            this.button_OpenFixedDirectory.Click += new System.EventHandler(this.button_OpenFixedDirectory_Click);
            // 
            // button_StartAllMissions
            // 
            this.button_StartAllMissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_StartAllMissions.Location = new System.Drawing.Point(1646, 110);
            this.button_StartAllMissions.Name = "button_StartAllMissions";
            this.button_StartAllMissions.Size = new System.Drawing.Size(100, 20);
            this.button_StartAllMissions.TabIndex = 8;
            this.button_StartAllMissions.Text = "开始所有任务";
            this.button_StartAllMissions.UseVisualStyleBackColor = true;
            this.button_StartAllMissions.Click += new System.EventHandler(this.button_StartAllMissions_Click);
            // 
            // button_PauseAllMissions
            // 
            this.button_PauseAllMissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_PauseAllMissions.Location = new System.Drawing.Point(1646, 135);
            this.button_PauseAllMissions.Name = "button_PauseAllMissions";
            this.button_PauseAllMissions.Size = new System.Drawing.Size(100, 20);
            this.button_PauseAllMissions.TabIndex = 9;
            this.button_PauseAllMissions.Text = "暂停所有任务";
            this.button_PauseAllMissions.UseVisualStyleBackColor = true;
            this.button_PauseAllMissions.Click += new System.EventHandler(this.button_PauseAllMissions_Click);
            // 
            // button_FinishAllMissions
            // 
            this.button_FinishAllMissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FinishAllMissions.Location = new System.Drawing.Point(1646, 160);
            this.button_FinishAllMissions.Name = "button_FinishAllMissions";
            this.button_FinishAllMissions.Size = new System.Drawing.Size(100, 20);
            this.button_FinishAllMissions.TabIndex = 10;
            this.button_FinishAllMissions.Text = "结束所有任务";
            this.button_FinishAllMissions.UseVisualStyleBackColor = true;
            this.button_FinishAllMissions.Click += new System.EventHandler(this.button_FinishAllMissions_Click);
            // 
            // button_CloseAllMissions
            // 
            this.button_CloseAllMissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CloseAllMissions.Location = new System.Drawing.Point(1646, 185);
            this.button_CloseAllMissions.Name = "button_CloseAllMissions";
            this.button_CloseAllMissions.Size = new System.Drawing.Size(100, 20);
            this.button_CloseAllMissions.TabIndex = 11;
            this.button_CloseAllMissions.Text = "关闭所有任务";
            this.button_CloseAllMissions.UseVisualStyleBackColor = true;
            this.button_CloseAllMissions.Click += new System.EventHandler(this.button_CloseAllMissions_Click);
            // 
            // CreateMission_Area
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_CloseAllMissions);
            this.Controls.Add(this.button_FinishAllMissions);
            this.Controls.Add(this.button_PauseAllMissions);
            this.Controls.Add(this.button_StartAllMissions);
            this.Controls.Add(this.button_OpenFixedDirectory);
            this.Controls.Add(this.button_OpenTempDirectory);
            this.Controls.Add(this.panel_MissionOptionSetting);
            this.Controls.Add(this.button_CreateNewMission);
            this.Controls.Add(this.comboBox_MissionType);
            this.Name = "CreateMission_Area";
            this.Size = new System.Drawing.Size(1760, 388);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_CreateNewMission;
        private System.Windows.Forms.ComboBox comboBox_MissionType;
        private System.Windows.Forms.Panel panel_MissionOptionSetting;
        private System.Windows.Forms.Button button_OpenTempDirectory;
        private System.Windows.Forms.Button button_OpenFixedDirectory;
        private System.Windows.Forms.Button button_StartAllMissions;
        private System.Windows.Forms.Button button_PauseAllMissions;
        private System.Windows.Forms.Button button_FinishAllMissions;
        private System.Windows.Forms.Button button_CloseAllMissions;
    }
}
