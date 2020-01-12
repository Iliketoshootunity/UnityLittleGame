using LFrameWork.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyFrameWork.UI
{

    public class UIWindowViewBase : UIViewBase
    {

        /// 挂点类型
        /// </summary>
        public UIWinndowContainerType ContainerType = UIWinndowContainerType.Center;

        /// <summary>
        /// 显示动画类型
        /// </summary>
        public UIWindowShowAniType ShowAniType = UIWindowShowAniType.Normal;

        /// <summary>
        /// 关闭或者打开动画的事件
        /// </summary>
        public float Duration = 0.2f;

        /// <summary>
        /// 自身窗口类型
        /// </summary>
        [HideInInspector] public UIViewType ViewType = UIViewType.None;

        private UIViewType m_NextViewType = UIViewType.None;

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void BeforeOnDestory()
        {
            LayerMgr.Instance.CheckUIPanelCount();
            if (m_NextViewType != UIViewType.None)
            {
                UIViewMgr.Instance.OpenView(m_NextViewType);
            }

            m_NextViewType = UIViewType.None;
        }

        protected override void OnBtnClick(GameObject go)
        {
            if (go.name.EndsWith("btnClose", StringComparison.CurrentCultureIgnoreCase))
            {
                Close();
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound);
        }

        public virtual void Close()
        {
            UIViewUtil.Instance.CloseWindow(ViewType);
        }

        public virtual void CloseAndOpenNextView(UIViewType nextViewType)
        {
            m_NextViewType = nextViewType;
            this.Close();
        }
    }
}
