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
    public partial class Component_Info : UserControl
    {
        public string Information { get { return label_Info.Text; } set { label_Info.Text = value; } }

        public Component_Info()
        {
            InitializeComponent();
        }
    }
}
