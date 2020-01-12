using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI层级管理器
/// </summary>
public class LayerMgr : Singleton<LayerMgr>
{

    private int layerIndex = 50;

    /// <summary>
    /// 重置，当切换场景或者当前场景没有UI窗口时的时候调用
    /// </summary>
    public void Reset()
    {
        layerIndex = 0;
    }

    public void CheckUIPanelCount()
    {
        if (UIViewUtil.Instance.OpenWindowCount == 0)
        {
            Reset();
        }
    }

    /// <summary>
    /// 设置UI深度，确保在最上层
    /// </summary>
    public void SetUILayerDepth(GameObject go)
    {
        layerIndex += 1;
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.sortingOrder = layerIndex;
    }

}
