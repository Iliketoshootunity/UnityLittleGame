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

            AddEventListen(ConstDefine.GameLevelViewClickReturnBtn, OnGameLevelViewClickReturnBtn);
            AddEventListen(ConstDefine.GameLevelViewClickRestartBtn, OnGameLevelViewClickRestartBtn);

            AddEventListen(ConstDefine.ClickNextGameLevelBtn, OnClickNextGameLevelBtn);
            AddEventListen(ConstDefine.ClickGameLeveSelectBtn, OnClickGameLeveSelectBtn);
            AddEventListen(ConstDefine.ClickRestartBtn, OnClickRestartBtn);
        }


        #region 关卡场景事件
        private void OnGameLevelViewClickReturnBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => { SceneManager.LoadScene("GameLevelSelect"); });
        }

        private void OnGameLevelViewClickRestartBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => { SceneManager.LoadScene("GameLevel"); });
        }


        #region 胜利界面事件
        private void OnClickGameLeveSelectBtn(object[] p)
        {
            OnWin();
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => { SceneManager.LoadScene("GameLevelSelect"); });
        }

        private void OnClickNextGameLevelBtn(object[] p)
        {
            bool isEnd = OnWin();
            if (isEnd) return;
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => { SceneManager.LoadScene("GameLevel"); });
        }


        private void OnClickRestartBtn(object[] p)
        {
            OnWin(false);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () => { SceneManager.LoadScene("GameLevel"); });
        }
        private bool OnWin(bool setCur = true)
        {
            int level = Global.Instance.CurLevel;
            level++;
            int maxLevel = GameLevelDBModel.Instance.GetList().Count;
            if (level > maxLevel)
            {
                return true;
            }
            if (level >= maxLevel)
            {
                maxLevel = level;
            }
            if (setCur)
            {
                Global.Instance.CurLevel = level;
            }
            if (Global.Instance.CurLevel >= Global.Instance.MaxPassLevel)
            {
                Global.Instance.MaxPassLevel = Global.Instance.CurLevel;
            }
            return false;
        }
        #endregion
        #endregion


        #region 关卡选择界面事件


        private void OnGameLevelSelectViewClickItmeBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, Global.Instance.ChangeSceneTime, () =>
            {
                int gamelevel = (int)p[0];
                Global.Instance.CurLevel = gamelevel;
                SceneManager.LoadScene("GameLevel");
            });

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
                case UIViewType.Fail:
                    OpenGamLevelFailView();
                    break;

            }
        }
        /// <summary>
        /// 游戏暂停界面
        /// </summary>
        private void OpenGameLevelPauseView()
        {
            //GameLevelSceneCtrl.Instance.IsPause = true;
            UIViewUtil.Instance.OpenWindow(UIViewType.Pause);
        }

        /// <summary>
        /// 游戏胜利界面
        /// </summary>
        private void OpenGameLevelWinView()
        {
            UIViewUtil.Instance.OpenWindow(UIViewType.Win);
        }

        private void OpenGamLevelFailView()
        {
            UIViewUtil.Instance.OpenWindow(UIViewType.Fail);
        }
        /// <summary>
        /// 打开游戏关卡选择
        /// </summary>
        private void OpenGameLevelSelestView()
        {
            m_GameLevelSelectView = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelSelect).GetComponent<UIGameLevelSelectView>();
            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.MaxPassLevel, Global.Instance.MaxPassLevel);
            data.SetData(ConstDefine.MaxLevel, GameLevelDBModel.Instance.GetList().Count);
            m_GameLevelSelectView.SetUI(data);
        }
    }
}
