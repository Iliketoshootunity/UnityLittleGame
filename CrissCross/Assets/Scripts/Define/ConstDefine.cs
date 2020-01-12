using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public sealed class ConstDefine : MonoBehaviour
    {
        //事件
        public static string InitSceneViewClickStartGameBtn = "InitSceneViewClickStartGameBtn";
        public static string InitSceneViewClickHelpBtn = "InitSceneViewClickHelpBtn";

        public static string GameLevelSceneViewClickAudioBtn = "GameLevelSceneViewClickAudioBtn";
        public static string GameLevelSceneViewClickPauseBtn = "GameLevelSceneViewClickPauseBtn";

        public static string GameLevelSelectViewClickItmeBtn = "GameLevelSelectViewClickItmeBtn";
        public static string GameLevelSelectClickReturnBtn = "GameLevelSelectClickReturnBtn";
        public static string GameLevelSelectClickClearBtn = "GameLevelSelectClickClearBtn";

        public static string PauseViewClickReturnGameBtn = "PauseViewClickReturnGameBtn";
        public static string PauseViewClickRestartGameBtn = "PauseViewClickRestartGameBtn";
        public static string PauseViewClickReturnGameLevelSelectBtn = "PauseViewClickReturnGameLevelSelectBtn";

        public static string WinViewClickNextGameLevelBtn = "WinViewClickNextGameLevelBtn";
        public static string WinViewClickGameLeveSelectBtn = "WinViewClickGameLeveSelectBtn";

        //数据
        public static string MaxPassLevel = "MaxPassLevel";
        public static string MaxLevel = "MaxLevel";

        public static string GameLevelItem_Level = "GameLevelItem_Level";
        public static string GameLevelItem_IsLock = "GameLevelItem_IsLock";

    }
}

