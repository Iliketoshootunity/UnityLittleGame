using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIWinView : UIWindowViewBase
    {
        [SerializeField]
        private AudioClip m_Clip;
        protected override void OnStart()
        {
            base.OnStart();
            EazySoundManager.PlayUISound(m_Clip);
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnNextLevel":
                    UIDispatcher.Instance.Dispatc(ConstDefine.NextLevel, null);
                    break;
                case "btnRestart":
                    UIDispatcher.Instance.Dispatc(ConstDefine.Restart, null);
                    break;
                case "btnReturn":
                    object[] objs = new object[1];
                    objs[0] = "Level";
                    UIDispatcher.Instance.Dispatc(ConstDefine.NextScene, objs);
                    break;
            }
        }
    }
}
