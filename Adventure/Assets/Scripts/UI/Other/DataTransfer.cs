using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// 数据传输类
    /// </summary>
    public class DataTransfer
    {

        private Dictionary<string, object> m_Data;

        public Dictionary<string, object> Data
        {
            get
            {
                return m_Data;
            }
        }

        public DataTransfer()
        {
            m_Data = new Dictionary<string, object>();
        }

        public T GetData<T>(string key)
        {
            if (Data.ContainsKey(key))
            {
                return (T)Data[key];
            }
            return default(T);
        }

        public void SetData<T>(string key, T value)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = value;
            }
            else
            {
                Data.Add(key, value);
            }
        }
    }
}

