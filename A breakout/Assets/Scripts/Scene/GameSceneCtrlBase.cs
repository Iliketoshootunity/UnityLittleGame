using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFrameWork
{
    /// <summary>
    /// 3d场景控制器基类
    /// </summary>
    public class GameSceneCtrlBase : MonoBehaviour
    {
        void Awake()
        {
            OnAwake();
        }
        void Start()
        {
            OnStart();
        }

        void Update()
        {
            OnUpdate();
        }
        void OnDestroy()
        {
            BeforeOnDestory();
        }
        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void BeforeOnDestory() { }
        protected virtual void OnMainUIComplete() { }

    }
}


