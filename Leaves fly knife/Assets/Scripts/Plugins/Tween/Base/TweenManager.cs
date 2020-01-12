using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

namespace EasyFrameWork
{
    public class TweenManager : MonoSingleton<TweenManager>
    {

        public static float DefaultDelay = 0;
        public static int DefaultLoopMaxCount = 0;
        public static TweenLoopType DefaultTweenLoopType = TweenLoopType.None;
        public static EaseType DefaultEasyType = EaseType.Linner;
        public static bool DefaultIsForm = false;
        public static bool DefaultIsAutonPlay = false;
        public static bool DefaultIsAutoRelease = true;


        public static int Index;

        private Dictionary<Tween, List<Coroutine>> m_CoroutineDic;
        private Dictionary<int, Tween> m_TweenListById = new Dictionary<int, Tween>();
        private Dictionary<MonoBehaviour, Tween> m_TweenListByTarget = new Dictionary<MonoBehaviour, Tween>();
        public int Test;
        private void Awake()
        {
            if (m_CoroutineDic == null)
            {
                m_CoroutineDic = new Dictionary<Tween, List<Coroutine>>();
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this.gameObject);
        }


        private void LateUpdate()
        {
            foreach (var item in m_TweenListById)
            {
                if (!item.Value.IsPlay)
                {
                    ((Tweener)item.Value).DoTween();
                    ((Tweener)item.Value).IsPlay = true;
                }

            }
            Test = m_TweenListById.Count;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Clear();
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Clear();
        }


        public void AddTween(Tween t)
        {
            if (t == null) return;
            if (t.Index == 0)
            {
                //EditorApplication.isPaused = true;
            }
            if (m_TweenListById == null)
            {
                m_TweenListById = new Dictionary<int, Tween>();
            }
            if (!m_CoroutineDic.ContainsKey(t))
            {
                m_CoroutineDic[t] = new List<Coroutine>();
            }
            if (!m_TweenListById.ContainsKey(t.Index))
            {
                m_TweenListById.Add(t.Index, t);
                ((Tweener)t).OnCoroutineChangeAction += OnCoroutineChangeAction;
            }
        }

        private void OnCoroutineChangeAction(Tween t, bool add, Coroutine c)
        {
            if (m_CoroutineDic.ContainsKey(t))
            {
                if (add)
                {
                    if (!m_CoroutineDic[t].Contains(c))
                    {
                        m_CoroutineDic[t].Add(c);
                    }
                }
                else
                {
                    if (m_CoroutineDic[t].Contains(c))
                    {
                        m_CoroutineDic[t].Remove(c);
                    }
                }
            }

        }

        /// <summary>
        /// 不要调用
        /// </summary>
        /// <param name="t"></param>
        public void RemoveTween(Tween t)
        {
            //移出Tween
            if (m_TweenListById.ContainsKey(t.Index))
            {
                m_TweenListById.Remove(t.Index);
                ((Tweener)t).OnCoroutineChangeAction -= OnCoroutineChangeAction;
            }
            //停掉所有的协程
            if (m_CoroutineDic.ContainsKey(t))
            {
                List<Coroutine> lst = m_CoroutineDic[t];
                int count = m_CoroutineDic[t].Count;
                for (int i = 0; i < lst.Count; i++)
                {
                    StopCoroutine(lst[i]);
                }
                m_CoroutineDic[t].Clear();
            }
        }
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            List<Tween> list = new List<Tween>();
            foreach (var item in m_TweenListById)
            {
                list.Add(item.Value);
            }
            for (int i = 0; i < list.Count; i++)
            {
                StopTween(list[i]);
            }
            m_TweenListById.Clear();
        }

        /// <summary>
        /// 停止Tween
        /// </summary>
        /// <param name="t"></param>
        public void StopTween(Tween t)
        {
            RemoveTween(t);
        }

        public static Tweener To(DoGetter<float> getter, DoSetter<float> setter, float toNumber, float variable)
        {
            Tweener t = new FloatTweener(getter, setter, toNumber, variable);
            t.DoTween();
            return t;
        }
    }
}
