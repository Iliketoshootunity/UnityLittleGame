using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  EasyFrameWork
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T m_Instance;
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;//1这里耗费性能，有风险
                    if (m_Instance == null)//2
                    {
                        m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }
                }
                return m_Instance;
            }
            
        }

    }

}

