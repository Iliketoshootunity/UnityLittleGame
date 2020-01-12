namespace CheckResourceTool.IndependentPanel.SmallComponents
{
    partial class Component_NormalTextBox
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
            this.textBox_NormalTextBox = new System.Windows.Forms.TextBox();
            this.label_TextBoxName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_NormalTextBox
            // 
            this.textBox_NormalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_NormalTextBox.Location = new System.Drawing.Point(87, 1);
            this.textBox_NormalTextBox.Name = "textBox_NormalTextBox";
            this.textBox_NormalTextBox.Size = new System.Drawing.Size(166, 21);
            this.textBox_NormalTextBox.TabIndex = 0;
            // 
            // label_TextBoxName
            // 
            this.label_TextBoxName.AutoSize = true;
            this.label_TextBoxName.Location = new System.Drawing.Point(4, 4);
            this.label_TextBoxName.Name = "label_TextBoxName";
            this.label_TextBoxName.Size = new System.Drawing.Size(77, 12);
            this.label_TextBoxName.TabIndex = 1;
            this.label_TextBoxName.Text = "文本框名称一";
            // 
            // Component_NormalTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_TextBoxName);
            this.Controls.Add(this.textBox_NormalTextBox);
            this.Name = "Component_NormalTextBox";
            this.Size = new System.Drawing.Size(256, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_NormalTextBox;
        private System.Windows.Forms.Label label_TextBoxName;
    }
}
