using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyFrameWork.UI
{
    /// <summary>
    /// UI ”Õºª˘¿‡
    /// </summary>

    public class UIViewBase : MonoBehaviour
    {
        public Action OnLoadComplete;

        private void Awake()
        {
            OnAwake();
        }

        void Start()
        {
            OnStart();
            Button[] buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                EventTriggerListener.Get(buttons[i].gameObject).onClick += OnBtnClick;
            }

            if (OnLoadComplete != null)
            {
                OnLoadComplete();
            }
        }

        private void OnDestroy()
        {
            BeforeOnDestory();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void BeforeOnDestory()
        {
        }

        protected virtual void OnBtnClick(GameObject go)
        {
        }


    }
}
