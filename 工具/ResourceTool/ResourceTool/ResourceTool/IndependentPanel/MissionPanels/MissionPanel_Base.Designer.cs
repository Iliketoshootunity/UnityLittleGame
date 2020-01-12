namespace CheckResourceTool.IndependentPanel.MissionPanels
{
    partial class MissionPanel_Base
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
            this.label_MissionType = new System.Windows.Forms.Label();
            this.button_FinishMission = new System.Windows.Forms.Button();
            this.progressBar_MissionPercentage = new System.Windows.Forms.ProgressBar();
            this.label_ProgressPercentage = new System.Windows.Forms.Label();
            this.button_StartMission = new System.Windows.Forms.Button();
            this.label_MissionBriefInformation = new System.Windows.Forms.Label();
            this.button_LogOutput = new System.Windows.Forms.Button();
            this.label_MissionOrder = new System.Windows.Forms.Label();
            this.button_PauseMission = new System.Windows.Forms.Button();
            this.button_CloseMission = new System.Windows.Forms.Button();
            this.tabPage_Info = new System.Windows.Forms.TabPage();
            this.tabControl_MissionInfo = new System.Windows.Forms.TabControl();
            this.tabControl_OperationLog = new System.Windows.Forms.TabPage();
            this.textBox_OperationLog = new System.Windows.Forms.TextBox();
            this.button_ClearLog = new System.Windows.Forms.Button();
            this.button_ExportLog = new System.Windows.Forms.Button();
            this.tabControl_MissionInfo.SuspendLayout();
            this.tabControl_OperationLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_MissionType
            // 
            this.label_MissionType.AutoSize = true;
            this.label_MissionType.Location = new System.Drawing.Point(14, 10);
            this.label_MissionType.Name = "label_MissionType";
            this.label_MissionType.Size = new System.Drawing.Size(53, 12);
            this.label_MissionType.TabIndex = 0;
            this.label_MissionType.Text = "任务类型";
            // 
            // button_FinishMission
            // 
            this.button_FinishMission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FinishMission.Enabled = false;
            this.button_FinishMission.Location = new System.Drawing.Point(1122, 374);
            this.button_FinishMission.Name = "button_FinishMission";
            this.button_FinishMission.Size = new System.Drawing.Size(75, 23);
            this.button_FinishMission.TabIndex = 1;
            this.button_FinishMission.Text = "结束";
            this.button_FinishMission.UseVisualStyleBackColor = true;
            this.button_FinishMission.Click += new System.EventHandler(this.button_FinishMission_Click);
            // 
            // progressBar_MissionPercentage
            // 
            this.progressBar_MissionPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_MissionPercentage.Location = new System.Drawing.Point(8, 443);
            this.progressBar_MissionPercentage.Name = "progressBar_MissionPercentage";
            this.progressBar_MissionPercentage.Size = new System.Drawing.Size(1104, 16);
            this.progressBar_MissionPercentage.TabIndex = 2;
            // 
            // label_ProgressPercentage
            // 
            this.label_ProgressPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ProgressPercentage.AutoSize = true;
            this.label_ProgressPercentage.Location = new System.Drawing.Point(1126, 445);
            this.label_ProgressPercentage.Name = "label_ProgressPercentage";
            this.label_ProgressPercentage.Size = new System.Drawing.Size(65, 12);
            this.label_ProgressPercentage.TabIndex = 3;
            this.label_ProgressPercentage.Text = "进度百分比";
            // 
            // button_StartMission
            // 
            this.button_StartMission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_StartMission.Location = new System.Drawing.Point(1122, 405);
            this.button_StartMission.Name = "button_StartMission";
            this.button_StartMission.Size = new System.Drawing.Size(75, 23);
            this.button_StartMission.TabIndex = 4;
            this.button_StartMission.Text = "开始";
            this.button_StartMission.UseVisualStyleBackColor = true;
            this.button_StartMission.Click += new System.EventHandler(this.button_StartMission_Click);
            // 
            // label_MissionBriefInformation
            // 
            this.label_MissionBriefInformation.AutoSize = true;
            this.label_MissionBriefInformation.Location = new System.Drawing.Point(144, 10);
            this.label_MissionBriefInformation.Name = "label_MissionBriefInformation";
            this.label_MissionBriefInformation.Size = new System.Drawing.Size(77, 12);
            this.label_MissionBriefInformation.TabIndex = 6;
            this.label_MissionBriefInformation.Text = "任务简略信息";
            // 
            // button_LogOutput
            // 
            this.button_LogOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LogOutput.Location = new System.Drawing.Point(1146, 919);
            this.button_LogOutput.Name = "button_LogOutput";
            this.button_LogOutput.Size = new System.Drawing.Size(75, 23);
            this.button_LogOutput.TabIndex = 8;
            this.button_LogOutput.Text = "导出日志";
            this.button_LogOutput.UseVisualStyleBackColor = true;
            this.button_LogOutput.Click += new System.EventHandler(this.button_LogOutput_Click);
            // 
            // label_MissionOrder
            // 
            this.label_MissionOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_MissionOrder.AutoSize = true;
            this.label_MissionOrder.Location = new System.Drawing.Point(1063, 10);
            this.label_MissionOrder.Name = "label_MissionOrder";
            this.label_MissionOrder.Size = new System.Drawing.Size(53, 12);
            this.label_MissionOrder.TabIndex = 9;
            this.label_MissionOrder.Text = "任务编号";
            // 
            // button_PauseMission
            // 
            this.button_PauseMission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_PauseMission.Enabled = false;
            this.button_PauseMission.Location = new System.Drawing.Point(1122, 343);
            this.button_PauseMission.Name = "button_PauseMission";
            this.button_PauseMission.Size = new System.Drawing.Size(75, 23);
            this.button_PauseMission.TabIndex = 10;
            this.button_PauseMission.Text = "暂停";
            this.button_PauseMission.UseVisualStyleBackColor = true;
            this.button_PauseMission.Click += new System.EventHandler(this.button_PauseMission_Click);
            // 
            // button_CloseMission
            // 
            this.button_CloseMission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CloseMission.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_CloseMission.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_CloseMission.Image = global::CheckResourceTool.Properties.Resources.X;
            this.button_CloseMission.Location = new System.Drawing.Point(1177, 3);
            this.button_CloseMission.Name = "button_CloseMission";
            this.button_CloseMission.Size = new System.Drawing.Size(20, 20);
            this.button_CloseMission.TabIndex = 11;
            this.button_CloseMission.UseVisualStyleBackColor = true;
            this.button_CloseMission.Click += new System.EventHandler(this.button_CloseMission_Click);
            // 
            // tabPage_Info
            // 
            this.tabPage_Info.AutoScroll = true;
            this.tabPage_Info.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Info.Name = "tabPage_Info";
            this.tabPage_Info.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Info.Size = new System.Drawing.Size(776, 372);
            this.tabPage_Info.TabIndex = 1;
            this.tabPage_Info.Text = "详情";
            this.tabPage_Info.UseVisualStyleBackColor = true;
            // 
            // tabControl_MissionInfo
            // 
            this.tabControl_MissionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_MissionInfo.Controls.Add(this.tabPage_Info);
            this.tabControl_MissionInfo.Controls.Add(this.tabControl_OperationLog);
            this.tabControl_MissionInfo.Location = new System.Drawing.Point(12, 39);
            this.tabControl_MissionInfo.Name = "tabControl_MissionInfo";
            this.tabControl_MissionInfo.SelectedIndex = 0;
            this.tabControl_MissionInfo.Size = new System.Drawing.Size(1104, 398);
            this.tabControl_MissionInfo.TabIndex = 7;
            // 
            // tabControl_OperationLog
            // 
            this.tabControl_OperationLog.Controls.Add(this.textBox_OperationLog);
            this.tabControl_OperationLog.Location = new System.Drawing.Point(4, 22);
            this.tabControl_OperationLog.Name = "tabControl_OperationLog";
            this.tabControl_OperationLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabControl_OperationLog.Size = new System.Drawing.Size(1096, 372);
            this.tabControl_OperationLog.TabIndex = 2;
            this.tabControl_OperationLog.Text = "记录";
            this.tabControl_OperationLog.UseVisualStyleBackColor = true;
            // 
            // textBox_OperationLog
            // 
            this.textBox_OperationLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_OperationLog.Location = new System.Drawing.Point(0, 0);
            this.textBox_OperationLog.MaxLength = 3276700;
            this.textBox_OperationLog.Multiline = true;
            this.textBox_OperationLog.Name = "textBox_OperationLog";
            this.textBox_OperationLog.ReadOnly = true;
            this.textBox_OperationLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_OperationLog.Size = new System.Drawing.Size(1096, 372);
            this.textBox_OperationLog.TabIndex = 0;
            // 
            // button_ClearLog
            // 
            this.button_ClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ClearLog.Location = new System.Drawing.Point(1122, 90);
            this.button_ClearLog.Name = "button_ClearLog";
            this.button_ClearLog.Size = new System.Drawing.Size(75, 23);
            this.button_ClearLog.TabIndex = 12;
            this.button_ClearLog.Text = "清空记录";
            this.button_ClearLog.UseVisualStyleBackColor = true;
            this.button_ClearLog.Click += new System.EventHandler(this.button_ClearLog_Click);
            // 
            // button_ExportLog
            // 
            this.button_ExportLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ExportLog.Location = new System.Drawing.Point(1122, 61);
            this.button_ExportLog.Name = "button_ExportLog";
            this.button_ExportLog.Size = new System.Drawing.Size(75, 23);
            this.button_ExportLog.TabIndex = 13;
            this.button_ExportLog.Text = "导出记录";
            this.button_ExportLog.UseVisualStyleBackColor = true;
            this.button_ExportLog.Click += new System.EventHandler(this.button_ExportLog_Click);
            // 
            // MissionPanel_Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button_ExportLog);
            this.Controls.Add(this.button_ClearLog);
            this.Controls.Add(this.button_CloseMission);
            this.Controls.Add(this.button_PauseMission);
            this.Controls.Add(this.label_MissionOrder);
            this.Controls.Add(this.button_LogOutput);
            this.Controls.Add(this.tabControl_MissionInfo);
            this.Controls.Add(this.label_MissionBriefInformation);
            this.Controls.Add(this.button_StartMission);
            this.Controls.Add(this.label_ProgressPercentage);
            this.Controls.Add(this.progressBar_MissionPercentage);
            this.Controls.Add(this.button_FinishMission);
            this.Controls.Add(this.label_MissionType);
            this.Name = "MissionPanel_Base";
            this.Size = new System.Drawing.Size(1200, 471);
            this.tabControl_MissionInfo.ResumeLayout(false);
            this.tabControl_OperationLog.ResumeLayout(false);
            this.tabControl_OperationLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_FinishMission;
        private System.Windows.Forms.ProgressBar progressBar_MissionPercentage;
        private System.Windows.Forms.Label label_ProgressPercentage;
        private System.Windows.Forms.Button button_StartMission;
        private System.Windows.Forms.Label label_MissionBriefInformation;
        private System.Windows.Forms.Button button_LogOutput;
        private System.Windows.Forms.Label label_MissionType;
        private System.Windows.Forms.Label label_MissionOrder;
        private System.Windows.Forms.Button button_PauseMission;
        private System.Windows.Forms.Button button_CloseMission;
        protected System.Windows.Forms.TabPage tabPage_Info;
        protected System.Windows.Forms.TabControl tabControl_MissionInfo;
        private System.Windows.Forms.TabPage tabControl_OperationLog;
        private System.Windows.Forms.TextBox textBox_OperationLog;
        private System.Windows.Forms.Button button_ClearLog;
        private System.Windows.Forms.Button button_ExportLog;
    }
}
