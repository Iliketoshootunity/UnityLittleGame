using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 英雄信息从数据持久化的数据库中读取
    /// </summary>
    [System.Serializable]
    public class HeroInfo : RoleInfoBase
    {
        /// <summary>
        /// 职业ID
        /// </summary>
        public int JobID;
        /// <summary>
        /// 英雄ID
        /// </summary>
        public int HeroID;
        /// <summary>
        /// 英雄星级
        /// </summary>
        public int HeroStar;
        /// <summary>
        /// 能使用的技能信息
        /// </summary>
        public List<RoleInfoSkill> SkillList = new List<RoleInfoSkill>();


        /// <summary>
        /// 获取节能等级
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        public int GetSkillLevel(int skillId)
        {
            for (int i = 0; i < SkillList.Count; i++)
            {
                if (SkillList[i].SkillId == skillId)
                {
                    return SkillList[i].SKillLevel;
                }
            }
            return 1;
        }
    }
}
