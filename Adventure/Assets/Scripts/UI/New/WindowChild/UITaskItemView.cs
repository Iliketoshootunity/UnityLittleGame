using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class UITaskItemView : UISubViewBase
    {
        private int m_TaskId;
        [SerializeField]
        private Text m_NameText;
        [SerializeField]
        private Text m_DescText;
        [SerializeField]
        private Text m_MaxCount;
        [SerializeField]
        private Text m_CurCount;
        [SerializeField]
        private Button m_Button;
        [SerializeField]
        private Image m_Finish;

        public Action<int> GetRewardEvent;
        private bool m_GetReward;

        public int TaskId { get { return m_TaskId; } }
        protected override void OnStart()
        {
            base.OnStart();
            m_Button.onClick.AddListener(OnClickGetReward);
            EventTriggerListener t = m_Button.GetComponent<EventTriggerListener>();
            Destroy(t);
        }

        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            GetRewardEvent = null;
        }
        public void SetUI(int taskId, string name, string desc, int curCount, int targetCount, bool getReward)
        {
            m_TaskId = taskId;
            m_NameText.text = name;
            m_DescText.text = desc;
            m_MaxCount.text = targetCount.ToString();
            m_CurCount.text = curCount.ToString();
            m_GetReward = getReward;
            if (!getReward)
            {
                if (curCount >= targetCount)
                {
                    m_Finish.gameObject.SetActive(true);
                }
                else
                {
                    m_Finish.gameObject.SetActive(false);
                }
            }
            else
            {
                m_Finish.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }


        }
        private void OnClickGetReward()
        {
            if (m_GetReward == true) return;
            m_Finish.color = new Color(0.5f, 0.5f, 0.5f, 1);
            if (GetRewardEvent != null)
            {
                GetRewardEvent(m_TaskId);
            }
            m_GetReward = true;
        }

        public void UpdateTask(int curCount)
        {
            m_CurCount.text = curCount.ToString();
        }
        public void FinishTask()
        {
            m_Finish.gameObject.SetActive(true);
        }

    }
}
