using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;

namespace EasyFrameWork
{
    public class UILevelItemView : UISubViewBase
    {
        [SerializeField]
        private Text m_LevelName;
        [SerializeField]
        private Image m_LevelIcon;
        [SerializeField]
        private Button m_Button;

        private bool m_CanPlay;

        private int m_LevelId;
        private int m_LevelGrade;

        public Action<int, int> OnClick;
        protected override void OnStart()
        {
            base.OnStart();
            m_Button.onClick.AddListener(() =>
            {
                if (m_CanPlay)
                {
                    if (OnClick != null)
                    {
                        OnClick(m_LevelId, m_LevelGrade);
                    }
                }

            });
        }

        public void SetUI(int levelId, int grade, bool canPlay, string name, string icon, Vector2 pos)
        {
            m_LevelId = levelId;
            m_LevelGrade = grade;
            m_CanPlay = canPlay;
            m_LevelName.text = name;
            ((RectTransform)transform).anchoredPosition = pos;
            CanPlayOrNot();
        }

        public void CanPlayOrNot()
        {
            if (!m_CanPlay)
            {
                m_LevelIcon.color = Color.grey;
            }
        }

    }
}
