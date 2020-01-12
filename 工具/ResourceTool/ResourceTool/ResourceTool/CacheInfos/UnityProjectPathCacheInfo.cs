using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckResourceTool
{
    [Serializable]
    [XmlRoot(ElementName = "Unity工程路径")]
    public class UnityProjectPathCacheInfo : CacheInfoBase
    {
        private string projectName;
        [XmlElement("工程名")]
        public string ProjectName { get { return projectName; } set { projectName = value; } }

        private string projectPath;
        [XmlElement("Unity工程路径")]
        public string ProjectPath { get { return projectPath; } set { projectPath = value; } }
    }
}
