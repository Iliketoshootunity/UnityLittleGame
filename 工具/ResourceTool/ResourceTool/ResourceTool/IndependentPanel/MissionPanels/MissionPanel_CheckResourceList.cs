using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckResourceTool.NormalConnects;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using CheckResourceTool.IndependentPanel.SmallComponents;

namespace CheckResourceTool.IndependentPanel.MissionPanels
{
    class MissionPanel_CheckResourceList : MissionPanel_Base
    {
        private URLWithISLocal commonResPath;
        private URLWithISLocal independentResPath;
        private URLWithISLocal resourceList;
        private string cdnVersion;

        /// <summary>
        /// 初始化任务
        ///     参数1:通用资源目录
        ///     参数2:独立资源目录
        ///     参数3:资源列表路径
        ///     参数4:CDN版本号
        /// </summary>
        /// <param name="input"></param>
        public override void InitializeMission(params object[] input)
        {
            base.InitializeMission(input);

            //参数初始化
            if (input == null || input.Length < 4)
            {
                AppendMissionLog("初始化参数错误");
            }
            else
            {
                commonResPath = new URLWithISLocal(Global.Instance.GetRightFolderPath(input[0].ToString()), (input[0] as URLWithISLocal).IsLocal);
                independentResPath = new URLWithISLocal(Global.Instance.GetRightFolderPath(input[1].ToString()), (input[1] as URLWithISLocal).IsLocal);
                resourceList = new URLWithISLocal(Global.Instance.GetRightFilePath(input[2].ToString()), (input[2] as URLWithISLocal).IsLocal);
                cdnVersion = Global.Instance.GetRightCDNVersion(input[3].ToString());
            }

            //添加详情标签页内容
            Component_Link commonResPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top);
            commonResPathLink.LinkName = "通用资源目录";
            commonResPathLink.Link = commonResPath.URL;

            Component_Link independentResPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top);
            independentResPathLink.LinkName = "独立资源目录";
            independentResPathLink.Link = independentResPath.URL;

            Component_Link resListPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top);
            resListPathLink.LinkName = "资源列表路径";
            resListPathLink.Link = resourceList.URL;

            if (!string.IsNullOrEmpty(cdnVersion))
            {
                Component_NormalInfoPair cdnVersionInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                cdnVersionInfo.InfoName = "CDN版本号";
                cdnVersionInfo.InfoContent = cdnVersion;
            }
        }

        //任务主体
        private List<URLWithCDN> resourceURLList;
        private List<URLWithCDN>.Enumerator resourceURLListEnumerator;
        private int urlSucceedNumber = 0;
        private int urlFailedNumber = 0;
        private int urlSumNumber = 0;
        private Connection_Base connection = null;

        protected override void Thread_Starting()
        {
            base.Thread_Starting();
            resourceURLList = null;
            string resourceListLocalPath = Global.Instance.DownloadFile(resourceList);
            if (!string.IsNullOrEmpty(resourceListLocalPath))
            {
                resourceURLList = Global.Instance.ParseLocalResourceListToURL(resourceListLocalPath, commonResPath, independentResPath, cdnVersion);
            }
            urlSucceedNumber = 0;
            urlFailedNumber = 0;
            urlSumNumber = 0;
            connection = null;
            if (resourceURLList == null || resourceURLList.Count == 0)
            {
                EndMissionThread();
            }
            if (resourceURLList != null)
            {
                urlSumNumber = resourceURLList.Count;
                resourceURLListEnumerator = resourceURLList.GetEnumerator();
            }
        }

        protected override void Thread_Looping()
        {
            base.Thread_Looping();
            if (resourceURLListEnumerator.MoveNext())
            {
                URLWithCDN urlTemp = resourceURLListEnumerator.Current;
                if (urlTemp == null)
                {
                    return;
                }
                if (urlTemp.IsLocal)
                {
                    connection = new LocalConnect(urlTemp.ToString());
                }
                else
                {
                    connection = new HttpConnect(urlTemp.ToString());
                }
                connection.CheckResource(delegate (bool result, object log)
                {
                    AppendMissionLog(log);
                    if (result)
                    {
                        urlSucceedNumber++;
                    }
                    else
                    {
                        urlFailedNumber++;
                    }
                    SetMissionProgress((urlSumNumber == 0) ? (0) : ((urlFailedNumber + urlSucceedNumber) / ((float)urlSumNumber)));
                });
            }
            else
            {
                EndMissionThread();
            }
        }

        protected override void Thread_Finishing()
        {
            base.Thread_Finishing();
            AppendMissionLog(string.Format("{0}项资源已检查,{1}项资源路径失效,{2}项资源路径有效", urlSucceedNumber + urlFailedNumber, urlFailedNumber, urlSucceedNumber));
        }
    }
}
