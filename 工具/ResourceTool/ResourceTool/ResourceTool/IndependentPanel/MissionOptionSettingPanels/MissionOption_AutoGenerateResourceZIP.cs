using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckResourceTool.IndependentPanel.MissionPanels;

namespace CheckResourceTool.IndependentPanel.MissionOptionSettingPanels
{
    class MissionOption_AutoGenerateResourceZIP : MissionOption_Base
    {
        #region 控件
        private System.Windows.Forms.Button button_EditorPrefab;
        private System.Windows.Forms.Button button_SaveAsPrefab;
        private SmallComponents.Component_URLSelect component_URLSelect_ExportDirectory;
        private System.Windows.Forms.CheckBox checkBox_ExportResListInApk;
        private System.Windows.Forms.CheckBox checkBox_ExportCompleteList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PackageNameLetters;
        private System.Windows.Forms.CheckBox checkBox_EncryptByMD5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_SingleRubbishSize;
        private System.Windows.Forms.ComboBox comboBox_UnityType;
        private System.Windows.Forms.Button button_SelcectAdditionalUnityPackage;
        private System.Windows.Forms.TextBox textBox_AdditionalUnityPackage;
        private SmallComponents.Component_NormalTextBox component_NormalTextBox_PrefabName;
        private System.Windows.Forms.ComboBox comboBox_Platform;
        private SmallComponents.Component_NormalTextBox component_NormalTextBox_Channel;
        private SmallComponents.Component_NormalTextBox component_NormalTextBox_PostFix;
        private SmallComponents.Component_URLSelect component_URLSelect_AdditionalResDirectory;
        private SmallComponents.Component_URLSelect component_URLSelect_ResourceOrderPath;
        private SmallComponents.Component_URLSelect component_URLSelect_SpecialResZipListPath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SmallComponents.Component_URLSelect component_URLSelect_FirstZipListPath;
        private SmallComponents.Component_URLSelect component_URLSelect_AssetDirectory;
        private SmallComponents.Component_URLSelect component_URLSelect_ResListInApkPath;
        private Button button_RefreshPrefabs;
        private TextBox textBox_ZIPPassword;
        private SmallComponents.Component_URLSelect component_URLSelect_IndependentListPath;
        private SmallComponents.Component_URLSelect component_URLSelect_PredownloadListPath;
        private CheckBox checkBox_ExportZips;
        private Button button_ChangeUnityEXEPath;
        private Button button_ChangeProjectPath;
        private ComboBox comboBox_ProjectName;
        private TextBox textBox_ProjectPath;
        private Button button_LinkProjectPath;
        private CheckBox checkBox_EncryptResList;
        private CheckBox checkBox_ForcePackage;
        private CheckBox checkBox_SplitedFolder;
        private CheckBox checkBox_EncodeByBase64;
        private TextBox textBox_FowardRandomByteArrayNumber;
        private CheckBox checkBox_ResListConvertToBytes;
        private CheckBox checkBox_ReplaceSensitiveWord;
        private Button button_SensitiveDictionary;
        private Button button_UploadSensitiveDictionary;
        private TextBox textBox_StringAddedAfterMD5Encrypt;
        private CheckBox checkBox_AddCheckBoxAfterMD5Encrypt;
        private System.Windows.Forms.ComboBox comboBox_GenerateResPrefab;

        private void InitializeComponent()
        {
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            this.button_RefreshPrefabs = new System.Windows.Forms.Button();
            this.component_NormalTextBox_PostFix = new CheckResourceTool.IndependentPanel.SmallComponents.Component_NormalTextBox();
            this.component_NormalTextBox_Channel = new CheckResourceTool.IndependentPanel.SmallComponents.Component_NormalTextBox();
            this.comboBox_Platform = new System.Windows.Forms.ComboBox();
            this.component_NormalTextBox_PrefabName = new CheckResourceTool.IndependentPanel.SmallComponents.Component_NormalTextBox();
            this.button_SaveAsPrefab = new System.Windows.Forms.Button();
            this.button_EditorPrefab = new System.Windows.Forms.Button();
            this.comboBox_GenerateResPrefab = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.component_URLSelect_IndependentListPath = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_FirstZipListPath = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_AssetDirectory = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_ResListInApkPath = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_PredownloadListPath = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_AdditionalResDirectory = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_SpecialResZipListPath = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_ResourceOrderPath = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.component_URLSelect_ExportDirectory = new CheckResourceTool.IndependentPanel.SmallComponents.Component_URLSelect();
            this.checkBox_SplitedFolder = new System.Windows.Forms.CheckBox();
            this.checkBox_ForcePackage = new System.Windows.Forms.CheckBox();
            this.checkBox_EncryptResList = new System.Windows.Forms.CheckBox();
            this.button_LinkProjectPath = new System.Windows.Forms.Button();
            this.textBox_ProjectPath = new System.Windows.Forms.TextBox();
            this.comboBox_ProjectName = new System.Windows.Forms.ComboBox();
            this.button_ChangeProjectPath = new System.Windows.Forms.Button();
            this.button_ChangeUnityEXEPath = new System.Windows.Forms.Button();
            this.checkBox_ExportZips = new System.Windows.Forms.CheckBox();
            this.textBox_ZIPPassword = new System.Windows.Forms.TextBox();
            this.textBox_AdditionalUnityPackage = new System.Windows.Forms.TextBox();
            this.button_SelcectAdditionalUnityPackage = new System.Windows.Forms.Button();
            this.comboBox_UnityType = new System.Windows.Forms.ComboBox();
            this.textBox_SingleRubbishSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_EncryptByMD5 = new System.Windows.Forms.CheckBox();
            this.textBox_PackageNameLetters = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_ExportCompleteList = new System.Windows.Forms.CheckBox();
            this.checkBox_ExportResListInApk = new System.Windows.Forms.CheckBox();
            this.checkBox_EncodeByBase64 = new System.Windows.Forms.CheckBox();
            this.textBox_FowardRandomByteArrayNumber = new System.Windows.Forms.TextBox();
            this.checkBox_ResListConvertToBytes = new System.Windows.Forms.CheckBox();
            this.checkBox_ReplaceSensitiveWord = new System.Windows.Forms.CheckBox();
            this.button_SensitiveDictionary = new System.Windows.Forms.Button();
            this.button_UploadSensitiveDictionary = new System.Windows.Forms.Button();
            this.textBox_StringAddedAfterMD5Encrypt = new System.Windows.Forms.TextBox();
            this.checkBox_AddCheckBoxAfterMD5Encrypt = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(923, 326);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(59, 12);
            label5.TabIndex = 22;
            label5.Text = "zip包密码";
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(270, 296);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(113, 12);
            label4.TabIndex = 18;
            label4.Text = "添加的UnityPackage";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            groupBox1.Controls.Add(this.button_RefreshPrefabs);
            groupBox1.Controls.Add(this.component_NormalTextBox_PostFix);
            groupBox1.Controls.Add(this.component_NormalTextBox_Channel);
            groupBox1.Controls.Add(this.comboBox_Platform);
            groupBox1.Controls.Add(this.component_NormalTextBox_PrefabName);
            groupBox1.Controls.Add(this.button_SaveAsPrefab);
            groupBox1.Controls.Add(this.button_EditorPrefab);
            groupBox1.Controls.Add(this.comboBox_GenerateResPrefab);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(this.splitContainer1);
            groupBox1.Controls.Add(this.component_URLSelect_ExportDirectory);
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            groupBox1.Location = new System.Drawing.Point(3, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(1630, 252);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "预设区";
            // 
            // button_RefreshPrefabs
            // 
            this.button_RefreshPrefabs.Image = global::CheckResourceTool.Properties.Resources.refresh;
            this.button_RefreshPrefabs.Location = new System.Drawing.Point(222, 20);
            this.button_RefreshPrefabs.Name = "button_RefreshPrefabs";
            this.button_RefreshPrefabs.Size = new System.Drawing.Size(20, 20);
            this.button_RefreshPrefabs.TabIndex = 16;
            this.button_RefreshPrefabs.UseVisualStyleBackColor = true;
            this.button_RefreshPrefabs.Click += new System.EventHandler(this.button_RefreshPrefabs_Click);
            // 
            // component_NormalTextBox_PostFix
            // 
            this.component_NormalTextBox_PostFix.Location = new System.Drawing.Point(503, 49);
            this.component_NormalTextBox_PostFix.Name = "component_NormalTextBox_PostFix";
            this.component_NormalTextBox_PostFix.Size = new System.Drawing.Size(256, 24);
            this.component_NormalTextBox_PostFix.TabIndex = 7;
            this.component_NormalTextBox_PostFix.TextBoxContent = "";
            this.component_NormalTextBox_PostFix.TextBoxName = "文本框名称一";
            // 
            // component_NormalTextBox_Channel
            // 
            this.component_NormalTextBox_Channel.Location = new System.Drawing.Point(241, 49);
            this.component_NormalTextBox_Channel.Name = "component_NormalTextBox_Channel";
            this.component_NormalTextBox_Channel.Size = new System.Drawing.Size(256, 24);
            this.component_NormalTextBox_Channel.TabIndex = 6;
            this.component_NormalTextBox_Channel.TextBoxContent = "";
            this.component_NormalTextBox_Channel.TextBoxName = "文本框名称一";
            // 
            // comboBox_Platform
            // 
            this.comboBox_Platform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Platform.FormattingEnabled = true;
            this.comboBox_Platform.Items.AddRange(new object[] {
            "iOS",
            "Android"});
            this.comboBox_Platform.Location = new System.Drawing.Point(503, 20);
            this.comboBox_Platform.Name = "comboBox_Platform";
            this.comboBox_Platform.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Platform.TabIndex = 5;
            // 
            // component_NormalTextBox_PrefabName
            // 
            this.component_NormalTextBox_PrefabName.Location = new System.Drawing.Point(241, 20);
            this.component_NormalTextBox_PrefabName.Name = "component_NormalTextBox_PrefabName";
            this.component_NormalTextBox_PrefabName.Size = new System.Drawing.Size(256, 24);
            this.component_NormalTextBox_PrefabName.TabIndex = 4;
            this.component_NormalTextBox_PrefabName.TextBoxContent = "";
            this.component_NormalTextBox_PrefabName.TextBoxName = "文本框名称一";
            // 
            // button_SaveAsPrefab
            // 
            this.button_SaveAsPrefab.Location = new System.Drawing.Point(141, 46);
            this.button_SaveAsPrefab.Name = "button_SaveAsPrefab";
            this.button_SaveAsPrefab.Size = new System.Drawing.Size(75, 23);
            this.button_SaveAsPrefab.TabIndex = 3;
            this.button_SaveAsPrefab.Text = "存为预设";
            this.button_SaveAsPrefab.UseVisualStyleBackColor = true;
            this.button_SaveAsPrefab.Click += new System.EventHandler(this.button_SaveAsPrefab_Click);
            // 
            // button_EditorPrefab
            // 
            this.button_EditorPrefab.Location = new System.Drawing.Point(7, 46);
            this.button_EditorPrefab.Name = "button_EditorPrefab";
            this.button_EditorPrefab.Size = new System.Drawing.Size(120, 23);
            this.button_EditorPrefab.TabIndex = 2;
            this.button_EditorPrefab.Text = "编辑预设缓存";
            this.button_EditorPrefab.UseVisualStyleBackColor = true;
            this.button_EditorPrefab.Click += new System.EventHandler(this.button_EditorPrefab_Click);
            // 
            // comboBox_GenerateResPrefab
            // 
            this.comboBox_GenerateResPrefab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_GenerateResPrefab.FormattingEnabled = true;
            this.comboBox_GenerateResPrefab.Location = new System.Drawing.Point(66, 20);
            this.comboBox_GenerateResPrefab.Name = "comboBox_GenerateResPrefab";
            this.comboBox_GenerateResPrefab.Size = new System.Drawing.Size(150, 20);
            this.comboBox_GenerateResPrefab.TabIndex = 0;
            this.comboBox_GenerateResPrefab.SelectedIndexChanged += new System.EventHandler(this.comboBox_GenerateResPrefab_SelectedIndexChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 12);
            label1.TabIndex = 1;
            label1.Text = "配置预设";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 76);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1630, 139);
            this.splitContainer1.SplitterDistance = 774;
            this.splitContainer1.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            panel2.Controls.Add(this.component_URLSelect_IndependentListPath);
            panel2.Controls.Add(this.component_URLSelect_FirstZipListPath);
            panel2.Controls.Add(this.component_URLSelect_AssetDirectory);
            panel2.Controls.Add(this.component_URLSelect_ResListInApkPath);
            panel2.Location = new System.Drawing.Point(3, 6);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(768, 118);
            panel2.TabIndex = 16;
            // 
            // component_URLSelect_IndependentListPath
            // 
            this.component_URLSelect_IndependentListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_IndependentListPath.DefaultPath = "";
            this.component_URLSelect_IndependentListPath.Location = new System.Drawing.Point(0, 87);
            this.component_URLSelect_IndependentListPath.Name = "component_URLSelect_IndependentListPath";
            this.component_URLSelect_IndependentListPath.Size = new System.Drawing.Size(768, 24);
            this.component_URLSelect_IndependentListPath.TabIndex = 14;
            this.component_URLSelect_IndependentListPath.URL = "";
            this.component_URLSelect_IndependentListPath.URLInfo = "URL选择路径";
            this.component_URLSelect_IndependentListPath.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_FirstZipListPath
            // 
            this.component_URLSelect_FirstZipListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_FirstZipListPath.DefaultPath = "";
            this.component_URLSelect_FirstZipListPath.Location = new System.Drawing.Point(0, 59);
            this.component_URLSelect_FirstZipListPath.Name = "component_URLSelect_FirstZipListPath";
            this.component_URLSelect_FirstZipListPath.Size = new System.Drawing.Size(768, 24);
            this.component_URLSelect_FirstZipListPath.TabIndex = 13;
            this.component_URLSelect_FirstZipListPath.URL = "";
            this.component_URLSelect_FirstZipListPath.URLInfo = "URL选择路径";
            this.component_URLSelect_FirstZipListPath.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_AssetDirectory
            // 
            this.component_URLSelect_AssetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_AssetDirectory.DefaultPath = "";
            this.component_URLSelect_AssetDirectory.Location = new System.Drawing.Point(0, 3);
            this.component_URLSelect_AssetDirectory.Name = "component_URLSelect_AssetDirectory";
            this.component_URLSelect_AssetDirectory.Size = new System.Drawing.Size(768, 24);
            this.component_URLSelect_AssetDirectory.TabIndex = 11;
            this.component_URLSelect_AssetDirectory.URL = "";
            this.component_URLSelect_AssetDirectory.URLInfo = "URL选择路径";
            this.component_URLSelect_AssetDirectory.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_ResListInApkPath
            // 
            this.component_URLSelect_ResListInApkPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_ResListInApkPath.DefaultPath = "";
            this.component_URLSelect_ResListInApkPath.Location = new System.Drawing.Point(0, 31);
            this.component_URLSelect_ResListInApkPath.Name = "component_URLSelect_ResListInApkPath";
            this.component_URLSelect_ResListInApkPath.Size = new System.Drawing.Size(768, 24);
            this.component_URLSelect_ResListInApkPath.TabIndex = 12;
            this.component_URLSelect_ResListInApkPath.URL = "";
            this.component_URLSelect_ResListInApkPath.URLInfo = "URL选择路径";
            this.component_URLSelect_ResListInApkPath.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // panel1
            // 
            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            panel1.Controls.Add(this.component_URLSelect_PredownloadListPath);
            panel1.Controls.Add(this.component_URLSelect_AdditionalResDirectory);
            panel1.Controls.Add(this.component_URLSelect_SpecialResZipListPath);
            panel1.Controls.Add(this.component_URLSelect_ResourceOrderPath);
            panel1.Location = new System.Drawing.Point(3, 6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(846, 118);
            panel1.TabIndex = 14;
            // 
            // component_URLSelect_PredownloadListPath
            // 
            this.component_URLSelect_PredownloadListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_PredownloadListPath.DefaultPath = "";
            this.component_URLSelect_PredownloadListPath.Location = new System.Drawing.Point(0, 59);
            this.component_URLSelect_PredownloadListPath.Name = "component_URLSelect_PredownloadListPath";
            this.component_URLSelect_PredownloadListPath.Size = new System.Drawing.Size(845, 24);
            this.component_URLSelect_PredownloadListPath.TabIndex = 14;
            this.component_URLSelect_PredownloadListPath.URL = "";
            this.component_URLSelect_PredownloadListPath.URLInfo = "URL选择路径";
            this.component_URLSelect_PredownloadListPath.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_AdditionalResDirectory
            // 
            this.component_URLSelect_AdditionalResDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_AdditionalResDirectory.DefaultPath = "";
            this.component_URLSelect_AdditionalResDirectory.Location = new System.Drawing.Point(0, 87);
            this.component_URLSelect_AdditionalResDirectory.Name = "component_URLSelect_AdditionalResDirectory";
            this.component_URLSelect_AdditionalResDirectory.Size = new System.Drawing.Size(845, 24);
            this.component_URLSelect_AdditionalResDirectory.TabIndex = 13;
            this.component_URLSelect_AdditionalResDirectory.URL = "";
            this.component_URLSelect_AdditionalResDirectory.URLInfo = "URL选择路径";
            this.component_URLSelect_AdditionalResDirectory.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_SpecialResZipListPath
            // 
            this.component_URLSelect_SpecialResZipListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_SpecialResZipListPath.DefaultPath = "";
            this.component_URLSelect_SpecialResZipListPath.Location = new System.Drawing.Point(0, 3);
            this.component_URLSelect_SpecialResZipListPath.Name = "component_URLSelect_SpecialResZipListPath";
            this.component_URLSelect_SpecialResZipListPath.Size = new System.Drawing.Size(845, 24);
            this.component_URLSelect_SpecialResZipListPath.TabIndex = 11;
            this.component_URLSelect_SpecialResZipListPath.URL = "";
            this.component_URLSelect_SpecialResZipListPath.URLInfo = "URL选择路径";
            this.component_URLSelect_SpecialResZipListPath.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_ResourceOrderPath
            // 
            this.component_URLSelect_ResourceOrderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_ResourceOrderPath.DefaultPath = "";
            this.component_URLSelect_ResourceOrderPath.Location = new System.Drawing.Point(0, 31);
            this.component_URLSelect_ResourceOrderPath.Name = "component_URLSelect_ResourceOrderPath";
            this.component_URLSelect_ResourceOrderPath.Size = new System.Drawing.Size(845, 24);
            this.component_URLSelect_ResourceOrderPath.TabIndex = 12;
            this.component_URLSelect_ResourceOrderPath.URL = "";
            this.component_URLSelect_ResourceOrderPath.URLInfo = "URL选择路径";
            this.component_URLSelect_ResourceOrderPath.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // component_URLSelect_ExportDirectory
            // 
            this.component_URLSelect_ExportDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.component_URLSelect_ExportDirectory.DefaultPath = "";
            this.component_URLSelect_ExportDirectory.Location = new System.Drawing.Point(0, 220);
            this.component_URLSelect_ExportDirectory.Name = "component_URLSelect_ExportDirectory";
            this.component_URLSelect_ExportDirectory.Size = new System.Drawing.Size(1630, 24);
            this.component_URLSelect_ExportDirectory.TabIndex = 5;
            this.component_URLSelect_ExportDirectory.URL = "";
            this.component_URLSelect_ExportDirectory.URLInfo = "URL选择路径";
            this.component_URLSelect_ExportDirectory.URLType = CheckResourceTool.URLType.FileURL;
            // 
            // label6
            // 
            label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(1068, 326);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(89, 12);
            label6.TabIndex = 36;
            label6.Text = "前置随机字节数";
            // 
            // label7
            // 
            label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(160, 356);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(89, 12);
            label7.TabIndex = 42;
            label7.Text = "加密后添加字符";
            // 
            // checkBox_SplitedFolder
            // 
            this.checkBox_SplitedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_SplitedFolder.AutoSize = true;
            this.checkBox_SplitedFolder.Enabled = false;
            this.checkBox_SplitedFolder.Location = new System.Drawing.Point(1549, 294);
            this.checkBox_SplitedFolder.Name = "checkBox_SplitedFolder";
            this.checkBox_SplitedFolder.Size = new System.Drawing.Size(84, 16);
            this.checkBox_SplitedFolder.TabIndex = 34;
            this.checkBox_SplitedFolder.Text = "文件夹分层";
            this.checkBox_SplitedFolder.UseVisualStyleBackColor = true;
            // 
            // checkBox_ForcePackage
            // 
            this.checkBox_ForcePackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ForcePackage.AutoSize = true;
            this.checkBox_ForcePackage.Location = new System.Drawing.Point(1427, 294);
            this.checkBox_ForcePackage.Name = "checkBox_ForcePackage";
            this.checkBox_ForcePackage.Size = new System.Drawing.Size(120, 16);
            this.checkBox_ForcePackage.TabIndex = 33;
            this.checkBox_ForcePackage.Text = "强制更新界面资源";
            this.checkBox_ForcePackage.UseVisualStyleBackColor = true;
            // 
            // checkBox_EncryptResList
            // 
            this.checkBox_EncryptResList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_EncryptResList.AutoSize = true;
            this.checkBox_EncryptResList.Location = new System.Drawing.Point(395, 324);
            this.checkBox_EncryptResList.Name = "checkBox_EncryptResList";
            this.checkBox_EncryptResList.Size = new System.Drawing.Size(96, 16);
            this.checkBox_EncryptResList.TabIndex = 32;
            this.checkBox_EncryptResList.Text = "资源列表加密";
            this.checkBox_EncryptResList.UseVisualStyleBackColor = true;
            // 
            // button_LinkProjectPath
            // 
            this.button_LinkProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LinkProjectPath.Location = new System.Drawing.Point(1591, 262);
            this.button_LinkProjectPath.Name = "button_LinkProjectPath";
            this.button_LinkProjectPath.Size = new System.Drawing.Size(40, 20);
            this.button_LinkProjectPath.TabIndex = 30;
            this.button_LinkProjectPath.Text = "Link";
            this.button_LinkProjectPath.UseVisualStyleBackColor = true;
            this.button_LinkProjectPath.Click += new System.EventHandler(this.button_LinkProjectPath_Click);
            // 
            // textBox_ProjectPath
            // 
            this.textBox_ProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ProjectPath.Location = new System.Drawing.Point(256, 262);
            this.textBox_ProjectPath.Name = "textBox_ProjectPath";
            this.textBox_ProjectPath.ReadOnly = true;
            this.textBox_ProjectPath.Size = new System.Drawing.Size(1329, 21);
            this.textBox_ProjectPath.TabIndex = 29;
            // 
            // comboBox_ProjectName
            // 
            this.comboBox_ProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_ProjectName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ProjectName.FormattingEnabled = true;
            this.comboBox_ProjectName.Location = new System.Drawing.Point(140, 263);
            this.comboBox_ProjectName.Name = "comboBox_ProjectName";
            this.comboBox_ProjectName.Size = new System.Drawing.Size(105, 20);
            this.comboBox_ProjectName.TabIndex = 27;
            this.comboBox_ProjectName.SelectedIndexChanged += new System.EventHandler(this.comboBox_ProjectName_SelectedIndexChanged);
            // 
            // button_ChangeProjectPath
            // 
            this.button_ChangeProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ChangeProjectPath.Location = new System.Drawing.Point(4, 262);
            this.button_ChangeProjectPath.Name = "button_ChangeProjectPath";
            this.button_ChangeProjectPath.Size = new System.Drawing.Size(126, 22);
            this.button_ChangeProjectPath.TabIndex = 26;
            this.button_ChangeProjectPath.Text = "配置工程目录";
            this.button_ChangeProjectPath.UseVisualStyleBackColor = true;
            this.button_ChangeProjectPath.Click += new System.EventHandler(this.button_ChangeProjectPath_Click);
            // 
            // button_ChangeUnityEXEPath
            // 
            this.button_ChangeUnityEXEPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ChangeUnityEXEPath.Location = new System.Drawing.Point(4, 291);
            this.button_ChangeUnityEXEPath.Name = "button_ChangeUnityEXEPath";
            this.button_ChangeUnityEXEPath.Size = new System.Drawing.Size(126, 22);
            this.button_ChangeUnityEXEPath.TabIndex = 25;
            this.button_ChangeUnityEXEPath.Text = "配置Unity.exe路径";
            this.button_ChangeUnityEXEPath.UseVisualStyleBackColor = true;
            this.button_ChangeUnityEXEPath.Click += new System.EventHandler(this.button_ChangeUnityEXEPath_Click);
            // 
            // checkBox_ExportZips
            // 
            this.checkBox_ExportZips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_ExportZips.AutoSize = true;
            this.checkBox_ExportZips.Checked = true;
            this.checkBox_ExportZips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ExportZips.Location = new System.Drawing.Point(751, 324);
            this.checkBox_ExportZips.Name = "checkBox_ExportZips";
            this.checkBox_ExportZips.Size = new System.Drawing.Size(108, 16);
            this.checkBox_ExportZips.TabIndex = 24;
            this.checkBox_ExportZips.Text = "导出资源压缩包";
            this.checkBox_ExportZips.UseVisualStyleBackColor = true;
            // 
            // textBox_ZIPPassword
            // 
            this.textBox_ZIPPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_ZIPPassword.Location = new System.Drawing.Point(984, 322);
            this.textBox_ZIPPassword.Name = "textBox_ZIPPassword";
            this.textBox_ZIPPassword.Size = new System.Drawing.Size(77, 21);
            this.textBox_ZIPPassword.TabIndex = 23;
            // 
            // textBox_AdditionalUnityPackage
            // 
            this.textBox_AdditionalUnityPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_AdditionalUnityPackage.Location = new System.Drawing.Point(406, 292);
            this.textBox_AdditionalUnityPackage.Name = "textBox_AdditionalUnityPackage";
            this.textBox_AdditionalUnityPackage.Size = new System.Drawing.Size(1011, 21);
            this.textBox_AdditionalUnityPackage.TabIndex = 20;
            // 
            // button_SelcectAdditionalUnityPackage
            // 
            this.button_SelcectAdditionalUnityPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_SelcectAdditionalUnityPackage.BackColor = System.Drawing.Color.LimeGreen;
            this.button_SelcectAdditionalUnityPackage.ForeColor = System.Drawing.Color.Silver;
            this.button_SelcectAdditionalUnityPackage.Location = new System.Drawing.Point(383, 295);
            this.button_SelcectAdditionalUnityPackage.Name = "button_SelcectAdditionalUnityPackage";
            this.button_SelcectAdditionalUnityPackage.Size = new System.Drawing.Size(14, 14);
            this.button_SelcectAdditionalUnityPackage.TabIndex = 19;
            this.button_SelcectAdditionalUnityPackage.UseVisualStyleBackColor = false;
            this.button_SelcectAdditionalUnityPackage.Click += new System.EventHandler(this.button_SelectAdditionalUnityPackage_Click);
            // 
            // comboBox_UnityType
            // 
            this.comboBox_UnityType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_UnityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_UnityType.FormattingEnabled = true;
            this.comboBox_UnityType.Location = new System.Drawing.Point(140, 292);
            this.comboBox_UnityType.Name = "comboBox_UnityType";
            this.comboBox_UnityType.Size = new System.Drawing.Size(122, 20);
            this.comboBox_UnityType.TabIndex = 17;
            this.comboBox_UnityType.SelectedIndexChanged += new System.EventHandler(this.comboBox_UnityType_SelectedIndexChanged);
            // 
            // textBox_SingleRubbishSize
            // 
            this.textBox_SingleRubbishSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_SingleRubbishSize.Location = new System.Drawing.Point(322, 322);
            this.textBox_SingleRubbishSize.Name = "textBox_SingleRubbishSize";
            this.textBox_SingleRubbishSize.Size = new System.Drawing.Size(61, 21);
            this.textBox_SingleRubbishSize.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "单个压缩包垃圾资源大小(MB)";
            // 
            // checkBox_EncryptByMD5
            // 
            this.checkBox_EncryptByMD5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_EncryptByMD5.AutoSize = true;
            this.checkBox_EncryptByMD5.Location = new System.Drawing.Point(861, 324);
            this.checkBox_EncryptByMD5.Name = "checkBox_EncryptByMD5";
            this.checkBox_EncryptByMD5.Size = new System.Drawing.Size(66, 16);
            this.checkBox_EncryptByMD5.TabIndex = 14;
            this.checkBox_EncryptByMD5.Text = "MD5加密";
            this.checkBox_EncryptByMD5.UseVisualStyleBackColor = true;
            this.checkBox_EncryptByMD5.CheckedChanged += new System.EventHandler(this.checkBox_EncryptByMD5_CheckedChanged);
            // 
            // textBox_PackageNameLetters
            // 
            this.textBox_PackageNameLetters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_PackageNameLetters.Location = new System.Drawing.Point(81, 322);
            this.textBox_PackageNameLetters.Name = "textBox_PackageNameLetters";
            this.textBox_PackageNameLetters.Size = new System.Drawing.Size(70, 21);
            this.textBox_PackageNameLetters.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "包名首字母";
            // 
            // checkBox_ExportCompleteList
            // 
            this.checkBox_ExportCompleteList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_ExportCompleteList.AutoSize = true;
            this.checkBox_ExportCompleteList.Location = new System.Drawing.Point(627, 324);
            this.checkBox_ExportCompleteList.Name = "checkBox_ExportCompleteList";
            this.checkBox_ExportCompleteList.Size = new System.Drawing.Size(120, 16);
            this.checkBox_ExportCompleteList.TabIndex = 11;
            this.checkBox_ExportCompleteList.Text = "导出完整资源列表";
            this.checkBox_ExportCompleteList.UseVisualStyleBackColor = true;
            // 
            // checkBox_ExportResListInApk
            // 
            this.checkBox_ExportResListInApk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_ExportResListInApk.AutoSize = true;
            this.checkBox_ExportResListInApk.Location = new System.Drawing.Point(503, 324);
            this.checkBox_ExportResListInApk.Name = "checkBox_ExportResListInApk";
            this.checkBox_ExportResListInApk.Size = new System.Drawing.Size(120, 16);
            this.checkBox_ExportResListInApk.TabIndex = 10;
            this.checkBox_ExportResListInApk.Text = "导出包内资源列表";
            this.checkBox_ExportResListInApk.UseVisualStyleBackColor = true;
            this.checkBox_ExportResListInApk.CheckedChanged += new System.EventHandler(this.checkBox_ExportResListInApk_CheckedChanged);
            // 
            // checkBox_EncodeByBase64
            // 
            this.checkBox_EncodeByBase64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_EncodeByBase64.AutoSize = true;
            this.checkBox_EncodeByBase64.Enabled = false;
            this.checkBox_EncodeByBase64.Location = new System.Drawing.Point(1549, 324);
            this.checkBox_EncodeByBase64.Name = "checkBox_EncodeByBase64";
            this.checkBox_EncodeByBase64.Size = new System.Drawing.Size(84, 16);
            this.checkBox_EncodeByBase64.TabIndex = 35;
            this.checkBox_EncodeByBase64.Text = "Base64加密";
            this.checkBox_EncodeByBase64.UseVisualStyleBackColor = true;
            this.checkBox_EncodeByBase64.CheckedChanged += new System.EventHandler(this.checkBox_EncodeByBase64_CheckedChanged);
            // 
            // textBox_FowardRandomByteArrayNumber
            // 
            this.textBox_FowardRandomByteArrayNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_FowardRandomByteArrayNumber.Location = new System.Drawing.Point(1161, 322);
            this.textBox_FowardRandomByteArrayNumber.Name = "textBox_FowardRandomByteArrayNumber";
            this.textBox_FowardRandomByteArrayNumber.Size = new System.Drawing.Size(82, 21);
            this.textBox_FowardRandomByteArrayNumber.TabIndex = 37;
            // 
            // checkBox_ResListConvertToBytes
            // 
            this.checkBox_ResListConvertToBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ResListConvertToBytes.AutoSize = true;
            this.checkBox_ResListConvertToBytes.Enabled = false;
            this.checkBox_ResListConvertToBytes.Location = new System.Drawing.Point(1427, 324);
            this.checkBox_ResListConvertToBytes.Name = "checkBox_ResListConvertToBytes";
            this.checkBox_ResListConvertToBytes.Size = new System.Drawing.Size(108, 16);
            this.checkBox_ResListConvertToBytes.TabIndex = 38;
            this.checkBox_ResListConvertToBytes.Text = "二进制资源列表";
            this.checkBox_ResListConvertToBytes.UseVisualStyleBackColor = true;
            // 
            // checkBox_ReplaceSensitiveWord
            // 
            this.checkBox_ReplaceSensitiveWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ReplaceSensitiveWord.AutoSize = true;
            this.checkBox_ReplaceSensitiveWord.Enabled = false;
            this.checkBox_ReplaceSensitiveWord.Location = new System.Drawing.Point(1297, 354);
            this.checkBox_ReplaceSensitiveWord.Name = "checkBox_ReplaceSensitiveWord";
            this.checkBox_ReplaceSensitiveWord.Size = new System.Drawing.Size(120, 16);
            this.checkBox_ReplaceSensitiveWord.TabIndex = 39;
            this.checkBox_ReplaceSensitiveWord.Text = "路径中敏感词替换";
            this.checkBox_ReplaceSensitiveWord.UseVisualStyleBackColor = true;
            // 
            // button_SensitiveDictionary
            // 
            this.button_SensitiveDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SensitiveDictionary.Location = new System.Drawing.Point(1427, 350);
            this.button_SensitiveDictionary.Name = "button_SensitiveDictionary";
            this.button_SensitiveDictionary.Size = new System.Drawing.Size(108, 23);
            this.button_SensitiveDictionary.TabIndex = 40;
            this.button_SensitiveDictionary.Text = "打开敏感词字典";
            this.button_SensitiveDictionary.UseVisualStyleBackColor = true;
            this.button_SensitiveDictionary.Click += new System.EventHandler(this.button_SensitiveDictionary_Click);
            // 
            // button_UploadSensitiveDictionary
            // 
            this.button_UploadSensitiveDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_UploadSensitiveDictionary.Location = new System.Drawing.Point(1549, 350);
            this.button_UploadSensitiveDictionary.Name = "button_UploadSensitiveDictionary";
            this.button_UploadSensitiveDictionary.Size = new System.Drawing.Size(75, 23);
            this.button_UploadSensitiveDictionary.TabIndex = 41;
            this.button_UploadSensitiveDictionary.Text = "上传字典";
            this.button_UploadSensitiveDictionary.UseVisualStyleBackColor = true;
            this.button_UploadSensitiveDictionary.Click += new System.EventHandler(this.button_UploadSensitiveDictionary_Click);
            // 
            // textBox_StringAddedAfterMD5Encrypt
            // 
            this.textBox_StringAddedAfterMD5Encrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_StringAddedAfterMD5Encrypt.Location = new System.Drawing.Point(255, 352);
            this.textBox_StringAddedAfterMD5Encrypt.Name = "textBox_StringAddedAfterMD5Encrypt";
            this.textBox_StringAddedAfterMD5Encrypt.Size = new System.Drawing.Size(100, 21);
            this.textBox_StringAddedAfterMD5Encrypt.TabIndex = 43;
            // 
            // checkBox_AddCheckBoxAfterMD5Encrypt
            // 
            this.checkBox_AddCheckBoxAfterMD5Encrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_AddCheckBoxAfterMD5Encrypt.AutoSize = true;
            this.checkBox_AddCheckBoxAfterMD5Encrypt.Enabled = false;
            this.checkBox_AddCheckBoxAfterMD5Encrypt.Location = new System.Drawing.Point(10, 354);
            this.checkBox_AddCheckBoxAfterMD5Encrypt.Name = "checkBox_AddCheckBoxAfterMD5Encrypt";
            this.checkBox_AddCheckBoxAfterMD5Encrypt.Size = new System.Drawing.Size(126, 16);
            this.checkBox_AddCheckBoxAfterMD5Encrypt.TabIndex = 44;
            this.checkBox_AddCheckBoxAfterMD5Encrypt.Text = "MD5加密后添加字符";
            this.checkBox_AddCheckBoxAfterMD5Encrypt.UseVisualStyleBackColor = true;
            // 
            // MissionOption_AutoGenerateResourceZIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Controls.Add(this.checkBox_AddCheckBoxAfterMD5Encrypt);
            this.Controls.Add(this.textBox_StringAddedAfterMD5Encrypt);
            this.Controls.Add(label7);
            this.Controls.Add(this.button_UploadSensitiveDictionary);
            this.Controls.Add(this.button_SensitiveDictionary);
            this.Controls.Add(this.checkBox_ReplaceSensitiveWord);
            this.Controls.Add(this.checkBox_ResListConvertToBytes);
            this.Controls.Add(this.textBox_FowardRandomByteArrayNumber);
            this.Controls.Add(label6);
            this.Controls.Add(this.checkBox_EncodeByBase64);
            this.Controls.Add(this.checkBox_SplitedFolder);
            this.Controls.Add(this.checkBox_ForcePackage);
            this.Controls.Add(this.checkBox_EncryptResList);
            this.Controls.Add(this.button_LinkProjectPath);
            this.Controls.Add(this.textBox_ProjectPath);
            this.Controls.Add(this.comboBox_ProjectName);
            this.Controls.Add(this.button_ChangeProjectPath);
            this.Controls.Add(this.button_ChangeUnityEXEPath);
            this.Controls.Add(this.checkBox_ExportZips);
            this.Controls.Add(this.textBox_ZIPPassword);
            this.Controls.Add(label5);
            this.Controls.Add(this.textBox_AdditionalUnityPackage);
            this.Controls.Add(this.button_SelcectAdditionalUnityPackage);
            this.Controls.Add(label4);
            this.Controls.Add(this.comboBox_UnityType);
            this.Controls.Add(this.textBox_SingleRubbishSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox_EncryptByMD5);
            this.Controls.Add(this.textBox_PackageNameLetters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_ExportCompleteList);
            this.Controls.Add(this.checkBox_ExportResListInApk);
            this.Controls.Add(groupBox1);
            this.Name = "MissionOption_AutoGenerateResourceZIP";
            this.Size = new System.Drawing.Size(1636, 381);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Dictionary<string, string> sensitiveWordReplaceDic = new Dictionary<string, string>();

        public MissionOption_AutoGenerateResourceZIP() : base()
        {
            prefabs = new Dictionary<string, AutoGenerateResourceCacheInfo>();
            InitializeComponent();
            InitializeAdditionalComponents();
            RefreshPrefabsFromCache();
            RefreshConfigComboBoxFromPrefabs();
            RefreshUnityVersionFromCache();
            RefreshProjectsFromCache();
            if (comboBox_UnityType.Items != null && comboBox_UnityType.Items.Count > 0) comboBox_UnityType.SelectedIndex = 0;
            if (comboBox_ProjectName.Items != null && comboBox_ProjectName.Items.Count > 0) comboBox_ProjectName.SelectedIndex = 0;
        }

        private void InitializeAdditionalComponents()
        {
            component_NormalTextBox_Channel.TextBoxName = "渠道名";
            component_NormalTextBox_PostFix.TextBoxName = "zip包后缀";
            component_NormalTextBox_PrefabName.TextBoxName = "预设名称";
            comboBox_Platform.SelectedIndex = 0;
            component_URLSelect_AdditionalResDirectory.URLType = URLType.LocalFolderURL;
            component_URLSelect_AdditionalResDirectory.URLInfo = "附加资源目录";
            component_URLSelect_AdditionalResDirectory.DefaultPath = (Global.Instance.FixedDirectory + "IOS包内更换地图").Replace('/', '\\');
            component_URLSelect_AssetDirectory.URLType = URLType.LocalFolderURL;
            component_URLSelect_AssetDirectory.URLInfo = "资源目录";
            component_URLSelect_ExportDirectory.URLType = URLType.LocalFolderURL;
            component_URLSelect_ExportDirectory.URLInfo = "导出目录";
            component_URLSelect_FirstZipListPath.URLType = URLType.LocalFileURL;
            component_URLSelect_FirstZipListPath.URLInfo = "First包配置";
            component_URLSelect_ResListInApkPath.URLType = URLType.LocalFileURL;
            component_URLSelect_ResListInApkPath.URLInfo = "包内资源配置";
            component_URLSelect_ResourceOrderPath.URLType = URLType.LocalFileURL;
            component_URLSelect_ResourceOrderPath.URLInfo = "资源顺序配置";
            component_URLSelect_IndependentListPath.URLType = URLType.LocalFileURL;
            component_URLSelect_IndependentListPath.URLInfo = "独立资源配置";
            component_URLSelect_PredownloadListPath.URLType = URLType.LocalFileURL;
            component_URLSelect_PredownloadListPath.URLInfo = "预下载配置";
            component_URLSelect_SpecialResZipListPath.URLType = URLType.LocalFileURL;
            component_URLSelect_SpecialResZipListPath.URLInfo = "Special包配置";

            comboBox_UnityType.Click += delegate (object o, EventArgs e)
            {
                RefreshUnityVersionFromCache();
            };

            comboBox_ProjectName.Click += delegate (object o, EventArgs e)
            {
                RefreshProjectsFromCache();
            };
        }

        private AutoGenerateResourceCacheInfo GetCurrentCacheInfo()
        {
            AutoGenerateResourceCacheInfo info = new AutoGenerateResourceCacheInfo();
            info.AdditionalResourcePathList = component_URLSelect_AdditionalResDirectory.URL;
            info.Channel = component_NormalTextBox_Channel.TextBoxContent;
            info.FirstZipJsonPath = component_URLSelect_FirstZipListPath.URL;
            ResourcePlatform res = ResourcePlatform.Android;
            Enum.TryParse<ResourcePlatform>(comboBox_Platform.SelectedItem.ToString(), out res);
            info.PlatForm = res;
            info.PrefabName = component_NormalTextBox_PrefabName.TextBoxContent;
            info.ResInApkJsonPath = component_URLSelect_ResListInApkPath.URL;
            info.ResourceDirectory = component_URLSelect_AssetDirectory.URL;
            info.ResourceOrderPath = component_URLSelect_ResourceOrderPath.URL;
            info.IndependentResListPath = component_URLSelect_IndependentListPath.URL;
            info.PredownloadResListPath = component_URLSelect_PredownloadListPath.URL;
            info.SpecialResZipJsonPath = component_URLSelect_SpecialResZipListPath.URL;
            info.ZipPostFix = component_NormalTextBox_PostFix.TextBoxContent;
            info.ExportDirectory = component_URLSelect_ExportDirectory.URL;
            return info;
        }

        private void SetCurrentCacheInfo(AutoGenerateResourceCacheInfo info)
        {
            if (info != null)
            {
                component_URLSelect_AdditionalResDirectory.URL = info.AdditionalResourcePathList;
                component_NormalTextBox_Channel.TextBoxContent = info.Channel;
                component_URLSelect_FirstZipListPath.URL = info.FirstZipJsonPath;
                comboBox_Platform.SelectedItem = info.PlatForm.ToString();
                component_NormalTextBox_PrefabName.TextBoxContent = info.PrefabName;
                component_URLSelect_ResListInApkPath.URL = info.ResInApkJsonPath;
                component_URLSelect_AssetDirectory.URL = info.ResourceDirectory;
                component_URLSelect_ResourceOrderPath.URL = info.ResourceOrderPath;
                component_URLSelect_IndependentListPath.URL = info.IndependentResListPath;
                component_URLSelect_PredownloadListPath.URL = info.PredownloadResListPath;
                component_URLSelect_SpecialResZipListPath.URL = info.SpecialResZipJsonPath;
                component_NormalTextBox_PostFix.TextBoxContent = info.ZipPostFix;
                if (!string.IsNullOrEmpty(info.ExportDirectory))
                {
                    component_URLSelect_ExportDirectory.URL = info.ExportDirectory;
                }
            }
        }

        private void button_EditorPrefab_Click(object sender, EventArgs e)
        {
            Global.Instance.OpenURL(CacheTool.Instance.GetCacheFilePath<AutoGenerateResourceCacheInfo>());
        }

        private void button_SaveAsPrefab_Click(object sender, EventArgs e)
        {
            object previourItem = comboBox_GenerateResPrefab.SelectedItem;
            if (string.IsNullOrEmpty(component_NormalTextBox_PrefabName.TextBoxContent))
            {
                return;
            }
            if (prefabs.ContainsKey(component_NormalTextBox_PrefabName.TextBoxContent))
            {
                if (!(MessageBox.Show("是否替换原预设") == DialogResult.OK))
                {
                    return;
                }
                prefabs.Remove(component_NormalTextBox_PrefabName.TextBoxContent);
            }
            AutoGenerateResourceCacheInfo info = GetCurrentCacheInfo();
            prefabs.Add(info.PrefabName, info);
            SavePrefabsToCache();
            RefreshConfigComboBoxFromPrefabs();
            if (previourItem != null && comboBox_GenerateResPrefab.Items.Contains(previourItem))
            {
                comboBox_GenerateResPrefab.SelectedItem = previourItem;
            }
            MessageBox.Show("保存完毕");
        }

        private void button_RefreshPrefabs_Click(object sender, EventArgs e)
        {
            object previourItem = comboBox_GenerateResPrefab.SelectedItem;
            RefreshPrefabsFromCache();
            if (previourItem != null && comboBox_GenerateResPrefab.Items.Contains(previourItem))
            {
                comboBox_GenerateResPrefab.SelectedItem = previourItem;
            }
        }

        private void button_SelectAdditionalUnityPackage_Click(object sender, EventArgs e)
        {
            string path = Global.Instance.SelectFileByDialog("选择资源包路径", "unity打包文件(*.unitypackage)|*.unitypackage");
            if (!string.IsNullOrEmpty(path))
            {
                if (!string.IsNullOrEmpty(textBox_AdditionalUnityPackage.Text))
                {
                    textBox_AdditionalUnityPackage.Text += " ; ";
                }
                textBox_AdditionalUnityPackage.Text += path;
            }
        }

        private void button_ChangeUnityEXEPath_Click(object sender, EventArgs e)
        {
            Global.Instance.OpenURL(CacheTool.Instance.GetCacheFilePath<UnityexePathCacheInfo>());
        }

        private void button_SelectAssetPath_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_GenerateResPrefab_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (!string.IsNullOrEmpty(cb.SelectedItem.ToString()))
            {
                AutoGenerateResourceCacheInfo info;
                if (prefabs.TryGetValue(cb.SelectedItem.ToString(), out info))
                {
                    SetCurrentCacheInfo(info);
                }
            }
        }

        private void comboBox_UnityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            unityExePath = null;
            if (unityVersions.ContainsKey(cb.SelectedItem.ToString()))
            {
                unityExePath = unityVersions[cb.SelectedItem.ToString()].UnityExePath;
            }
        }

        private void button_ChangeProjectPath_Click(object sender, EventArgs e)
        {
            Global.Instance.OpenURL(CacheTool.Instance.GetCacheFilePath<UnityProjectPathCacheInfo>());
        }

        private void comboBox_ProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            textBox_ProjectPath.Clear();
            if (projects.ContainsKey(cb.SelectedItem.ToString()))
            {
                textBox_ProjectPath.Text = projects[cb.SelectedItem.ToString()].ProjectPath;
            }
        }

        private void button_LinkProjectPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_ProjectPath.Text))
            {
                Global.Instance.OpenURL(textBox_ProjectPath.Text);
            }
        }

        private void button_unitypackagePath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_AdditionalUnityPackage.Text))
            {
                Global.Instance.OpenURL(Path.GetDirectoryName(textBox_AdditionalUnityPackage.Text));
            }
        }

        //自动打包资源配置
        Dictionary<string, AutoGenerateResourceCacheInfo> prefabs;//预设名-预设集合
        Dictionary<string, UnityexePathCacheInfo> unityVersions;//unity.exe版本列表
        Dictionary<string, UnityProjectPathCacheInfo> projects;//工程列表

        private string unityExePath = string.Empty;//unity.exe路径        

        private void RefreshPrefabsFromCache()
        {
            List<AutoGenerateResourceCacheInfo> tempList;
            if (CacheTool.Instance.GetCache<AutoGenerateResourceCacheInfo>(out tempList))
            {
                prefabs.Clear();
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (!prefabs.ContainsKey(tempList[i].PrefabName) && !string.IsNullOrEmpty(tempList[i].PrefabName))
                    {
                        prefabs.Add(tempList[i].PrefabName, tempList[i]);
                    }
                }
            }
            RefreshConfigComboBoxFromPrefabs();
        }

        private void SavePrefabsToCache()
        {
            List<AutoGenerateResourceCacheInfo> list = prefabs.Values.ToList();
            CacheTool.Instance.SaveCache<AutoGenerateResourceCacheInfo>(list);
            RefreshPrefabsFromCache();
        }

        //从本地文件获取预设
        private void RefreshUnityVersionFromCache()
        {
            List<UnityexePathCacheInfo> unityVersionList;
            if (CacheTool.Instance.GetCache(out unityVersionList) && unityVersionList != null)
            {
                object previousVersion = comboBox_UnityType.SelectedItem;
                comboBox_UnityType.Items.Clear();
                unityVersions = unityVersions ?? new Dictionary<string, UnityexePathCacheInfo>();
                unityVersions.Clear();
                for (int i = 0; i < unityVersionList.Count; i++)
                {
                    comboBox_UnityType.Items.Add(unityVersionList[i].UnityVersion);
                    if (!unityVersions.ContainsKey(unityVersionList[i].UnityVersion))
                    {
                        unityVersions.Add(unityVersionList[i].UnityVersion, unityVersionList[i]);
                    }
                }
                if (previousVersion != null && comboBox_UnityType.Items.Contains(previousVersion))
                {
                    comboBox_UnityType.SelectedItem = previousVersion;
                }
            }
        }

        private void RefreshProjectsFromCache()
        {
            List<UnityProjectPathCacheInfo> projectList;
            if (CacheTool.Instance.GetCache(out projectList) && projectList != null)
            {
                object previousProject = comboBox_ProjectName.SelectedItem;
                comboBox_ProjectName.Items.Clear();
                projects = projects ?? new Dictionary<string, UnityProjectPathCacheInfo>();
                projects.Clear();
                for (int i = 0; i < projectList.Count; i++)
                {
                    comboBox_ProjectName.Items.Add(projectList[i].ProjectName);
                    if (!projects.ContainsKey(projectList[i].ProjectName))
                    {
                        projects.Add(projectList[i].ProjectName, projectList[i]);
                    }
                }
                if (previousProject != null && comboBox_ProjectName.Items.Contains(previousProject))
                {
                    comboBox_ProjectName.SelectedItem = previousProject;
                }
            }
        }

        private void RefreshConfigComboBoxFromPrefabs()
        {
            comboBox_GenerateResPrefab.Items.Clear();
            Dictionary<string, AutoGenerateResourceCacheInfo>.Enumerator prefabEnumerator = prefabs.GetEnumerator();
            while (prefabEnumerator.MoveNext())
            {
                comboBox_GenerateResPrefab.Items.Add(prefabEnumerator.Current.Key);
            }
        }

        public override void CreateNewMission()
        {
            base.CreateNewMission();
            if ((!checkBox_ExportCompleteList.Checked) && (!checkBox_ExportResListInApk.Checked) && (!checkBox_ExportZips.Checked))
            {
                Global.Instance.WriteGlobalLog("请选择 导出完整资源列表/导出包内资源列表/导出资源压缩包 中至少一项,不要搞事情");
            }
            else if (string.IsNullOrEmpty(component_URLSelect_AssetDirectory.URL))
            {
                Global.Instance.WriteGlobalLog("请选择 资源目录,不要搞事情");
            }
            else if (string.IsNullOrEmpty(component_URLSelect_ExportDirectory.URL))
            {
                Global.Instance.WriteGlobalLog("请选择 导出目录,不要搞事情");
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox_AdditionalUnityPackage.Text))
                {
                    if (comboBox_UnityType.SelectedItem == null)
                    {
                        Global.Instance.WriteGlobalLog("添加unitypackage包之前请先选择unity版本");
                        return;
                    }
                    if (!unityVersions.ContainsKey(comboBox_UnityType.SelectedItem.ToString()))
                    {
                        Global.Instance.WriteGlobalLog("请确认unity.exe路径配置是否正确");
                        return;
                    }
                    if (string.IsNullOrEmpty(unityExePath) || !File.Exists(unityExePath))
                    {
                        Global.Instance.WriteGlobalLog("unity.exe " + unityExePath + " 路径不存在");
                        return;
                    }
                }
                MissionPanel_AutoGenerateResourceZIP mission = Global.Instance.MainForm.CreateNewMissionPanel<MissionPanel_AutoGenerateResourceZIP>();
                mission.MissionType = "自动打包资源";
                mission.MissionBriefInformation = GetBriefInformation();
                bool needCheckSensitiveWord = (checkBox_ReplaceSensitiveWord.Enabled && checkBox_ReplaceSensitiveWord.Checked);
                if (needCheckSensitiveWord)
                {
                    RefreshSensitiveWordDic();
                }
                object[] paramenters = new object[31]
                {
                    component_URLSelect_AssetDirectory.URL,
                    component_URLSelect_ExportDirectory.URL,
                    component_URLSelect_ResListInApkPath.URL,
                    component_URLSelect_FirstZipListPath.URL,
                    component_URLSelect_SpecialResZipListPath.URL,
                    component_URLSelect_IndependentListPath.URL,
                    component_URLSelect_PredownloadListPath.URL,
                    component_URLSelect_ResourceOrderPath.URL,
                    comboBox_Platform.SelectedItem,
                    component_NormalTextBox_Channel.TextBoxContent,
                    component_NormalTextBox_PostFix.TextBoxContent,
                    component_URLSelect_AdditionalResDirectory.URL,
                    textBox_AdditionalUnityPackage.Text,
                    textBox_SingleRubbishSize.Text,
                    checkBox_ExportZips.Checked,
                    checkBox_ExportCompleteList.Checked,
                    checkBox_ExportResListInApk.Checked,
                    checkBox_EncryptResList.Checked,
                    textBox_PackageNameLetters.Text,
                    checkBox_EncryptByMD5.Checked,
                    textBox_ZIPPassword.Text,
                    unityExePath,
                    textBox_ProjectPath.Text,
                    checkBox_ForcePackage.Checked,
                    (checkBox_SplitedFolder.Enabled &&checkBox_SplitedFolder.Checked),
                    (checkBox_EncodeByBase64.Enabled && checkBox_EncodeByBase64.Checked),
                    textBox_FowardRandomByteArrayNumber.Text,
                    checkBox_ResListConvertToBytes.Checked,
                    needCheckSensitiveWord,
                    sensitiveWordReplaceDic,
                    (checkBox_AddCheckBoxAfterMD5Encrypt.Enabled&&checkBox_AddCheckBoxAfterMD5Encrypt.Checked)?textBox_StringAddedAfterMD5Encrypt.Text:string.Empty
            };
                mission.InitializeMission(paramenters);
            }
        }

        private void RefreshSensitiveWordDic()
        {
            sensitiveWordReplaceDic.Clear();
            List<SensitiveWordDictionaryCacheInfo> sensitiveWordList;
            if (CacheTool.Instance.GetCache(out sensitiveWordList) && sensitiveWordList != null)
            {
                for (int i = 0; i < sensitiveWordList.Count; i++)
                {
                    if (sensitiveWordReplaceDic.ContainsKey(sensitiveWordList[i].SensitiveWord))
                    {
                        Global.Instance.WriteGlobalLog(string.Format("第{0}个敏感词重复: {1}", i + 1, sensitiveWordList[i]));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sensitiveWordList[i].SensitiveWord))
                        {
                            Global.Instance.WriteGlobalLog(string.Format("敏感词不能为空"));
                        }
                        else if (string.IsNullOrEmpty(sensitiveWordList[i].ReplaceWord))
                        {
                            Global.Instance.WriteGlobalLog(string.Format("替换词不能为空"));
                        }
                        else if (sensitiveWordList[i].SensitiveWord == sensitiveWordList[i].ReplaceWord)
                        {
                            Global.Instance.WriteGlobalLog(string.Format("敏感词和替换词不能相同 {0}", sensitiveWordList[i].SensitiveWord));
                        }
                        else
                        {
                            sensitiveWordReplaceDic.Add(sensitiveWordList[i].SensitiveWord, sensitiveWordList[i].ReplaceWord);
                        }
                    }
                }
            }
        }

        private void checkBox_EncryptByMD5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_SplitedFolder.Enabled = ((CheckBox)sender).Checked;
            checkBox_EncodeByBase64.Enabled = ((CheckBox)sender).Checked;
            checkBox_ReplaceSensitiveWord.Enabled = checkBox_EncodeByBase64.Checked && checkBox_EncryptByMD5.Checked;
            checkBox_AddCheckBoxAfterMD5Encrypt.Enabled = ((CheckBox)sender).Checked;
        }

        private void checkBox_ExportResListInApk_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_ResListConvertToBytes.Enabled = ((CheckBox)sender).Checked;
        }

        private void checkBox_EncodeByBase64_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_ReplaceSensitiveWord.Enabled = ((CheckBox)sender).Checked && checkBox_EncryptByMD5.Checked;
        }

        private void button_SensitiveDictionary_Click(object sender, EventArgs e)
        {
            Global.Instance.OpenURL(CacheTool.Instance.GetCacheFilePath<SensitiveWordDictionaryCacheInfo>());
        }

        private void button_UploadSensitiveDictionary_Click(object sender, EventArgs e)
        {
            Global.Instance.ProcSVNCmd(CacheTool.Instance.GetCacheFilePath<SensitiveWordDictionaryCacheInfo>(), "commit", null, null, true);
        }
    }
}
