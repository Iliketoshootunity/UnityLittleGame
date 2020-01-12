﻿using UnityEngine;
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
        OpenAndClose,
        CloseAndOpen
    }
    public class ChangeSceneCtrl : Singleton<ChangeSceneCtrl>
    {
        private GameObject go;

        public void Show(ChangeSceneType changeType = ChangeSceneType.Open, Action onEnd = null)
        {
            if (go == null)
            {
                go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindow, "panChangeScene", true);
            }
            if (go == null) return;
            go.SetActive(true);
            Transform parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            UIChangeSceneView window = go.GetComponent<UIChangeSceneView>();
            window.Show(changeType, () =>
            {
                if (onEnd != null) onEnd();
                if (changeType == ChangeSceneType.Open || changeType == ChangeSceneType.CloseAndOpen)
                {
                    go.SetActive(false);
                }
               
            });
        }
    }
}