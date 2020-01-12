namespace CheckResourceTool.IndependentPanel.SmallComponents
{
    partial class Component_URLSelect
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
            System.Windows.Forms.Label label1;
            this.comboBox_SelectURLType = new System.Windows.Forms.ComboBox();
            this.label_URLInfo = new System.Windows.Forms.Label();
            this.button_SelectLocalURL = new System.Windows.Forms.Button();
            this.textBox_URL = new System.Windows.Forms.TextBox();
            this.button_LinkURL = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(47, 12);
            label1.TabIndex = 0;
            label1.Text = "URL位置";
            // 
            // comboBox_SelectURLType
            // 
            this.comboBox_SelectURLType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SelectURLType.FormattingEnabled = true;
            this.comboBox_SelectURLType.Items.AddRange(new object[] {
            "本地位置",
            "网络位置"});
            this.comboBox_SelectURLType.Location = new System.Drawing.Point(58, 2);
            this.comboBox_SelectURLType.Name = "comboBox_SelectURLType";
            this.comboBox_SelectURLType.Size = new System.Drawing.Size(90, 20);
            this.comboBox_SelectURLType.TabIndex = 1;
            this.comboBox_SelectURLType.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectURLType_SelectedIndexChanged);
            // 
            // label_URLInfo
            // 
            this.label_URLInfo.AutoSize = true;
            this.label_URLInfo.Location = new System.Drawing.Point(155, 6);
            this.label_URLInfo.Name = "label_URLInfo";
            this.label_URLInfo.Size = new System.Drawing.Size(71, 12);
            this.label_URLInfo.TabIndex = 2;
            this.label_URLInfo.Text = "URL选择路径";
            this.label_URLInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_SelectLocalURL
            // 
            this.button_SelectLocalURL.BackColor = System.Drawing.Color.Lime;
            this.button_SelectLocalURL.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_SelectLocalURL.Location = new System.Drawing.Point(233, 5);
            this.button_SelectLocalURL.Name = "button_SelectLocalURL";
            this.button_SelectLocalURL.Size = new System.Drawing.Size(14, 14);
            this.button_SelectLocalURL.TabIndex = 3;
            this.button_SelectLocalURL.UseVisualStyleBackColor = false;
            this.button_SelectLocalURL.Click += new System.EventHandler(this.button_SelectLocalURL_Click);
            // 
            // textBox_URL
            // 
            this.textBox_URL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_URL.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_URL.Location = new System.Drawing.Point(253, 1);
            this.textBox_URL.Name = "textBox_URL";
            this.textBox_URL.Size = new System.Drawing.Size(448, 23);
            this.textBox_URL.TabIndex = 4;
            // 
            // button_LinkURL
            // 
            this.button_LinkURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LinkURL.Location = new System.Drawing.Point(707, 2);
            this.button_LinkURL.Name = "button_LinkURL";
            this.button_LinkURL.Size = new System.Drawing.Size(40, 20);
            this.button_LinkURL.TabIndex = 5;
            this.button_LinkURL.Text = "Link";
            this.button_LinkURL.UseVisualStyleBackColor = true;
            this.button_LinkURL.Click += new System.EventHandler(this.button_LinkURL_Click);
            // 
            // Component_URLSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_LinkURL);
            this.Controls.Add(this.textBox_URL);
            this.Controls.Add(this.button_SelectLocalURL);
            this.Controls.Add(this.label_URLInfo);
            this.Controls.Add(this.comboBox_SelectURLType);
            this.Controls.Add(label1);
            this.Name = "Component_URLSelect";
            this.Size = new System.Drawing.Size(750, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_SelectURLType;
        private System.Windows.Forms.Button button_SelectLocalURL;
        private System.Windows.Forms.TextBox textBox_URL;
        private System.Windows.Forms.Button button_LinkURL;
        private System.Windows.Forms.Label label_URLInfo;
    }
}
