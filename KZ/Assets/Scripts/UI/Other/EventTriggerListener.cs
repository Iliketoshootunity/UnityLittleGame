
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace EasyFrameWork.UI
{
    public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
    {
        public delegate void VoidDelegate(GameObject go);

        public static List<IEventSystemHandler> EventSystemList = new List<IEventSystemHandler>();

        public PointerEventData CurPointerEventData;

        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onSelect;
        public VoidDelegate onUpdateSelect;
        public VoidDelegate onBeginDrag;
        public VoidDelegate onDrag;
        public VoidDelegate onEndDrag;

        static public EventTriggerListener Get(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null) listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }
        static public void FindAllEventSystem(GameObject go)
        {
            EventSystemList.Clear();
            IEventSystemHandler[] eventArr = go.GetComponentsInChildren<IEventSystemHandler>();
            for (int i = 0; i < eventArr.Length; i++)
            {
                if (EventSystemList.Contains(eventArr[i])) continue;
                EventSystemList.Add(eventArr[i]);
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (onBeginDrag != null)
            {
                onBeginDrag(gameObject);
            }
            CurPointerEventData = eventData;
        }
        public override void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null)
            {
                onDrag(gameObject);
            }
            CurPointerEventData = eventData;
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null)
            {
                onEndDrag(gameObject);
            }
            CurPointerEventData = eventData;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject);
            CurPointerEventData = eventData;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null) onDown(gameObject);
            CurPointerEventData = eventData;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null) onEnter(gameObject);
            CurPointerEventData = eventData;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null) onExit(gameObject);
            CurPointerEventData = eventData;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null) onUp(gameObject);
            CurPointerEventData = eventData;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            if (onSelect != null) onSelect(gameObject);
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (onUpdateSelect != null) onUpdateSelect(gameObject);
        }
    }
}
