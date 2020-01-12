using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;
using EasyFrameWork.UI;
using System;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class UISimpleHeroInfoView : UISubViewBase
    {
        [SerializeField]
        private Text m_HeroName;
        [SerializeField]
        private Image m_HeroIcon;
        [SerializeField]
        private Image m_SkillIcon;
        [SerializeField]
        private List<Image> m_StarList;
        [SerializeField]
        private Button m_Button;
        private int m_Star;
        private long m_RoleId;

        public Action<long> OnClick;

        protected override void OnStart()
        {
            base.OnStart();
            m_Button.onClick.AddListener(() =>
            {
                if (OnClick != null)
                {
                    OnClick(m_RoleId);
                }
            });
            Destroy(GetComponentInChildren<EventTriggerListener>());
        }
        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            OnClick = null;
        }
        public void SetUI(string name, string heroICon, string skillIcon, int star, long roleId)
        {
            m_HeroName.text = name;
            m_Star = star;
            m_RoleId = roleId;
            for (int i = 0; i < m_Star; i++)
            {
                m_StarList[i].color = Color.yellow;
            }
        }

    }
}
