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
    public partial class Component_NormalInfoPair : UserControl
    {
        /// <summary>
        /// 信息名称
        /// </summary>
        public string InfoName { get { return label_InfoName.Text; } set { label_InfoName.Text = value; } }

        /// <summary>
        /// 信息内容
        /// </summary>
        public string InfoContent { get { return textBox_InfoContent.Text; } set { textBox_InfoContent.Text = value; } }

        public Component_NormalInfoPair()
        {
            InitializeComponent();
        }
    }
}
