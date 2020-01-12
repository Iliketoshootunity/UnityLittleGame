using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class MainSceneCtrl : GameSceneCtrlBase
    {
        private UIMainSceneView m_View;
        public static MainSceneCtrl Instance;
        protected override void OnAwake()
        {
            base.OnAwake();
            Instance = this;
            m_View = UISceneCtrl.Instance.Load(UISceneType.Main).GetComponent<UIMainSceneView>();
            m_View.OnLoadComplete += OnMainUIComplete;
            //-------------测试
            SimulatedDatabase.Instance.AddCoin(100000);
            //-------------测试结束
        }

        protected override void OnMainUIComplete()
        {
            GameLevelCtrl.Instance.SetWorldMap(0);
        }

    }
}
