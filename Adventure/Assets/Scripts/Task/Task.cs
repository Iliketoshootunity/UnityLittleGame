using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public enum TaskStatus
    {
        Executory,
        Finish,
        GetReward,
        Cancel
    }
    public class Task
    {
        public TaskEntity TaskEntity;
        public List<TaskCondition> TaskConditionList;
        public List<TaskReward> TaskRewardList;

        private int m_CurExcuteCondtionId = -1;
        public int CurExcuteCondtionId { get { return m_CurExcuteCondtionId; } }

        public TaskStatus TaskStatus;


        public int Id
        {
            get
            {
                return TaskEntity.Id;
            }
        }

        public string Name
        {
            get
            {
                return TaskEntity.Name;
            }
        }

        public string Desc
        {
            get
            {
                return TaskEntity.Desc;
            }
        }

        public Task(int taskId)
        {
            TaskEntity = TaskDBModel.Instance.Get(taskId);
            TaskConditionList = new List<TaskCondition>();
            TaskRewardList = new List<TaskReward>();

            for (int i = 0; i < TaskEntity.GetTaskConditionEntityList().Count; i++)
            {
                TaskCondition condition = new TaskCondition(TaskEntity.GetTaskConditionEntityList()[i].Id);
                TaskConditionList.Add(condition);
            }

            for (int i = 0; i < TaskEntity.GetTaskRewardList().Count; i++)
            {
                TaskReward reward = new TaskReward(TaskEntity.GetTaskRewardList()[i].Id);
                TaskRewardList.Add(reward);
            }
            TaskStatus = TaskStatus.Executory;
        }

        //尝试执行任务
        //满足条件时会更新任务进度
        public bool TryExecuteTheTask(int conditionId, int count)
        {
            TaskCondition tc;
            bool isExecute = false;
            for (int i = 0; i < TaskConditionList.Count; i++)
            {
                tc = TaskConditionList[i];
                if (tc.TargetIdList.Contains(conditionId))
                {
                    isExecute = true;
                    tc.CurCount += count;
                    if (tc.CurCount < 0) tc.CurCount = 0;
                    if (tc.CurCount >= tc.TargetCount)
                    {
                        tc.IsFinish = true;
                    }
                    else
                    {
                        tc.IsFinish = false;
                    }
                    m_CurExcuteCondtionId = tc.Id;
                    TaskManager.Instance.UpdateTheTask(Id);
                }
            }
            bool isFinish = true;
            for (int i = 0; i < TaskConditionList.Count; i++)
            {
                if (!TaskConditionList[i].IsFinish)
                {
                    isFinish = false;
                }
            }
            if (isFinish)
            {
                TaskStatus = TaskStatus.Finish;
                TaskManager.Instance.FinishTask(Id);
            }
            return isExecute;
        }

        public bool TryMatchCondition(int conditionType)
        {
            if (TaskStatus != TaskStatus.Executory) return false;
            for (int i = 0; i < TaskConditionList.Count; i++)
            {
                bool isMatch = TaskConditionList[i].TryMatchCondition(conditionType);
                if (isMatch)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
