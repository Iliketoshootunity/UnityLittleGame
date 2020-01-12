using System.Collections.Generic;
using CheckResourceTool.IndependentPanel.SmallComponents;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using LitJson;
using System.Text;
using CheckResourceTool.NormalConnects;

namespace CheckResourceTool.IndependentPanel.MissionPanels
{
    class MissionPanel_CheckConfigFiles : MissionPanel_Base
    {
        private string[] configPathArray;
        private Dictionary<string, ConfigFile> configDic;//配置文件路径-配置文件数据
        private Dictionary<string, int> pathToCheckCollection;//直接检查的文件集合(如更新列表,dll文件,SpecialRes文件,完整资源列表等)
        private Dictionary<string, int>.KeyCollection.Enumerator pathcheckEnumerator;
        private int resourceFinished = 0;
        private int resourceSucceed = 0;
        private bool checkUpdateListAtSameTime = false;
        private bool checkResourceListAtSameTime = false;

        #region 配置文件错误LOG格式
        public string ERROR_VersionListIsNull = "error: 版本列表为空! file:{0}";//配置文件版本列表为空
        public string ERROR_UpdateListIsIllegal = "error: 更新列表不合法,{0}! file:{1} version:{2} updateID:{3} updateListFileName:{4}";//更新列表不合法
        public string ERROR_MissingDefaultVersion = "error: 缺失Default版本! file:{0}";//配置文件缺失Default版本
        public string ERROR_VersionConflict = "error: 版本冲突! file:{0} version:{1}";//配置文件有重复版本
        public string ERROR_CDNVersionIllegal = "error: CDN版本号不合法! file:{0} version:{1}";//CDN版本号不合法
        public string ERROR_MissingVersionUpdateList = "error: 缺少版本更新! file:{0}";//缺少版本更新项
        public string ERROR_VersionUpdateIllegal = "error: 版本更新不合法! resVersion:{0} updateFileName:{1}";//版本更新不合法

        public string WARNING_VersionIllegal = "warning: {0}中包含如下不合法版本号:{1}";
        #endregion

        public override void InitializeMission(params object[] input)
        {
            base.InitializeMission(input);

            if (input != null && input.Length >= 2)
            {
                checkUpdateListAtSameTime = (bool)input[0];
                checkResourceListAtSameTime = (bool)input[1];
                string[] paths = (string[])input[2];
                configPathArray = new string[paths.Length];
                for (int i = 0; i < paths.Length; i++)
                {
                    Component_Link link = AddComponentInInfoTabpage<Component_Link>(10, AnchorStyles.Left, AnchorStyles.Top);
                    link.LinkName = "链接" + (i + 1).ToString();
                    link.Link = paths[i].ToString();
                    configPathArray[i] = link.Link;
                }
            }
            else
            {
                AppendMissionLog("配置文件数目为0");
            }
        }

        //任务主体
        protected override void Thread_Starting()
        {
            base.Thread_Starting();

            #region 获取配置文件
            AppendMissionLog("获取配置文件中...");
            //下载,解析配置文件
            configDic = new Dictionary<string, ConfigFile>();
            for (int i = 0; i < configPathArray.Length; i++)
            {
                string localPath = Global.Instance.DownloadFile(new URLWithISLocal(configPathArray[i], Regex.IsMatch(configPathArray[i], Global.Instance.PATTERN_IsLocalURL)));//将配置文件下载在本地
                string unEncryptedConfig = File.ReadAllText(localPath);
                unEncryptedConfig = EncryptTool.Instance.DeEncryptData(unEncryptedConfig);//解密后的数据
                ConfigFile configFile = new ConfigFile(JsonMapper.ToObject(unEncryptedConfig));
                configDic.Add(configPathArray[i], configFile);
            }
            #endregion

            #region 检查配置文件
            AppendMissionLog("开始检查配置文件...");
            //获取所有的更新列表,待检查dll文件,待检查SpecialRes.zip文件
            pathToCheckCollection = new Dictionary<string, int>();
            StringBuilder pathConnectSB = new StringBuilder();
            foreach (KeyValuePair<string, ConfigFile> pair in configDic)
            {
                ConfigFile config = pair.Value;
                string configFile = pair.Key;
                List<string> resList = new List<string>();
                if (config.versionList != null)//检查各个版本
                {
                    bool hasDefaultVersion = false;
                    List<string> illegalVersion = new List<string>();
                    List<string> legalVersion = new List<string>();
                    for (int i = 0; i < config.versionList.Count; i++)
                    {
                        Version version = config.versionList[i];
                        if (version.version == "default")
                        {
                            hasDefaultVersion = true;
                        }
                        if (version.version != null && Global.Instance.CheckVersionCodeIsLegal(version.version))//版本号合法的情况下
                        {
                            string resURL = version.resUrl + '#' + version.commonResUrl;
                            if (!resList.Contains(resURL))
                            {
                                resList.Add(resURL);
                            }
                            if (version.resUpdateList != null)//检查版本内每个小更新
                            {
                                List<int> updateidlist = new List<int>();
                                for (int j = 0; j < version.resUpdateList.Count; j++)
                                {
                                    MiniResUpdate miniUpdate = version.resUpdateList[j];
                                    if (string.IsNullOrEmpty(miniUpdate.updateFileName))//检查是否缺失更新列表名一项
                                    {
                                        AppendMissionLog(string.Format(ERROR_UpdateListIsIllegal, "更新列表名缺失", configFile, version.version, miniUpdate.updateID.ToString(), version.version));
                                    }
                                    if (miniUpdate.updateID < 3)//检查更新列表ID,不能小于3,-1代表缺少更新列表ID一项
                                    {
                                        AppendMissionLog(string.Format(ERROR_UpdateListIsIllegal, "更新列表ID不合法", configFile, version.version, miniUpdate.updateID.ToString(), version.version));
                                    }
                                    if (updateidlist.Contains(miniUpdate.updateID))//检查更新列表ID是否重复
                                    {
                                        AppendMissionLog(string.Format(ERROR_UpdateListIsIllegal, "更新列表ID重复", configFile, version.version, miniUpdate.updateID.ToString(), version.version));
                                    }
                                    //向更新列表路径内添加更新列表URL
                                    updateidlist.Add(miniUpdate.updateID);
                                    pathConnectSB.Clear();
                                    pathConnectSB.Append(version.resUrl);
                                    pathConnectSB.Append(miniUpdate.updateFileName);
                                    if (!pathToCheckCollection.ContainsKey(pathConnectSB.ToString()))
                                    {
                                        pathToCheckCollection.Add(pathConnectSB.ToString(), 1);
                                    }
                                    else
                                    {
                                        pathToCheckCollection[pathConnectSB.ToString()]++;
                                    }
                                }
                            }
                            if (!legalVersion.Contains(version.version))
                            {
                                legalVersion.Add(version.version);
                            }
                            else
                            {
                                AppendMissionLog(string.Format(ERROR_VersionConflict, configFile, version.version));
                            }
                            if (version.version != version.dllVersion && version.version != "default")//dll版本与版本号不一致时,检查是否有Assembly-CSharp.dll
                            {
                                pathConnectSB.Clear();
                                pathConnectSB.Append(version.resUrl);
                                pathConnectSB.Append("Assembly-CSharp.dll");
                                if (!pathToCheckCollection.ContainsKey(pathConnectSB.ToString()))
                                {
                                    pathToCheckCollection.Add(pathConnectSB.ToString(), 1);
                                }
                                else
                                {
                                    pathToCheckCollection[pathConnectSB.ToString()]++;
                                }
                            }
                            if (version.specialResVersion != version.version && version.version != "default")//特殊资源压缩包与版本号不一致时,检查是否有SpecialRes.zip
                            {
                                pathConnectSB.Clear();
                                pathConnectSB.Append(version.resUrl);
                                pathConnectSB.Append("SpecialRes.zip");
                                if (!pathToCheckCollection.ContainsKey(pathConnectSB.ToString()))
                                {
                                    pathToCheckCollection.Add(pathConnectSB.ToString(), 1);
                                }
                                else
                                {
                                    pathToCheckCollection[pathConnectSB.ToString()]++;
                                }
                            }
                            if (!string.IsNullOrEmpty(version.cdnVersion) && version.cdnVersion[0] != '?')
                            {
                                AppendMissionLog(string.Format(ERROR_CDNVersionIllegal, configFile, version.version));
                            }
                            //检验独立资源目录中是否含有ResourceList.txt文件
                            pathConnectSB.Clear();
                            pathConnectSB.Append(version.resUrl);
                            pathConnectSB.Append(version.resList);
                            if (!pathToCheckCollection.ContainsKey(pathConnectSB.ToString()))
                            {
                                pathToCheckCollection.Add(pathConnectSB.ToString(), 1);
                            }
                            else
                            {
                                pathToCheckCollection[pathConnectSB.ToString()]++;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(version.version))
                            {
                                illegalVersion.Add(version.version);
                            }
                            else
                            {
                                illegalVersion.Add("NULL");
                            }
                        }
                    }
                    if (!hasDefaultVersion)//没有Default版本的情况
                    {
                        AppendMissionLog(string.Format(ERROR_MissingDefaultVersion, configFile));
                    }
                    if (illegalVersion.Count > 0)//报出不合法版本号
                    {
                        StringBuilder illegalVersionSB = new StringBuilder();
                        for (int i = 0; i < illegalVersion.Count; i++)
                        {
                            illegalVersionSB.Append(illegalVersion[i]);
                            if (i != illegalVersion.Count - 1)
                            {
                                illegalVersionSB.Append(",");
                            }
                        }
                        AppendMissionLog(string.Format(WARNING_VersionIllegal, configFile, illegalVersionSB.ToString()));
                    }
                }
                else
                {
                    AppendMissionLog(string.Format(ERROR_VersionListIsNull, configFile));
                }
                if (config.resDiffToLatestVersionList != null)//检查版本更新文件
                {
                    for (int i = 0; i < config.resDiffToLatestVersionList.Count; i++)
                    {
                        VersionUpdate versionUpdate = config.resDiffToLatestVersionList[i];
                        if (!Global.Instance.CheckVersionCodeIsLegal(versionUpdate.version) || string.IsNullOrEmpty(versionUpdate.versionUpdateFileName))
                        {
                            AppendMissionLog(string.Format(ERROR_VersionUpdateIllegal, versionUpdate.version ?? "NULL", versionUpdate.versionUpdateFileName ?? "NULL"));
                        }
                    }
                }
            }
            #endregion

            #region 检查更新列表内资源
            if (checkUpdateListAtSameTime && configDic != null && configDic.Count > 0)
            {
                AppendMissionLog("向待检查列表中添加更新列表内资源...");
                SetMissionProgress(0);
                int num = 0;
                float unit = 1 / ((float)configDic.Count);
                foreach (var item in configDic)
                {
                    num++;
                    ConfigFile config = item.Value;
                    List<string> paths = new List<string>();
                    List<string> paths1 = config.GetMiniUpdateResourcePathList();
                    if (paths1 != null && paths1.Count > 0)
                    {
                        paths.AddRange(paths1);
                    }
                    List<string> paths2 = config.GetVersionUpdateResourcePathList();
                    if (paths2 != null && paths2.Count > 0)
                    {
                        paths.AddRange(paths2);
                    }
                    if (paths.Count == 0)
                    {
                        continue;
                    }
                    for (int i = 0; i < paths.Count; i++)
                    {
                        if (!pathToCheckCollection.ContainsKey(paths[i]))
                        {
                            pathToCheckCollection.Add(paths[i], 1);
                        }
                        else
                        {
                            pathToCheckCollection[paths[i]]++;
                        }
                        SetMissionProgress(num / ((float)configDic.Count) + i * unit / paths.Count);
                    }
                    SetMissionProgress(num / ((float)configDic.Count));
                }
            }
            #endregion

            #region 检查完整资源列表内资源
            if (checkResourceListAtSameTime && configDic != null && configDic.Count > 0)
            {
                AppendMissionLog("向待检查列表中添加完整资源列表内资源...");
                SetMissionProgress(0);
                int num = 0;
                float unit = 1 / ((float)configDic.Count);
                foreach (var item in configDic)
                {
                    num++;
                    ConfigFile config = item.Value;
                    List<string> paths = new List<string>();
                    List<string> paths1 = config.GetFullResourcePathList();
                    if (paths1 != null && paths1.Count > 0)
                    {
                        paths.AddRange(paths1);
                    }
                    if (paths.Count == 0)
                    {
                        continue;
                    }
                    for (int i = 0; i < paths.Count; i++)
                    {
                        if (!pathToCheckCollection.ContainsKey(paths[i]))
                        {
                            pathToCheckCollection.Add(paths[i], 1);
                        }
                        else
                        {
                            pathToCheckCollection[paths[i]]++;
                        }
                        SetMissionProgress(num / ((float)configDic.Count) + i * unit / paths.Count);
                    }
                    SetMissionProgress(num / ((float)configDic.Count));
                }
            }
            #endregion

            pathcheckEnumerator = pathToCheckCollection.Keys.GetEnumerator();
            AppendMissionLog("开始检查资源可访问性...");
            SetMissionProgress(0);
        }

        protected override void Thread_Looping()
        {
            base.Thread_Looping();
            if (pathcheckEnumerator.MoveNext())
            {
                string path = pathcheckEnumerator.Current;
                HttpConnect httpconnect = new HttpConnect(path);
                bool isLegal = false;
                bool isOver = false;
                httpconnect.CheckResource(delegate (bool result, object obj)
                {
                    isOver = true;
                    isLegal = result;
                    if (!isLegal)
                    {
                        AppendMissionLog(obj.ToString());
                    }
                });
                while (!isOver) { }
                resourceFinished++;
                if (isLegal)
                {
                    resourceSucceed++;
                }
                SetMissionProgress((resourceFinished) / ((float)pathToCheckCollection.Count));
            }
            else
            {
                AppendMissionLog(string.Format("共检查{0}项配置文件中的{1}项资源,其中{2}项资源访问出错", configDic.Count, resourceFinished, resourceFinished - resourceSucceed));
                EndMissionThread();
            }
        }

        protected override void Thread_Finishing()
        {
            base.Thread_Finishing();
        }
    }
}
