using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

public partial class GameLevelDBModel
{
    private List<List<int>> m_MapData;
    public List<List<int>> GetMapData(int level)
    {
        if (m_MapData == null)
        {
            m_MapData = new List<List<int>>();
        }
        m_MapData.Clear();
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        if (entity == null) return null;
        string[] strArr = entity.Map.Split('|');
        for (int i = 0; i < strArr.Length; i++)
        {
            List<int> lst = new List<int>();
            string[] map = strArr[i].Split(',');
            for (int j = 0; j < map.Length; j++)
            {
                lst.Add(map[j].ToInt());
            }
            m_MapData.Add(lst);
        }
        return m_MapData;

    }

    public int GetLevelCount()
    {
        return m_List.Count;
    }

    public Vector2 GetRowAndColumn(int level)
    {
        Vector2 t = Vector2.one;
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        string[] strArr = entity.Map.Split('|');
        int row = strArr.Length;
        int column = strArr[0].Split(',').Length;
        t = new Vector2(row, column);
        return t;
    }
}

