using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class TaskCondition
    {

        private TaskConditionEntity m_TaskConditionEntity;

        private List<int> m_ConditionTypeList;

        private List<int> m_TargetIdList;
        public List<int> ConditionTypeList { get { return m_ConditionTypeList; } }

        public List<int> TargetIdList { get { return m_TargetIdList; } }

        public int Id { get { return m_TaskConditionEntity.Id; } }
        public int TargetCount { get { return m_TaskConditionEntity.TargetCount; } }
        public string ConditionType { get { return m_TaskConditionEntity.ConditionType; } }

        public int CurCount;

        public bool IsFinish;
        public TaskCondition(int taskCondtionId)
        {
            m_TaskConditionEntity = TaskConditionDBModel.Instance.Get(taskCondtionId);
            m_ConditionTypeList = new List<int>();
            m_TargetIdList = new List<int>();
            string[] arr1 = m_TaskConditionEntity.ConditionType.Split('|');
            for (int i = 0; i < arr1.Length; i++)
            {
                m_ConditionTypeList.Add(arr1[i].ToInt());
            }
            string[] arr2 = m_TaskConditionEntity.TargetId.Split('|');
            for (int i = 0; i < arr2.Length; i++)
            {
                m_TargetIdList.Add(arr2[i].ToInt());
            }
        }
        public bool TryMatchCondition(int conditionType)
        {
            if (m_ConditionTypeList.Contains(conditionType)) return true;
            return false;
        }
        public bool CheckFinish()
        {
            if (CurCount >= TargetCount) return true;
            return false;
        }

    }
}
