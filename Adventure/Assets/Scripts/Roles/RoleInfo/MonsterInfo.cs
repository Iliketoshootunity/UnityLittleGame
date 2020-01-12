using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 怪物信息从表里面读取
    /// </summary>
    [System.Serializable]
    public class MonsterInfo : RoleInfoBase
    {
        public SpriteEntity SpriteEntity;

        public MonsterInfo(SpriteEntity spriteEntity) : base()
        {
            SpriteEntity = spriteEntity;
        }
        List<int> m_SkillList;
        public List<int> GetSkillList()
        {
            if (m_SkillList == null)
            {
                m_SkillList = new List<int>();
                string[] skillArr = SpriteEntity.UesSkill.Split('|');
                for (int i = 0; i < skillArr.Length; i++)
                {
                    m_SkillList.Add(skillArr[0].ToInt());
                }
            }
            return m_SkillList;

        }
    }
}
