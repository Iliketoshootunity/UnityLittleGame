using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckResourceTool.IndependentPanel.SmallComponents;
using System.Windows.Forms;
using CheckResourceTool.IndependentPanel.MissionPanels;

namespace CheckResourceTool.IndependentPanel.MissionOptionSettingPanels
{
    class MissionOption_DownloadResourceInList : MissionOption_Base
    {
        private Component_URLSelect common;
        private Component_URLSelect independent;
        private Component_URLSelect resList;
        private Component_URLSelect downloadFolder;
        private Component_NormalTextBox cdnVersionInput;

        protected override void InitializeIndependentComponents()
        {
            base.InitializeIndependentComponents();

            common = AddComponentInArea<Component_URLSelect>(0, Size.Width, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            common.URLType = URLType.FolderURL;
            common.URLInfo = "通用资源目录";

            independent = AddComponentInArea<Component_URLSelect>(0, Size.Width, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            independent.URLType = URLType.FolderURL;
            independent.URLInfo = "独立资源目录";

            resList = AddComponentInArea<Component_URLSelect>(0, Size.Width, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            resList.URLType = URLType.FileURL;
            resList.URLInfo = "资源列表路径";

            downloadFolder = AddComponentInArea<Component_URLSelect>(0, Size.Width, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            downloadFolder.URLType = URLType.SavingFolderURL;
            downloadFolder.URLInfo = "保存目录";

            cdnVersionInput = AddComponentInArea<Component_NormalTextBox>(0, AnchorStyles.Left, AnchorStyles.Top);
            cdnVersionInput.TextBoxName = "CDN版本号";
        }

        public override void CreateNewMission()
        {
            base.CreateNewMission();
            MissionPanel_DownloadResourceInList mission = Global.Instance.MainForm.CreateNewMissionPanel<MissionPanel_DownloadResourceInList>();
            mission.MissionType = "下载列表内资源";
            mission.MissionBriefInformation = GetBriefInformation();
            mission.InitializeMission(new URLWithISLocal(common.URL, common.IsLocal), new URLWithISLocal(independent.URL, independent.IsLocal), new URLWithISLocal(resList.URL, resList.IsLocal), new URLWithISLocal(downloadFolder.URL, downloadFolder.IsLocal), cdnVersionInput.TextBoxContent);
        }
    }
}
