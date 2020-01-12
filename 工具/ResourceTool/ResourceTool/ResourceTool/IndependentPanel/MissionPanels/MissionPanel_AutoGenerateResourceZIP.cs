using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckResourceTool.IndependentPanel.SmallComponents;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace CheckResourceTool.IndependentPanel.MissionPanels
{
    class MissionPanel_AutoGenerateResourceZIP : MissionPanel_Base
    {
        private string assetFolderPath = string.Empty;//Asset文件夹路径
        private string outputPath = string.Empty;//导出路径
        private string resListInApkPath = string.Empty;//ResListInApk.txt路径
        private string firstListPath = string.Empty;//FirstList路径
        private string specialResListPath = string.Empty;//SpecialResList路径
        private string independentResListPath = string.Empty;//独立资源列表路径
        private string predownloadListPath = string.Empty;//预下载资源列表路径
        private string resourceOrderPath = string.Empty;//ResourceOrder.txt路径
        private string platform = string.Empty;//平台iOS/Android
        private string channel = string.Empty;//渠道名
        private string zipPostFix = string.Empty;//zip包的后缀
        private string additionalAssetPath = string.Empty;//附加的资源目录
        private string[] additionalUnityPackagePaths = null;//附加的unitypackage包路径
        private string unityPath = string.Empty;//使用的unity.exe路径
        private string projectAssetPath = string.Empty;//工程路径
        private float rubbishSize = -1;//单包垃圾资源大小
        private bool isExportZips = false;//导出资源压缩包
        private bool isExportCompleteList = false;//导出完整资源列表
        private bool isExportResListInApk = false;//导出包内资源列表
        private bool isEncryptResList = false;//资源列表加密
        private bool isForcePackage = false;//强制打包界面资源
        private string packageNameLetters = string.Empty;//包名首字母
        private bool isEncryptMD5 = false;//资源使用md5加密
        private string packageZIPPassword = string.Empty;//资源zip包密码
        private bool isSplitedFolder = false;//md5加密的情况下,资源文件夹进行分层加密
        private bool isFileRelativePathEncodedByBase64 = false;//md5加密的情况下,加密后路径再经过一次Base64加密
        private int randomForwardByteArrayNumber = 0;//前置随机字节数组长度
        private bool exportResListInAPKByBytes = false;//导出包内资源列表的二进制文件
        private bool needReplaceSensitiveWithBase64 = false;//Base64加密时,需要对路径进行敏感词替换
        private Dictionary<string, string> sensitiveWordReplaceDic = new Dictionary<string, string>();//敏感词字典
        private string stringAddedAfterMD5Encrypt = string.Empty;//MD5加密后添加的路径

        private string importUnityPackageToUnity = @"-quit -batchmode -nographics -projectPath {0} -importPackage {1}";
        private string generateAssetBundleForUnity = @"-quit -batchmode -nographics -projectPath {0} -executeMethod {1} -logFile {2}";
        private Process unityProcess;

        public override void InitializeMission(params object[] input)
        {
            base.InitializeMission(input);

            if (input == null || input.Length < 31)
            {
                AppendMissionLog("初始化参数错误");
            }
            else
            {
                assetFolderPath = input[0].ToString();//Asset文件夹目录
                outputPath = input[1].ToString();//导出目录
                resListInApkPath = input[2].ToString();//包内资源列表路径
                firstListPath = input[3].ToString();//first包内资源列表路径
                specialResListPath = input[4].ToString();//specialRes包内资源列表路径
                independentResListPath = input[5].ToString();//独立资源列表路径
                predownloadListPath = input[6].ToString();//预下载资源列表路径
                resourceOrderPath = input[7].ToString();//资源顺序列表路径
                platform = input[8].ToString();//平台
                channel = input[9].ToString();//渠道
                zipPostFix = input[10].ToString();//zip包后缀
                additionalAssetPath = input[11].ToString();//附加资源目录
                additionalUnityPackagePaths = input[12].ToString().Split(';');//附加unitypackage包路径
                for (int i = 0; i < additionalUnityPackagePaths.Length; i++)
                {
                    additionalUnityPackagePaths[i] = additionalUnityPackagePaths[i].Trim();
                }
                float.TryParse(input[13].ToString(), out rubbishSize);//单包垃圾资源大小
                isExportZips = (bool)input[14];//导出资源压缩包
                isExportCompleteList = (bool)input[15];//导出完整资源列表
                isExportResListInApk = (bool)input[16];//导出包内资源列表
                isEncryptResList = (bool)input[17];//导出的资源列表加密
                packageNameLetters = input[18].ToString();//包名首字母
                isEncryptMD5 = (bool)input[19];//资源使用md5加密
                packageZIPPassword = input[20].ToString();//资源zip压缩包密码
                unityPath = input[21].ToString();//unity.exe路径
                projectAssetPath = input[22].ToString();//工程路径
                isForcePackage = (bool)input[23];//强制打包界面
                isSplitedFolder = (bool)input[24];//文件夹分层
                isFileRelativePathEncodedByBase64 = (bool)input[25];//Base64加密
                int.TryParse(input[26].ToString(), out randomForwardByteArrayNumber);//前置随机数组长度
                exportResListInAPKByBytes = (bool)input[27];//导出包内资源列表的二进制文件
                needReplaceSensitiveWithBase64 = (bool)input[28];//Base64加密时需要替换敏感词
                sensitiveWordReplaceDic = (Dictionary<string, string>)input[29];//敏感词替换字典
                stringAddedAfterMD5Encrypt = (string)input[30];//MD5加密后需要在路径尾部添加的字符串
            }

            //添加详情标签页内容
            if (isForcePackage)
            {
                Component_Info isForcePackageInfo = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                isForcePackageInfo.Information = "强制打包界面资源";
            }

            if (isExportZips)
            {
                Component_Info isExportZipsInfo = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                isExportZipsInfo.Information = "导出资源压缩包";
            }

            if (isEncryptMD5)
            {
                Component_Info isEncryptMD5Info = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                isEncryptMD5Info.Information = "(资源包使用MD5方式加密)";

                if (isSplitedFolder)
                {
                    Component_Info isSplitedFolder = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                    isSplitedFolder.Information = "(文件夹分层)";
                }
            }

            if (isExportCompleteList)
            {
                Component_Info isExportCompleteListInfo = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                isExportCompleteListInfo.Information = "导出完整资源列表";
            }

            if (isExportResListInApk)
            {
                Component_Info isExportResListInApkInfo = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                isExportResListInApkInfo.Information = "导出包内资源列表";
            }

            if (isEncryptResList)
            {
                Component_Info isEncryptResListInfo = AddComponentInInfoTabpage<Component_Info>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                isEncryptResListInfo.Information = "加密资源列表";
            }

            Component_Link assetFolderPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            assetFolderPathLink.LinkName = "资源文件夹目录";
            assetFolderPathLink.Link = assetFolderPath;

            if (!string.IsNullOrEmpty(projectAssetPath))
            {
                Component_Link projectFolderPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                projectFolderPathLink.LinkName = "工程文件夹目录";
                projectFolderPathLink.Link = projectAssetPath;
            }

            Component_Link outputPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            outputPathLink.LinkName = "导出目录";
            outputPathLink.Link = outputPath;

            Component_Link resListInApkPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            resListInApkPathLink.LinkName = "包内资源列表";
            resListInApkPathLink.Link = resListInApkPath;

            Component_Link firstListPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            firstListPathLink.LinkName = "First资源列表";
            firstListPathLink.Link = firstListPath;

            Component_Link specialResListPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            specialResListPathLink.LinkName = "Special资源列表";
            specialResListPathLink.Link = specialResListPath;

            Component_Link independentResListPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            independentResListPathLink.LinkName = "独立资源目录";
            independentResListPathLink.Link = independentResListPath;

            Component_Link resourceOrderPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            resourceOrderPathLink.LinkName = "资源顺序列表";
            resourceOrderPathLink.Link = resourceOrderPath;

            Component_NormalInfoPair platformInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
            platformInfo.InfoName = "平台";
            platformInfo.InfoContent = platform;

            if (!string.IsNullOrEmpty(channel))
            {
                Component_NormalInfoPair channelInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                channelInfo.InfoName = "渠道";
                channelInfo.InfoContent = platform;
            }

            if (!string.IsNullOrEmpty(zipPostFix))
            {
                Component_NormalInfoPair zippostfixInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                zippostfixInfo.InfoName = "zip后缀";
                zippostfixInfo.InfoContent = zipPostFix;
            }

            if (!string.IsNullOrEmpty(unityPath))
            {
                Component_Link unityPathLink = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                unityPathLink.LinkName = "unity.exe路径";
                unityPathLink.Link = unityPath;
            }

            if (!string.IsNullOrEmpty(additionalAssetPath))
            {
                Component_Link additionalAssetPathInfo = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                additionalAssetPathInfo.LinkName = "附加资源目录";
                additionalAssetPathInfo.Link = additionalAssetPath;
            }

            if (additionalUnityPackagePaths != null && additionalUnityPackagePaths.Length > 0)
            {
                for (int i = 0; i < additionalUnityPackagePaths.Length; i++)
                {
                    if (string.IsNullOrEmpty(additionalUnityPackagePaths[i]))
                    {
                        continue;
                    }
                    Component_Link additionalUnityPackagePathInfo = AddComponentInInfoTabpage<Component_Link>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                    additionalUnityPackagePathInfo.LinkName = "附加资源包";
                    additionalUnityPackagePathInfo.Link = additionalUnityPackagePaths[i];
                }
            }

            if (rubbishSize > 0)
            {
                Component_NormalInfoPair rubbishSizeInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                rubbishSizeInfo.InfoName = "添加的垃圾资源大小";
                rubbishSizeInfo.InfoContent = rubbishSize.ToString();
            }

            if (!string.IsNullOrEmpty(packageNameLetters))
            {
                Component_NormalInfoPair packageNameLettersInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                packageNameLettersInfo.InfoName = "包名首字母";
                packageNameLettersInfo.InfoContent = packageNameLetters;
            }

            if (!string.IsNullOrEmpty(packageZIPPassword))
            {
                Component_NormalInfoPair packageZIPPasswordInfo = AddComponentInInfoTabpage<Component_NormalInfoPair>(0, AnchorStyles.Left, AnchorStyles.Top, AnchorStyles.Right);
                packageZIPPasswordInfo.InfoName = "zip压缩密码";
                packageZIPPasswordInfo.InfoContent = packageZIPPassword;
            }
        }

        ~MissionPanel_AutoGenerateResourceZIP()
        {
            unityProcess?.Close();
        }

        //任务线程
        private StringBuilder missionOrderStringBuilder = new StringBuilder();
        private bool missionSucceed = false;//任务成功结束
        private bool isImportUnityPackage = false;
        private string uiassetFolder;
        private string interfaceFolder;

        protected override void Thread_Starting()
        {
            base.Thread_Starting();
        }

        protected override void Thread_Looping()
        {
            //本任务中线程并不循环
            base.Thread_Looping();

            missionSucceed = false;

            #region 需要向unity工程中导入unitypackage包并打包界面资源
            SetMissionProgress(0);
            uiassetFolder = Global.Instance.GetRightFolderPath(projectAssetPath) + "UIAsset";//工程中UIAsset路径
            interfaceFolder = Global.Instance.GetRightFolderPath(assetFolderPath) + platform;//资源目录的iOS或者Android文件夹
            isImportUnityPackage = false;
            if (!string.IsNullOrEmpty(unityPath) && File.Exists(unityPath) && !string.IsNullOrEmpty(uiassetFolder) && Directory.Exists(uiassetFolder) && (!Global.Instance.IsStringArrayEmptyOrNull(additionalUnityPackagePaths) || isForcePackage))
            {
                SetMissionProgress(0);
                isImportUnityPackage = true;
                string projectPath = projectAssetPath.Replace("\\Assets", string.Empty);

                //将工程的Asset文件夹的UIAsset文件夹更新到最新
                AppendMissionLog("更新UIAsset文件夹");
                Global.Instance.ProcSVNCmd(uiassetFolder, "update", OnReceiveData, OnReceiveData);
                AppendMissionLog("UIAsset文件夹更新完毕");
                SetMissionProgress(0.2f);

                //unity.exe命令行打开该工程并向工程中导入unitypackage包
                if (additionalUnityPackagePaths != null)
                {
                    for (int i = 0; i < additionalUnityPackagePaths.Length; i++)
                    {
                        if (string.IsNullOrEmpty(additionalUnityPackagePaths[i]))
                        {
                            continue;
                        }
                        if (!File.Exists(additionalUnityPackagePaths[i]))
                        {
                            AppendMissionLog(additionalUnityPackagePaths[i] + "  unitypackage包不存在");
                            continue;
                        }
                        AppendMissionLog("向工程中导入unitypackage");
                        Process importingProcess = Global.Instance.RunProcessInBackground(unityPath, string.Format(importUnityPackageToUnity, projectPath, additionalUnityPackagePaths[i]), OnReceiveData, OnReceiveData);
                        importingProcess.WaitForExit();
                        AppendMissionLog("导入完毕");
                        SetMissionProgress(0.2f + 0.2f * (i + 1) / additionalUnityPackagePaths.Length);
                    }
                }

                //分别执行工程中的静态函数DoAssetbundle.ClearAssetBundlesName; DoAssetbundle.SetMainAssetBundleName; DoAssetbundle.CreateAllAssetBundles;
                string logTempFile = Global.Instance.GetRandomTempFilePath().Replace(" ", string.Empty) + "-log.txt";

                AppendMissionLog("开启工程进程,清除所有AB包名字");
                Process clearProcess = Global.Instance.RunProcessInBackground(unityPath, string.Format(generateAssetBundleForUnity, projectPath, "DoAssetbundle.ClearAssetBundlesName", logTempFile), OnReceiveData, OnReceiveData);
                clearProcess.WaitForExit();
                clearProcess.Close();
                AppendMissionLog("清除完毕");
                SetMissionProgress(0.6f);

                AppendMissionLog("开启工程进程,设置AB包名称");
                Process setNameProcess = Global.Instance.RunProcessInBackground(unityPath, string.Format(generateAssetBundleForUnity, projectPath, "DoAssetbundle.SetMainAssetBundleName", logTempFile), OnReceiveData, OnReceiveData);
                setNameProcess.WaitForExit();
                setNameProcess.Close();
                AppendMissionLog("清除完毕");
                SetMissionProgress(0.8f);

                AppendMissionLog("开启工程进程,执行界面打包函数");
                unityProcess = Global.Instance.RunProcessInBackground(unityPath, string.Format(generateAssetBundleForUnity, projectPath, "DoAssetbundle.CreateAllAssetBundles", logTempFile), OnReceiveData, OnReceiveData);
                string[] logLines;
                string newLine = "\r\n";
                bool isStartTemp = false;
                FileStream logFS = null;
                StreamReader logFSSR = null;
                StringBuilder sbTemp = new StringBuilder();
                while (true)
                {
                    Thread.Sleep(100);
                    try
                    {
                        if (!isStartTemp)
                        {
                            if (!File.Exists(logTempFile))
                            {
                                continue;
                            }
                            isStartTemp = true;
                        }
                        logFS = new FileStream(logTempFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        logFSSR = new StreamReader(logFS);
                        sbTemp.Append(logFSSR.ReadLine());
                        sbTemp.Append(newLine);
                        logLines = sbTemp.ToString().Split(new string[] { newLine }, StringSplitOptions.None);
                    }
                    catch (Exception)
                    {
                        //AppendMissionLog(ex.Message);
                        continue;
                    }
                    logFS.Close();
                    logFSSR.Close();
                    SetMissionProgress((logLines.Length / 7600f) * 0.2f + 0.8f);
                    if (!unityProcess.HasExited)
                    {
                        continue;
                    }
                    break;
                }
                unityProcess.WaitForExit();
                unityProcess?.Close();
                if (File.Exists(logTempFile))
                {
                    logLines = File.ReadAllLines(logTempFile);
                    if (logLines.Contains("Exiting batchmode successfully now!"))
                    {
                        AppendMissionLog("unity界面打包完毕");
                    }
                    else if (logLines.Contains("Multiple Unity instances cannot open the same project."))
                    {
                        AppendMissionLog(projectPath + "已打开");
                        MessageBox.Show("请先关闭工程: " + projectPath);
                        EndMissionThread();
                        return;
                    }
                }
                else
                {
                    AppendMissionLog("界面资源未打包");
                }
                SetMissionProgress(1);
            }
            #endregion

            #region 指令准备
            missionOrderStringBuilder.Clear();

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(assetFolderPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(outputPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(resListInApkPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(firstListPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(specialResListPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(independentResListPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(predownloadListPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(resourceOrderPath.Replace('\\', '/'));
            missionOrderStringBuilder.Append('\"');
            missionOrderStringBuilder.Append(' ');

            missionOrderStringBuilder.Append("PrintProgress");
            missionOrderStringBuilder.Append(' ');

            if (isExportZips)
            {
                missionOrderStringBuilder.Append("7-zipPath:");
                missionOrderStringBuilder.Append('\"');
                missionOrderStringBuilder.Append(ExternTool.Instance._7zPath.Replace('\\', '/'));
                missionOrderStringBuilder.Append('\"');
                missionOrderStringBuilder.Append(' ');

                if (!string.IsNullOrEmpty(zipPostFix))
                {
                    missionOrderStringBuilder.Append("ZipPostfix:");
                    missionOrderStringBuilder.Append(zipPostFix);
                    missionOrderStringBuilder.Append(' ');
                }

                if (rubbishSize > 0)
                {
                    missionOrderStringBuilder.Append("RubbishSumsizePerZip:");
                    missionOrderStringBuilder.Append(rubbishSize);
                    missionOrderStringBuilder.Append(' ');
                }

                if (!string.IsNullOrEmpty(packageZIPPassword))
                {
                    missionOrderStringBuilder.Append("ZipPassword:");
                    missionOrderStringBuilder.Append(packageZIPPassword);
                    missionOrderStringBuilder.Append(' ');
                }

                if (!string.IsNullOrEmpty(additionalAssetPath))
                {
                    missionOrderStringBuilder.Append("AdditionalResDir:");
                    missionOrderStringBuilder.Append('\"');
                    missionOrderStringBuilder.Append(additionalAssetPath);
                    missionOrderStringBuilder.Append('\"');
                    missionOrderStringBuilder.Append(' ');
                }

                if (isEncryptMD5)
                {
                    missionOrderStringBuilder.Append("ResourceEncrypted:");
                    missionOrderStringBuilder.Append('\"');
                    missionOrderStringBuilder.Append(packageNameLetters);
                    missionOrderStringBuilder.Append('\"');
                    missionOrderStringBuilder.Append(' ');
                }
            }

            if (isExportResListInApk)
            {
                missionOrderStringBuilder.Append("SubListOutput");
                missionOrderStringBuilder.Append(' ');
            }

            if (isExportCompleteList)
            {
                missionOrderStringBuilder.Append("ResourceListOutput");
                missionOrderStringBuilder.Append(' ');
            }

            if (isEncryptResList)
            {
                missionOrderStringBuilder.Append("EncryptResList");
                missionOrderStringBuilder.Append(' ');
            }

            if (isSplitedFolder)
            {
                missionOrderStringBuilder.Append("SplitedSavingPath_2_12_8_10");
                missionOrderStringBuilder.Append(' ');
            }

            if (isFileRelativePathEncodedByBase64)
            {
                missionOrderStringBuilder.Append("EncodedByBase64");
                missionOrderStringBuilder.Append(' ');
            }

            if (randomForwardByteArrayNumber > 0)
            {
                missionOrderStringBuilder.Append("ForwardBytesNumber:");
                missionOrderStringBuilder.Append(randomForwardByteArrayNumber);
                missionOrderStringBuilder.Append(' ');
            }

            if (exportResListInAPKByBytes)
            {
                missionOrderStringBuilder.Append("AddResListTable:");
                missionOrderStringBuilder.Append('\"' + Global.Instance.ResListInAPKPath + '\"');
                missionOrderStringBuilder.Append(' ');
            }

            if (needReplaceSensitiveWithBase64)
            {
                missionOrderStringBuilder.Append("NeedReplaceSensitiveWithBase64:");
                missionOrderStringBuilder.Append(GetSensitiveWordString(sensitiveWordReplaceDic));
                missionOrderStringBuilder.Append(' ');
            }

            if (!string.IsNullOrEmpty(stringAddedAfterMD5Encrypt))
            {
                missionOrderStringBuilder.Append("AddStringAfterMD5Encrypt:");
                missionOrderStringBuilder.Append(stringAddedAfterMD5Encrypt);
                missionOrderStringBuilder.Append(' ');
            }
            #endregion

            #region 发送指令
            AppendMissionLog("发送指令: " + missionOrderStringBuilder.ToString() + " 至 " + ExternTool.Instance._AutoGenerateResourceZIPsPath);
            Process autoGenerateRes = Global.Instance.RunProcessInBackground(ExternTool.Instance._AutoGenerateResourceZIPsPath, missionOrderStringBuilder.ToString(), OnReceiveData, OnReceiveData);
            autoGenerateRes.WaitForExit();
            autoGenerateRes?.Close();
            #endregion

            EndMissionThread();
        }

        private string GetSensitiveWordString(Dictionary<string, string> dic)
        {
            StringBuilder sbtemp = new StringBuilder(30);
            foreach (var item in dic)
            {
                sbtemp.Append(item.Key);
                sbtemp.Append("=>");
                sbtemp.Append(item.Value);
                sbtemp.Append("|||");
            }
            return sbtemp.ToString();
        }

        protected override void Thread_Finishing()
        {
            base.Thread_Finishing();
            if (isImportUnityPackage)
            {
                #region 将工程和资源目录的界面资源回退至SVN当前版本
                if (isImportUnityPackage)
                {
                    //回退工程UIAsset文件夹下的资源至SVN版本
                    Global.Instance.ProcSVNCmd(uiassetFolder, "revert", OnReceiveData, OnReceiveData, false);
                    if (!string.IsNullOrEmpty(interfaceFolder) && Directory.Exists(interfaceFolder))
                    {
                        //回退资源目录的界面资源至SVN版本
                        Global.Instance.ProcSVNCmd(interfaceFolder, "revert", OnReceiveData, OnReceiveData, false);
                    }
                }
                #endregion
            }
            Console.WriteLine((missionSucceed) ? ("任务完成") : ("任务失败"));
            Global.Instance.OpenURL(outputPath);//任务结束时打开输出文件夹
        }

        private void OnReceiveData(object sender, DataReceivedEventArgs e)
        {
            DealProcessCallbackString(e.Data);
        }

        private void DealProcessCallbackString(string callback)
        {
            if (string.IsNullOrEmpty(callback))
            {
                return;
            }
            if (callback == "Finished")
            {
                missionSucceed = true;
                EndMissionThread();
                return;
            }
            if (callback == "Failed")
            {
                missionSucceed = false;
                EndMissionThread();
                return;
            }
            if (Regex.IsMatch(callback, @"^\d+(.\d+)*%"))
            {
                float progress;
                callback = callback.Replace("%", "").Trim();
                if (float.TryParse(callback.Replace("%", ""), out progress))
                {
                    SetMissionProgress(progress / 100f);
                }
            }
            else
            {
                AppendMissionLog(callback);
            }
        }
    }
}
