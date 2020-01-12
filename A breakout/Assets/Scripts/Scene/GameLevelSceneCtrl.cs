using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFramework.Plugins.Astart;
using System.Collections.Generic;
using System;
using EasyFrameWork.UI;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public enum GameLevelStatus
    {
        Playing,
        Pause,
        End
    }
    public class GameLevelSceneCtrl : GameSceneCtrlBase
    {
        public Faction Faction1;
        public Faction Faction2;
        public RoleCtrl Player;
        public List<RoleCtrl> Monsters;
        public GameObject OverPoint;
        public AudioClip ErrorClip;
        public AudioClip WinClip;
        private bool m_IsPlayerRound;
        private int m_TurnCount;
        public GameLevelStatus GameLevelStatus;
        public static GameLevelSceneCtrl Instance;

        protected override void OnAwake()
        {
            Instance = this;
        }
        protected override void OnStart()
        {
            UIGameLevelSceneView view = UISceneCtrl.Instance.Load(UISceneType.GameLevel).GetComponent<UIGameLevelSceneView>();
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open, null);
            view.SetUI(Global.Instance.CurLevel);
            UIDispatcher.Instance.AddEventListen(ConstDefine.SceneGameLevelViewClickPauseBtn, OnClickGameLevelViewClickPauseBtn);
            //Global.Instance.SetMusic();
            GridManager.Instance.CreateNode();
            CreateMap(Global.Instance.CurLevel);
            m_TurnCount = -1;
            m_IsPlayerRound = false;
            Turn();
            GameLevelStatus = GameLevelStatus.Playing;


        }

        private void OnClickGameLevelViewClickPauseBtn(object[] p)
        {
            UIViewMgr.Instance.OpenView(UIViewType.GameLevelPause);
            GameLevelStatus = GameLevelStatus.Pause;
        }


        private void OnRoleWin(bool isPlayer)
        {
            if (isPlayer)
            {
                OnWin();
                ChangeSceneCtrl.Instance.Show(ChangeSceneType.Close, () => SceneManager.LoadScene("GameLevel"));
            }
            else
            {
                UIViewMgr.Instance.OpenView(UIViewType.GameLevelFail);
            }
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
            EazySoundManager.PlaySound(WinClip);
            return false;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (GameLevelStatus == GameLevelStatus.Playing)
            {
                if (m_IsPlayerRound)
                {
                    if (Faction1.IsRound)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.up, 0.05f);
                            if (hit.collider == null) return;
                            Node n = GridManager.Instance.GetNodeByPos(hit.collider.transform.position);
                            bool inRange = Player.CheckTargetInRange(n);
                            if (inRange)
                            {
                                Faction1.RoundStart();
                                Faction2.RoundStart();
                                Faction1.Action(hit.collider.transform.position);
                            }
                            else
                            {
                                //播放点击错误声音
                                EazySoundManager.PlaySound(ErrorClip);
                            }
                        }
                    }
                }
                else
                {
                    if (Faction2.IsRound)
                    {
                        Faction2.Action(Player.transform.position);
                    }
                }
            }


        }
        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.SceneGameLevelViewClickPauseBtn, OnClickGameLevelViewClickPauseBtn);
        }

        /// <summary>
        /// 创建地图 角色 怪物 障碍物 终点
        /// </summary>
        private void CreateMap(int level)
        {
            //生成障碍物
            List<ObstacleInfo> oobstaclePos = GameLevelDBModel.Instance.GetObstaclesInfoList(level);
            if (oobstaclePos != null)
            {
                for (int i = 0; i < oobstaclePos.Count; i++)
                {
                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacle" + oobstaclePos[i].ObstacleType, isCache: true);
                    go.transform.position = oobstaclePos[i].Pos;
                    //将此位置设为障碍物
                    Node n = GridManager.Instance.GetNodeByPos(oobstaclePos[i].Pos);
                    if (n != null)
                    {
                        n.IsObstacle = true;
                    }
                }
            }

            //生成终点
            Vector2 pos = GameLevelDBModel.Instance.GetOverPoint(level);
            GameObject go1 = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "OverPoint", isCache: true);
            go1.transform.position = pos;
            Node n1 = GridManager.Instance.GetNodeByPos(go1.transform.position);
            if (n1 != null)
            {
                n1.IsObstacle = true;
            }
            OverPoint = go1;

            //生成角色
            List<RoleInfo> playerInfo = GameLevelDBModel.Instance.GetPlayerInfoList(level);
            if (playerInfo != null)
            {
                for (int i = 0; i < playerInfo.Count; i++)
                {
                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Role, "Player", isCache: true);
                    go.transform.position = playerInfo[i].Pos;
                    Player = go.GetComponent<RoleCtrl>();
                    Player.Init(playerInfo[i].Range, OverPoint);
                    Faction1.AddRole(Player);
                    Player.OnWin += OnRoleWin;
                }
            }

            //生成怪物
            List<RoleInfo> monsterInfo = GameLevelDBModel.Instance.GetMonsterInfoList(level);
            if (monsterInfo != null)
            {
                for (int i = 0; i < monsterInfo.Count; i++)
                {
                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Role, "Monster", isCache: true);
                    go.transform.position = monsterInfo[i].Pos;
                    RoleCtrl roleCtrl = go.GetComponent<RoleCtrl>();
                    roleCtrl.Init(monsterInfo[i].Range, Player.gameObject);
                    Faction2.AddRole(roleCtrl);
                    Node n = GridManager.Instance.GetNodeByPos(go.transform.position);
                    if (n != null)
                    {
                        n.IsObstacle = true;
                    }
                    Monsters.Add(roleCtrl);
                    roleCtrl.OnWin += OnRoleWin;
                }
            }
        }



        /// <summary>
        /// 回合阵营转换
        /// </summary>
        public void Turn()
        {
            m_TurnCount++;
            if (m_TurnCount >= 2)
            {
                m_TurnCount = 0;
                Faction1.RoundEnd();
                Faction2.RoundEnd();
                SetOverPoint(false);
                Player.RefreshRange(GridManager.Instance.GetNodeByPos(Player.transform.position));
                Player.ShowRange();
                if (Player.CheckFail())
                {
                    OnRoleWin(false);
                }
            }
            m_IsPlayerRound = !m_IsPlayerRound;
            if (m_IsPlayerRound)
            {
                Faction1.ActionStart(OverPoint.transform.position);
            }
            else
            {
                Faction2.ActionStart(Player.transform.position);
            }
        }

        public void SetOverPoint(bool isObstacle)
        {
            Node overNode = GridManager.Instance.GetNodeByPos(OverPoint.transform.position);
            if (overNode != null)
            {
                overNode.IsObstacle = isObstacle;
            }
        }
    }
}
