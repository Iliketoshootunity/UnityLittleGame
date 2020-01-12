using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

public struct StrongholdInfo
{
    public int Type;
    public int Row;
    public int Column;
    public int Range;
}


public partial class GameLevelDBModel
{
    private List<StrongholdInfo> m_StrongholdInfoList;
    /// <summary>
    /// 获取堡垒信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<StrongholdInfo> GetStrongholdInfo(int level)
    {
        if (m_StrongholdInfoList == null)
        {
            m_StrongholdInfoList = new List<StrongholdInfo>();
        }
        m_StrongholdInfoList.Clear();
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        if (entity == null || string.IsNullOrEmpty(entity.Stronghold))
        {
            return null;
        }
        string[] strongholdArr = entity.Stronghold.Split('|');
        for (int i = 0; i < strongholdArr.Length; i++)
        {
            string[] strArr = strongholdArr[i].Split('_');
            StrongholdInfo info = new StrongholdInfo();
            info.Type = strArr[0].ToInt();
            info.Row = strArr[1].ToInt();
            info.Column = strArr[2].ToInt();
            info.Range = strArr[3].ToInt();
            m_StrongholdInfoList.Add(info);
        }
        return m_StrongholdInfoList;
    }

    public Vector2 GetMonsterRowAndColunm(int level)
    {
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        if (entity == null || string.IsNullOrEmpty(entity.Monster))
        {
            return -Vector2.one;
        }
        string[] strArr = entity.Monster.Split('_');
        Vector2 pos = new Vector2();
        pos.x = strArr[0].ToInt();
        pos.y = strArr[1].ToInt();
        return pos;
    }
}

