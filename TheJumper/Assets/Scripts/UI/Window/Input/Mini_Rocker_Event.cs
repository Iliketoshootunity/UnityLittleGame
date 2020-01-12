using UnityEngine.EventSystems;
using System;


internal class Mini_Rocker_Event : UISubViewBase, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public event Action<Mini_Rocker_Event, PointerEventData> onRockerBeginDraggedEvent;
    public event Action<Mini_Rocker_Event, PointerEventData> onRockerEndDraggedEvent;
    public event Action<Mini_Rocker_Event, PointerEventData> onRockerDraggedEvent;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (onRockerBeginDraggedEvent != null)
        {
            onRockerBeginDraggedEvent(this, eventData);
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (onRockerDraggedEvent != null)
        {
            onRockerDraggedEvent(this, eventData);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (onRockerEndDraggedEvent != null)
        {
            onRockerEndDraggedEvent(this, eventData);
        }
    }
}

