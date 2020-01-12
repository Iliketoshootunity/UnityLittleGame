using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork.UI
{
    public class UISubViewBase : MonoBehaviour
    {

        void Awake()
        {
            OnAwake();
        }

        // Use this for initialization
        void Start()
        {
            OnStart();

        }

        void OnDestroy()
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

    }
}
