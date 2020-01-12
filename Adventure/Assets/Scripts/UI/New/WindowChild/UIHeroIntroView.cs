using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UIHeroIntroView : UISubViewBase
    {
        public Text HeroName;
        public Text HeroDesc;
        public Text Skill1Name;
        public Text Skill2Name;
        public Text Skill1Desc;
        public Text Skill2Desc;

        public void SetUI(DataTransfer data)
        {
            string heroName = data.GetData<string>("HeroName");
            string heroDesc = data.GetData<string>("HeroDesc");
            string skill1Name = data.GetData<string>("Skill1Name");
            string skill1Desc = data.GetData<string>("Skill1Desc");
            string skill2Name = data.GetData<string>("Skill2Name");
            string skill2Desc = data.GetData<string>("Skill2Desc");

            HeroName.text = heroName;
            HeroDesc.text = heroDesc;
            Skill1Name.text = skill1Name;
            Skill1Desc.text = skill1Desc;
            Skill2Name.text = skill2Name;
            Skill2Desc.text = skill2Desc;
        }
    }
}
