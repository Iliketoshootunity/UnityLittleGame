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
    public class UIInitShopView : UIWindowViewBase
    {

        [SerializeField]
        private List<UIJobItemView> m_JobList;
        [SerializeField]
        private GameObject m_MessageObj;
        [SerializeField]
        private Text m_MessageContent;
        [SerializeField]
        private Text m_Coin;

        private UIJobItemView m_JobItem;

        protected override void OnStart()
        {
            base.OnStart();
            for (int i = 0; i < m_JobList.Count; i++)
            {
                m_JobList[i].OnClickButton += OnClickJonItem;
            }
        }
        private void OnClickJonItem(UIJobItemView jobItem)
        {
            if (m_JobItem != null)
            {
                m_JobItem.OnChange();
            }
            m_JobItem = jobItem;
        }

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            object[] objs = null;
            switch (go.name)
            {
                case "btnBuy":
                    if (m_JobItem == null)
                    {
                        return;
                    }
                    objs = new object[2];
                    objs[0] = m_JobItem.Index;
                    objs[1] = m_JobItem.Coin;
                    UIDispatcher.Instance.Dispatc(ConstDefine.InitScene_ShopWindow_Buy, objs);
                    break;
            }
            EazySoundManager.PlayUISound(Global.Instance.BtnClip);
        }

        public void SetUI(DataTransfer data)
        {
            //拥有的英雄
            List<int> jobList = data.GetData<List<int>>(ConstDefine.Has_Job);
            //当前的英雄
            int curJob = data.GetData<int>(ConstDefine.Cur_Job);
            //拥有的金币
            int coin = data.GetData<int>(ConstDefine.Has_Coin);
            for (int i = 0; i < jobList.Count; i++)
            {
                for (int j = 0; j < m_JobList.Count; j++)
                {
                    if (jobList[i] == m_JobList[i].Index)
                    {
                        m_JobList[i].HasJob();
                    }

                }
            }
            for (int i = 0; i < m_JobList.Count; i++)
            {
                if (m_JobList[i].Index == curJob)
                {
                    m_JobItem = m_JobList[i];
                    m_JobList[i].IsCurJob();
                }
            }
            m_Coin.text = coin.ToString();
        }

        public void ShowShopMessage(string content)
        {
            StopCoroutine("ShowShopMessageIE");
            StartCoroutine("ShowShopMessageIE", content);
        }

        private IEnumerator ShowShopMessageIE(string content)
        {
            m_MessageObj.SetActive(true);
            m_MessageContent.text = content;
            yield return new WaitForSeconds(3f);
            m_MessageObj.SetActive(false);
        }
    }
}
