using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

public struct ObstacleInfo
{
    public Vector2 Pos;
    public int ObstacleType;
}

public struct RoleInfo
{
    public Vector2 Pos;
    public int Range;
}



public partial class GameLevelDBModel
{

    private List<ObstacleInfo> m_ObstaclesInfoList;
    public List<ObstacleInfo> GetObstaclesInfoList(int level)
    {
        if (m_ObstaclesInfoList == null)
        {
            m_ObstaclesInfoList = new List<ObstacleInfo>();
        }
        m_ObstaclesInfoList.Clear();
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        if (string.IsNullOrEmpty(entity.Obstacle))
        {
            return null;
        }
        string[] strArr = entity.Obstacle.Split('|');
        for (int i = 0; i < strArr.Length; i++)
        {
            string[] posArr = strArr[i].Split('_');
            float x = posArr[0].ToFloat();
            float y = posArr[1].ToFloat();
            int type = posArr[2].ToInt();
            m_ObstaclesInfoList.Add(new ObstacleInfo() { Pos = new Vector2(x, y), ObstacleType = type });
        }
        return m_ObstaclesInfoList;
    }
    private List<RoleInfo> m_RoleInfoList;
    public List<RoleInfo> GetPlayerInfoList(int level)
    {
        if (m_RoleInfoList == null)
        {
            m_RoleInfoList = new List<RoleInfo>();
        }
        m_RoleInfoList.Clear();
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        if (string.IsNullOrEmpty(entity.Player))
        {
            return null;
        }
        string[] strArr = entity.Player.Split('|');
        for (int i = 0; i < strArr.Length; i++)
        {
            string[] posArr = strArr[i].Split('_');
            float x = posArr[0].ToFloat();
            float y = posArr[1].ToFloat();
            int range = posArr[2].ToInt();
            m_RoleInfoList.Add(new RoleInfo() { Pos = new Vector2(x, y), Range = range });
        }
        return m_RoleInfoList;
    }
    public List<RoleInfo> GetMonsterInfoList(int level)
    {
        if (m_RoleInfoList == null)
        {
            m_RoleInfoList = new List<RoleInfo>();
        }
        m_RoleInfoList.Clear();
        GameLevelEntity entity = m_List.Find(x => x.Level == level);
        if (string.IsNullOrEmpty(entity.Monster))
        {
            return null;
        }
        string[] strArr = entity.Monster.Split('|');
        for (int i = 0; i < strArr.Length; i++)
        {
            string[] posArr = strArr[i].Split('_');
            float x = posArr[0].ToFloat();
            float y = posArr[1].ToFloat();
            int range = posArr[2].ToInt();
            m_RoleInfoList.Add(new RoleInfo() { Pos = new Vector2(x, y), Range = range });
        }
        return m_RoleInfoList;
    }

    public Vector2 GetOverPoint(int level)
    {
        Vector2 pos = Vector2.zero;
        GameLevelEntity entity = m_List.Find(i => i.Level == level);
        string str = entity.OverPoint;
        string[] posArr = str.Split('_');
        float x = posArr[0].ToFloat();
        float y = posArr[1].ToFloat();
        pos = new Vector2(x, y);
        return pos;
    }

    public int GetLevelCount()
    {
        return m_List.Count;
    }

}

