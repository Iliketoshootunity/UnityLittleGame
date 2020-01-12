using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckResourceTool
{
    [Serializable]
    public class CacheInfoBase
    {
        [NonSerialized]
        private string cachePath = null;
        /// <summary>
        /// 缓存路径
        /// </summary>
        public string CachePath { get { return cachePath; } }

        public CacheInfoBase()
        {
            cachePath = cachePath ?? CacheTool.Instance.CacheDirectory + GetType().ToString().Split('.').Last() + ".xml";
        }
    }
}
