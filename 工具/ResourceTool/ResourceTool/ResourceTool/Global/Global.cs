using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using CheckResourceTool.IndependentPanel.MissionPanels;
using System.Text.RegularExpressions;

namespace CheckResourceTool
{
    public enum MissionType
    {
        资源自动打包,
        检验资源列表,
        下载列表资源,
        检验配置文件,
    }

    public enum URLType
    {
        FileURL,
        FolderURL,
        SavingFileURL,
        SavingFolderURL,
        LocalFileURL,
        LocalFolderURL,
        RemoteFileURL,
        RemoteFolderURL
    }

    public class Global
    {
        private static Global _Instance;
        public static Global Instance { get { if (_Instance == null) _Instance = new Global(); return _Instance; } }

        #region 全局变量
        private MainForm _MainForm;
        public MainForm MainForm { get { return _MainForm; } set { if (_MainForm != null) return; _MainForm = value; } }

        public List<object> MissionOptionList { get { return MainForm.createMissionArea.MissionOptions; } }

        private List<MissionPanel_Base> missionList;
        public List<MissionPanel_Base> MissionList { get { missionList = missionList ?? new List<MissionPanel_Base>(); return missionList; } }

        private string _TempDirectory = string.Empty;
        public string TempDirectory
        {
            get
            {
                if (_TempDirectory == string.Empty) _TempDirectory = Directory.GetCurrentDirectory().Replace('\\', '/') + "/Temp/";
                if (!Directory.Exists(_TempDirectory))
                {
                    DirectoryInfo info = Directory.CreateDirectory(_TempDirectory);
                    info.Attributes = FileAttributes.Hidden;
                }
                return _TempDirectory;
            }
        }

        private string _FixedDirectory = string.Empty;
        public string FixedDirectory
        {
            get
            {
                if (_FixedDirectory == string.Empty) _FixedDirectory = Directory.GetCurrentDirectory().Replace('\\', '/') + "/Fixed/";
                if (!Directory.Exists(_FixedDirectory))
                {
                    Directory.CreateDirectory(_FixedDirectory);
                }
                return _FixedDirectory;
            }
        }

        private string[] missionTypes;
        public string[] MissionTypes
        {
            get
            {
                if (missionTypes == null || missionTypes.Length <= 0)
                {
                    missionTypes = Enum.GetNames(typeof(MissionType));
                }
                return missionTypes;
            }
        }

        public string STRING_NewLine = "\r\n";
        public string STRING_StartMissionThread = "开始任务线程";
        public string STRING_CloseMissionThread = "关闭任务线程";
        public string STRING_FinishMissionThread = "任务线程结束";
        public string STRING_ClosePanelMission = "关闭任务面板";
        public string STRING_PauseMissionThread = "暂停任务线程";
        public string STRING_ContinueMissionThread = "继续任务线程";

        public string PATTERN_IsLocalURL = "^[A-Z]:";
        public string PATTERN_IsIllegalVersion = "^([0-9]+(.[0-9]+){2,3})";//合法版本号格式
        #endregion

        #region 全局路径
        private string resListInAPKPath;
        public string ResListInAPKPath { get { resListInAPKPath = resListInAPKPath ?? FixedDirectory + "AdditionalFiles/c_table_reslistinapk.proto"; return resListInAPKPath; } }
        #endregion

        #region 私有变量
        private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        #endregion

        #region 工具方法

        #region UI
        public T CreateUserControlInControl<T>(Control parent, Point point, Size size, params AnchorStyles[] anchors) where T : UserControl
        {
            T temp = Activator.CreateInstance<T>();
            temp.Parent = parent;
            for (int i = 0; i < anchors.Length; i++)
            {
                temp.Anchor |= anchors[i];
            }
            temp.Size = size;
            temp.Location = point;
            return temp;
        }

        public T CreateUserControlInControl<T>(Control parent, Size size, params AnchorStyles[] anchors) where T : UserControl
        {
            T temp = Activator.CreateInstance<T>();
            temp.Parent = parent;
            for (int i = 0; i < anchors.Length; i++)
            {
                temp.Anchor |= anchors[i];
            }
            temp.Size = size;
            return temp;
        }

        public T CreateUserControlInControl<T>(Control parent, params AnchorStyles[] anchors) where T : UserControl
        {
            T temp = Activator.CreateInstance<T>();
            temp.Parent = parent;
            for (int i = 0; i < anchors.Length; i++)
            {
                temp.Anchor |= anchors[i];
            }
            return temp;
        }

        public UserControl CreateUserControlInControl(Type type, Point point, Size size, Control parent, params AnchorStyles[] anchors)
        {
            UserControl obj = Activator.CreateInstance(type) as UserControl;
            if (obj != null)
            {
                obj.Parent = parent;
                for (int i = 0; i < anchors.Length; i++)
                {
                    obj.Anchor |= anchors[i];
                }
            }
            obj.Size = size;
            obj.Location = point;
            return obj;
        }

        public UserControl CreateUserControlInControl(Type type, Size size, Control parent, params AnchorStyles[] anchors)
        {
            UserControl obj = Activator.CreateInstance(type) as UserControl;
            if (obj != null)
            {
                obj.Parent = parent;
                for (int i = 0; i < anchors.Length; i++)
                {
                    obj.Anchor |= anchors[i];
                }
            }
            obj.Size = size;
            return obj;
        }

        public UserControl CreateUserControlInControl(Type type, Control parent, params AnchorStyles[] anchors)
        {
            UserControl obj = Activator.CreateInstance(type) as UserControl;
            if (obj != null)
            {
                obj.Parent = parent;
                for (int i = 0; i < anchors.Length; i++)
                {
                    obj.Anchor |= anchors[i];
                }
            }
            return obj;
        }
        #endregion

        #region File
        public string SelectFileByDialog(string title)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = title;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                return fd.FileName;
            }
            return string.Empty;
        }

        public string SelectFileByDialog(string title, string filter)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = title;
            fd.Filter = filter;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                return fd.FileName;
            }
            return string.Empty;
        }

        public string SelectFolderByDialog(string title)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = title;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return string.Empty;
        }

        public string SelectFolderByDialog(string title, string defaultPath)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = defaultPath;
            fbd.Description = title;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return string.Empty;
        }

        public string SelectSavePathByDialog(string title, string filter = "文本文件(*.txt)|*.txt")
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = title;
            sfd.Filter = filter;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            return string.Empty;
        }

        public string GetRandomTempFilePath()
        {
            string tempFileName = DateTime.Now.ToLongTimeString().Replace(':', ' ') + DateTime.Now.ToLongTimeString().GetHashCode().ToString();
            if (File.Exists(tempFileName))
            {
                return GetRandomTempFilePath();
            }
            return TempDirectory + tempFileName;
        }

        public bool SaveFile(string filepath, string content)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return false;
            }
            try
            {
                File.WriteAllText(filepath, content);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool SaveFileByDialog(string fileContent, string title, string filter = "文本文件(*.txt)|*.txt")
        {
            string filePath = SelectSavePathByDialog(title, filter);
            return SaveFile(filePath, fileContent);
        }

        public void OpenURL(string url)
        {
            if (string.IsNullOrEmpty(url.Trim()))
            {
                return;
            }
            try
            {
                if (Regex.IsMatch(url, PATTERN_IsLocalURL))
                {
                    url = url.Replace('/', '\\');
                }
                Process.Start("explorer", url);
            }
            catch (Exception ex)
            {
                WriteGlobalLog(string.Format("URL:{0} 打开出错  {1}", url, ex.Message));
            }
        }

        public string DownloadFile(URLWithISLocal url)
        {
            if (url.IsLocal)
            {
                return url.URL;
            }
            string tempPath = GetRandomTempFilePath();
            if (!DownloadFile(url, tempPath))
            {
                tempPath = string.Empty;
            }
            return tempPath;
        }

        public bool DownloadFile(URLWithISLocal url, string localPath)
        {
            NormalConnects.Connection_Base connection;
            if (url.IsLocal)
            {
                connection = new NormalConnects.LocalConnect(url.URL, localPath);
            }
            else
            {
                connection = new NormalConnects.HttpConnect(url.URL, localPath, string.Empty);
            }
            bool downloadFinished = false;
            bool downloadResult = false;
            connection.DownloadResource(delegate (bool result, object str)
            {
                downloadFinished = true;
                downloadResult = result;
            });
            while (!downloadFinished)
            {
                Thread.Sleep(10);
            }
            return downloadResult;
        }

        public List<URLWithCDN> ParseLocalResourceListToURL(string localPath)
        {
            if (!File.Exists(localPath))
            {
                return null;
            }
            string[] fileLines = File.ReadAllLines(localPath);
            List<URLWithCDN> cdnResList = new List<URLWithCDN>();
            for (int i = 0; i < fileLines.Length; i++)
            {
                string[] splitedOptions = fileLines[i].Split('#');
                if (!string.IsNullOrEmpty(splitedOptions[0]))
                {
                    cdnResList.Add(new URLWithCDN(splitedOptions[0]));
                }
            }
            return cdnResList;
        }

        public List<URLWithCDN> ParseLocalResourceListToURL(string localPath, URLWithISLocal commonPath, URLWithISLocal independentPath, string cdnversion, out List<string> relativePathList)
        {
            if (!File.Exists(localPath))
            {
                relativePathList = null;
                return null;
            }
            relativePathList = new List<string>();
            string[] fileLines = File.ReadAllLines(localPath);
            List<URLWithCDN> cdnResList = new List<URLWithCDN>();
            for (int i = 0; i < fileLines.Length; i++)
            {
                string[] splitedOptions = fileLines[i].Split('#');
                if (splitedOptions != null && splitedOptions.Length >= 5)
                {
                    URLWithISLocal selectedFolderPath = ((splitedOptions[3] == "0") ? (commonPath) : (independentPath));
                    string fullPath = GetRightFilePath(selectedFolderPath.URL + splitedOptions[0]);
                    cdnResList.Add(new URLWithCDN(fullPath, cdnversion, selectedFolderPath.IsLocal));
                    relativePathList.Add(splitedOptions[0]);
                }
            }
            return cdnResList;
        }

        public List<URLWithCDN> ParseLocalResourceListToURL(string localPath, URLWithISLocal commonPath, URLWithISLocal independentPath, string cdnversion)
        {
            if (!File.Exists(localPath))
            {
                return null;
            }
            string[] fileLines = File.ReadAllLines(localPath);
            List<URLWithCDN> cdnResList = new List<URLWithCDN>();
            for (int i = 0; i < fileLines.Length; i++)
            {
                string[] splitedOptions = fileLines[i].Split('#');
                if (splitedOptions != null && splitedOptions.Length >= 5)
                {
                    URLWithISLocal selectedFolderPath = ((splitedOptions[3] == "0") ? (commonPath) : (independentPath));
                    cdnResList.Add(new URLWithCDN(selectedFolderPath.URL + splitedOptions[0], cdnversion, selectedFolderPath.IsLocal));
                }
            }
            return cdnResList;
        }

        public List<URLWithCDN> ParseLocalResourceListToURL(string localPath, URLWithISLocal commonPath, URLWithISLocal independentPath)
        {
            if (!File.Exists(localPath))
            {
                return null;
            }
            string[] fileLines = File.ReadAllLines(localPath);
            List<URLWithCDN> cdnResList = new List<URLWithCDN>();
            for (int i = 0; i < fileLines.Length; i++)
            {
                string[] splitedOptions = fileLines[i].Split('#');
                if (splitedOptions != null && splitedOptions.Length >= 5)
                {
                    URLWithISLocal selectedFolderPath = ((splitedOptions[3] == "0") ? (commonPath) : (independentPath));
                    cdnResList.Add(new URLWithCDN(selectedFolderPath.URL + splitedOptions[0], string.Empty, selectedFolderPath.IsLocal));
                }
            }
            return cdnResList;
        }
        #endregion

        #region Encrypt
        public string CalculateMD5Value(string inputString, Encoding encoding)
        {
            return CalculateMD5Value(encoding.GetBytes(inputString));
        }

        public string CalculateMD5Value(byte[] inputByteArray)
        {
            byte[] outputArray = md5.ComputeHash(inputByteArray);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < outputArray.Length; i++)
            {
                sb.AppendFormat("{0:x2}", outputArray[i]);
            }
            return sb.ToString();
        }
        #endregion

        #region Logic
        public void WriteGlobalLog(string log)
        {
            try
            {
                MainForm.writeLogDel.Invoke(GetCurrentTime() + "----  " + log);
            }
            catch (Exception) { }
        }

        public void WriteGlobalLog(object obj)
        {
            if (obj == null)
            {
                WriteGlobalLog("null");
            }
            else
            {
                WriteGlobalLog(obj.ToString());
            }
        }

        public string GetRightCDNVersion(string cdnversion)
        {
            if (!string.IsNullOrEmpty(cdnversion) && cdnversion[0] != '?')
            {
                cdnversion = '?' + cdnversion;
            }
            return cdnversion;
        }

        public string GetRightFolderPath(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                return folderPath;
            }
            folderPath = GetRightFilePath(folderPath);
            if (Regex.IsMatch(folderPath, PATTERN_IsLocalURL))
            {
                if (folderPath[folderPath.Length - 1] != '\\')
                {
                    folderPath = folderPath + '\\';
                }
            }
            else
            {
                if (folderPath[folderPath.Length - 1] != '/')
                {
                    folderPath = folderPath + '/';
                }
            }
            return folderPath;
        }

        public string GetRightFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
            if (Regex.IsMatch(filePath, PATTERN_IsLocalURL))
            {
                filePath = filePath.Replace('/', '\\');
            }
            else
            {
                filePath = filePath.Replace('\\', '/');
            }
            return filePath;
        }

        public string GetResourceFullPath(string resPath, string commonURL, string independentURL)
        {
            if (string.IsNullOrEmpty(resPath))
            {
                return resPath;
            }
            string[] splitedPath = resPath.Split('#');
            if (splitedPath.Length >= 5)
            {
                bool isIndependent = splitedPath[3] == "1";
                if (isIndependent)
                {
                    return GetRightFilePath(independentURL + resPath);
                }
                else
                {
                    return GetRightFilePath(commonURL + resPath);
                }
            }
            else
            {
                return resPath;
            }
        }

        public string CombineFullPath(string folderPath, string relativePath)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(relativePath))
            {
                return folderPath ?? string.Empty + relativePath ?? string.Empty;
            }
            folderPath = folderPath.Replace('\\', '/');
            relativePath = relativePath.Replace('\\', '/');
            string fullpath;
            if (folderPath[folderPath.Length - 1] != '/' && relativePath[0] != '/')
            {
                fullpath = folderPath + '/' + relativePath;
            }
            else
            {
                fullpath = folderPath + relativePath;
            }
            if (Regex.IsMatch(fullpath, PATTERN_IsLocalURL))
            {
                fullpath = fullpath.Replace('/', '\\');
            }
            return fullpath;
        }

        public bool CheckVersionCodeIsLegal(string versionCode)
        {
            return versionCode == "default" || Regex.IsMatch(versionCode, PATTERN_IsIllegalVersion);
        }

        public bool IsStringArrayEmptyOrNull(string[] array)
        {
            if (array == null)
            {
                return true;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Process
        /// <summary>
        /// 隐藏运行进程
        /// </summary>
        /// <param name="fileName">进程路径</param>
        /// <param name="arguments">运行参数</param>
        /// <param name="outputData">StandardOutput回调</param>
        /// <param name="errorData">StandardError回调</param>
        /// <returns></returns>
        public Process RunProcessInBackground(string fileName, string arguments, DataReceivedEventHandler outputData, DataReceivedEventHandler errorData)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = fileName;
            processInfo.Arguments = arguments;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processInfo.CreateNoWindow = true;
            Process process = Process.Start(processInfo);
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.OutputDataReceived += outputData;
            process.ErrorDataReceived += errorData;
            return process;
        }
        #endregion

        #region SVN
        public void ProcSVNCmd(string path, string cmd, DataReceivedEventHandler output, DataReceivedEventHandler error, bool isWaitingForExit = true)
        {
            if (!string.IsNullOrEmpty(path) && (File.Exists(path) || Directory.Exists(path)))
            {
                string argument = @"/command:" + cmd + " /path:" + path + " /closeonend:1";
                Process pro = RunProcessInBackground("TortoiseProc.exe", argument, output, error);
                if (isWaitingForExit)
                {
                    pro.WaitForExit();
                    pro?.Close();
                }
            }
        }
        #endregion

        #region Other
        public string GetCurrentTime()
        {
            return string.Format("{0}  {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
        }

        public string TransferTimeSpanToSTR(TimeSpan timespan)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            if (timespan.Days != 0 || !first)
            {
                sb.Append(timespan.Days);
                sb.Append("天");
            }
            if (timespan.Days != 0)
            {
                first = false;
            }
            if (timespan.Hours != 0 || !first)
            {
                sb.Append(timespan.Hours);
                sb.Append("小时");
            }
            if (timespan.Hours != 0)
            {
                first = false;
            }
            if (timespan.Minutes != 0 || !first)
            {
                sb.Append(timespan.Minutes);
                sb.Append("分钟");
            }
            if (timespan.Minutes != 0)
            {
                first = false;
            }
            sb.Append(timespan.Seconds);
            sb.Append("秒");
            return sb.ToString();
        }
        #endregion

        #endregion
    }
}
