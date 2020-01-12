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

        /// �ҵ�����
        /// </summary>
        public UIWinndowContainerType ContainerType = UIWinndowContainerType.Center;

        /// <summary>
        /// ��ʾ��������
        /// </summary>
        public UIWindowShowAniType ShowAniType = UIWindowShowAniType.Normal;

        /// <summary>
        /// �رջ��ߴ򿪶������¼�
        /// </summary>
        public float Duration = 0.2f;

        /// <summary>
        /// ����������
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
