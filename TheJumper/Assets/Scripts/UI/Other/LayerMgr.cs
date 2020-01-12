using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI�㼶������
/// </summary>
public class LayerMgr : Singleton<LayerMgr>
{

    private int layerIndex = 50;

    /// <summary>
    /// ���ã����л��������ߵ�ǰ����û��UI����ʱ��ʱ�����
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
    /// ����UI��ȣ�ȷ�������ϲ�
    /// </summary>
    public void SetUILayerDepth(GameObject go)
    {
        layerIndex += 1;
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.sortingOrder = layerIndex;
    }

}
