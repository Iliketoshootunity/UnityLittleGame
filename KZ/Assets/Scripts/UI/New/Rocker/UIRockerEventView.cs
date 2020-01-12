using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.EventSystems;
using System;

namespace EasyFrameWork
{
    /// <summary>
    /// 摇杆中心 负责接受事件 并向上层提交事件
    /// </summary>
	public class UIRockerEventView : UISubViewBase, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {

        public Action<UIRockerEventView, PointerEventData> OnRockerBeginDragEvent;
        public Action<UIRockerEventView, PointerEventData> OnRockerEndDragEvent;
        public Action<UIRockerEventView, PointerEventData> OnRockerDragEvent;
        public Action<UIRockerEventView, PointerEventData> OnRockerPointDownEvent;
        public Action<UIRockerEventView, PointerEventData> OnRockerPointUpEvent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (OnRockerBeginDragEvent != null)
            {
                OnRockerBeginDragEvent(this, eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnRockerDragEvent != null)
            {
                OnRockerDragEvent(this, eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (OnRockerEndDragEvent != null)
            {
                OnRockerEndDragEvent(this, eventData);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnRockerPointDownEvent != null)
            {
                OnRockerPointDownEvent(this, eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnRockerPointUpEvent != null)
            {
                OnRockerPointUpEvent(this, eventData);
            }
        }
    }
}
