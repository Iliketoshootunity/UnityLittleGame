using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public interface IStateMachine
    {
        bool TriggerEvents { get; set; }
    }

    public struct StateMachineEvents<T>
    {
        public GameObject Target;

        public T CurState;

        public T PreState;

        public StateMachineEvents(StateMachine<T> sm)
        {
            CurState = sm.CurState;
            PreState = sm.PreState;
            Target = sm.Target;
        }
    }

    public class StateMachine<T> : IStateMachine
    {
        //切换状态，触发事件
        public bool TriggerEvents { get; set; }
        //事件名字
        public string EventsName { get; protected set; }
        //载体
        public GameObject Target;
        //上个状态  
        public T PreState { get; protected set; }
        //当前状态
        public T CurState { get; protected set; }

        protected List<T> m_StateList;

        public StateMachine(GameObject target, bool triggerEvents = false, string eventsName = "")
        {
            Target = target;
            TriggerEvents = triggerEvents;
            EventsName = eventsName;
            m_StateList = new List<T>();
        }


        public virtual void ChangeState(T newSate)
        {
            if (!m_StateList.Contains(newSate) || newSate.Equals(CurState)) return;
            PreState = CurState;
            CurState = newSate;
            if (TriggerEvents)
            {
                StateMachineEvents<T> events = new StateMachineEvents<T>(this);
                StateMachineDispatcher.Instance.Dispatc(EventsName, events);
            }
        }

        public void AddState(T state)
        {
            if (m_StateList.Contains(state)) return;
            m_StateList.Add(state);
        }

        public virtual void RestorePreviousState()
        {
            CurState = PreState;
            if (TriggerEvents)
            {
                StateMachineEvents<T> events = new StateMachineEvents<T>(this);
                StateMachineDispatcher.Instance.Dispatc(EventsName, events);
            }
        }
    }
}
