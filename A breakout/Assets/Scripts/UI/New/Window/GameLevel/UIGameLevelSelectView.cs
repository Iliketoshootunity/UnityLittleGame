using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelSelectView : UIWindowViewBase
    {
        [SerializeField]
        private RectTransform m_Grid;
        [SerializeField]
        private RectTransform m_BtnLeft;
        [SerializeField]
        private RectTransform m_BtnRight;
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
        private List<UIGameLevelItemView> m_LevelViewList;
        private List<Tween> m_TweenList;
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
                m_BtnLeft.transform.DoScale(Vector3.one * 1.1f, 0.1f).SetAnimatorCurve(m_BtnCurve).SetLoopType(TweenLoopType.YoYo).SetLoopCount(1); ;
            }
            else if (go.name == "btnRight")
            {
                Refresh(false);
                m_BtnRight.transform.DoScale(-Vector3.one * 1.1f, 0.1f).SetAnimatorCurve(m_BtnCurve).SetLoopType(TweenLoopType.YoYo).SetLoopCount(1);
            }
            EazySoundManager.PlayUISound(Global.Instance.UISound, 1, true);
        }
        public void SetUI(DataTransfer data)
        {
            StartCoroutine(SetUIIE(data));
        }
        private IEnumerator SetUIIE(DataTransfer data)
        {
            m_LevelViewList = new List<UIGameLevelItemView>();
            m_TweenList = new List<Tween>();
            m_GridLayoutGroup = m_Grid.GetComponent<GridLayoutGroup>();
            float weightScale = (Screen.width * (1136f / Screen.height)) / 852f;
            m_GridLayoutGroup.cellSize *= weightScale;
            m_BtnLeft.sizeDelta *= weightScale;
            m_BtnRight.sizeDelta *= weightScale;
            m_BtnLeft.anchoredPosition3D *= weightScale;
            m_BtnRight.anchoredPosition3D *= weightScale;
            for (int i = 0; i < 9; i++)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "GameLevelItem", isCache: true);
                go.transform.SetParent(m_Grid);
                go.transform.localScale = Vector3.one;
                go.transform.localScale = Vector3.one;
                UIGameLevelItemView view = go.GetComponent<UIGameLevelItemView>();
                view.OnClickBtn += OnClickGameLevelItemBtn;
                m_LevelViewList.Add(view);
            }
            m_MaxPassLevel = data.GetData<int>(ConstDefine.MaxPassLevel);
            m_MaxLevel = data.GetData<int>(ConstDefine.MaxLevel);
            yield return null;
            bool isRun = true;
            m_Index = 1;
            while (isRun)
            {
                int index = m_Index;
                index += 9;
                if (index <= m_MaxPassLevel)
                {
                    m_Index += 9;
                }
                else
                {
                    m_Index += 9;
                    isRun = false;
                }
            }
            Refresh(true);
        }
        private void Refresh(bool isLeft)
        {
            for (int i = 0; i < 9; i++)
            {
                m_LevelViewList[i].gameObject.SetActive(true);
            }
            if (isLeft)
            {
                if (m_Index < m_MaxLevel)
                {
                    m_BtnRight.gameObject.SetActive(true);
                }
                else
                {
                    m_BtnRight.gameObject.SetActive(false);
                }
                m_Index -= 9;
                if (m_Index <= 1)
                {
                    m_Index = 1;
                    m_BtnLeft.gameObject.SetActive(false);
                }


            }
            else
            {
                m_Index += 9;
                if (m_Index >= m_MaxLevel - 9)
                {
                    m_BtnRight.gameObject.SetActive(false);
                }
                m_BtnLeft.gameObject.SetActive(true);
            }
            int end = m_Index + 9;
            int index = 0;
            for (int i = m_Index; i < end; i++)
            {
                if (i <= m_MaxLevel)
                {
                    DataTransfer data = new DataTransfer();
                    data.SetData<int>(ConstDefine.GameLevelItem_Level, i);
                    if (i <= m_MaxPassLevel)
                    {
                        data.SetData<bool>(ConstDefine.GameLevelItem_IsLock, false);
                    }
                    else
                    {
                        data.SetData<bool>(ConstDefine.GameLevelItem_IsLock, true);
                    }
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
            int index2 = index1 + 3;
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(ColumnAniamtion(index1, index2));
                yield return new WaitForSeconds(m_RowInterval);
                index1 += 3;
                index2 = index1 + 3;
            }
        }

        private IEnumerator ColumnAniamtion(int index1, int index2)
        {
            for (int j = index1; j < index2; j++)
            {
                if (m_LevelViewList[j].gameObject.activeSelf)
                {
                    Tween t = m_LevelViewList[j].transform.DoScale(Vector3.one, m_ScaleTime).SetIsFrom(false).SetAnimatorCurve(m_ScaleCurve).SetBaseSpeed(false);
                    m_TweenList.Add(t);
                    yield return new WaitForSeconds(m_ColumnInterval);
                }
            }
        }
        private void OnClickGameLevelItemBtn(int obj)
        {
            object[] objs = new object[1];
            objs[0] = obj;
            UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSelectViewClickItmeBtn, objs);
        }
    }
}
