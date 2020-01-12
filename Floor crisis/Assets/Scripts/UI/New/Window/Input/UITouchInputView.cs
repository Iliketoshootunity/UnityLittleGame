using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using EasyFrameWork.UI;

public class UITouchInputView : UISubViewBase
{
    [SerializeField]
    private Button m_TopButton;
    [SerializeField]
    private Button m_BottomButton;
    [SerializeField]
    private Button m_LeftButton;
    [SerializeField]
    private Button m_RightButton;

    //public Action<MoveDirection, int> OnClick;

    public static UITouchInputView Instance;

    protected override void OnAwake()
    {
        Instance = this;
    }

    protected override void OnStart()
    {
        EventTriggerListener.Get(m_TopButton.gameObject).onDown += OnDown;
        EventTriggerListener.Get(m_TopButton.gameObject).onUp += OnUp;
        EventTriggerListener.Get(m_BottomButton.gameObject).onDown += OnDown;
        EventTriggerListener.Get(m_BottomButton.gameObject).onUp += OnUp;
        EventTriggerListener.Get(m_LeftButton.gameObject).onDown += OnDown;
        EventTriggerListener.Get(m_LeftButton.gameObject).onUp += OnUp;
        EventTriggerListener.Get(m_RightButton.gameObject).onDown += OnDown;
        EventTriggerListener.Get(m_RightButton.gameObject).onUp += OnUp;
    }

    private void OnUp(GameObject go)
    {
        //if (go.name == m_TopButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Up, 0);
        //    }
        //}
        //else if (go.name == m_BottomButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Down, 0);
        //    }
        //}
        //else if (go.name == m_LeftButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Left, 0);
        //    }
        //}
        //else if (go.name == m_RightButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Right, 0);
        //    }
        //}
    }

    private void OnDown(GameObject go)
    {
        //if (go.name == m_TopButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Up, 1);
        //    }
        //}
        //else if (go.name == m_BottomButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Down, 1);
        //    }
        //}
        //else if (go.name == m_LeftButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Left, 1);
        //    }
        //}
        //else if (go.name == m_RightButton.name)
        //{
        //    if (OnClick != null)
        //    {
        //        OnClick(MoveDirection.Right, 1);
        //    }
        //}
    }


    protected override void BeforeOnDestory()
    {
        //OnClick = null;
        EventTriggerListener.Get(m_TopButton.gameObject).onDown = null;
        EventTriggerListener.Get(m_TopButton.gameObject).onUp = null;
        EventTriggerListener.Get(m_BottomButton.gameObject).onDown = null;
        EventTriggerListener.Get(m_BottomButton.gameObject).onUp = null;
        EventTriggerListener.Get(m_LeftButton.gameObject).onDown = null;
        EventTriggerListener.Get(m_LeftButton.gameObject).onUp = null;
        EventTriggerListener.Get(m_RightButton.gameObject).onDown = null;
        EventTriggerListener.Get(m_RightButton.gameObject).onUp = null;
    }
}
