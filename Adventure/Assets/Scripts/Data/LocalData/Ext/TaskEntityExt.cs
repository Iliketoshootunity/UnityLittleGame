using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

public partial class TaskEntity
{

    private List<TaskConditionEntity> m_TaskConditionEntityList;
    private List<TaskRewardEntity> m_TaskRewardEntityList;

    public List<TaskConditionEntity> GetTaskConditionEntityList()
    {
        if (m_TaskConditionEntityList == null)
        {
            m_TaskConditionEntityList = new List<TaskConditionEntity>();
            string[] conditionArr = Conditions.Split('|');
            for (int i = 0; i < conditionArr.Length; i++)
            {
                TaskConditionEntity condition = TaskConditionDBModel.Instance.Get(conditionArr[i].ToInt());
                if (condition == null)
                {
                    Debug.LogError("错误：条件实体找不到");
                    continue;
                }
                m_TaskConditionEntityList.Add(condition);
            }
            return m_TaskConditionEntityList;
        }
        return m_TaskConditionEntityList;
    }

    public List<TaskRewardEntity> GetTaskRewardList()
    {
        if (m_TaskRewardEntityList == null)
        {
            m_TaskRewardEntityList = new List<TaskRewardEntity>();
            string[] rewardArr = Rewards.Split('|');
            for (int i = 0; i < rewardArr.Length; i++)
            {
                TaskRewardEntity rewardEntity = TaskRewardDBModel.Instance.Get(rewardArr[i].ToInt());
                if (rewardEntity == null)
                {
                    Debug.LogError("错误：奖励实体找不到");
                    continue;
                }
                m_TaskRewardEntityList.Add(rewardEntity);
            }

        }
        return m_TaskRewardEntityList;

    }
}

