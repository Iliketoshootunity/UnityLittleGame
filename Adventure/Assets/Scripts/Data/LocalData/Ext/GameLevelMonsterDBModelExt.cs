using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

public partial class GameLevelMonsterDBModel
{

    private List<GameLevelMonsterEntity> m_MonsterList = new List<GameLevelMonsterEntity>();

    public List<GameLevelMonsterEntity> GetMonsterListByLevelAndGeade(int level, int grade)
    {
        m_MonsterList.Clear();
        m_MonsterList = m_List.FindAll(x => x.GameLevelId == level && x.Grade == grade);
        return m_MonsterList;
    }
}
