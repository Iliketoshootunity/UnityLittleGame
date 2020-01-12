using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameLevelCtrl : SystemCtrlBase<GameLevelCtrl>, ISystemCtrl
{
    private UIGameLevelPauseView m_GameLevelPauseView;
    private UIGameLevelFailView m_UIGameLevelFailView;
    private UIGameLevelHelpView m_UIGameLevelHelpView;
    public void OpenView(UIViewType type)
    {
        switch (type)
        {
            case UIViewType.GameLevelPause:
                OpenGameLevelPauseView();
                break;
            case UIViewType.GameLevelFail:
                OpenGameLevelFailView();
                break;
            case UIViewType.GameLevelHelp:
                OpenGameLevelHelpView();
                break;
        }
    }

    private void OpenGameLevelHelpView()
    {
        m_UIGameLevelHelpView = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelHelp).GetComponent<UIGameLevelHelpView>();
    }

    public void CloseHelpView()
    {
        m_UIGameLevelHelpView.Close();
    }
    private void OpenGameLevelFailView()
    {
        m_UIGameLevelFailView = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelFail).GetComponent<UIGameLevelFailView>();
        m_UIGameLevelFailView.OnClickReturnButton = OnClickGameLevelFailViewReturnButton;
        m_UIGameLevelFailView.OnClickRestartGameButton = OnClickGameLevelFailViewRestartGameButton;
        int maxScore = PlayerPrefs.GetInt("MaxScore");
        m_UIGameLevelFailView.SetUI(GameLevelSceneCtrl.Instance.Step, maxScore);
    }

    private void OnClickGameLevelFailViewRestartGameButton()
    {
        SceneManager.LoadScene("Mini_2");
    }

    private void OnClickGameLevelFailViewReturnButton()
    {
        SceneManager.LoadScene("Mini_1");
    }

    private void OpenGameLevelPauseView()
    {
        m_GameLevelPauseView = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelPause).GetComponent<UIGameLevelPauseView>();
        m_GameLevelPauseView.OnClickReturnButton = OnClickGameLevelPauseViewReturnButton;
        m_GameLevelPauseView.OnClickContineButton = OnClickGameLevelPauseViewContineButton;
    }

    private void OnClickGameLevelPauseViewContineButton()
    {
        Time.timeScale = 1;
        m_GameLevelPauseView.Close();
        GameLevelSceneCtrl.Instance.GameStatus = GameStatus.Playing;
    }

    private void OnClickGameLevelPauseViewReturnButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Mini_1");
    }
}
