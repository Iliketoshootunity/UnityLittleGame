using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using LFramework.Plugins.Astart;
using System;

namespace EasyFrameWork
{
    /// <summary>
    /// 派系
    /// </summary>
	public class Faction : MonoBehaviour
    {
        /// <summary>
        /// 玩家阵营
        /// </summary>
        public bool IsPlayer;
        public List<RoleCtrl> RoleCtrlList;
        /// <summary>
        /// 
        /// </summary>
        /// 是否是玩家 是否胜利
        //public Action<bool, bool> OnFactionWinOrFail;
        public bool IsRound;
        private int m_RoleActionCount;
        private Vector3 m_EndPos;
        private List<Node> m_EndNodeList;
        private void Start()
        {
            IsRound = true;
            m_EndNodeList = new List<Node>();
        }
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="roleCtrl"></param>
        public void AddRole(RoleCtrl roleCtrl)
        {
            if (RoleCtrlList == null)
            {
                RoleCtrlList = new List<RoleCtrl>();
            }
            RoleCtrlList.Add(roleCtrl);
        }
        /// <summary>
        /// 排序
        /// </summary>
        public void SortRole()
        {
            if (IsPlayer) return;
            SetAllMonsterObstacle(false);
            Node pNode = GridManager.Instance.GetNodeByPos(GameLevelSceneCtrl.Instance.Player.transform.position);
            RoleCtrlList.Sort((x, y) =>
            {
                Node selfNode1 = GridManager.Instance.GetNodeByPos(x.transform.position);
                Node selfNode2 = GridManager.Instance.GetNodeByPos(y.transform.position);
                ArrayList path1 = AStar.FindPath(selfNode1, pNode);
                ArrayList path2 = AStar.FindPath(selfNode2, pNode);
                if (path1.Count < path2.Count)
                {
                    return -1;
                }
                else if (path1.Count == path2.Count)
                {
                    float xDis = Vector2.Distance(x.transform.position, GameLevelSceneCtrl.Instance.Player.transform.position);
                    float yDis = Vector2.Distance(y.transform.position, GameLevelSceneCtrl.Instance.Player.transform.position);
                    if (xDis < yDis)
                    {
                        return -1;
                    }
                    else if (xDis == yDis)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                    return 0;
                }
                else
                {
                    return 1;
                }

            });
            SetAllMonsterObstacle(true);
        }


        /// <summary>
        /// 回合开始
        /// </summary>
        public void RoundStart()
        {
            for (int i = 0; i < RoleCtrlList.Count; i++)
            {
                RoleCtrlList[i].HideRange();
            }
        }

        /// <summary>
        /// 回合结束
        /// </summary>
        public void RoundEnd()
        {
            //刷新范围
            for (int i = 0; i < RoleCtrlList.Count; i++)
            {
                RoleCtrlList[i].RefreshRange(GridManager.Instance.GetNodeByPos(RoleCtrlList[i].transform.position));
            }
            for (int i = 0; i < RoleCtrlList.Count; i++)
            {
                RoleCtrlList[i].ShowRange();
            }

        }
        /// <summary>
        ///行动开始
        /// </summary>
        public void ActionStart(Vector3 endPos)
        {
            m_EndPos = endPos;
            m_RoleActionCount = 0;
            SortRole();
        }
        /// <summary>
        /// 行动进行
        /// </summary>
        public void Action(Vector3 movePos)
        {
            StartCoroutine(FindPath1(movePos));

        }

        private void SetAllMonsterObstacle(bool isObstacle)
        {
            if (!IsPlayer)
            {
                for (int i = 0; i < RoleCtrlList.Count; i++)
                {
                    Node overNode = GridManager.Instance.GetNodeByPos(RoleCtrlList[i].transform.position);
                    if (overNode != null)
                    {
                        overNode.IsObstacle = isObstacle;
                    }
                }
            }
        }

        private IEnumerator FindPath1(Vector3 movePos)
        {
            m_EndNodeList.Clear();
            IsRound = false;
            bool isWin = false;
            if (RoleCtrlList.Count == 0)
            {
                RoleActionEnd();
                yield break;
            }
            for (int i = 0; i < RoleCtrlList.Count; i++)
            {
                for (int j = 0; j < m_EndNodeList.Count; j++)
                {
                    m_EndNodeList[j].IsObstacle = false;
                }
                Node startNode = GridManager.Instance.GetNodeByPos(RoleCtrlList[i].transform.position);
                Node endNode = GridManager.Instance.GetNodeByPos(movePos);
                if (startNode == null || endNode == null) continue;
                if (IsPlayer)
                {
                    GameLevelSceneCtrl.Instance.SetOverPoint(false);
                }
                else
                {
                    GameLevelSceneCtrl.Instance.SetOverPoint(true);
                }
                ArrayList arr = AStar.FindPath(startNode, endNode);

                for (int j = 0; j < m_EndNodeList.Count; j++)
                {
                    m_EndNodeList[j].IsObstacle = true;
                }
                if (IsPlayer)
                {
                    GameLevelSceneCtrl.Instance.SetOverPoint(true);
                }
                if (arr == null)
                {
                    RoleActionEnd();
                    if (IsPlayer)
                    {
                        //OnFactionWinOrFail(IsPlayer, false);
                    }
                    continue;
                }
                List<Node> pathNode = new List<Node>(); ;
                foreach (var item in arr)
                {
                    Node n = (Node)item;
                    bool inRange1 = RoleCtrlList[i].CheckTargetInRange(n);
                    if (inRange1)
                    {

                        if (m_EndNodeList.Find(x => x == n) == null)
                        {
                            pathNode.Add(n);
                            Vector2 pos = GridManager.Instance.GetScalePos(n.Position, GridManager.Instance.YScale);
                            //判断游戏结束
                            Node overNode = GridManager.Instance.GetNodeByPos(GameLevelSceneCtrl.Instance.OverPoint.transform.position);
                            Node playerNode = GridManager.Instance.GetNodeByPos(GameLevelSceneCtrl.Instance.Player.transform.position);
                            Vector2 overPos = GridManager.Instance.GetScalePos(overNode.Position, GridManager.Instance.YScale);
                            Vector2 playerPos = GridManager.Instance.GetScalePos(playerNode.Position, GridManager.Instance.YScale);
                            if (IsPlayer && pos == overPos)
                            {
                                isWin = true;
                            }
                            else if (!IsPlayer && pos == playerPos)
                            {
                                isWin = true;
                            }
                        }
                    }
                    else
                    {
                        ;
                    }
                }

                Node newNode = startNode;
                startNode.IsObstacle = false;
                if (pathNode.Count >= 1)
                {
                    if (!IsPlayer)
                    {
                        pathNode[pathNode.Count - 1].IsObstacle = true;
                    }
                    newNode = pathNode[pathNode.Count - 1];
                }
                else
                {
                    if (!IsPlayer)
                    {
                        startNode.IsObstacle = true;
                    }
                }
                m_EndNodeList.Add(newNode);
                //旧节点IsObstacle = false 更新其他角色的范围
                for (int x = 0; x < RoleCtrlList.Count; x++)
                {
                    if (RoleCtrlList[x] == RoleCtrlList[i]) continue;
                    RoleCtrlList[x].RefreshRange(GridManager.Instance.GetNodeByPos(RoleCtrlList[x].transform.position));
                }
                //新节点IsObstacle = true 更新自己的范围
                RoleCtrlList[i].RefreshRange(newNode);
                RoleCtrlList[i].ToMove(pathNode, RoleActionEnd);
                if (isWin) yield break;
            }
        }


        /// <summary>
        /// 行动结束
        /// </summary>
        public void ActionEnd()
        {
            IsRound = true;
            GameLevelSceneCtrl.Instance.Turn();

        }
        /// <summary>
        /// 角色行动完成
        /// </summary>
        private void RoleActionEnd()
        {
            m_RoleActionCount++;
            if (m_RoleActionCount >= RoleCtrlList.Count)
            {
                ActionEnd();
            }
        }

    }
}
