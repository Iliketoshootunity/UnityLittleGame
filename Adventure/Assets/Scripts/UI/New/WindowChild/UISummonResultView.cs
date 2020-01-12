using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;

namespace EasyFrameWork
{
    public class UISummonResultView : UISubViewBase
    {
        [SerializeField]
        private Text m_NickName;
        [SerializeField]
        private Image m_HeroPic;
        [SerializeField]
        private Image m_Quality;
        [SerializeField]
        private Button m_Button;
        [SerializeField]
        private Text m_DebrisCount;
        [SerializeField]
        private Transform m_NewHero;
        [SerializeField]
        private Transform m_OwerHero;
        public Action OnClick;


        protected override void OnStart()
        {
            base.OnStart();
            m_Button.onClick.AddListener(() => { if (OnClick != null) { OnClick(); } });
        }

        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            OnClick = null;
        }
        public void Show(SummonInfoTransfer summonInfo)
        {
            m_NickName.text = summonInfo.NickName;
            if (summonInfo.IsDebris)
            {
                m_NewHero.gameObject.SetActive(false);
                m_OwerHero.gameObject.SetActive(true);
                m_DebrisCount.text = summonInfo.DebrisCount.ToString();
            }
            else
            {
                m_NewHero.gameObject.SetActive(true);
                m_OwerHero.gameObject.SetActive(false);
            }
            ShowAni();
        }

        private void ShowAni()
        {
            ((RectTransform)transform).anchoredPosition3D = Vector3.zero;
        }
        public void Hide()
        {
            transform.localPosition = Vector3.one * 10000;
        }
    }
}
