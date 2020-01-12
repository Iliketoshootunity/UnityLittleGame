using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2d场景控制器基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SystemCtrlBase<T> : IDisposable where T : new()
{

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

    public virtual void Dispose()
    {

    }

    /// <summary>
    /// 显示提示窗口
    /// </summary>
    /// <param name="titleName"></param>
    /// <param name="content"></param>
    /// <param name="buttonType"></param>
    /// <param name="OnClickOK"></param>
    /// <param name="OnClickCancle"></param>
    public void ShowMsg(string titleName, string content, MsgButtonType buttonType = MsgButtonType.Ok, Action OnClickOK = null, Action OnClickCancle = null)
    {
        MessageCtrl.Instance.Show(titleName, content, buttonType, OnClickOK, OnClickCancle);
    }
    /// <summary>
    /// 监听UI观察者派发的消息
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    public void AddEventListen(string key,DispatcherBase<UIDispatcher,object[],string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.AddEventListen(key, handler);
    }
    /// <summary>
    ///取消监听UI观察者派发的消息
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    public void RemoveEventListen(string key, DispatcherBase<UIDispatcher, object[], string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.RemoveEventListen(key, handler);
    }
}
