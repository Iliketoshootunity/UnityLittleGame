using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class GameLevelInfo
    {
        public int MaxLevel;
        public int MaxCanPlay;
        public List<int> StarList;

        public void SetGameLevelInfo()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(MaxLevel.ToString() + "_");
            sb.Append(MaxCanPlay.ToString() + "_");
            for (int i = 0; i < MaxLevel; i++)
            {
                if (i == MaxLevel - 1)
                {
                    sb.Append(StarList[i].ToString());
                }
                else
                {
                    sb.Append(StarList[i].ToString() + "|");
                }
            }
            PlayerPrefs.SetString("GameLevelInfo", sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info">格式 MaxLevel_MaxPass_第一关星星|第二关星星。。。</param>
        /// <returns></returns>
        public static GameLevelInfo GetGameLevelInfo()
        {
            string info = PlayerPrefs.GetString("GameLevelInfo");
            GameLevelInfo gi = new GameLevelInfo();
            string[] str1Arr = info.Split('_');
            gi.MaxLevel = str1Arr[0].ToInt();
            gi.MaxCanPlay = str1Arr[1].ToInt();
            string[] str2Arr = str1Arr[2].Split('|');
            if (str2Arr.Length != gi.MaxLevel)
            {
                Debug.LogError("Error");
                return null;
            }
            gi.StarList = new List<int>();
            for (int i = 0; i < str2Arr.Length; i++)
            {
                gi.StarList.Add(str2Arr[i].ToInt());
            }
            return gi;
        }

        public static GameLevelInfo GetEmptyGameLevelInfo(int maxLevel)
        {
            GameLevelInfo gi = new GameLevelInfo();
            gi.MaxLevel = maxLevel;
            gi.MaxCanPlay = 1;
            gi.StarList = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                gi.StarList.Add(-1);
            }
            gi.SetGameLevelInfo();
            return gi;
        }
    }
}
