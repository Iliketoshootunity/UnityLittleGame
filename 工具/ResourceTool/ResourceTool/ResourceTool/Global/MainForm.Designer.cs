namespace CheckResourceTool
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_CreateMission = new System.Windows.Forms.Panel();
            this.panel_Mission = new System.Windows.Forms.Panel();
            this.flowLayoutPanel_MissionList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_Log = new System.Windows.Forms.Panel();
            this.button_ClearGlobalLog = new System.Windows.Forms.Button();
            this.textBox_GlobalLog = new System.Windows.Forms.TextBox();
            this.panel_Mission.SuspendLayout();
            this.panel_Log.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_CreateMission
            // 
            this.panel_CreateMission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_CreateMission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CreateMission.Location = new System.Drawing.Point(12, 12);
            this.panel_CreateMission.Name = "panel_CreateMission";
            this.panel_CreateMission.Size = new System.Drawing.Size(1760, 388);
            this.panel_CreateMission.TabIndex = 0;
            // 
            // panel_Mission
            // 
            this.panel_Mission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Mission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Mission.Controls.Add(this.flowLayoutPanel_MissionList);
            this.panel_Mission.Location = new System.Drawing.Point(12, 406);
            this.panel_Mission.Name = "panel_Mission";
            this.panel_Mission.Size = new System.Drawing.Size(1760, 427);
            this.panel_Mission.TabIndex = 1;
            // 
            // flowLayoutPanel_MissionList
            // 
            this.flowLayoutPanel_MissionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel_MissionList.AutoScroll = true;
            this.flowLayoutPanel_MissionList.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_MissionList.Name = "flowLayoutPanel_MissionList";
            this.flowLayoutPanel_MissionList.Size = new System.Drawing.Size(1752, 419);
            this.flowLayoutPanel_MissionList.TabIndex = 0;
            // 
            // panel_Log
            // 
            this.panel_Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Log.Controls.Add(this.button_ClearGlobalLog);
            this.panel_Log.Controls.Add(this.textBox_GlobalLog);
            this.panel_Log.Location = new System.Drawing.Point(12, 839);
            this.panel_Log.Name = "panel_Log";
            this.panel_Log.Size = new System.Drawing.Size(1760, 161);
            this.panel_Log.TabIndex = 2;
            // 
            // button_ClearGlobalLog
            // 
            this.button_ClearGlobalLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ClearGlobalLog.Location = new System.Drawing.Point(1680, 133);
            this.button_ClearGlobalLog.Name = "button_ClearGlobalLog";
            this.button_ClearGlobalLog.Size = new System.Drawing.Size(75, 23);
            this.button_ClearGlobalLog.TabIndex = 1;
            this.button_ClearGlobalLog.Text = "清空";
            this.button_ClearGlobalLog.UseVisualStyleBackColor = true;
            this.button_ClearGlobalLog.Click += new System.EventHandler(this.button_ClearGlobalLog_Click);
            // 
            // textBox_GlobalLog
            // 
            this.textBox_GlobalLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_GlobalLog.Location = new System.Drawing.Point(4, 4);
            this.textBox_GlobalLog.MaxLength = 327670;
            this.textBox_GlobalLog.Multiline = true;
            this.textBox_GlobalLog.Name = "textBox_GlobalLog";
            this.textBox_GlobalLog.ReadOnly = true;
            this.textBox_GlobalLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_GlobalLog.Size = new System.Drawing.Size(1659, 152);
            this.textBox_GlobalLog.TabIndex = 0;
            this.textBox_GlobalLog.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1784, 1012);
            this.Controls.Add(this.panel_Log);
            this.Controls.Add(this.panel_Mission);
            this.Controls.Add(this.panel_CreateMission);
            this.MinimumSize = new System.Drawing.Size(1800, 1050);
            this.Name = "MainForm";
            this.Text = "资源工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel_Mission.ResumeLayout(false);
            this.panel_Log.ResumeLayout(false);
            this.panel_Log.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_CreateMission;
        private System.Windows.Forms.Panel panel_Mission;
        private System.Windows.Forms.Panel panel_Log;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_MissionList;
        private System.Windows.Forms.TextBox textBox_GlobalLog;
        private System.Windows.Forms.Button button_ClearGlobalLog;
    }
}

