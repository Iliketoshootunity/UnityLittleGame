using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using System.Collections.Generic;
using EasyFrameWork.UI;
using UnityEngine.EventSystems;
using LFrameWork.Sound;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public class GameLevelSceneCtrl : GameSceneCtrlBase
    {
        public bool IsGuide;
        public Player Player;
        public RoleCtrl Monster;
        public Sprite[] NumberSprite;
        public static GameLevelSceneCtrl Instance;
        public AudioClip WinClip;
        public AudioClip FailClip;
        private UIGameLevelView m_View;
        /// <summary>
        /// 引导物体
        /// </summary>
        private List<GameObject> GuideObjList;
        public GameObject GuideObj;
        protected override void OnAwake()
        {
            base.OnAwake();
            Instance = this;
        }
        protected override void OnStart()
        {
            GameLevelCtrl t = GameLevelCtrl.Instance;
            base.OnStart();
            m_View = UISceneCtrl.Instance.Load(UISceneType.GameLevel).GetComponent<UIGameLevelView>();
            m_View.SetStepCount(false, 0);
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, Global.Instance.ChangeSceneTime, null);
            m_View.SetUI(Global.Instance.CurLevel);
            GridManager.Instance.CreateNode(Global.Instance.CurLevel);
            Monster.OnRoleStatusChange += OnRoleStatusChange;
            Player.ValidLineCountChange += OnValidLineCountChange;
            Player.OnGuideEnd += HideGuide;
            Turn(true);
            GuideObjList = new List<GameObject>();
            GuideObjList.Add(GridManager.Instance.GetCell(6, 2).gameObject);
            if (Global.Instance.CurLevel == 1)
            {
                ShowGuide();
            }
            else
            {
                HideGuide();
            }
        }

        public bool ContainsGuideObj(GameObject obj)
        {
            return GuideObjList.Contains(obj);
        }
        /// 显示新手指引
        /// </summary>
        private void ShowGuide()
        {
            GuideObj.SetActive(true);
            IsGuide = true;
        }
        /// <summary>
        /// 隐藏新手引导
        /// </summary>
        private void HideGuide()
        {
            GuideObj.SetActive(false);
            IsGuide = false;
        }
        protected override void BeforeOnDestory()
        {
            Monster.OnRoleStatusChange -= OnRoleStatusChange;
        }
        private void OnRoleStatusChange(RoleStatus obj)
        {
            StartCoroutine(OnRoleStatusChangeIE(obj));
        }

        private IEnumerator OnRoleStatusChangeIE(RoleStatus obj)
        {
            if (obj == RoleStatus.Win)
            {
                Debug.Log("失败");
                Monster.OnWin();
                EazySoundManager.PlaySound(FailClip);
                yield return new WaitForSeconds(1);
                UIViewMgr.Instance.OpenView(UIViewType.Fail);

            }
            else if (obj == RoleStatus.Fail)
            {
                EazySoundManager.PlaySound(WinClip);
                UIViewMgr.Instance.OpenView(UIViewType.Win);
                Debug.Log("胜利");
            }
        }
        private void OnValidLineCountChange(int obj)
        {
            if (obj != -1)
            {
                m_View.SetStepCount(true, obj);
            }
            else
            {
                m_View.SetStepCount(false, obj);
            }

        }
        public void SetMonster(RoleCtrl monster)
        {
            Monster = monster;
        }


        public List<Sprite> GetNumber(int number)
        {
            List<Sprite> m_Sprite = new List<Sprite>(); ;
            if (number >= 10)
            {
                int s = number % 10;
                m_Sprite.Add(NumberSprite[1]);
                m_Sprite.Add(NumberSprite[s]);
            }
            else
            {
                m_Sprite.Add(NumberSprite[number]);
            }
            return m_Sprite;
        }
        public void Turn(bool toPlayer)
        {
            if (toPlayer)
            {
                Player.SetAction(true);
                Monster.SetAction(false);
            }
            else
            {
                m_View.SetStepCount(true, 0);
                Player.SetAction(false);
                Monster.SetAction(true);
            }
        }

        private void OnDestroy()
        {
            Monster.OnRoleStatusChange -= OnRoleStatusChange;
            Player.ValidLineCountChange -= OnValidLineCountChange;
            Player.OnGuideEnd -= HideGuide;
        }
    }

}
