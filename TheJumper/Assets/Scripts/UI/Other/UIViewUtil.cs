using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// UI��ͼ������
/// </summary>
public class UIViewUtil : Singleton<UIViewUtil>
{

    /// <summary>
    /// �򿪵Ĵ���
    /// </summary>
    private Dictionary<UIViewType, UIWindowViewBase> dicWindow = new Dictionary<UIViewType, UIWindowViewBase>();

    public int OpenWindowCount
    {
        get
        {
            return dicWindow.Count;
        }
    }

    public void CloseAll()
    {
        if (dicWindow != null)
        {
            dicWindow.Clear();
        }
    }

    /// <summary>
    /// �򿪴���
    /// </summary>
    /// <param name="type">��������</param>
    /// <returns></returns>
    public GameObject OpenWindow(UIViewType type)
    {
        GameObject go = null;
        //������ڲ�����
        if (!dicWindow.ContainsKey(type) || dicWindow[type] == null)
        {
            //UI����ע��������ʽ ������pan+UIWindowTypeö������
            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindow, string.Format("pan{0}", type), true);
            if (go == null) return null;
            UIWindowViewBase window = go.GetComponent<UIWindowViewBase>();
            if (window == null) return null;
            if (!dicWindow.ContainsKey(type))
            {
                dicWindow.Add(type, window);
            }
            else
            {
                dicWindow[type] = window;
            }
            window.ViewType = type;

            Transform parent = null;
            switch (window.ContainerType)
            {
                case UIWinndowContainerType.Center:
                    parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
                    break;
                case UIWinndowContainerType.BL:
                    parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
                    break;
                case UIWinndowContainerType.BR:
                    parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
                    break;
                case UIWinndowContainerType.TL:
                    parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
                    break;
                case UIWinndowContainerType.TR:
                    parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
                    break;
            }
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            go.SetActive(false);
            ShowWindowAni(window, true);
        }
        else
        {
            go = dicWindow[type].gameObject;
        }

        //����UI�㼶����
        LayerMgr.Instance.SetUILayerDepth(go);
        return go;

    }
    /// <summary>
    /// �رմ���
    /// </summary>
    /// <param name="type"></param>
    public void CloseWindow(UIViewType windowType)
    {
        if (!dicWindow.ContainsKey(windowType)) return;
        UIWindowViewBase window = dicWindow[windowType];
        ShowWindowAni(window, false);
    }
    #region ShowWindowAni �򿪻��߹رմ��ڶ���
    /// <summary>
    /// �򿪻��߹رմ��ڶ���
    /// </summary>
    /// <param name="go"></param>
    /// <param name="showType"></param>
    /// <param name="isOpen"></param>
    private void ShowWindowAni(UIWindowViewBase window, bool isOpen)
    {
        switch (window.ShowAniType)
        {
            case UIWindowShowAniType.Normal:
                ShowWindowNormal(window, isOpen);
                break;
            case UIWindowShowAniType.CenterToBig:
                ShowWindowCenterToBig(window, isOpen);
                break;
            case UIWindowShowAniType.FormTop:
                ShowWindoweFromDir(window, 0, isOpen);
                break;
            case UIWindowShowAniType.ForBottom:
                ShowWindoweFromDir(window, 1, isOpen);
                break;
            case UIWindowShowAniType.FormLeft:
                ShowWindoweFromDir(window, 2, isOpen);
                break;
            case UIWindowShowAniType.FormRight:
                ShowWindoweFromDir(window, 3, isOpen);
                break;
        }

    }
    /// <summary>
    /// ������
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isOpen"></param>
    private void ShowWindowNormal(UIWindowViewBase window, bool isOpen)
    {
        if (isOpen)
        {

            window.gameObject.SetActive(true);
        }
        else
        {
            DestoryWindow(window);
        }
    }

    /// <summary>
    /// ���м俪ʼ���
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isOpen"></param>
    private void ShowWindowCenterToBig(UIWindowViewBase window, bool isOpen)
    {
        window.gameObject.SetActive(true);
        window.transform.localScale = Vector3.zero;
        window.transform.DOScale(Vector3.one, window.Duration).SetAutoKill(false).Pause().OnRewind(() =>
         {
             DestoryWindow(window);
         });
        if (isOpen)
        {
            window.transform.DOPlayForward();
        }
        else
        {
            window.transform.DOPlayBackwards();
        }
    }
    /// <summary>
    /// �Ӳ�ͬ������ʾ����
    /// </summary>
    /// <param name="window">������</param>
    /// <param name="dirIndex">0--���ϵ��£� 1--���µ��� 2--������ 3--���ҵ���</param>
    /// <param name="isOpen">�Ƿ��</param>

    private void ShowWindoweFromDir(UIWindowViewBase window, int dirIndex, bool isOpen)
    {
        window.gameObject.SetActive(true);
        Vector2 from = Vector3.zero;
        Vector3 to = window.transform.localPosition;
        switch (dirIndex)
        {
            case 0:
                from = new Vector2(0, 1000);
                break;
            case 1:
                from = new Vector2(0, -1000);
                break;
            case 2:
                from = new Vector2(-1400, 0);
                break;
            case 3:
                from = new Vector2(1400, 0);
                break;
        }
        window.transform.localPosition = from;
        window.transform.DOLocalMove(to, window.Duration).SetAutoKill(false).Pause().OnRewind(() =>
        {
            DestoryWindow(window);
        });

        if (isOpen)
        {
            window.transform.DOPlayForward();
        }
        else
        {
            window.transform.DOPlayBackwards();
        }
    }
    #endregion
    /// <summary>
    /// ���ٴ���
    /// </summary>
    private void DestoryWindow(UIWindowViewBase window)
    {
        dicWindow.Remove(window.ViewType);
        GameObject.Destroy(window.gameObject);
    }




}
