using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public abstract class DispatcherBase<T, P, X> : IDisposable
        where T : new()
        where P : class
    {

        #region µ¥Àý

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        #endregion

        public delegate void OnActionHandler(P p);

        public Dictionary<X, List<OnActionHandler>> m_Dic = new Dictionary<X, List<OnActionHandler>>();


        public virtual void AddEventListen(X key, OnActionHandler handler)
        {
            if (!m_Dic.ContainsKey(key))
            {
                List<OnActionHandler> list = new List<OnActionHandler>();
                list.Add(handler);
                m_Dic.Add(key, list);
            }
            else
            {
                m_Dic[key].Add(handler);
            }
        }

        public virtual void RemoveEventListen(X key, OnActionHandler handler)
        {
            if (m_Dic.ContainsKey(key))
            {
                m_Dic[key].Remove(handler);
                if (m_Dic[key].Count == 0)
                {
                    m_Dic.Remove(key);
                }
            }

        }

        public virtual void Dispatc(X key, P p)
        {
            if (m_Dic.ContainsKey(key))
            {

                List<OnActionHandler> list = m_Dic[key];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null)
                    {
                        list[i](p);
                    }
                }
            }
        }

        public virtual void Dispose()
        {

        }
    }
}
