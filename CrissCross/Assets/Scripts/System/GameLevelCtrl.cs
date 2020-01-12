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
        private UIGameLevelSelectView m_GameLevelSelectView;
        public GameLevelCtrl()
        {
            AddEventListen(ConstDefine.GameLevelSelectViewClickItmeBtn, OnGameLevelSelectViewClickItmeBtn);
            AddEventListen(ConstDefine.GameLevelSelectClickReturnBtn, OnGameLevelSelectClickReturnBtn);
            AddEventListen(ConstDefine.GameLevelSelectClickClearBtn, OnGameLevelSelectClickClearBtn);

            AddEventListen(ConstDefine.PauseViewClickRestartGameBtn, OnPauseViewClickRestartGameBtn);
            AddEventListen(ConstDefine.PauseViewClickReturnGameBtn, OnPauseViewClickReturnGameBtn);
            AddEventListen(ConstDefine.PauseViewClickReturnGameLevelSelectBtn, OnPauseViewClickReturnGameLevelSelectBtn);

            AddEventListen(ConstDefine.WinViewClickNextGameLevelBtn, OnWinViewClickNextGameLevelBtn);
            AddEventListen(ConstDefine.WinViewClickGameLeveSelectBtn, OnWinViewClickGameLeveSelectBtn);
        }

        #region 胜利界面事件
        private void OnWinViewClickGameLeveSelectBtn(object[] p)
        {
            OnWin();
            SceneManager.LoadScene("GameLevelSelect");
        }

        private void OnWinViewClickNextGameLevelBtn(object[] p)
        {
            bool isEnd = OnWin();
            if (isEnd) return;
            SceneManager.LoadScene("GameLevel");
        }

        private bool OnWin()
        {
            int level = Global.Instance.CurLevel;
            level++;
            int maxLevel = GameLevelDBModel.Instance.GetLevelCount();
            if (level > maxLevel)
            {
                return true;
            }
            if (level >= maxLevel)
            {
                maxLevel = level;
            }
            Global.Instance.CurLevel = level;
            if (Global.Instance.CurLevel >= Global.Instance.MaxPassLevel)
            {
                Global.Instance.MaxPassLevel = Global.Instance.CurLevel;
            }
            return false;
        }
        #endregion

        #region 暂停界面事件
        private void OnPauseViewClickRestartGameBtn(object[] p)
        {
            SceneManager.LoadScene("GameLevel");
            GameLevelSceneCtrl.Instance.IsPause = false;
        }
        private void OnPauseViewClickReturnGameBtn(object[] p)
        {
            GameLevelSceneCtrl.Instance.IsPause = false;
        }
        private void OnPauseViewClickReturnGameLevelSelectBtn(object[] p)
        {
            SceneManager.LoadScene("GameLevelSelect");
            GameLevelSceneCtrl.Instance.IsPause = false;
        }
        #endregion

        #region 关卡选择界面事件

        private void OnGameLevelSelectClickClearBtn(object[] p)
        {
            m_GameLevelSelectView.Close();
            Global.Instance.CurLevel = 1;
            Global.Instance.MaxPassLevel = 1;
            OpenGameLevelSelestView();
        }
        private void OnGameLevelSelectClickReturnBtn(object[] p)
        {
            SceneManager.LoadScene("Init");
        }

        private void OnGameLevelSelectViewClickItmeBtn(object[] p)
        {
            int gamelevel = (int)p[0];
            Global.Instance.CurLevel = gamelevel;
            SceneManager.LoadScene("GameLevel");
        }

        #endregion

        public void OpenView(UIViewType type)
        {
            switch (type)
            {
                case UIViewType.GameLevelSelect:
                    OpenGameLevelSelestView();
                    break;
                case UIViewType.Pause:
                    OpenGameLevelPauseView();
                    break;
                case UIViewType.Win:
                    OpenGameLevelWinView();
                    break;

            }
        }
        /// <summary>
        /// 游戏暂停界面
        /// </summary>
        private void OpenGameLevelPauseView()
        {
            GameLevelSceneCtrl.Instance.IsPause = true;
            UIViewUtil.Instance.OpenWindow(UIViewType.Pause);
        }

        /// <summary>
        /// 游戏胜利界面
        /// </summary>
        private void OpenGameLevelWinView()
        {
            UIViewUtil.Instance.OpenWindow(UIViewType.Win);
        }

        /// <summary>
        /// 打开游戏关卡选择
        /// </summary>
        private void OpenGameLevelSelestView()
        {
            m_GameLevelSelectView = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelSelect).GetComponent<UIGameLevelSelectView>();
            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.MaxPassLevel, Global.Instance.MaxPassLevel);
            data.SetData(ConstDefine.MaxLevel, GameLevelDBModel.Instance.GetLevelCount());
            m_GameLevelSelectView.SetUI(data);
        }
    }
}
