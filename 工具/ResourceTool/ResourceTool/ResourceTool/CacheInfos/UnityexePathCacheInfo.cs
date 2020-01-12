using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckResourceTool
{
    [Serializable]
    [XmlRoot(ElementName = "Unity.exe路径")]
    public class UnityexePathCacheInfo : CacheInfoBase
    {
        private string unityVersion;
        [XmlElement("Unity版本号")]
        public string UnityVersion { get { return unityVersion; } set { unityVersion = value; } }

        private string unityExePath;
        [XmlElement("路径")]
        public string UnityExePath { get { return unityExePath; } set { unityExePath = value; } }
    }
}
