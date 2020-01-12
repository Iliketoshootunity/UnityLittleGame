using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork.UI
{
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
            go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene,
                string.Format("UI Root_{0}", type.ToString()));
            CurrentUIScene = go.GetComponent<UISceneViewBase>();
            return go;
        }


    }
}
