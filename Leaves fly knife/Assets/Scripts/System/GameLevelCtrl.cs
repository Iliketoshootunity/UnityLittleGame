using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;

namespace EasyFrameWork
{
    public class GameLevelCtrl : SystemCtrlBase<GameLevelCtrl>, ISystemCtrl
    {
        public GameLevelCtrl()
        {
            UIDispatcher.Instance.AddEventListen(ConstDefine.Pause, OnPause);
            UIDispatcher.Instance.AddEventListen(ConstDefine.Item, OnItem);
            UIDispatcher.Instance.AddEventListen(ConstDefine.NextScene, OnReturn);
            UIDispatcher.Instance.AddEventListen(ConstDefine.NextLevel, OnNextLevel);
            UIDispatcher.Instance.AddEventListen(ConstDefine.Restart, OnRestart);
        }

        private void OnRestart(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("Game" + Global.CurLevel.ToString()); });
        }

        private void OnNextLevel(object[] p)
        {
            if (Global.CurLevel == Global.Instance.MaxLevel + 1) return;
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("Game" + Global.CurLevel.ToString()); });
        }
        private void OnReturn(object[] p)
        {
            string nextScene = p[0].ToString();
            if (nextScene == "Level")
            {
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene(nextScene); });
            }
        }
        private void OnItem(object[] p)
        {
            int curLevel = (int)p[0];
            Global.CurLevel = curLevel;
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("Game" + Global.CurLevel.ToString()); });
        }

        private void OnPause(object[] p)
        {
            bool isPause = (bool)p[0];
            if (isPause == true)
            {
                UIViewMgr.Instance.OpenView(UIViewType.Pause);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        public void OpenView(UIViewType type)
        {
            switch (type)
            {
                case UIViewType.None:
                    break;
                case UIViewType.SelectLevel:
                    OpenSelectLevelView();
                    break;
                case UIViewType.Win:
                    OpenWinView();
                    break;
                case UIViewType.Fail:
                    OpenFailView();
                    break;
                case UIViewType.Pause:
                    OpenPauseView();
                    break;
                default:
                    break;
            }
        }

        private void OpenPauseView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.Pause);
        }

        private void OpenSelectLevelView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.SelectLevel);
            if (go != null)
            {
                UILevelSelectView view = go.GetComponent<UILevelSelectView>();
                DataTransfer data = new DataTransfer();
                data.SetData(ConstDefine.MaxLevel, Global.GameLevelInfo.MaxLevel);
                data.SetData(ConstDefine.MaxCanPlay, Global.GameLevelInfo.MaxCanPlay);
                data.SetData(ConstDefine.StarInfo, Global.GameLevelInfo.StarList);
                view.SetUI(data);
            }
        }

        private void OpenWinView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.Win);
            Global.OnWin(GameSceneCtrl.Instance.StarCount);
        }
        private void OpenFailView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.Fail);
        }
    }
}
