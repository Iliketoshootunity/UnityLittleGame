namespace CheckResourceTool.IndependentPanel.SmallComponents
{
    partial class Component_NormalInfoPair
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
            this.label_InfoName = new System.Windows.Forms.Label();
            this.textBox_InfoContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_InfoName
            // 
            this.label_InfoName.AutoSize = true;
            this.label_InfoName.Location = new System.Drawing.Point(10, 8);
            this.label_InfoName.Name = "label_InfoName";
            this.label_InfoName.Size = new System.Drawing.Size(53, 12);
            this.label_InfoName.TabIndex = 0;
            this.label_InfoName.Text = "信息名称";
            // 
            // textBox_InfoContent
            // 
            this.textBox_InfoContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_InfoContent.Location = new System.Drawing.Point(116, 2);
            this.textBox_InfoContent.Name = "textBox_InfoContent";
            this.textBox_InfoContent.ReadOnly = true;
            this.textBox_InfoContent.Size = new System.Drawing.Size(977, 21);
            this.textBox_InfoContent.TabIndex = 2;
            // 
            // Component_NormalInfoPair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox_InfoContent);
            this.Controls.Add(this.label_InfoName);
            this.Name = "Component_NormalInfoPair";
            this.Size = new System.Drawing.Size(1096, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_InfoName;
        private System.Windows.Forms.TextBox textBox_InfoContent;
    }
}
