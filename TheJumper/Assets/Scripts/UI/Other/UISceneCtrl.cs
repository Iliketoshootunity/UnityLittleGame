using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����UI������
/// </summary>

public class UISceneCtrl : Singleton<UISceneCtrl>
{

    public UISceneViewBase CurrentUIScene;

    public GameObject Load(UISceneType type)
    {
        GameObject go = null;
        //ע��������ʽҪ��ö��һ�� UI Root_Init
        go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIScene, string.Format("UI Root_{0}", type.ToString()));
        CurrentUIScene = go.GetComponent<UISceneViewBase>();
        return go;
    }

    
}
