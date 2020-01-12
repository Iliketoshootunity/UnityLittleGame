using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public enum ChangeSceneType
    {
        Open,
        Close,
        All
    }
    public class ChangeSceneCtrl : Singleton<ChangeSceneCtrl>
    {
        private GameObject go;
        public void Show(ChangeSceneType changeType, float time = 1, Action onEnd = null)
        {
            if (go == null)
            {
                go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindow, "panChangeScene", true);
            }
            if (go == null) return;
            UIChangeSceneView window = go.GetComponent<UIChangeSceneView>();
            window.SetEnable(true);
            Transform parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            window.Show(changeType, time, () => { if (onEnd != null) { onEnd(); }; window.SetEnable(false); ; });
            //EditorApplication.isPaused = true;
        }

    }


}
