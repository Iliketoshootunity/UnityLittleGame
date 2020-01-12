using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class UIWinView : UIWindowViewBase
    {
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnNextLevel":
                    UIDispatcher.Instance.Dispatc(ConstDefine.NextLevel, null);
                    break;
            }
        }
    }
}
