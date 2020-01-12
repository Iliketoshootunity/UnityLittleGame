using UnityEngine;
using System.Collections;
using EasyFrameWork;

public partial class SkillLevelDBModel
{
    public SkillLevelEntity GetSkillLevelBySkillIdAndLevel(int skillid, int skillLevel)
    {
        return m_List.Find(x => x.SkillId == skillid && x.Level == skillLevel);
    }
}