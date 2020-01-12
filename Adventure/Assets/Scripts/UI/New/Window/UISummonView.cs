using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public struct SummonInfoTransfer
    {
        public string NickName;
        public int Quality;
        public string HeroPic;
        public bool IsDebris;
        public int DebrisCount;
        public static SummonInfoTransfer Empty()
        {
            return new SummonInfoTransfer();
        }
    }

    public class UISummonView : UIWindowViewBase
    {
        [SerializeField]
        private Text m_AllCoin;
        [SerializeField]
        private Text m_OneCoin;
        [SerializeField]
        private Text m_FiveCoin;
        private Stack<SummonInfoTransfer> m_SummonInfoStack;
        private UISummonResultView m_SummonResultView;


        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnSummonOne":
                    RoleDispatcher.Instance.Dispatc(ConstDefine.UI_Summon_OneSummon, null);
                    break;
                case "btnSummonFive":
                    RoleDispatcher.Instance.Dispatc(ConstDefine.UI_Summon_FiveSummon, null);
                    break;
            }
        }


        public void SetUI(int allCoin, int oneCoin, int fiveCoin)
        {
            m_AllCoin.text = allCoin.ToString();
            m_OneCoin.text = oneCoin.ToString();
            m_FiveCoin.text = fiveCoin.ToString();
            if (m_SummonResultView == null)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "SummonResultView");
                go.transform.SetParent(this.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                m_SummonResultView = go.GetComponent<UISummonResultView>();
                m_SummonResultView.Hide();
                m_SummonResultView.OnClick += PopSummonResult;
            }
        }

        public void Refresh(int allCoin)
        {
            m_AllCoin.text = allCoin.ToString();
        }

        public void SummonResult(Stack<SummonInfoTransfer> summonInfoStack)
        {
            m_SummonInfoStack = summonInfoStack;
            PopSummonResult();
        }
        private void PopSummonResult()
        {
            if (m_SummonInfoStack == null) return;
            if (m_SummonInfoStack.Count <= 0)
            {
                m_SummonResultView.Hide();
                return;
            }
            SummonInfoTransfer temp = m_SummonInfoStack.Pop();
            m_SummonResultView.Show(temp);
        }

    }
}
