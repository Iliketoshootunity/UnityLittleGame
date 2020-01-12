using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using LFrameWork.Sound;
using System.Collections.Generic;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public enum MoveDirction
    {
        Up,
        Down,
        Left,
        Right
    }
    public enum RoleStatus
    {
        None,
        Win,
        Fail,
        Running
    }
    public class RoleCtrl : Stander, IAction
    {
        [SerializeField]
        private float m_Speed = 1;
        [SerializeField]
        private float m_ShowSpeed;
        [SerializeField]
        private float m_ShowDelay;
        [SerializeField]
        private AnimationCurve m_ShowCurve;
        [SerializeField]
        private SpriteRenderer m_NumberSprite;
        [SerializeField]
        private GameObject m_Body;


        private bool m_IsAction;
        private Node m_PriorityNode;
        private List<ArrayList> m_PathList;
        private List<ArrayList> m_MinPathList;
        private ArrayList path;
        private int m_Range = -1;
        private Animator m_Animator;
        private MoveDirction m_Dirction;
        private RoleStatus m_Status;
        public Action<RoleStatus> OnRoleStatusChange;
        void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
            Show();
        }

        private void Show()
        {
            Debug.Log(transform);
            m_Body.SetActive(false);
            transform.DoMove(new Vector3(0, 10, 0), m_ShowSpeed).SetIsSpeed(true).SetIsRelative(true).SetIsFrom(true).SetDelay(m_ShowDelay).SetAnimatorCurve(m_ShowCurve)
             .SetStartAction(() => m_Body.SetActive(true));
        }
        public void SetAction(bool isAction)
        {
            m_IsAction = isAction;
            if (m_IsAction)
            {
                FindPath();
                ToMove();
            }
        }
        private void FindPath()
        {
            m_Status = RoleStatus.Running;
            //开始寻路
            if (m_PathList == null)
            {
                m_PathList = new List<ArrayList>();
            }
            if (m_MinPathList == null)
            {
                m_MinPathList = new List<ArrayList>();
            }
            m_PathList.Clear();
            m_MinPathList.Clear();
            List<Cell> sideCell = GridManager.Instance.GetSideCell();
            for (int i = 0; i < sideCell.Count; i++)
            {
                if (sideCell[i].Node.IsObstacle == false)
                {

                    ArrayList path = AStar.FindPath(Cell.Node, sideCell[i].Node);
                    if (path != null)
                    {
                        if (!m_PathList.Contains(path))
                        {
                            m_PathList.Add(path);
                        }
                    }
                }
            }
            if (m_PathList.Count > 0)
            {
                int minPath = m_PathList[0].Count;
                for (int i = 1; i < m_PathList.Count; i++)
                {
                    if (m_PathList[i].Count < minPath)
                    {
                        minPath = m_PathList[i].Count;
                    }
                }
                m_MinPathList = m_PathList.FindAll(x => x.Count == minPath);
                if (m_PriorityNode != null)
                {
                    ArrayList priorityPath = m_MinPathList.Find((x) => x[x.Count - 1] == m_PriorityNode);
                    if (priorityPath != null)
                    {
                        path = priorityPath;
                    }
                    else
                    {
                        path = m_MinPathList[0];
                    }
                }
                else
                {
                    path = m_MinPathList[0];
                }
                m_PriorityNode = (Node)path[path.Count - 1];
            }
            else
            {
                m_Status = RoleStatus.Fail;
                Debug.Log("玩家胜利");
            }
        }
        private void ToMove()
        {
            StartCoroutine("ToMoveIE");
        }
        private IEnumerator ToMoveIE()
        {
            if (m_Status == RoleStatus.Running)
            {
                if (path.Count > 0)
                {
                    if (m_Range == -1)
                    {
                        m_Range = 1;
                    }
                    int cutRange = (path.Count - 1) - m_Range;
                    if (cutRange > 0)
                    {
                        for (int i = 0; i < cutRange; i++)
                        {
                            path.RemoveAt(path.Count - 1);
                        }
                    }
                    for (int i = 0; i < path.Count; i++)
                    {
                        bool isRun = true;
                        Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
                        Vector2 endPos = ((Node)path[i]).Position;
                        float distance = Vector2.Distance(transform.position, endPos);
                        float time = distance / m_Speed;
                        float process = 0;
                        float timer = 0;
                        //四个方向
                        Node selfNode = Cell.Node;
                        if (selfNode == path[i]) continue;
                        float row = ((Node)path[i]).Row - selfNode.Row;
                        float colum = ((Node)path[i]).Column - selfNode.Column;

                        if (row == -1 && colum == 0)
                        {
                            m_Animator.SetInteger("WalkType", 0);
                            m_Dirction = MoveDirction.Up;
                        }
                        else if (row == 1 && colum == 0)
                        {
                            m_Animator.SetInteger("WalkType", 1);
                            m_Dirction = MoveDirction.Down;
                        }
                        else if (row == 0 && colum == -1)
                        {
                            m_Animator.SetInteger("WalkType", 2);
                            m_Dirction = MoveDirction.Left;
                        }
                        else if (row == 0 && colum == 1)
                        {
                            m_Animator.SetInteger("WalkType", 3);
                            m_Dirction = MoveDirction.Right;
                        }
                        m_Animator.SetTrigger("ToWalk");
                        while (isRun)
                        {
                            timer += Time.deltaTime;
                            process = timer / time;
                            if (process >= 1)
                            {
                                process = 1;
                                RefreshCell(GridManager.Instance.GetCellByNode((Node)path[i]));
                                isRun = false;
                                if ((Node)path[i] == m_PriorityNode)
                                {
                                    m_Status = RoleStatus.Win;
                                }
                            }
                            Vector2 pos = Vector2.Lerp(startPos, endPos, process);
                            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
                            yield return null;
                        }
                    }
                    m_Range++;
                    if (m_Range >= 5)
                    {
                        m_Range = 5;
                    }
                    m_NumberSprite.sprite = GameLevelSceneCtrl.Instance.GetNumber(m_Range)[0];
                }
            }
            if (OnRoleStatusChange != null)
            {
                OnRoleStatusChange(m_Status);
            }
            if (m_Status == RoleStatus.Running)
            {
                GameLevelSceneCtrl.Instance.Turn(true);
            }
            ToIdle();
        }

        private void ToIdle()
        {
            m_Animator.SetTrigger("ToIdle");
            switch (m_Dirction)
            {
                case MoveDirction.Up:
                    m_Animator.SetInteger("IdleType", 0);
                    break;
                case MoveDirction.Down:
                    m_Animator.SetInteger("IdleType", 1);
                    break;
                case MoveDirction.Left:
                    m_Animator.SetInteger("IdleType", 2);
                    break;
                case MoveDirction.Right:
                    m_Animator.SetInteger("IdleType", 3);
                    break;
                default:
                    break;
            }
        }

        public void OnWin()
        {
            Vector3 target = Vector3.zero;
            m_Animator.SetTrigger("ToWalk");
            switch (m_Dirction)
            {
                case MoveDirction.Up:
                    transform.DoMove(Vector3.up * 3, 1).SetIsRelative(true);
                    m_Animator.SetInteger("WalkType", 0);
                    break;
                case MoveDirction.Down:
                    transform.DoMove(Vector3.down * 3, 1).SetIsRelative(true);
                    m_Animator.SetInteger("WalkType", 1);
                    break;
                case MoveDirction.Left:
                    transform.DoMove(Vector3.left * 3, 1).SetIsRelative(true);
                    m_Animator.SetInteger("WalkType", 2);
                    break;
                case MoveDirction.Right:
                    transform.DoMove(Vector3.right * 3, 1).SetIsRelative(true);
                    m_Animator.SetInteger("WalkType", 3);
                    break;
            }
        }

    }
}
