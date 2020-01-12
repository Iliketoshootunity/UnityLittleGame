using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyFrameWork.UI;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class MessageCtrl : Singleton<MessageCtrl>
    {

        private GameObject go;
        public void Show(string titleName, string content, MsgButtonType buttonType = MsgButtonType.Ok,
            Action OnClickOK = null, Action OnClickCancle = null)
        {
            if (go == null)
            {
                go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindow, "panMsg", true);
            }

            if (go == null) return;
            Transform parent = UISceneCtrl.Instance.CurrentUIScene.ContainerCenter;
            UIMessageView window = go.GetComponent<UIMessageView>();
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            window.Show(titleName, content, buttonType, OnClickOK, OnClickCancle);
        }

    }
}