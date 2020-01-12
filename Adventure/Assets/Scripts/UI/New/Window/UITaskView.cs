using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public class UITaskView : UIWindowViewBase
    {
        [SerializeField]
        private GridLayoutGroup m_Grid;
        private List<UITaskItemView> m_TaskItemList;
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
        }
        public void SetUI(DataTransfer data)
        {
            m_TaskItemList = new List<UITaskItemView>();
            List<DataTransfer> ds = data.GetData<List<DataTransfer>>(ConstDefine.UI_Task_Content);
            for (int i = 0; i < ds.Count; i++)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "TaskItem");
                go.transform.SetParent(m_Grid.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                UITaskItemView view = go.GetComponent<UITaskItemView>();
                int taskId = ds[i].GetData<int>(ConstDefine.UI_Task_Id);
                string name = ds[i].GetData<string>(ConstDefine.UI_TaskItem_Name);
                string desc = ds[i].GetData<string>(ConstDefine.UI_TaskItem_Desc);
                int targetCount = ds[i].GetData<int>(ConstDefine.UI_TaskItem_TargetCount);
                int curCount = ds[i].GetData<int>(ConstDefine.UI_TaskItem_CurCount);
                bool getReward = ds[i].GetData<bool>(ConstDefine.UI_TaskItem_GetReward);
                view.SetUI(taskId, name, desc, curCount, targetCount, getReward);
                view.GetRewardEvent += OnGetRewardCallBack;
                m_TaskItemList.Add(view);
            }
        }
        public void UpdateTaskView(int taskId, int conditionId, int curCount)
        {
            UITaskItemView view = m_TaskItemList.Find(x => x.TaskId == taskId);
            if (view == null)
            {
                Debug.LogError("错误：找不到任务");
                return;
            }
            view.UpdateTask(curCount);
        }

        public void FinishTheTask(int taskId)
        {
            UITaskItemView view = m_TaskItemList.Find(x => x.TaskId == taskId);
            if (view == null)
            {
                Debug.LogError("错误：找不到任务");
                return;
            }
            view.FinishTask();
        }

        private void OnGetRewardCallBack(int taskId)
        {
            object[] objs = new object[1];
            objs[0] = taskId;
            UIDispatcher.Instance.Dispatc(ConstDefine.UI_TaskItem_ClickGetReward, objs);
        }
    }
}
