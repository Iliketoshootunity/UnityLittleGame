using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public class UIAllHeroView : UIWindowViewBase
    {
        [SerializeField]
        private GridLayoutGroup m_Grid;
        private List<UISimpleHeroInfoView> m_HeroInfoViewList;

        public void SetUI(DataTransfer data)
        {
            StartCoroutine(SetUIIE(data));
        }

        private IEnumerator SetUIIE(DataTransfer data)
        {
            List<DataTransfer> datas = data.GetData<List<DataTransfer>>(ConstDefine.UI_AllHero_Content);
            m_HeroInfoViewList = new List<UISimpleHeroInfoView>();
            for (int i = 0; i < datas.Count; i++)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "SimpleHeroInfo", isCache: true);
                if (go != null)
                {
                    go.transform.SetParent(m_Grid.transform);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;
                    UISimpleHeroInfoView heroInfo = go.GetComponent<UISimpleHeroInfoView>();
                    if (heroInfo != null)
                    {
                        long roleId = datas[i].GetData<long>(ConstDefine.UI_AllHero_RoleId);
                        int heroStar = datas[i].GetData<int>(ConstDefine.UI_AllHero_HeroStar);
                        string heroName = datas[i].GetData<string>(ConstDefine.UI_AllHero_HeroName);
                        string heroIcon = datas[i].GetData<string>(ConstDefine.UI_AllHero_HeroHead);
                        string heroSkillIcon = datas[i].GetData<string>(ConstDefine.UI_AllHero_HeroSkillICon);
                        heroInfo.SetUI(heroName, heroIcon, heroSkillIcon, heroStar, roleId);
                        m_HeroInfoViewList.Add(heroInfo);
                        heroInfo.OnClick += OnClickSimpleHeroInfoCallBack;
                    }
                    if (i % 9 == 0)
                    {
                        yield return null;
                    }

                }

            }
        }

        private void OnClickSimpleHeroInfoCallBack(long roleid)
        {
            object[] objs = new object[1];
            objs[0] = roleid;
            UIDispatcher.Instance.Dispatc(ConstDefine.UI_AllHero_ClickSimpleHeroInfo, objs);
        }
    }
}
