using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckResourceTool.NormalConnects
{
    /// <summary>
    /// 本地连接
    ///     1.检验本地资源是否存在
    ///     2.复制本地资源
    /// </summary>
    class LocalConnect : Connection_Base
    {
        #region 属性
        private string localUrl;
        /// <summary>
        /// 资源URL
        /// </summary>
        public override string URL
        {
            get
            {
                return localUrl;
            }
        }

        private string targetPath;
        /// <summary>
        /// 目标路径
        /// </summary>
        public string TargetPath { get { return targetPath; } }

        /// <summary>
        /// 总移动字节数
        /// </summary>
        public override long SummaryDownloadingBytes
        {
            get
            {
                return summaryRemoveBytes;
            }
        }
        #endregion

        #region 字段
        private byte[] buffer;

        private long summaryRemoveBytes = 0;
        #endregion

        #region 构造与析构
        /// <summary>
        /// 本地连接
        /// </summary>
        /// <param name="url">本地资源路径</param>
        public LocalConnect(string url)
        {
            url = url.Replace('\\', '/');
            localUrl = url;
        }

        /// <summary>
        /// 本地连接
        /// </summary>
        /// <param name="url">本地资源路径</param>
        /// <param name="targetPath">目标资源路径</param>
        public LocalConnect(string url, string targetPath)
        {
            url = url.Replace('\\', '/');
            localUrl = url;
            targetPath = targetPath.Replace('\\', '/');
            this.targetPath = targetPath;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 检验资源是否存在
        /// </summary>
        /// <param name="callback"></param>
        public override void CheckResource(Action<bool, object> callback)
        {
            bool result = true;
            if (!File.Exists(URL))
            {
                result = false;
            }
            string responseStr = result ? (string.Format("{0}  资源存在", URL)) : (string.Format("{0}  资源不存在", URL));
            callback?.Invoke(result, responseStr);
        }

        /// <summary>
        /// 移动资源
        /// </summary>
        /// <param name="callback"></param>
        public override void DownloadResource(Action<bool, object> callback)
        {
            if (!File.Exists(URL))
            {
                callback?.Invoke(false, string.Format("{0}  资源不存在", URL));
                return;
            }
            if (!CheckLocalDirectory(TargetPath))
            {
                callback?.Invoke(false, string.Format("{0}  输出目录违法", URL));
            }
            if (buffer == null)
            {
                buffer = new byte[1024];
            }
            try
            {
                using (FileStream readFS = new FileStream(URL, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream writeFS = new FileStream(TargetPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        int length = readFS.Read(buffer, 0, buffer.Length);
                        while (length > 0)
                        {
                            writeFS.Write(buffer, 0, length);
                            writeFS.Flush();
                            length = readFS.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                callback?.Invoke(false, string.Format("{0}  复制失败 {1}", URL, ex.Message));
            }
            callback?.Invoke(true, string.Format("{0}  {1}移动成功", URL + "  =>  " + TargetPath, GetDownloadSummaryStr()));
        }

        /// <summary>
        /// 资源下载总量对应的字符串
        /// </summary>
        /// <returns></returns>
        public override string GetDownloadSummaryStr()
        {
            if (summaryRemoveBytes > _GBbytes)
            {
                return string.Format("{0}GB", summaryRemoveBytes / ((float)_GBbytes));
            }
            if (summaryRemoveBytes > _MBbytes)
            {
                return string.Format("{0}MB", summaryRemoveBytes / ((float)_MBbytes));
            }
            if (summaryRemoveBytes > _KBbytes)
            {
                return string.Format("{0}KB", summaryRemoveBytes / ((float)_KBbytes));
            }
            return string.Format("{0}B", summaryRemoveBytes);
        }
        #endregion
    }
}
