using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckResourceTool.NormalConnects
{
    abstract class Connection_Base
    {
        /// <summary>
        /// 资源URL
        /// </summary>
        public abstract string URL { get; }
        /// <summary>
        /// 总下载字节数
        /// </summary>
        public abstract long SummaryDownloadingBytes { get; }

        /// <summary>
        /// 检查资源
        /// </summary>
        /// <param name="callback"></param>
        public abstract void CheckResource(Action<bool, object> callback);

        /// <summary>
        /// 下载资源
        /// </summary>
        /// <param name="callback"></param>
        public abstract void DownloadResource(Action<bool, object> callback);

        /// <summary>
        /// 获取下载总量对应的字符串
        /// </summary>
        /// <returns></returns>
        public abstract string GetDownloadSummaryStr();

        #region 常量
        public const int _GBbytes = 1024 * 1024 * 1024;//GB单位的字节数
        public const int _MBbytes = 1024 * 1024;//MB单位的字节数
        public const int _KBbytes = 1024;//KB单位的字节数
        #endregion

        #region 工具
        /// <summary>
        /// 检查本地目录
        /// </summary>
        /// <param name="localPath">本地路径</param>
        /// <returns>检查结果</returns>
        protected bool CheckLocalDirectory(string localPath)
        {
            if (string.IsNullOrEmpty(localPath))
            {
                return false;
            }
            try
            {
                string localDirectory = Path.GetDirectoryName(localPath);
                if (!Directory.Exists(localDirectory))
                {
                    Directory.CreateDirectory(localDirectory);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
