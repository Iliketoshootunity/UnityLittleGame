using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UILevelSceneView : UISceneViewBase
    {
        [SerializeField]
        private Text m_MaxStarCount;
        [SerializeField]
        private Text m_GetStarCount;
        protected override void OnStart()
        {
            base.OnStart();
            UIViewMgr.Instance.OpenView(UIViewType.SelectLevel);
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnReturn")
            {
                object[] os = new object[1];
                os[0] = "Init";
                UIDispatcher.Instance.Dispatc(ConstDefine.NextScene, os);
            }

        }

        public void SetUI(int maxStar, int getStar)
        {
            m_MaxStarCount.text = maxStar.ToString();
            m_GetStarCount.text = getStar.ToString();
        }
    }
}
