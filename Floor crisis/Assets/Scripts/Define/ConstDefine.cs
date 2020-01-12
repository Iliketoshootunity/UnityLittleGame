using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public sealed class ConstDefine : MonoBehaviour
    {
        //------------ui消息----------------//
        public static string InitScene_StartGame = "InitScene_StartGame";
        public static string InitScene_Shop = "InitScene_Shop";
        public static string InitScene_Help = "InitScene_Help";
        public static string InitScene_Music = "InitScene_Music";

        public static string InitScene_ShopWindow_Job = "InitScene_ShopWindow_Job";
        public static string InitScene_ShopWindow_Buy = "InitScene_ShopWindow_Buy";

        public static string GameLevelScene_Pause = "GameLevelScene_Pause";

        public static string GameLevelScene_FailWindow_Return = "GameLevelScene_FailWindow_Return";
        public static string GameLevelScene_FailWindow_Continue = "GameLevelScene_FailWindow_Continue";
        public static string GameLevelScene_VictoryWindow_Return = "GameLevelScene_VictoryWindow_Return";
        public static string GameLevelScene_VictoryWindow_Continue = "GameLevelScene_VictoryWindow_Continue";
        public static string GameLevelScene_PauseWindow_Return = "GameLevelScene_PauseWindow_Return";
        public static string GameLevelScene_PauseWindow_Continue = "GameLevelScene_PauseWindow_Continue";

        //-----------------ui传递的数据--------------------//
        public static string Has_Job = "Has_Job";
        public static string Has_Coin = "Has_Coin";
        public static string Cur_Job = "Cur_Job";
    }
}

