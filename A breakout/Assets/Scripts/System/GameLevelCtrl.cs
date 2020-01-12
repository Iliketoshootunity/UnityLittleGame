using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.SceneManagement;
using System;

namespace EasyFrameWork
{
    public class GameLevelCtrl : SystemCtrlBase<GameLevelCtrl>, ISystemCtrl
    {
        private UIGameLevelSelectView m_GameLevelSelectView;
        private UIGameLevelFailView m_FailView;
        private UIGameLevelPauseView m_PauseView;

        public GameLevelCtrl()
        {
            AddEventListen(ConstDefine.GameLevelSelectViewClickItmeBtn, OnGameLevelSelectViewClickItmeBtn);
            AddEventListen(ConstDefine.PauseOrFailViewClickContinueBtn, OnPauseOrFailViewClickContinueBtn);
            AddEventListen(ConstDefine.PauseOrFailViewClickReturnBtn, OnPauseOrFailViewClickReturnBtn);
            AddEventListen(ConstDefine.FailViewClickRestartBtn, OnFailViewClickRstartBtn);
        }



        public void OpenView(UIViewType type)
        {
            switch (type)
            {
                case UIViewType.None:
                    break;
                case UIViewType.GameLevelSelect:
                    OpenGameLevelSelect();
                    break;
                case UIViewType.GameLevelPause:
                    OpenGameLevelPause();
                    break;
                case UIViewType.GameLevelFail:
                    OpenGameLevelFail();
                    break;
                default:
                    break;
            }
        }

        private void OpenGameLevelSelect()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelSelect);
            if (go != null)
            {
                m_GameLevelSelectView = go.GetComponent<UIGameLevelSelectView>();
                DataTransfer data = new DataTransfer();
                data.SetData(ConstDefine.MaxPassLevel, Global.Instance.MaxPassLevel);
                data.SetData(ConstDefine.MaxLevel, GameLevelDBModel.Instance.GetLevelCount());
                m_GameLevelSelectView.SetUI(data);
            }
        }

        private void OpenGameLevelPause()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelPause);
            m_PauseView = go.GetComponent<UIGameLevelPauseView>();
            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.MusicStatus, Global.Instance.IsPlaySound);
            m_PauseView.SetUI(data);
        }
        private void OpenGameLevelFail()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.GameLevelFail);
            m_FailView = go.GetComponent<UIGameLevelFailView>();
            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.MusicStatus, Global.Instance.IsPlaySound);
            m_FailView.SetUI(data);
        }
        private void OnGameLevelSelectViewClickItmeBtn(object[] p)
        {
            int gamelevel = (int)p[0];
            Global.Instance.CurLevel = gamelevel;
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("GameLevel"); });
        }

        private void OnPauseOrFailViewClickReturnBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("GameLevelSelect"); });
        }

        private void OnPauseOrFailViewClickContinueBtn(object[] p)
        {
            GameLevelSceneCtrl.Instance.GameLevelStatus = GameLevelStatus.Playing;
        }

        private void OnFailViewClickRstartBtn(object[] p)
        {
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => { SceneManager.LoadScene("GameLevel"); });
        }
    }
}
