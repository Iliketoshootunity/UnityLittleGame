using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyFrameWork.UI
{



    public enum MsgButtonType
    {
        Ok,
        OkAndCancel
    }

    public class UIMessageView : MonoBehaviour
    {

        /// <summary>
        /// 标题
        /// </summary>
        public Text TitleText;

        /// <summary>
        /// 内容
        /// </summary>
        public Text ContentText;

        /// <summary>
        /// 确定按钮
        /// </summary>
        public Button OKButton;

        /// <summary>
        /// 取消按钮
        /// </summary>
        public Button CancleButton;

        /// <summary>
        /// 点击按钮事件
        /// </summary>
        [HideInInspector] public Action ClickOkAction;

        /// <summary>
        /// 点击取消按钮事件
        /// </summary>
        [HideInInspector] public Action ClickCancleAction;

        private void Start()
        {
            EventTriggerListener.Get(OKButton.gameObject).onClick += OnClickOKButton;
            EventTriggerListener.Get(CancleButton.gameObject).onClick += OnClickCancelButton;
        }

        private void OnClickCancelButton(GameObject go)
        {
            if (ClickCancleAction != null) ClickCancleAction();
            Cloese();
        }

        private void OnClickOKButton(GameObject go)
        {
            if (ClickOkAction != null) ClickOkAction();
            Cloese();
        }

        private void Cloese()
        {
            transform.localPosition = new Vector3(0, 5000, 0);
        }

        public void Show(string titleName, string content, MsgButtonType buttonType = MsgButtonType.Ok,
            Action OnClickOK = null, Action OnClickCancle = null)
        {
            transform.localPosition = Vector3.zero;
            TitleText.text = titleName;
            ContentText.text = content;
            this.ClickOkAction = OnClickOK;
            this.ClickCancleAction = OnClickCancle;
            if (buttonType == MsgButtonType.Ok)
            {
                OKButton.transform.localPosition = new Vector3(0, OKButton.transform.localPosition.y,
                    OKButton.transform.localPosition.z);
                CancleButton.gameObject.SetActive(false);
            }
            else
            {
                OKButton.transform.localPosition = new Vector3(-80, OKButton.transform.localPosition.y,
                    OKButton.transform.localPosition.z);
                CancleButton.gameObject.SetActive(true);
            }

        }

        private void OnDestroy()
        {
            TitleText = null;
            ContentText = null;
            OKButton = null;
            CancleButton = null;
            ClickOkAction = null;
            ClickCancleAction = null;
        }
    }
}
