using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class UIAllHeroInfoTransfer
    {
        public struct UIHeroInfo
        {
            public long RoleId;
            public int HeroId;
            public string HeroIcon;
            public string HeroName;
            public int AttackArea;
            public int AttackRange;
            public string JobIcon;
            public bool IsFight;
        }
        public List<UIHeroInfo> HeroInfoList = new List<UIHeroInfo>();
    }

    public class UIGameSceneView : UISceneViewBase
    {
        public Action ClickFight;
        public GridLayoutGroup HeroGrid;
        public ScrollRect HeroScoll;

        public RectTransform HeroGridRect;
        public RectTransform HerosContains;

        public AnimationCurve AniCurve;
        public float AniTime;
        public float Height;

        private List<UIHeroView> m_HeroViewList;
        public Func<long, bool, GameObject, bool> CongfigueHeroEvent;

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnEditorHero":
                    HeroGridAni(true);
                    break;
                case "btnReturn":
                    HeroGridAni(false);
                    break;
                case "btnFight":
                    if (ClickFight != null)
                    {
                        ClickFight();
                    }
                    break;
            }
        }

        public void SetUI(UIAllHeroInfoTransfer info)
        {
            StartCoroutine(SetUIIE(info));
        }

        private IEnumerator SetUIIE(UIAllHeroInfoTransfer info)
        {
            //第一次载入,创建角色在主界面做，传过来的永远是一样的数量
            if (m_HeroViewList == null)
            {
                m_HeroViewList = new List<UIHeroView>();
                for (int i = 0; i < info.HeroInfoList.Count; i++)
                {
                    UIHeroView view = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "HeroItem").GetComponent<UIHeroView>();
                    m_HeroViewList.Add(view);
                    view.transform.SetParent(HeroGrid.transform);
                    view.transform.localScale = Vector3.one;
                    view.transform.localPosition = Vector3.zero;
                    view.ShowHeroInfoEvent += OnShowHeroInfoEventBack;
                    view.HideHeroInfoEvent += OnHideHeroInfoEventBack;
                    view.BeginDragHeroEvent += OnBeginDragHeroEventCallBack;
                    view.UpdateDragHeroEvent += OnUpdateDragHeroEventBack;
                    view.GoFightEvent += OnGoFightCallBack;
                    if (i % 9 == 0)
                    {
                        yield return null;
                    }
                }
            }

            //再次刷新
            for (int i = 0; i < m_HeroViewList.Count; i++)
            {
                m_HeroViewList[i].SetUI(HeroScoll, info.HeroInfoList[i].IsFight, info.HeroInfoList[i].RoleId, info.HeroInfoList[i].HeroId, info.HeroInfoList[i].HeroIcon, info.HeroInfoList[i].AttackArea, info.HeroInfoList[i].AttackRange, info.HeroInfoList[i].JobIcon, info.HeroInfoList[i].HeroName);
            }
        }


        private void OnGoFightCallBack(long roleId, Vector2 screenPos)
        {
            object[] objs = new object[2];
            objs[0] = roleId;
            objs[1] = screenPos;
            UIDispatcher.Instance.Dispatc(ConstDefine.UIHero_GoFight, objs);
        }

        private void OnShowHeroInfoEventBack(long roleId)
        {
            object[] objs = new object[1];
            objs[0] = roleId;
            UIDispatcher.Instance.Dispatc(ConstDefine.Hero_ShowHeroIntro, objs);
        }
        private void OnHideHeroInfoEventBack()
        {
            UIDispatcher.Instance.Dispatc(ConstDefine.Hero_HideHeroIntro, null);
        }
        private void OnBeginDragHeroEventCallBack(int heroId)
        {
            object[] objs = new object[1];
            objs[0] = heroId;
            UIDispatcher.Instance.Dispatc(ConstDefine.UIHero_BeginDragTheHeroItem, objs);
        }

        private void OnUpdateDragHeroEventBack(Vector2 pos)
        {
            object[] objs = new object[1];
            objs[0] = pos;
            UIDispatcher.Instance.Dispatc(ConstDefine.UIHero_UpdateDragTheHeroItem, objs);
        }
        private void HeroGridAni(bool isUp)
        {
            Vector2 relative1 = Vector2.zero;
            if (isUp)
            {
                relative1 = new Vector2(0, Height);
            }
            else
            {
                relative1 = new Vector2(0, -Height);

            }
            HeroGridRect.DoMove(relative1, AniTime).SetIsRelative(true).SetIsLocal(true).SetAnimatorCurve(AniCurve);
        }
        private void BottomAni()
        {

        }

    }
}
