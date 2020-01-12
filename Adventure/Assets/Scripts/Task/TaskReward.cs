using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class TaskReward
    {

        private TaskRewardEntity m_TaskRewardEntity;

        public int Id { get { return m_TaskRewardEntity.Id; } }

        public int RewardType { get { return m_TaskRewardEntity.RewardType; } }

        public int RewardCount { get { return m_TaskRewardEntity.Count; } }

        public TaskReward(int taskRewardId)
        {
            m_TaskRewardEntity = TaskRewardDBModel.Instance.Get(taskRewardId);
        }

    }
}
