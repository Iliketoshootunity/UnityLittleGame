using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public enum RockerPos
    {
        Left,
        Right
    }
    public class UIRockerView : UISubViewBase
    {

        public static UIRockerView Instance;
        protected override void OnAwake()
        {
            base.OnAwake();
            Instance = this;
        }
        private Dictionary<string, UISingleRockerView> m_RockerDic;
        protected Dictionary<string, UISingleRockerView> RockerDic
        {
            get
            {
                if (m_RockerDic == null)
                {
                    m_RockerDic = new Dictionary<string, UISingleRockerView>();
                    UISingleRockerView[] arr = GetComponentsInChildren<UISingleRockerView>();
                    for (int i = 0; i < arr.Length; i++)
                    {
                        m_RockerDic.Add(arr[i].name, arr[i]);
                    }
                }
                return m_RockerDic;
            }

        }
        private Dictionary<string, List<Action<Vector2, float, bool>>> m_RockerEvent;

        protected Dictionary<string, List<Action<Vector2, float, bool>>> RockerEvent
        {
            get
            {
                if (m_RockerEvent == null)
                {
                    m_RockerEvent = new Dictionary<string, List<Action<Vector2, float, bool>>>();
                    foreach (var item in RockerDic)
                    {
                        List<Action<Vector2, float, bool>> events = new List<Action<Vector2, float, bool>>();
                        m_RockerEvent.Add(item.Key, events);
                    }
                }
                return m_RockerEvent;
            }
        }
        public void AddEventOnRocker(string name, Action<Vector2, float, bool> rockerEvent)
        {
            if (RockerEvent.ContainsKey(name))
            {
                if (!RockerEvent[name].Contains(rockerEvent))
                {
                    RockerEvent[name].Add(rockerEvent);
                }
            }
        }

        public void RemoveEventOnRocker(string name, Action<Vector2, float, bool> rockerEvent)
        {
            if (RockerEvent.ContainsKey(name))
            {
                if (RockerEvent[name].Contains(rockerEvent))
                {
                    RockerEvent[name].Remove(rockerEvent);
                }
            }
        }

        private void OnRockerDragged(string name, Vector2 v, float p, bool isEnd)
        {
            if (RockerEvent.ContainsKey(name))
            {
                List<Action<Vector2, float, bool>> events = RockerEvent[name];
                if (events != null)
                {
                    for (int i = 0; i < events.Count; i++)
                    {
                        if (events[i] != null)
                        {
                            events[i](v, p, isEnd);
                        }
                    }
                }
            }
        }

        protected override void OnStart()
        {
            foreach (var item in RockerDic)
            {
                item.Value.OnRockerDrag += OnRockerDragged;
            }
        }
    }


}
