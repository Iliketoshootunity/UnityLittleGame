using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace EasyFrameWork
{
    /// <summary>
    /// 主场景
    /// </summary>
	public class UIMainSceneView : UISceneViewBase
    {
        private List<UILevelItemView> m_LevelItemList;
        [SerializeField]
        private Transform m_LevelMap;
        [SerializeField]
        private Button m_MapCtrlButton;
        private RectTransform m_LevelMapRect;

        private int m_CurLevelPos;
        private Vector2 m_LevelMapMin;
        private Vector2 m_LevelMapMax;
        private Vector2 m_PreDragMapPos;

        private RectTransform m_CenterRect;
        private bool m_Init;
        private Vector2 m_PreDragPos;

        protected override void OnStart()
        {
            base.OnStart();

        }

        private void Update()
        {
            if (!m_Init)
            {
                EventTriggerListener mapEvents = m_MapCtrlButton.GetComponent<EventTriggerListener>();
                mapEvents.onBeginDrag += OnBeginDragMap;
                mapEvents.onDrag += OnDragMap;
                mapEvents.onEndDrag += OnEndDragMap;
                m_CenterRect = ContainerCenter.GetComponent<RectTransform>();
                m_Init = true;
            }
        }



        protected override void OnBtnClick(GameObject go)
        {
            object[] objs = new object[1];
            switch (go.name)
            {
                case "btnHero":
                    UIViewMgr.Instance.OpenView(UIViewType.AllHeroView);
                    break;
                case "btnSummon":
                    UIViewMgr.Instance.OpenView(UIViewType.SummonView);
                    break;
                case "btnTask":
                    UIViewMgr.Instance.OpenView(UIViewType.TaskView);
                    break;
                case "btnNormalMap":
                    objs[0] = 0;
                    UIDispatcher.Instance.Dispatc(ConstDefine.UI_WorldMap_ClickMapGrade, objs);
                    break;
                case "btnEliteMap":
                    objs[0] = 1;
                    UIDispatcher.Instance.Dispatc(ConstDefine.UI_WorldMap_ClickMapGrade, objs);
                    break;
                case "btnHellMap":
                    objs[0] = 2;
                    UIDispatcher.Instance.Dispatc(ConstDefine.UI_WorldMap_ClickMapGrade, objs);
                    break;
            }
        }

        public void SetWorldMap(DataTransfer data)
        {
            m_LevelMapRect = m_LevelMap.GetComponent<RectTransform>();
            m_LevelMapRect.anchoredPosition = Vector2.zero;
            Vector2 screenRect = new Vector2(Screen.width, Screen.height);
            Vector2 uiRect = new Vector2(Screen.width, 1136f * Screen.height / (float)Screen.width);
            Vector2 uiLeftAndTopPos = new Vector2(uiRect.x / 2f, uiRect.y / 2f);
            Vector2 LevelMapLeftAndTopPos = m_LevelMapRect.anchoredPosition + new Vector2(m_LevelMapRect.rect.width / 2f, m_LevelMapRect.rect.height / 2f);
            Vector2 relativePos = LevelMapLeftAndTopPos - uiLeftAndTopPos;
            m_LevelMapMin = m_LevelMapRect.anchoredPosition - relativePos;
            m_LevelMapMax = m_LevelMapRect.anchoredPosition + relativePos;

            m_LevelItemList = new List<UILevelItemView>();
            List<DataTransfer> levelList = data.GetData<List<DataTransfer>>(ConstDefine.UI_WorldMap_Content);
            bool isMatch = false;
            int maxCanPlayLevelId = data.GetData<int>(ConstDefine.UI_WorldMap_MaxCanPlayGameLevelId);
            for (int i = 0; i < levelList.Count; i++)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "LevelItem", isCache: true);
                if (go != null)
                {
                    go.transform.SetParent(m_LevelMap);
                    go.transform.localPosition = Vector2.zero;
                    go.transform.localScale = Vector3.one;
                    UILevelItemView view = go.GetComponent<UILevelItemView>();
                    if (view != null)
                    {
                        int levelId = levelList[i].GetData<int>(ConstDefine.UI_WorldMap_LevelId);
                        int grade = levelList[i].GetData<int>(ConstDefine.UI_WorldMap_LevelGrade);
                        string levelName = levelList[i].GetData<string>(ConstDefine.UI_WorldMap_LevelName);
                        string levelIcon = levelList[i].GetData<string>(ConstDefine.UI_WorldMap_LevelIcon);
                        Vector2 levelPos = levelList[i].GetData<Vector2>(ConstDefine.UI_WorldMap_LevelPos);
                        bool canPlay = levelList[i].GetData<bool>(ConstDefine.UI_WorldMap_CanPlay);
                        view.SetUI(levelId, grade, canPlay, levelName, levelIcon, levelPos);
                        view.OnClick += OnClickGameLevelItemCallBack;
                        if (maxCanPlayLevelId == -1001 && !isMatch)//未通关上一难度，从第一关开始
                        {
                            MatchPassGameLevel(levelPos);
                            isMatch = true;
                        }
                        else if (!isMatch)
                        {
                            if (maxCanPlayLevelId == levelId)
                            {
                                MatchPassGameLevel(levelPos);
                                isMatch = true;
                            }
                        }
                    }
                }
            }
            if (!isMatch)
            {
                Debug.LogError("错误：没有找到匹配的关卡");
            }

        }

        private void OnClickGameLevelItemCallBack(int levelId, int grade)
        {
            object[] objs = new object[2];
            objs[0] = levelId;
            objs[1] = grade;
            UIDispatcher.Instance.Dispatc(ConstDefine.UI_WorldMap_ClickLevel, objs);
        }

        private void OnBeginDragMap(GameObject go)
        {
            m_PreDragMapPos = Input.mousePosition;
        }

        private void OnDragMap(GameObject go)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 uiPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_CenterRect, mousePos, UICamera, out uiPos);
            Vector2 relative = Input.mousePosition - new Vector3(m_PreDragMapPos.x, m_PreDragMapPos.y, 0);
            m_LevelMapRect.anchoredPosition += relative;
            m_PreDragMapPos = Input.mousePosition;
            LevelMapRectBoundary();

        }
        private void OnEndDragMap(GameObject go)
        {
            m_PreDragMapPos = Vector2.zero;
        }

        private void MatchPassGameLevel(Vector2 pos)
        {
            Vector2 relative = pos - m_LevelMapRect.anchoredPosition;
            m_LevelMapRect.anchoredPosition = m_LevelMapRect.anchoredPosition - relative;
            LevelMapRectBoundary();
        }

        private void LevelMapRectBoundary()
        {
            if (m_LevelMapRect.anchoredPosition.x <= m_LevelMapMin.x)
            {
                m_LevelMapRect.anchoredPosition = new Vector2(m_LevelMapMin.x, m_LevelMapRect.anchoredPosition.y);
            }
            else if (m_LevelMapRect.anchoredPosition.x >= m_LevelMapMax.x)
            {
                m_LevelMapRect.anchoredPosition = new Vector2(m_LevelMapMax.x, m_LevelMapRect.anchoredPosition.y);
            }

            if (m_LevelMapRect.anchoredPosition.y <= m_LevelMapMin.y)
            {
                m_LevelMapRect.anchoredPosition = new Vector2(m_LevelMapRect.anchoredPosition.x, m_LevelMapMin.y);
            }
            else if (m_LevelMapRect.anchoredPosition.y >= m_LevelMapMax.y)
            {
                m_LevelMapRect.anchoredPosition = new Vector2(m_LevelMapRect.anchoredPosition.x, m_LevelMapMax.y);
            }
        }
    }
}
