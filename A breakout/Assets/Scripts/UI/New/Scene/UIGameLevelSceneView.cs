using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelSceneView : UISceneViewBase
    {
        [SerializeField]
        private Text m_GameLevelText;

        public void SetUI(int level)
        {
            m_GameLevelText.text = level.ToString();
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnPause")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.SceneGameLevelViewClickPauseBtn, null);
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound);
        }
    }
}
