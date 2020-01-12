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
    public partial class Component_URLSelect : UserControl
    {
        public URLType URLType
        {
            get { return urlType; }
            set
            {
                urlType = value;
                if (value == URLType.SavingFileURL || value == URLType.SavingFolderURL)
                {
                    comboBox_SelectURLType.Text = "本地位置"; comboBox_SelectURLType.Enabled = false;
                }
                else if (value == URLType.RemoteFileURL || value == URLType.RemoteFolderURL)
                {
                    comboBox_SelectURLType.Text = "网络位置"; comboBox_SelectURLType.Enabled = false;
                }
                else if (value == URLType.LocalFileURL || value == URLType.LocalFolderURL)
                {
                    comboBox_SelectURLType.Text = "本地位置"; comboBox_SelectURLType.Enabled = false;
                }
                else
                {
                    comboBox_SelectURLType.Enabled = true;
                }
            }
        }
        protected URLType urlType;

        public string URLInfo { get { return label_URLInfo.Text; } set { label_URLInfo.Text = value; } }

        public string URL { get { return textBox_URL.Text; } set { textBox_URL.Text = value; } }

        public bool IsLocal { get { return comboBox_SelectURLType.Text == "本地位置"; } }

        private string defaultPath = string.Empty;
        public string DefaultPath { get { return defaultPath; } set { defaultPath = value; } }

        public Component_URLSelect()
        {
            InitializeComponent();
            comboBox_SelectURLType.SelectedIndex = 0;
        }

        private void button_SelectLocalURL_Click(object sender, EventArgs e)
        {
            string urlTemp = null;
            switch (urlType)
            {
                case URLType.FileURL://文件路径
                    urlTemp = Global.Instance.SelectFileByDialog(string.Format("选择{0}", label_URLInfo.Text));
                    break;
                case URLType.FolderURL://文件夹路径
                    if (string.IsNullOrEmpty(DefaultPath))
                    {
                        urlTemp = Global.Instance.SelectFolderByDialog(string.Format("选择{0}", label_URLInfo.Text));
                    }
                    else
                    {
                        urlTemp = Global.Instance.SelectFolderByDialog(string.Format("选择{0}", label_URLInfo.Text), DefaultPath);
                    }
                    break;
                case URLType.SavingFileURL://保存文件路径
                    urlTemp = Global.Instance.SelectSavePathByDialog(string.Format("选择{0}", label_URLInfo.Text), "");
                    break;
                case URLType.SavingFolderURL://保存文件夹路径
                    if (string.IsNullOrEmpty(DefaultPath))
                    {
                        urlTemp = Global.Instance.SelectFolderByDialog(string.Format("选择{0}", label_URLInfo.Text));
                    }
                    else
                    {
                        urlTemp = Global.Instance.SelectFolderByDialog(string.Format("选择{0}", label_URLInfo.Text), DefaultPath);
                    }
                    break;
                case URLType.LocalFileURL://本地文件路径
                    urlTemp = Global.Instance.SelectFileByDialog(string.Format("选择{0}", label_URLInfo.Text));
                    break;
                case URLType.LocalFolderURL://本地文件夹路径
                    if (string.IsNullOrEmpty(DefaultPath))
                    {
                        urlTemp = Global.Instance.SelectFolderByDialog(string.Format("选择{0}", label_URLInfo.Text));
                    }
                    else
                    {
                        urlTemp = Global.Instance.SelectFolderByDialog(string.Format("选择{0}", label_URLInfo.Text), DefaultPath);
                    }
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(urlTemp))
            {
                textBox_URL.Text = urlTemp;
            }
        }

        private void comboBox_SelectURLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Text == "本地位置")
            {
                button_SelectLocalURL.Enabled = true;
                button_SelectLocalURL.BackColor = Color.Lime;
            }
            else if (cb.Text == "网络位置")
            {
                button_SelectLocalURL.Enabled = false;
                button_SelectLocalURL.BackColor = Color.Black;
            }
        }

        private void button_LinkURL_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_URL.Text))
            {
                Global.Instance.OpenURL(textBox_URL.Text);
            }
        }
    }
}
