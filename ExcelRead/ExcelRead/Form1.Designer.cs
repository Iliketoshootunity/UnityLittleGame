namespace ExcelRead
{
    public partial class Form1
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
            this.pathGroupBox = new System.Windows.Forms.GroupBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.select_Excel_Button = new System.Windows.Forms.Button();
            this.create_Data_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.testTextBox = new System.Windows.Forms.TextBox();
            this.testReadIput = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pathGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathGroupBox
            // 
            this.pathGroupBox.Controls.Add(this.pathTextBox);
            this.pathGroupBox.Location = new System.Drawing.Point(12, 12);
            this.pathGroupBox.Name = "pathGroupBox";
            this.pathGroupBox.Size = new System.Drawing.Size(776, 175);
            this.pathGroupBox.TabIndex = 0;
            this.pathGroupBox.TabStop = false;
            this.pathGroupBox.Text = "Excel路径";
            this.pathGroupBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(18, 29);
            this.pathTextBox.Multiline = true;
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(752, 115);
            this.pathTextBox.TabIndex = 0;
            // 
            // select_Excel_Button
            // 
            this.select_Excel_Button.Location = new System.Drawing.Point(12, 201);
            this.select_Excel_Button.Name = "select_Excel_Button";
            this.select_Excel_Button.Size = new System.Drawing.Size(75, 23);
            this.select_Excel_Button.TabIndex = 1;
            this.select_Excel_Button.Text = "选择Excel";
            this.select_Excel_Button.UseVisualStyleBackColor = true;
            this.select_Excel_Button.Click += new System.EventHandler(this.select_Excel_Button_Click);
            // 
            // create_Data_button
            // 
            this.create_Data_button.Location = new System.Drawing.Point(113, 202);
            this.create_Data_button.Name = "create_Data_button";
            this.create_Data_button.Size = new System.Drawing.Size(102, 22);
            this.create_Data_button.TabIndex = 2;
            this.create_Data_button.Text = "生成DataTable";
            this.create_Data_button.UseVisualStyleBackColor = true;
            this.create_Data_button.Click += new System.EventHandler(this.create_Data_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel表格|*.xls;*.xlsx";
            this.openFileDialog1.Multiselect = true;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "加密文件|*.data";
            // 
            // textBox1
            // 
            this.testTextBox.Location = new System.Drawing.Point(12, 256);
            this.testTextBox.Multiline = true;
            this.testTextBox.Name = "textBox1";
            this.testTextBox.Size = new System.Drawing.Size(770, 127);
            this.testTextBox.TabIndex = 3;
            // 
            // testReadIput
            // 
            this.testReadIput.Location = new System.Drawing.Point(13, 402);
            this.testReadIput.Name = "testReadIput";
            this.testReadIput.Size = new System.Drawing.Size(75, 23);
            this.testReadIput.TabIndex = 4;
            this.testReadIput.Text = "测试读取";
            this.testReadIput.UseVisualStyleBackColor = true;
            this.testReadIput.Click += new System.EventHandler(this.testReadIput_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.testReadIput);
            this.Controls.Add(this.testTextBox);
            this.Controls.Add(this.create_Data_button);
            this.Controls.Add(this.select_Excel_Button);
            this.Controls.Add(this.pathGroupBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pathGroupBox.ResumeLayout(false);
            this.pathGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox pathGroupBox;
        private System.Windows.Forms.Button select_Excel_Button;
        private System.Windows.Forms.Button create_Data_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.TextBox testTextBox;
        private System.Windows.Forms.Button testReadIput;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

