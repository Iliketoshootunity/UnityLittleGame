using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIFailView : UIWindowViewBase
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
                case "btnReturn":
                    object[] o = new object[1];
                    o[0] = "Level";
                    UIDispatcher.Instance.Dispatc(ConstDefine.NextScene, o);
                    break;
                case "btnRestart":
                    UIDispatcher.Instance.Dispatc(ConstDefine.Restart, null);
                    break;
            }
        }
    }
}
