using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UILevelSelectView : UIWindowViewBase
    {
        [SerializeField]
        private RectTransform m_Grid;
        [SerializeField]
        private RectTransform m_BtnLeft;
        [SerializeField]
        private RectTransform m_BtnRight;
        private int m_Count = 25;
        [SerializeField]
        private Vector2 m_RowAnCol = new Vector2(5, 5);
        [SerializeField]
        private float m_RowInterval = 0.5f;
        [SerializeField]
        private float m_ColumnInterval = 0.1f;
        [SerializeField]
        private float m_ScaleTime = 0.2f;
        [SerializeField]
        private AnimationCurve m_ScaleCurve;
        [SerializeField]
        private AnimationCurve m_BtnCurve;

        private GridLayoutGroup m_GridLayoutGroup;
        private List<UILevelItemView> m_LevelViewList;
        private List<Tween> m_TweenList;
        private List<int> m_StarInfo;
        private int m_Index;
        private int m_MaxLevel;
        private int m_MaxPassLevel;
        protected override void OnStart()
        {
            base.OnStart();
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnLeft")
            {
                Refresh(true);
                m_BtnLeft.transform.DoScale(-Vector3.one * 1.1f, 0.1f).SetAnimatorCurve(m_BtnCurve).SetLoopType(TweenLoopType.YoYo).SetLoopCount(1); ;
            }
            else if (go.name == "btnRight")
            {
                Refresh(false);
                m_BtnRight.transform.DoScale(Vector3.one * 1.1f, 0.1f).SetAnimatorCurve(m_BtnCurve).SetLoopType(TweenLoopType.YoYo).SetLoopCount(1);
            }
            //EazySoundManager.PlayUISound(Global.Instance.UISound, 1, true);
        }
        public void SetUI(DataTransfer data)
        {
            StartCoroutine(SetUIIE(data));
        }
        private IEnumerator SetUIIE(DataTransfer data)
        {
            m_Count = (int)(m_RowAnCol.x * m_RowAnCol.y);
            m_LevelViewList = new List<UILevelItemView>();
            m_TweenList = new List<Tween>();
            m_GridLayoutGroup = m_Grid.GetComponent<GridLayoutGroup>();
            float heightScale = Screen.height / (Screen.width * (852 / 1136f));
            m_GridLayoutGroup.cellSize *= heightScale;
            m_GridLayoutGroup.spacing *= heightScale;
            m_BtnLeft.sizeDelta *= heightScale;
            m_BtnRight.sizeDelta *= heightScale;
            m_BtnLeft.anchoredPosition3D *= heightScale;
            m_BtnRight.anchoredPosition3D *= heightScale;
            for (int i = 0; i < m_Count; i++)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "LevelItem", isCache: true);
                go.transform.SetParent(m_Grid);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                UILevelItemView view = go.GetComponent<UILevelItemView>();
                view.OnClickBtn += OnClickGameLevelItemBtn;
                m_LevelViewList.Add(view);
            }
            m_MaxPassLevel = data.GetData<int>(ConstDefine.MaxCanPlay);
            m_MaxLevel = data.GetData<int>(ConstDefine.MaxLevel);
            m_StarInfo = data.GetData<List<int>>(ConstDefine.StarInfo);
            yield return null;
            bool isRun = true;
            m_Index = 1;
            while (isRun)
            {
                int index = m_Index;
                index += m_Count;
                if (index <= m_MaxPassLevel)
                {
                    m_Index += m_Count;
                }
                else
                {
                    m_Index += m_Count;
                    isRun = false;
                }
            }
            Refresh(true);
        }
        private void Refresh(bool isLeft)
        {
            for (int i = 0; i < m_Count; i++)
            {
                m_LevelViewList[i].gameObject.SetActive(true);
            }
            if (isLeft)
            {
                m_Index -= m_Count;
                if (m_Index <= 1)
                {
                    m_Index = 1;
                    m_BtnLeft.gameObject.SetActive(false);
                }
                m_BtnRight.gameObject.SetActive(true);
            }
            else
            {
                m_Index += m_Count;
                if (m_Index >= m_MaxLevel - m_Count)
                {
                    m_BtnRight.gameObject.SetActive(false);
                }
                m_BtnLeft.gameObject.SetActive(true);
            }
            int end = m_Index + m_Count;
            int index = 0;
            for (int i = m_Index; i < end; i++)
            {
                if (i <= m_MaxLevel)
                {
                    DataTransfer data = new DataTransfer();
                    data.SetData<int>(ConstDefine.Item_Level, i);
                    if (i <= m_MaxPassLevel)
                    {
                        data.SetData<bool>(ConstDefine.Item_IsLock, false);
                    }
                    else
                    {
                        data.SetData<bool>(ConstDefine.Item_IsLock, true);
                    }
                    data.SetData<int>(ConstDefine.Item_HasStar, m_StarInfo[i - 1]);
                    m_LevelViewList[index].SetUI(data);
                }
                else
                {
                    m_LevelViewList[index].gameObject.SetActive(false);
                }

                index++;
            }
            Animation();
        }

        private void Animation()
        {
            this.StopAllCoroutines();
            for (int i = 0; i < m_TweenList.Count; i++)
            {
                m_TweenList[i].DoKill();
            }
            m_TweenList.Clear();
            StartCoroutine(AnimationIE());
        }
        private IEnumerator AnimationIE()
        {
            for (int i = 0; i < m_LevelViewList.Count; i++)
            {
                m_LevelViewList[i].transform.localScale = Vector3.zero;
            }
            int index1 = 0;
            int index2 = index1 + (int)m_RowAnCol.y;
            for (int i = 0; i < (int)m_RowAnCol.x; i++)
            {
                StartCoroutine(ColumnAniamtion(index1, index2));
                yield return new WaitForSeconds(m_RowInterval);
                index1 += (int)m_RowAnCol.y;
                index2 = index1 + (int)m_RowAnCol.y;
            }
        }

        private IEnumerator ColumnAniamtion(int index1, int index2)
        {
            for (int j = index1; j < index2; j++)
            {
                if (m_LevelViewList[j].gameObject.activeSelf)
                {
                    Tween t = m_LevelViewList[j].transform.DoScale(Vector3.one, m_ScaleTime).SetIsFrom(false).SetAnimatorCurve(m_ScaleCurve).SetIsSpeed(false);
                    m_TweenList.Add(t);
                    yield return new WaitForSeconds(m_ColumnInterval);
                }
            }
        }
        private void OnClickGameLevelItemBtn(int obj)
        {
            object[] objs = new object[1];
            objs[0] = obj;
            UIDispatcher.Instance.Dispatc(ConstDefine.Item, objs);
        }
    }
}
