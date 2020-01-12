using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class UIFailView : UIWindowViewBase
    {
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
            }
        }
    }
}
