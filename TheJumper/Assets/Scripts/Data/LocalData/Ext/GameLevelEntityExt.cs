using UnityEngine;
using System.Collections;

public partial class GameLevelEntity
{

    public byte[,] GetMapTypeArr()
    {
        byte[,] mapType = null;
        int row = 0;
        int column = 0;
        if (!string.IsNullOrEmpty(MapTypeInfo))
        {
            string[] rowArr = MapTypeInfo.Split('|');
            row = rowArr.Length;
            for (int i = 0; i < rowArr.Length; i++)
            {
                string[] columnArr = rowArr[i].Split('_');
                if (column == 0)
                {
                    column = columnArr.Length;
                    mapType = new byte[row, column];
                }

                for (int j = 0; j < columnArr.Length; j++)
                {
                    byte.TryParse(columnArr[j], out mapType[i, j]);
                }
            }
            return mapType;
        }
        else
        {
            return null;
        }


    }


}
