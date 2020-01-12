using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace EasyFrameWork
{
    public enum HeroItemStatus
    {
        None = 0,
        NoOperations,
        DragView,
        DragItem,
        ShowIntro,
        HideIntro,
        Empty,
        Count
    }

    public abstract class UIHeroBaseItem : UISubViewBase, IBeginDragHandler, IDragHandler, IPointerExitHandler, IEndDragHandler, IPointerEnterHandler
    {
        public Image HeroIcon;
        public Image SkillIcon;
        public Image JobIcon;
        public Text HeroName;

        public Action<long> ShowHeroInfoEvent;
        public Action HideHeroInfoEvent;
        public Action<int> BeginDragHeroEvent;
        public Action<long, Vector2> GoFightEvent;
        public Action<Vector2> UpdateDragHeroEvent;

        protected long m_RoleId;
        protected int m_AttackType;
        protected int m_AttackRange;
        protected int m_HeroId;
        protected bool m_Enter;

        //按下超过0.5秒 开始发送生成拖拽物体消息
        protected float m_PointDownTimer = 0;
        protected Vector2 m_PreEventDataPos;
        protected RectTransform m_ParentRect;

        [SerializeField]
        protected HeroItemStatus m_CurStatus;
        [SerializeField]
        protected HeroItemStatus m_NextStatus;

        public HeroItemStatus GetCurStatus
        {
            get
            {
                return m_CurStatus;
            }
        }
        public long GetRoleId()
        {
            return m_RoleId;
        }

        protected abstract bool m_FightHeroItemView { get; }
        protected override void OnStart()
        {
            base.OnStart();
            m_ParentRect = transform.parent.parent.GetComponent<RectTransform>();

        }

        private void Update()
        {
            if (m_NextStatus != HeroItemStatus.None)
            {
                m_CurStatus = m_NextStatus;
                m_NextStatus = HeroItemStatus.None;
                switch (m_CurStatus)
                {
                    case HeroItemStatus.NoOperations:
                        EnterNoOperationsStatus();
                        break;
                    case HeroItemStatus.DragView:
                        EnterDragViewStatus();
                        break;
                    case HeroItemStatus.DragItem:
                        EnterDragItemStatus();
                        break;
                    case HeroItemStatus.ShowIntro:
                        EnterShowIntroStatus();
                        break;
                    case HeroItemStatus.HideIntro:
                        EneterHideIntroStatus();
                        break;
                    case HeroItemStatus.Empty:
                        EnterEmptyStatus();
                        break;
                }

            }
            if (m_CurStatus == HeroItemStatus.NoOperations)
            {
                if (m_Enter)
                {
                    m_PointDownTimer += Time.deltaTime;
                    if (m_PointDownTimer > 1.5f)
                    {
                        m_NextStatus = HeroItemStatus.ShowIntro;
                    }
                }
            }
        }

        protected virtual void EnterNoOperationsStatus()
        {
            m_PreEventDataPos = Vector2.zero;
            m_PointDownTimer = 0;
            m_Enter = false;
        }

        protected virtual void EnterDragViewStatus()
        {
            if (HideHeroInfoEvent != null)
            {
                HideHeroInfoEvent();
            }
            m_NextStatus = HeroItemStatus.NoOperations;
        }
        protected virtual void EnterDragItemStatus()
        {
            if (BeginDragHeroEvent != null)
            {
                BeginDragHeroEvent(m_HeroId);
            }
        }
        protected virtual void EnterShowIntroStatus()
        {
            if (ShowHeroInfoEvent != null)
            {
                ShowHeroInfoEvent(m_RoleId);
            }
        }
        protected virtual void EneterHideIntroStatus()
        {
            m_NextStatus = HeroItemStatus.NoOperations;
            UIDispatcher.Instance.Dispatc(ConstDefine.Hero_HideHeroIntro, null);
  
        }
        protected virtual void EnterEmptyStatus()
        {
            ShowOrEmpty(false);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag(eventData);
        }

        protected virtual void BeginDrag(PointerEventData eventData)
        {

        }
        public void OnDrag(PointerEventData eventData)
        {
            Drag(eventData);
        }

        protected virtual void Drag(PointerEventData eventData)
        {
            if (m_CurStatus == HeroItemStatus.NoOperations)
            {
                if (m_PreEventDataPos != Vector2.zero)
                {
                    Vector2 relative = eventData.position - m_PreEventDataPos;
                    if (Mathf.Abs(relative.x) >= Mathf.Abs(relative.y))
                    {
                        m_NextStatus = HeroItemStatus.DragView;
                    }
                    else
                    {
                        m_NextStatus = HeroItemStatus.DragItem;
                    }
                }
            }

            if (m_CurStatus == HeroItemStatus.DragItem)
            {
                if (UpdateDragHeroEvent != null)
                {
                    UpdateDragHeroEvent(eventData.position);
                }
            }
            m_PreEventDataPos = eventData.position;

        }
        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag(eventData);
        }

        protected virtual void EndDrag(PointerEventData eventData)
        {
            if (m_CurStatus == HeroItemStatus.DragItem)
            {
                if (GoFightEvent != null)
                {
                    GoFightEvent(m_RoleId, eventData.position);
                }
            }
            else if (m_CurStatus == HeroItemStatus.DragView)
            {
                m_NextStatus = HeroItemStatus.NoOperations;
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointEnter(eventData);
        }

        protected virtual void PointEnter(PointerEventData eventData)
        {
            if (m_CurStatus == HeroItemStatus.NoOperations)
            {
                m_Enter = true;
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit(eventData);
        }

        protected virtual void PointerExit(PointerEventData eventData)
        {
            m_Enter = false;
            if (m_CurStatus == HeroItemStatus.ShowIntro)
            {
                m_NextStatus = HeroItemStatus.HideIntro;
            }
        }
        protected virtual void PrepareView(long roleId, int heroId, string heroIcon, int attackArea, int attackRange, string jobIcon, string heroName)
        {
            HeroName.text = heroName;
            m_RoleId = roleId;
            m_HeroId = heroId;
            m_AttackType = attackArea;
            m_AttackRange = attackRange;
        }

        protected virtual void ShowOrEmpty(bool isShow)
        {
            if (!isShow)
            {
                HeroIcon.gameObject.SetActive(false);
                SkillIcon.gameObject.SetActive(false);
                JobIcon.gameObject.SetActive(false);
                HeroName.gameObject.SetActive(false);
            }
            else
            {
                HeroIcon.gameObject.SetActive(true);
                SkillIcon.gameObject.SetActive(true);
                JobIcon.gameObject.SetActive(true);
                HeroName.gameObject.SetActive(true);
            }
        }


    }
}

