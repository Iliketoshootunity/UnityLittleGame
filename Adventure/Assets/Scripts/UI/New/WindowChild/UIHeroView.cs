using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace EasyFrameWork
{
    public class UIHeroView : UIHeroBaseItem
    {
        [SerializeField]
        private ScrollRect m_ScrollRect;

        protected override bool m_FightHeroItemView
        {
            get
            {
                return false;
            }
        }

        public virtual void SetUI(ScrollRect scrollRect, bool isEmpty, long roleId, int heroId, string heroIcon, int attackArea, int attackRange, string jobIcon, string heroName)
        {
            m_ScrollRect = scrollRect;
            PrepareView(roleId, heroId, heroIcon, attackArea, attackRange, jobIcon, heroName);
            if (isEmpty)
            {
                m_CurStatus = HeroItemStatus.Empty;
                ShowOrEmpty(false);
            }
            else
            {
                m_CurStatus = HeroItemStatus.NoOperations;
                ShowOrEmpty(true);
            }
            //m_NextStatus = HeroItemStatus.None;
        }
        protected override void BeginDrag(PointerEventData eventData)
        {
            if (m_ScrollRect == null || eventData == null)
            {
                return;
            }
            base.BeginDrag(eventData);
            m_ScrollRect.OnBeginDrag(eventData);
        }
        protected override void Drag(PointerEventData eventData)
        {
            base.Drag(eventData);
            bool inRect = RectTransformUtility.RectangleContainsScreenPoint(m_ParentRect, eventData.position, UISceneCtrl.Instance.CurrentUIScene.UICamera);
            if (inRect)
            {
                m_ScrollRect.OnDrag(eventData);
            }

        }
        protected override void EndDrag(PointerEventData eventData)
        {
            base.EndDrag(eventData);
            m_ScrollRect.OnEndDrag(eventData);
        }

        public void ToShow()
        {
            m_NextStatus = HeroItemStatus.NoOperations;
            ShowOrEmpty(true);
        }
    }
}
