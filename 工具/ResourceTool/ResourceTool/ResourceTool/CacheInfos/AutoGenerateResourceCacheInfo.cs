using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckResourceTool
{
    public enum ResourcePlatform
    {
        iOS,
        Android,
    }

    [Serializable]
    [XmlRoot(ElementName = "资源包自动导出配置")]
    public class AutoGenerateResourceCacheInfo : CacheInfoBase
    {
        private string prefabName;
        [XmlElement("预设名")]
        public string PrefabName { get { return prefabName; } set { prefabName = value; } }

        private ResourcePlatform platform;
        [XmlElement("平台")]
        public ResourcePlatform PlatForm { get { return platform; } set { platform = value; } }

        private string resourceDirectory;
        [XmlElement("资源目录")]
        public string ResourceDirectory { get { return resourceDirectory; } set { resourceDirectory = value; } }

        private string resInApkJsonPath;
        [XmlElement("包内资源列表路径")]
        public string ResInApkJsonPath { get { return resInApkJsonPath; } set { resInApkJsonPath = value; } }

        private string firstZipJsonPath;
        [XmlElement("First包资源列表路径")]
        public string FirstZipJsonPath { get { return firstZipJsonPath; } set { firstZipJsonPath = value; } }

        private string specialResZipJsonPath;
        [XmlElement("SpecialRes包资源列表路径")]
        public string SpecialResZipJsonPath { get { return specialResZipJsonPath; } set { specialResZipJsonPath = value; } }

        private string resourceOrderPath;
        [XmlElement("资源顺序列表路径")]
        public string ResourceOrderPath { get { return resourceOrderPath; } set { resourceOrderPath = value; } }

        private string independentResListPath;
        [XmlElement("独立资源列表路径")]
        public string IndependentResListPath { get { return independentResListPath; } set { independentResListPath = value; } }

        private string predownloadResListPath;
        [XmlElement("预下载资源列表路径")]
        public string PredownloadResListPath { get { return predownloadResListPath; } set { predownloadResListPath = value; } }

        private string zipPostFix;
        [XmlElement("zip包后缀")]
        public string ZipPostFix { get { return zipPostFix; } set { zipPostFix = value; } }

        private string channel;
        [XmlElement("渠道名")]
        public string Channel { get { return channel; } set { channel = value; } }

        private string additionalResourcePathList;
        [XmlElement("附加的资源目录")]
        public string AdditionalResourcePathList { get { return additionalResourcePathList; } set { additionalResourcePathList = value; } }

        private string exportDirectory;
        [XmlElement("导出目录")]
        public string ExportDirectory { get { return exportDirectory; } set { exportDirectory = value; } }

        public AutoGenerateResourceCacheInfo() : base()
        {
            prefabName = string.Empty;
            platform = ResourcePlatform.Android;
            resourceDirectory = string.Empty;
            resInApkJsonPath = string.Empty;
            firstZipJsonPath = string.Empty;
            specialResZipJsonPath = string.Empty;
            independentResListPath = string.Empty;
            predownloadResListPath = string.Empty;
            resourceOrderPath = string.Empty;
            zipPostFix = string.Empty;
            channel = string.Empty;
            additionalResourcePathList = string.Empty;
            exportDirectory = string.Empty;
        }
    }
}
