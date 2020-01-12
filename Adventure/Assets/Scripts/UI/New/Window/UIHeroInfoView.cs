using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UIHeroInfoView : UIWindowViewBase
    {
        [SerializeField]
        private Text m_HName;
        [SerializeField]
        private Text m_Quality;
        [SerializeField]
        private List<Image> m_StarList;
        [SerializeField]
        private Text m_Level;
        [SerializeField]
        private Text m_Hp;
        [SerializeField]
        private Text m_PhyAtk;
        [SerializeField]
        private Text m_MgicAtk;
        [SerializeField]
        private Text m_Cri;
        [SerializeField]
        private Text m_CriValue;
        [SerializeField]
        private Text m_PhyDef;
        [SerializeField]
        private Text m_MgicDef;
        [SerializeField]
        private Text m_NeedDebris;
        [SerializeField]
        private Text m_OwnedDebris;

        private long m_RoleId;


        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnUpgrade")
            {
                object[] objs = new object[1];
                objs[0] = m_RoleId;
                UIDispatcher.Instance.Dispatc(ConstDefine.UI_AllHero_ClickHeroUpgradeStar, objs);
            }
        }

        public void SetUI(DataTransfer data)
        {
            m_RoleId = data.GetData<long>(ConstDefine.UI_HeroInfo_RoleId);
            string heroName = data.GetData<string>(ConstDefine.UI_HeroInfo_HeroName);
            m_HName.text = heroName;

            int heroStar = data.GetData<int>(ConstDefine.UI_HeroInfo_HeroStar);
            RefreshStar(heroStar);

            int heroQuality = data.GetData<int>(ConstDefine.UI_HeroInfo_HeroQuality);
            if (heroQuality == 0)
            {
                m_Quality.text = "普通";
            }
            else if (heroQuality == 1)
            {
                m_Quality.text = "精英";
            }
            else if (heroQuality == 2)
            {
                m_Quality.text = "传奇";
            }

            int heroLevel = data.GetData<int>(ConstDefine.UI_HeroInfo_HeroLevel);
            m_Level.text = heroLevel.ToString();

            int hp = data.GetData<int>(ConstDefine.UI_HeroInfo_HeroHp);
            int mgicAtk = data.GetData<int>(ConstDefine.UI_HeroInfo_MgicAtk);
            int phyAtk = data.GetData<int>(ConstDefine.UI_HeroInfo_PhyAtk);
            int cri = data.GetData<int>(ConstDefine.UI_HeroInfo_Cri);
            float criValue = data.GetData<float>(ConstDefine.UI_HeroInfo_CriValue);
            int mgicDef = data.GetData<int>(ConstDefine.UI_HeroInfo_MgicDef);
            int phyDef = data.GetData<int>(ConstDefine.UI_HeroInfo_PhyDef);
            m_Hp.text = hp.ToString();
            m_MgicAtk.text = mgicAtk.ToString();
            m_PhyAtk.text = phyAtk.ToString();
            m_Cri.text = cri.ToString();
            m_CriValue.text = criValue.ToString();
            m_PhyDef.text = phyDef.ToString();
            m_MgicDef.text = phyDef.ToString();

            List<DataTransfer> skillDatas = data.GetData<List<DataTransfer>>(ConstDefine.UI_HeroInfo_SkillContent);
            for (int i = 0; i < skillDatas.Count; i++)
            {
                string skillName = skillDatas[i].GetData<string>(ConstDefine.UI_HeroInfo_SkillName);
                string skillIcon = skillDatas[i].GetData<string>(ConstDefine.UI_HeroInfo_SkillIcon);
                string skillDesc = skillDatas[i].GetData<string>(ConstDefine.UI_HeroInfo_SkillDesc);
            }

            int needDebris = data.GetData<int>(ConstDefine.UI_HeroInfo_NeedDebris);
            int ownedDebris = data.GetData<int>(ConstDefine.UI_HeroInfo_OwnedDebris);
            m_NeedDebris.text = needDebris.ToString();
            m_OwnedDebris.text = ownedDebris.ToString();
        }

        public void RefreshAfterUpgradeStar(DataTransfer data)
        {
            SetUI(data);
            //特效动画啥的
            //TODO
        }

        private void RefreshStar(int heroStar)
        {
            for (int i = 0; i < m_StarList.Count; i++)
            {
                m_StarList[i].color = Color.grey;
            }
            for (int i = 0; i < heroStar; i++)
            {
                m_StarList[i].color = Color.yellow;
            }
        }
    }
}
