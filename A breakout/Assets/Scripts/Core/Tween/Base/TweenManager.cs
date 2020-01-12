using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class TweenManager : MonoSingleton<TweenManager>
    {
        public static int Index;

        private Dictionary<int, List<Coroutine>> m_CoroutineDic;

        private void Awake()
        {
            if (m_CoroutineDic == null)
            {
                m_CoroutineDic = new Dictionary<int, List<Coroutine>>();
            }
        }

        public void AddCoroutine(int index, Coroutine c)
        {
            if (m_CoroutineDic == null)
            {
                m_CoroutineDic = new Dictionary<int, List<Coroutine>>();
            }
            if (m_CoroutineDic.ContainsKey(index))
            {
                if (!m_CoroutineDic[index].Contains(c))
                {
                    m_CoroutineDic[index].Add(c);
                }
            }
            else
            {
                List<Coroutine> list = new List<Coroutine>();
                list.Add(c);
                m_CoroutineDic[index] = list;
            }

        }

        public void RemoveCoroutine(int index, Coroutine c)
        {
            if (m_CoroutineDic.ContainsKey(index))
            {
                if (m_CoroutineDic[index].Contains(c))
                {
                    m_CoroutineDic[index].Remove(c);
                    if (m_CoroutineDic[index].Count == 0)
                    {
                        m_CoroutineDic.Remove(index);
                    }
                }
            }
        }

        public void StopCoroutineByIndex(int index)
        {
            if (m_CoroutineDic == null)
            {
                m_CoroutineDic = new Dictionary<int, List<Coroutine>>();
            }
            if (m_CoroutineDic.ContainsKey(index))
            {
                List<Coroutine> list = m_CoroutineDic[index];
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        StopCoroutine(list[i]);
                    }
                }
                list.Clear();
                m_CoroutineDic[index] = null;
                m_CoroutineDic.Remove(index);
            }
        }
    }
}
