using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class GameLevelCtrl : SystemCtrlBase<GameLevelCtrl>, ISystemCtrl
    {
        public UISwitchView UISwitchView;

        public int CurLevel;
        public int CurLevelGrade;

        private int m_CurMapGrade;
        public GameLevelCtrl()
        {
            UIDispatcher.Instance.AddEventListen(ConstDefine.UI_WorldMap_ClickLevel, OnClickLevelMapCallBack);
            UIDispatcher.Instance.AddEventListen(ConstDefine.UI_WorldMap_ClickMapGrade, OnClickMapGradeCallBack);
        }



        public override void Dispose()
        {
            base.Dispose();
            if (UISwitchView != null)
            {
                UISwitchView.OnSwitchEnd -= GameSceneCtrl.Instance.OnSwitchEndCallBack;
            }
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UI_WorldMap_ClickLevel, OnClickLevelMapCallBack);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UI_WorldMap_ClickMapGrade, OnClickMapGradeCallBack);
        }

        public void OpenView(UIViewType type)
        {

        }

        //显示切换动画
        public void ShowSwitchView()
        {
            if (UISwitchView == null)
            {
                UISwitchView = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "SwitchView").GetComponent<UISwitchView>();
                UISwitchView.transform.SetParent(UISceneCtrl.Instance.CurrentUIScene.MainCanvans.transform);
                UISwitchView.transform.localScale = Vector3.one;
                UISwitchView.transform.localPosition = Vector3.zero;
                UISwitchView.OnSwitchEnd += GameSceneCtrl.Instance.OnSwitchEndCallBack;
            }
            UISwitchView.SetUI(GameSceneCtrl.Instance.PlayerRound, 1, 1);
        }

        private void OpenWinView()
        {
            //打开胜利界面
            //TODO
            //查看有没有通关副本类的任务
            bool has = TaskManager.Instance.TryMathchCondition(1);
            if (has)
            {
                //完成任务
                TaskManager.Instance.ExecuteTheTask(GameSceneCtrl.Instance.CurLevel, 1);
            }
        }

        private void OnClickLevelMapCallBack(object[] p)
        {
            int curLevel = (int)p[0];
            int curGrade = (int)p[1];
            CurLevel = curLevel;
            CurLevelGrade = curGrade;
            SceneManager.LoadScene("Game");
        }
        private void OnClickMapGradeCallBack(object[] p)
        {
            int grade = (int)p[0];
            if (grade == m_CurMapGrade) return;
            SetWorldMap(grade);
        }
        public void SetWorldMap(int grade)
        {
            m_CurMapGrade = grade;
            UIMainSceneView view = (UIMainSceneView)UISceneCtrl.Instance.CurrentUIScene;
            if (view == null) return;
            DataTransfer data = new DataTransfer();
            List<DataTransfer> datas = new List<DataTransfer>();
            List<GameLevelEntity> levelEntityList = GameLevelDBModel.Instance.GetList();
            int maxCanPlayGameLevelId = SimulatedDatabase.Instance.GetMaxCanPlayGameLevel(grade);
            data.SetData(ConstDefine.UI_WorldMap_MaxCanPlayGameLevelId, maxCanPlayGameLevelId);
            for (int i = 0; i < levelEntityList.Count; i++)
            {
                int id = levelEntityList[i].Id;
                string name = levelEntityList[i].Name;
                string icon = levelEntityList[i].Ico;
                string[] posStrArr = levelEntityList[i].Pos.Split('_');
                Vector2 pos = Vector2.zero;
                pos.x = posStrArr[0].ToFloat();
                pos.y = posStrArr[1].ToFloat();
                DataTransfer d = new DataTransfer();
                d.SetData(ConstDefine.UI_WorldMap_LevelId, id);
                d.SetData(ConstDefine.UI_WorldMap_LevelGrade, grade);
                d.SetData(ConstDefine.UI_WorldMap_LevelName, name);
                d.SetData(ConstDefine.UI_WorldMap_LevelIcon, icon);
                d.SetData(ConstDefine.UI_WorldMap_LevelPos, pos);
                if (maxCanPlayGameLevelId == -1001)//未通关上一难度
                {
                    d.SetData(ConstDefine.UI_WorldMap_CanPlay, false);
                }
                else
                {
                    if (id <= maxCanPlayGameLevelId)//关卡ID升序
                    {
                        d.SetData(ConstDefine.UI_WorldMap_CanPlay, true);
                    }
                    else
                    {
                        d.SetData(ConstDefine.UI_WorldMap_CanPlay, false);
                    }
                }

                datas.Add(d);
            }
            data.SetData(ConstDefine.UI_WorldMap_Content, datas);
            view.SetWorldMap(data);
        }
    }
}
