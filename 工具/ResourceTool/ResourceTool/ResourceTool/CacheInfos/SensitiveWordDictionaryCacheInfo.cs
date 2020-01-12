using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckResourceTool
{
    [Serializable]
    [XmlRoot(ElementName = "敏感词字典")]
    public class SensitiveWordDictionaryCacheInfo : CacheInfoBase
    {
        private string sensitiveWord;
        [XmlElement("敏感词")]
        public string SensitiveWord { get { return sensitiveWord; } set { sensitiveWord = value; } }

        private string replaceWord;
        [XmlElement("替换词")]
        public string ReplaceWord { get { return replaceWord; } set { replaceWord = value; } }

        public override string ToString()
        {
            return string.Format("{0} => {1}", SensitiveWord, ReplaceWord);
        }
    }
}
