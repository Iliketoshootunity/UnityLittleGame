using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLevelSpriteBornInfo
{
    public int Row;
    public int Column;
    public MoveDirection Dir;
}
public partial class GameLevelSpriteDBModel
{
    List<GameLevelSpriteEntity> lst = new List<GameLevelSpriteEntity>();
    public List<GameLevelSpriteEntity> GetByGameLvelIdAndType(int levelID, int type)
    {
        lst.Clear();
        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].GameLevelId == levelID && m_List[i].Type == type)
            {
                lst.Add(m_List[i]);
            }
        }
        return lst;
    }

    public GameLevelSpriteBornInfo GetSpriteBornInfo(int spriteId)
    {

        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].SpriteId == spriteId)
            {
                GameLevelSpriteEntity e = m_List[i];
                GameLevelSpriteBornInfo info = new GameLevelSpriteBornInfo();
                string[] arr = e.AllBronPos.Split('|');
                for (int j = 0; j < arr.Length; j++)
                {
                    string[] infoArr = arr[j].Split('_');
                    info.Row = infoArr[0].ToInt();
                    info.Column = infoArr[1].ToInt();
                    string dir = infoArr[2];
                    switch (dir)
                    {
                        case "1":
                            info.Dir = MoveDirection.Up;
                            break;
                        case "2":
                            info.Dir = MoveDirection.Down;
                            break;
                        case "3":
                            info.Dir = MoveDirection.Left;
                            break;
                        case "4":
                            info.Dir = MoveDirection.Right;
                            break;
                    }

                    return info;
                }
            }
        }

        return null;

    }

}
