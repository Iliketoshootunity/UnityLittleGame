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
        private UIGameLevelFailView m_FailWindow;
        private UIGameLevelVictoryView m_VictoryWindow;
        private UIGameLevelPauseView m_PauseWindow;

        public GameLevelCtrl()
        {
            AddEventListen(ConstDefine.GameLevelScene_FailWindow_Continue, OnClickContinueBtn);
            AddEventListen(ConstDefine.GameLevelScene_FailWindow_Return, OnClickReturnBtn);

            AddEventListen(ConstDefine.GameLevelScene_VictoryWindow_Continue, OnClickContinueBtn);
            AddEventListen(ConstDefine.GameLevelScene_VictoryWindow_Return, OnClickReturnBtn);

            AddEventListen(ConstDefine.GameLevelScene_PauseWindow_Continue, OnClickContinueBtnForTimeScale);
            AddEventListen(ConstDefine.GameLevelScene_PauseWindow_Return, OnClickReturnBtn);

        }

        public void OpenView(UIViewType type)
        {
            switch (type)
            {
                case UIViewType.Fail:
                    OpenGameLevelFailWindow();
                    break;
                case UIViewType.Victory:
                    OpenGameLevelVictoryWindow();
                    break;
                case UIViewType.Pause:
                    OpenGameLevelPauseWindow();
                    break;
            }
        }

        private void OpenGameLevelFailWindow()
        {
            m_FailWindow = UIViewUtil.Instance.OpenWindow(UIViewType.Fail).GetComponent<UIGameLevelFailView>();
            m_FailWindow.SetUI(GameLevelSceneCtrl.Instance.CurFloor);
        }

        private void OpenGameLevelVictoryWindow()
        {
            m_VictoryWindow = UIViewUtil.Instance.OpenWindow(UIViewType.Victory).GetComponent<UIGameLevelVictoryView>();
        }
        private void OpenGameLevelPauseWindow()
        {
            m_PauseWindow = UIViewUtil.Instance.OpenWindow(UIViewType.Pause).GetComponent<UIGameLevelPauseView>();
        }
        private void OnClickReturnBtn(object[] p)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Mini_1");
        }

        private void OnClickContinueBtn(object[] p)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameLevel");
        }
        private void OnClickContinueBtnForTimeScale(object[] p)
        {
            Time.timeScale = 1;
            m_PauseWindow.Close();
            Debug.Log("PP");
        }
    }
}
