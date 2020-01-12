using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using System.IO;

namespace CheckResourceTool.IndependentPanel.MissionPanels
{
    //本地/远程路径相关
    public class URLWithCDN
    {
        private string url = string.Empty;
        private bool isLocal = true;
        private string cdnVersion = string.Empty;
        private string realURL = string.Empty;

        public bool IsLocal { get { return isLocal; } }

        public URLWithCDN(string url, string cdnVersion, bool isLocal)
        {
            this.url = url;
            if (!string.IsNullOrEmpty(cdnVersion) && cdnVersion.Length > 0 && cdnVersion[0] != '?')
            {
                cdnVersion = '?' + cdnVersion;
            }
            this.cdnVersion = cdnVersion;
            this.isLocal = isLocal;
        }

        public URLWithCDN(string url)
        {
            this.url = url;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(realURL))
            {
                realURL = (isLocal) ? (url) : (url + cdnVersion);
            }
            return realURL;
        }
    }

    public class URLWithISLocal
    {
        private string url;
        public string URL { get { return url; } }

        private bool isLocal;
        public bool IsLocal { get { return isLocal; } }

        public URLWithISLocal(string url, bool isLocal)
        {
            this.url = url;
            this.isLocal = isLocal;
        }

        public override string ToString()
        {
            return url;
        }
    }

    //配置文件相关
    public class ConfigFile
    {
        public string URL = null;
        public List<Version> versionList = null;
        public List<VersionUpdate> resDiffToLatestVersionList = null;

        private Dictionary<string, int> fullResourcePathList;//完整资源列表内的资源地址
        private Dictionary<string, int> miniResourcePathList;//小更新列表内的资源地址
        private Dictionary<string, int> versionResourcePathList;//版本更新列表内的资源地址

        public ConfigFile(JsonData configData)
        {
            if (configData == null || configData.Count == 0)
            {
                return;
            }
            if (configData.Keys.Contains("URL"))
            {
                URL = configData["URL"].ToString();
            }
            if (configData.Keys.Contains("VersionList"))
            {
                JsonData versionData = configData["VersionList"];
                if (versionData != null && versionData.Count > 0)
                {
                    versionList = new List<Version>();
                    for (int i = 0; i < versionData.Count; i++)
                    {
                        versionList.Add(new Version(versionData[i]));
                    }
                }
            }
            if (configData.Keys.Contains("ResDifToLatestVersion"))
            {
                JsonData versionUpdateData = configData["ResDifToLatestVersion"];
                if (versionUpdateData != null && versionUpdateData.Count > 0)
                {
                    resDiffToLatestVersionList = new List<VersionUpdate>();
                    for (int i = 0; i < versionUpdateData.Count; i++)
                    {
                        resDiffToLatestVersionList.Add(new VersionUpdate(versionUpdateData[i]));
                    }
                }
            }
        }

        /// <summary>
        /// 获取完整资源列表内的单个资源地址列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFullResourcePathList()
        {
            if (fullResourcePathList == null)
            {
                fullResourcePathList = new Dictionary<string, int>();
                try
                {
                    List<string> completeResourceList = new List<string>();
                    for (int i = 0; i < versionList.Count; i++)
                    {
                        string completeResListPath = versionList[i].resUrl + versionList[i].resList;//完整资源列表路径
                        if (!completeResourceList.Contains(completeResListPath))//避免重复读取完整资源列表路径
                        {
                            completeResourceList.Add(completeResListPath);
                            string tempPath = Global.Instance.DownloadFile(new URLWithISLocal(completeResListPath, false));
                            if (string.IsNullOrEmpty(tempPath))
                            {
                                continue;
                            }
                            string[] lines = File.ReadAllLines(tempPath);
                            for (int j = 0; j < lines.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(lines[j]))
                                {
                                    string path = Global.Instance.GetResourceFullPath(lines[j], versionList[i].commonResUrl, versionList[i].resUrl);
                                    if (!fullResourcePathList.ContainsKey(path))//避免列表中包含相同资源
                                    {
                                        fullResourcePathList.Add(path, 1);
                                    }
                                    else
                                    {
                                        fullResourcePathList[path]++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception) { fullResourcePathList?.Clear(); fullResourcePathList = null; return null; }
            }
            return fullResourcePathList.Keys.ToList();
        }

        /// <summary>
        /// 获取小更新资源列表内的单个资源地址列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetMiniUpdateResourcePathList()
        {
            if (miniResourcePathList == null)
            {
                miniResourcePathList = new Dictionary<string, int>();
                try
                {
                    List<string> miniUpdateList = new List<string>();
                    for (int i = 0; i < versionList.Count; i++)
                    {
                        Version version = versionList[i];
                        if (version.resUpdateList != null)
                        {
                            for (int j = 0; j < version.resUpdateList.Count; j++)
                            {
                                string miniUpdateFile = version.resUrl + version.resUpdateList[j].updateFileName;
                                if (!miniUpdateList.Contains(miniUpdateFile))//避免读取重复的小更新列表
                                {
                                    miniUpdateList.Add(miniUpdateFile);
                                    string miniupdateLocalPath = Global.Instance.DownloadFile(new URLWithISLocal(miniUpdateFile, false));
                                    if (string.IsNullOrEmpty(miniupdateLocalPath))
                                    {
                                        continue;
                                    }
                                    string[] lines = File.ReadAllLines(miniupdateLocalPath);
                                    for (int k = 0; k < lines.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(lines[k]))
                                        {
                                            string path = Global.Instance.GetResourceFullPath(lines[k], version.commonResUrl, version.resUrl);
                                            if (!miniResourcePathList.ContainsKey(path))//避免列表中包含相同资源
                                            {
                                                miniResourcePathList.Add(path, 1);
                                            }
                                            else
                                            {
                                                miniResourcePathList[path]++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception) { miniResourcePathList?.Clear(); miniResourcePathList = null; return null; }
            }
            return miniResourcePathList.Keys.ToList();
        }

        /// <summary>
        /// 获取版本更新资源列表内的单个资源地址列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetVersionUpdateResourcePathList()
        {
            if (versionResourcePathList == null)
            {
                versionResourcePathList = new Dictionary<string, int>();
                try
                {
                    List<string> versionUpdateList = new List<string>();
                    if (resDiffToLatestVersionList != null)
                    {
                        for (int i = 0; i < resDiffToLatestVersionList.Count; i++)
                        {
                            VersionUpdate versionupdate = resDiffToLatestVersionList[i];
                            for (int j = 0; j < versionList.Count; j++)
                            {
                                string versionUpdateFilePath = versionList[j].resUrl + versionupdate.versionUpdateFileName;
                                if (!versionUpdateList.Contains(versionUpdateFilePath))
                                {
                                    versionUpdateList.Add(versionUpdateFilePath);
                                    string temp = Global.Instance.DownloadFile(new URLWithISLocal(versionUpdateFilePath, false));
                                    if (string.IsNullOrEmpty(temp))
                                    {
                                        continue;
                                    }
                                    string[] lines = File.ReadAllLines(temp);
                                    for (int k = 0; k < lines.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(lines[k]))
                                        {
                                            string path = Global.Instance.GetResourceFullPath(lines[k], versionList[j].commonResUrl, versionList[j].resUrl);
                                            if (!versionResourcePathList.ContainsKey(path))
                                            {
                                                versionResourcePathList.Add(path, 1);
                                            }
                                            else
                                            {
                                                versionResourcePathList[path]++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception) { versionResourcePathList?.Clear(); versionResourcePathList = null; return null; }
            }
            return versionResourcePathList.Keys.ToList();
        }
    }

    public class Version
    {
        public string version = null;
        public string newVersion = null;
        public string dllVersion = null;
        public string specialResVersion = null;
        public string resVersion = null;
        public string resUrl = null;
        public string commonResUrl = null;
        public string resList = null;
        public string cdnVersion = null;
        public List<MiniResUpdate> resUpdateList = null;

        public Version(JsonData versionData)
        {
            if (versionData == null || versionData.Count == 0)
            {
                return;
            }
            if (versionData.Keys.Contains("version"))
            {
                version = versionData["version"].ToString();
            }
            if (versionData.Keys.Contains("newVersion"))
            {
                newVersion = versionData["newVersion"].ToString();
            }
            if (versionData.Keys.Contains("dllVersion"))
            {
                dllVersion = versionData["dllVersion"].ToString();
            }
            if (versionData.Keys.Contains("specialResVersion"))
            {
                specialResVersion = versionData["specialResVersion"].ToString();
            }
            if (versionData.Keys.Contains("resVersion"))
            {
                resVersion = versionData["resVersion"].ToString();
            }
            if (versionData.Keys.Contains("resUrl"))
            {
                resUrl = versionData["resUrl"].ToString();
            }
            if (versionData.Keys.Contains("commonResUrl"))
            {
                commonResUrl = versionData["commonResUrl"].ToString();
            }
            if (versionData.Keys.Contains("resList"))
            {
                resList = versionData["resList"].ToString();
            }
            if (versionData.Keys.Contains("cdnVersion"))
            {
                cdnVersion = versionData["cdnVersion"].ToString();
            }
            if (versionData.Keys.Contains("resUpdate"))
            {
                JsonData jsonData = versionData["resUpdate"];
                if (jsonData != null && jsonData.Count > 0)
                {
                    resUpdateList = new List<MiniResUpdate>();
                    for (int i = 0; i < jsonData.Count; i++)
                    {
                        resUpdateList.Add(new MiniResUpdate(jsonData[i]));
                    }
                }
            }
        }
    }

    public class MiniResUpdate
    {
        public int updateID = -1;
        public string updateFileName = null;

        public MiniResUpdate(JsonData miniJsondata)
        {
            if (miniJsondata == null || miniJsondata.Count == 0)
            {
                return;
            }
            if (miniJsondata.Keys.Contains("update") && miniJsondata["update"].IsInt)
            {
                updateID = int.Parse(miniJsondata["update"].ToString());
            }
            if (miniJsondata.Keys.Contains("updateFileName"))
            {
                updateFileName = miniJsondata["updateFileName"].ToString();
            }
        }
    }

    public class VersionUpdate
    {
        public string version = null;
        public string versionUpdateFileName = null;

        public VersionUpdate(JsonData versionUpdateData)
        {
            if (versionUpdateData == null || versionUpdateData.Count == 0)
            {
                return;
            }
            if (versionUpdateData.Keys.Contains("resVersion"))
            {
                version = versionUpdateData["resVersion"].ToString();
            }
            if (versionUpdateData.Keys.Contains("updateFileName"))
            {
                versionUpdateFileName = versionUpdateData["updateFileName"].ToString();
            }
        }
    }
}
