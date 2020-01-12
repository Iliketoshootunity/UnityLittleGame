using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public class TaskArgs
    {
        public int TaskID;//当前任务的ID
        public int ConditionId;//发生事件的对象的ID(例如敌人,商品)
        public int RewardId;   //奖励ID 
        public int Amount;//数量
    }

    public class TaskManager : MonoSingleton<TaskManager>
    {
        public event Action<int> TakeTaskEvent;//接受任务（任务Id）
        public event Action<int> FinishTaskEvent;//完成任务（任务Id）
        public event Action<int> GetRewardEvent;//得到奖励(奖励Id)
        public event Action<int> CancalTaskEvent;//取消任务时（任务Id）
        public event Action<int, int, int> UpdateProcessEvent;//更新 (任务Id,条件Id,当前数量)

        private Dictionary<int, Task> m_TaskIdToTask = new Dictionary<int, Task>();       //任务不存在相同的

        //接受任务
        public Task TakeTheTask(int taskId)
        {
            if (!m_TaskIdToTask.ContainsKey(taskId))
            {
                Task t = new Task(taskId);

                m_TaskIdToTask.Add(taskId, t);

                if (TakeTaskEvent != null)
                {
                    TakeTaskEvent(taskId);
                }
                return t;
            }
            return null;
        }
        //尝试匹配条件
        public bool TryMathchCondition(int conditionType)
        {
            foreach (var item in m_TaskIdToTask)
            {
                bool isMatch = item.Value.TryMatchCondition(conditionType);
                if (isMatch)
                {
                    return true;
                }
            }
            return false;
        }

        //外部调用 执行任务
        public void ExecuteTheTask(int conditionId, int count)
        {
            foreach (var item in m_TaskIdToTask)
            {
                if (item.Value.TaskStatus == TaskStatus.Executory)
                {
                    item.Value.TryExecuteTheTask(conditionId, count);
                }
            }
        }
        //完成任务
        public void FinishTask(int taskId)
        {
            if (FinishTaskEvent == null) return;
            Task t = m_TaskIdToTask[taskId];
            if (t == null)
            {
                Debug.LogError("错误：找不到任务");
                return;
            }
            FinishTaskEvent(taskId);

        }
        // Task调用 更新任务
        public void UpdateTheTask(int taskId)
        {
            if (UpdateProcessEvent == null) return;
            Task t = m_TaskIdToTask[taskId];
            if (t == null)
            {
                Debug.LogError("错误：找不到任务");
                return;
            }
            if (t.CurExcuteCondtionId == -1) return;
            TaskCondition condition = t.TaskConditionList.Find(x => x.Id == t.CurExcuteCondtionId);
            if (condition == null)
            {
                Debug.LogError("错误：找不到任务条件");
                return;
            }
            UpdateProcessEvent(taskId, condition.Id, condition.CurCount);
        }

        //取消任务
        public void CancelTheTask(int taskId)
        {
            if (CancalTaskEvent == null) return;
            Task t = m_TaskIdToTask[taskId];
            if (t == null)
            {
                Debug.LogError("错误：找不到任务");
                return;
            }
            m_TaskIdToTask.Remove(taskId);
            CancalTaskEvent(taskId);
        }
        //获取任务奖励
        public void GetReward(int taskId)
        {
            if (GetRewardEvent == null) return;
            Task t = m_TaskIdToTask[taskId];
            if (t == null)
            {
                Debug.LogError("错误：找不到任务");
                return;
            }
            t.TaskStatus = TaskStatus.GetReward;
            for (int i = 0; i < t.TaskRewardList.Count; i++)
            {
                GetRewardEvent(t.TaskRewardList[i].Id);
            }
            m_TaskIdToTask.Remove(taskId);
        }


    }
}
