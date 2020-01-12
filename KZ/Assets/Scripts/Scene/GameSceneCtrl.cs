using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using EasyFrameWork.UI;
namespace EasyFrameWork
{
    public class GameSceneCtrl : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            UISceneCtrl.Instance.Load(UISceneType.Game);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open);
        }
        // Update is called once per frame
        void Update()
        {

        }
        private void OnGUI()
        {
            if (GUI.Button(new Rect(30, 50, 50, 50), "胜利"))
            {
                UIViewMgr.Instance.OpenView(UIViewType.Win);
            }
            if (GUI.Button(new Rect(30, 100, 50, 50), "失败"))
            {
                UIViewMgr.Instance.OpenView(UIViewType.Fail);
            }
        }
    }
}
