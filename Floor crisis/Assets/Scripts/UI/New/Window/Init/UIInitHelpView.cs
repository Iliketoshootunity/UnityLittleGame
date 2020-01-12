using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
	public class UIInitHelpView : UIWindowViewBase
    {
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            EazySoundManager.PlayUISound(Global.Instance.BtnClip);
        }
    }
}
