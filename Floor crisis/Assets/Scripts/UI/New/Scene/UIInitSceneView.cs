using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIInitSceneView : UISceneViewBase
    {
        private bool m_IsPlaySound;

        protected override void OnStart()
        {
            base.OnStart();
            m_IsPlaySound = Global.Instance.IsPlaySound;
        }
        protected override void OnBtnClick(GameObject go)
        {
            EazySoundManager.PlayUISound(Global.Instance.BtnClip);
            switch (go.name)
            {
                case "btnStartGame":
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitScene_StartGame, null);
                    break;
                case "btnShop":
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitScene_Shop, null);
                    break;
                case "btnHelp":
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitScene_Help, null);
                    break;
                case "btnMusic":
                    m_IsPlaySound = !m_IsPlaySound;
                    object[] obj = new object[1];
                    obj[0] = m_IsPlaySound;
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitScene_Music, obj);
                    break;
            }

        }
    }
}
