using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckResourceTool
{
    class CacheTool
    {
        private static CacheTool _Instance;
        public static CacheTool Instance { get { _Instance = _Instance ?? new CacheTool(); return _Instance; } }

        private string cacheDirectory;
        /// <summary>
        /// 缓存目录
        /// </summary>
        public string CacheDirectory { get { if (cacheDirectory == null) cacheDirectory = Global.Instance.FixedDirectory + @"Cache/"; if (!Directory.Exists(cacheDirectory)) { Directory.CreateDirectory(cacheDirectory); } return cacheDirectory; } }

        private Dictionary<Type, XmlSerializer> xmlSerializerDic;

        public CacheTool()
        {
            xmlSerializerDic = new Dictionary<Type, XmlSerializer>();
        }

        private XmlSerializer GetXmlSerializer<T>()
        {
            XmlSerializer xmlSe;
            if (!xmlSerializerDic.TryGetValue(typeof(T), out xmlSe))
            {
                xmlSe = new XmlSerializer(typeof(T));
                xmlSerializerDic.Add(typeof(T), xmlSe);
            }
            return xmlSe;
        }

        public string GetCacheFilePath<T>() where T : CacheInfoBase
        {
            T temp = Activator.CreateInstance<T>();
            if (!File.Exists(temp.CachePath))
            {
                SaveCache(temp);
            }
            return temp.CachePath;
        }

        public void SaveCache<T>(T cache) where T : CacheInfoBase
        {
            try
            {
                using (FileStream fs = new FileStream(cache.CachePath, FileMode.Create))
                {
                    GetXmlSerializer<T>().Serialize(fs, cache);
                }
            }
            catch (Exception ex)
            {
                Global.Instance.WriteGlobalLog("写入" + cache.CachePath + "失败  " + ex.Message);
            }
        }

        public void SaveCache<T>(List<T> cacheList) where T : CacheInfoBase
        {
            if (cacheList == null || cacheList.Count <= 0)
            {
                return;
            }
            string path = cacheList[0].CachePath;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    GetXmlSerializer<List<T>>().Serialize(fs, cacheList);
                }
            }
            catch (Exception ex)
            {
                Global.Instance.WriteGlobalLog("打开" + path + "失败  " + ex.Message);
            }
        }

        public bool GetCache<T>(out T cache) where T : CacheInfoBase
        {
            T temp = Activator.CreateInstance<T>();
            cache = null;
            if (!File.Exists(temp.CachePath))
            {
                Global.Instance.WriteGlobalLog("缓存文件" + temp.CachePath + "不存在");
                return false;
            }
            try
            {
                using (FileStream fs = File.OpenRead(temp.CachePath))
                {
                    cache = GetXmlSerializer<T>().Deserialize(fs) as T;
                }
            }
            catch (Exception ex)
            {
                cache = null;
                Global.Instance.WriteGlobalLog("打开" + temp.CachePath + "失败  " + ex.Message);
                return false;
            }
            return true;
        }

        public bool GetCache<T>(out List<T> list) where T : CacheInfoBase
        {
            list = null;
            T temp = Activator.CreateInstance<T>();
            if (!File.Exists(temp.CachePath))
            {
                Global.Instance.WriteGlobalLog("缓存文件" + temp.CachePath + "不存在");
                return false;
            }
            try
            {
                using (FileStream fs = File.OpenRead(temp.CachePath))
                {
                    list = GetXmlSerializer<List<T>>().Deserialize(fs) as List<T>;
                }
            }
            catch (Exception ex)
            {
                list = null;
                Global.Instance.WriteGlobalLog("打开" + temp.CachePath + "失败  " + ex.Message);
                return false;
            }
            return true;
        }
    }
}
