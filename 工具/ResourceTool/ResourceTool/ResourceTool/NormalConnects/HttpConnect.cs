using System;
using System.Net;
using System.IO;

namespace CheckResourceTool.NormalConnects
{
    /// <summary>
    /// http协议连接
    ///     1.检验服务器资源是否存在
    ///     2.下载服务器资源(后缀或会添加CDN版本号)
    /// </summary>
    class HttpConnect : Connection_Base
    {
        #region 属性
        private string url;
        /// <summary>
        /// 检验/下载URL
        /// </summary>
        public override string URL { get { return url; } }

        private string cdnPostfix;
        /// <summary>
        /// CDN版本号后缀
        /// </summary>
        public string CDNPostfix { get { return cdnPostfix; } }

        /// <summary>
        /// 总下载字节数
        /// </summary>
        public override long SummaryDownloadingBytes { get { return summaryBytes; } }
        #endregion

        #region 字段
        private HttpWebRequest req;//http请求
        private HttpWebResponse rsp;//http响应
        private byte[] buffer;//缓冲

        private string localPath;//本地路径
        private FileStream fileStream;//本地文件读写流
        private Stream webStream;//协议流
        private long summaryBytes = 0;//总下载字节数
        #endregion

        #region 构造与析构
        /// <summary>
        /// http连接
        /// </summary>
        /// <param name="url">远程路径</param>
        public HttpConnect(string url)
        {
            url = url.Replace('\\', '/');
            this.url = url;
            cdnPostfix = string.Empty;
        }

        /// <summary>
        /// http连接
        /// </summary>
        /// <param name="url">远程路径</param>
        /// <param name="cdnPostfix">cdn版本号后缀</param>
        public HttpConnect(string url, string cdnPostfix)
        {
            this.cdnPostfix = cdnPostfix ?? string.Empty;
            url = url.Replace('\\', '/');
            this.url = url + this.cdnPostfix;
        }

        /// <summary>
        /// http连接
        /// </summary>
        /// <param name="url">远程路径</param>
        /// <param name="localPath">下载使用的本地路径</param>
        /// <param name="cdnPostfix">cdn版本号后缀</param>
        public HttpConnect(string url, string localPath, string cdnPostfix)
        {
            this.cdnPostfix = cdnPostfix ?? string.Empty;
            url = url.Replace('\\', '/');
            this.url = url + this.cdnPostfix;
            this.localPath = localPath;
            this.localPath = this.localPath.Replace('\\', '/');
        }

        ~HttpConnect()
        {
            HttpClose();
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 校验资源是否存在
        /// </summary>
        /// <param name="callback">校验结果回调</param>
        public override void CheckResource(Action<bool, object> callback)
        {
            bool result = DoConnect();
            string responseStr = result ? (string.Format("{0}  资源存在", url)) : (string.Format("{0}  资源不存在", url));
            callback?.Invoke(result, responseStr);
            HttpClose();
        }

        /// <summary>
        /// 下载资源
        /// </summary>
        /// <param name="callback"></param>
        public override void DownloadResource(Action<bool, object> callback)
        {
            //连接服务器
            bool connectResult = DoConnect();
            //检查资源是否存在
            if (!connectResult)
            {
                callback?.Invoke(false, string.Format("{0}  资源获取失败", url));
                HttpClose();
                return;
            }
            //检查本地路径是否为空
            if (string.IsNullOrEmpty(localPath))
            {
                callback?.Invoke(false, string.Format("{0}  本地路径不能为空", url));
                HttpClose();
                return;
            }
            //检查本地目录是否违法
            if (!CheckLocalDirectory(localPath))
            {
                callback?.Invoke(false, string.Format("{0}  本地目录违法", url));
                HttpClose();
                return;
            }
            try
            {
                //获取http响应
                rsp = req.GetResponse() as HttpWebResponse;
                //检查目标文件是否已存在,若存在,则删除
                if (File.Exists(localPath))
                {
                    File.Delete(localPath);
                }
                //分配缓存
                if (buffer == null)
                {
                    buffer = new byte[1024];
                }
                //下载
                using (fileStream = new FileStream(localPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                {
                    summaryBytes = 0;
                    int lengthTemp = 0;
                    using (webStream = rsp.GetResponseStream())
                    {
                        lengthTemp = webStream.Read(buffer, 0, buffer.Length);
                        while (lengthTemp > 0)
                        {
                            summaryBytes += lengthTemp;
                            fileStream.Write(buffer, 0, lengthTemp);
                            fileStream.Flush();
                            lengthTemp = webStream.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                callback?.Invoke(false, string.Format("{0}  下载失败 {1}", URL, ex.Message));
                HttpClose();
                return;
            }
            HttpClose();
            callback?.Invoke(true, string.Format("{0}  {1}下载成功\r\n", url + "  =>\r\n  " + localPath, GetDownloadSummaryStr()));
        }

        /// <summary>
        /// 获取下载总量对应的字符串
        /// </summary>
        /// <returns></returns>
        public override string GetDownloadSummaryStr()
        {
            if (summaryBytes > _GBbytes)
            {
                return string.Format("{0}GB", summaryBytes / ((float)_GBbytes));
            }
            if (summaryBytes > _MBbytes)
            {
                return string.Format("{0}MB", summaryBytes / ((float)_MBbytes));
            }
            if (summaryBytes > _KBbytes)
            {
                return string.Format("{0}KB", summaryBytes / ((float)_KBbytes));
            }
            return string.Format("{0}B", summaryBytes);
        }
        #endregion

        #region http连接与关闭
        /// <summary>
        /// http连接
        /// </summary>
        private bool DoConnect()
        {
            try
            {
                req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = "GET";
                req.Timeout = 120000;
                req.ReadWriteTimeout = 120000;
                req.Proxy = null;
                rsp = req.GetResponse() as HttpWebResponse;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 关闭http连接
        /// </summary>
        private void HttpClose()
        {
            if (req != null)
            {
                req.Abort();
                req.KeepAlive = false;
                req = null;
            }
            if (rsp != null)
            {
                rsp.Close();
                rsp = null;
            }
            if (buffer != null)
            {
                buffer = null;
            }
            if (fileStream != null)
            {
                fileStream.Close();
                fileStream = null;
            }
            if (webStream != null)
            {
                webStream.Close();
                webStream = null;
            }
        }
        #endregion
    }
}
