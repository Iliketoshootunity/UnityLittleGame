using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CheckResourceTool.IndependentPanel.SmallComponents
{
    public partial class Component_Link : UserControl
    {
        /// <summary>
        /// 链接名
        /// </summary>
        public string LinkName { get { return label_Sign.Text; } set { label_Sign.Text = value; } }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get { return linkLabel_Link.Text; } set { linkLabel_Link.Text = value; } }

        public Component_Link()
        {
            InitializeComponent();
            linkLabel_Link.ContextMenuStrip = new ContextMenuStrip();
            ToolStripItem copyToolStripItem = linkLabel_Link.ContextMenuStrip.Items.Add("复制");
            copyToolStripItem.Click += CopyToolStripItem_Click;
        }

        private void CopyToolStripItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(linkLabel_Link.Text);
        }

        private void linkLabel_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (linkLabel.Text.Contains(".unitypackage") || linkLabel.Text.Contains(".exe"))
                {
                    Global.Instance.OpenURL(Path.GetDirectoryName(linkLabel.Text));
                }
                else
                {
                    Global.Instance.OpenURL(linkLabel.Text);
                }
            }
        }
    }
}
