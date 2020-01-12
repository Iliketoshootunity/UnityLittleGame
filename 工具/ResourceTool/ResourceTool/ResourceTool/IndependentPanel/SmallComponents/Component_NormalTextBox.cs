using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckResourceTool.IndependentPanel.SmallComponents
{
    public partial class Component_NormalTextBox : UserControl
    {
        public string TextBoxName
        {
            get { return label_TextBoxName.Text; }
            set { label_TextBoxName.Text = value; }
        }

        public string TextBoxContent
        {
            get { return textBox_NormalTextBox.Text; }
            set { textBox_NormalTextBox.Text = value; }
        }

        public Component_NormalTextBox()
        {
            InitializeComponent();
        }
    }
}
