using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 场景UI控制器
/// </summary>

public class UISceneCtrl : Singleton<UISceneCtrl>
{

    public UISceneViewBase CurrentUIScene;

    public GameObject Load(UISceneType type)
    {
        GameObject go = null;
        //注意命名格式要和枚举一致 UI Root_Init
        go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIScene, string.Format("UI Root_{0}", type.ToString()));
        CurrentUIScene = go.GetComponent<UISceneViewBase>();
        return go;
    }

    
}
