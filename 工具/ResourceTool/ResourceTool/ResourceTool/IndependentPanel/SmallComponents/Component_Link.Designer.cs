namespace CheckResourceTool.IndependentPanel.SmallComponents
{
    partial class Component_Link
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
            this.label_Sign = new System.Windows.Forms.Label();
            this.linkLabel_Link = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label_Sign
            // 
            this.label_Sign.AutoSize = true;
            this.label_Sign.Location = new System.Drawing.Point(10, 8);
            this.label_Sign.Name = "label_Sign";
            this.label_Sign.Size = new System.Drawing.Size(53, 12);
            this.label_Sign.TabIndex = 0;
            this.label_Sign.Text = "链接标识";
            // 
            // linkLabel_Link
            // 
            this.linkLabel_Link.AutoSize = true;
            this.linkLabel_Link.Location = new System.Drawing.Point(120, 8);
            this.linkLabel_Link.Name = "linkLabel_Link";
            this.linkLabel_Link.Size = new System.Drawing.Size(29, 12);
            this.linkLabel_Link.TabIndex = 1;
            this.linkLabel_Link.TabStop = true;
            this.linkLabel_Link.Text = "链接";
            this.linkLabel_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Link_LinkClicked);
            // 
            // Component_Link
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel_Link);
            this.Controls.Add(this.label_Sign);
            this.Name = "Component_Link";
            this.Size = new System.Drawing.Size(1096, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Sign;
        private System.Windows.Forms.LinkLabel linkLabel_Link;
    }
}
