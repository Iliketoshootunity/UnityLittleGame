using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class GameLevelSelectSceneCtrl : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            UISceneCtrl.Instance.Load(UISceneType.GameLevelSelect);
            Global.Instance.SetMusic();
        }

    }
}
