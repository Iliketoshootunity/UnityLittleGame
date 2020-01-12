using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelFailView : UIWindowViewBase
    {
        [SerializeField]
        private Text m_ReportValue;
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnReturn":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelScene_FailWindow_Return, null);
                    break;
                case "btnContinue":
                    UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelScene_FailWindow_Continue, null);
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.BtnClip);
        }

        public void SetUI(int floorCount)
        {
            m_ReportValue.text = floorCount.ToString();
        }
    }
}
