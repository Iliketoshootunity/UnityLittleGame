using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public sealed class ConstDefine : MonoBehaviour
    {
        public static string MusicSetting = "MusicSetting";
        public static string StartGame = "StartGame";
        //事件

        public static string InitSceneViewClickHelpBtn = "InitSceneViewClickHelpBtn";

        public static string GameLevelSelectViewClickItmeBtn = "GameLevelSelectViewClickItmeBtn";
        public static string GameLevelSelectViewClickReturnBtn = "GameLevelSelectViewClickReturnBtn";

        public static string GameLevelViewClickReturnBtn = "GameLevelViewClickReturnBtn";
        public static string GameLevelViewClickRestartBtn = "GameLevelViewClickRestartBtn";

        public static string PauseViewClickReturnGameBtn = "PauseViewClickReturnGameBtn";
        public static string PauseViewClickRestartGameBtn = "PauseViewClickRestartGameBtn";
        public static string PauseViewClickReturnGameLevelSelectBtn = "PauseViewClickReturnGameLevelSelectBtn";

        public static string ClickNextGameLevelBtn = "WinViewClickNextGameLevelBtn";
        public static string ClickGameLeveSelectBtn = "WinViewClickGameLeveSelectBtn";
        public static string ClickRestartBtn = "WinViewClickRestartBtn";

        //数据
        public static string MaxPassLevel = "MaxPassLevel";
        public static string MaxLevel = "MaxLevel";

        public static string GameLevelItem_Level = "GameLevelItem_Level";
        public static string GameLevelItem_IsLock = "GameLevelItem_IsLock";

    }
}

