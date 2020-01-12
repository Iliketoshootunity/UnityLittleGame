using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using LFramework.Plugins.Astart;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public struct ChainInfo
    {
        public Stronghold StartStronghold;
        public Stronghold EndStronghold;
        public List<Line> LineList;
        public Action<int> ValidLineCountChange;
        private int m_ValidLineCount;
        public int ValidLineCount
        {
            get
            {
                return m_ValidLineCount;
            }
            set
            {
                m_ValidLineCount = value;
                if (ValidLineCountChange != null)
                {
                    ValidLineCountChange(m_ValidLineCount);
                }
            }
        }

        public void Start(Stronghold startStronghold, Action<int> validLineCountChange)
        {
            StartStronghold = startStronghold;
            ValidLineCountChange = validLineCountChange;
            if (StartStronghold != null)
            {
                ValidLineCount = StartStronghold.Range;
            }
            ValidLineCount = StartStronghold.Range;
        }

        public void AddLine(Line line)
        {
            if (LineList == null)
            {
                LineList = new List<Line>();
            }
            LineList.Add(line);
            m_ValidLineCount = StartStronghold.Range - LineList.Count + 1;
            ValidLineCount = m_ValidLineCount;
        }
        public void RemoveLine(Line line)
        {
            LineList.Remove(line);
            if (LineList.Count == 0)
            {
                m_ValidLineCount = StartStronghold.Range;
            }
            else
            {
                m_ValidLineCount = StartStronghold.Range - LineList.Count + 1;
            }
            ValidLineCount = m_ValidLineCount;
        }

        public void Scueess()
        {
            StartStronghold.ConnectStronghold = EndStronghold;
            EndStronghold.ConnectStronghold = StartStronghold;
            StartStronghold.ShowOrHideFlag(true);
            EndStronghold.ShowOrHideFlag(true);
        }

        public void Destory()
        {
            if (StartStronghold != null)
            {
                StartStronghold.ConnectStronghold = null;
                StartStronghold.ShowOrHideFlag(false);
                StartStronghold = null;

            }
            if (EndStronghold != null)
            {
                EndStronghold.ConnectStronghold = null;
                EndStronghold.ShowOrHideFlag(false);
                EndStronghold = null;

            }
            int count = LineList.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject.Destroy(LineList[LineList.Count - 1].gameObject);
                LineList.RemoveAt(LineList.Count - 1);
            }
            ValidLineCount = 0;
            Clear();
        }

        public void Clear()
        {
            StartStronghold = null;
            EndStronghold = null;
            LineList.Clear();
            ValidLineCountChange = null;
            ValidLineCount = 0;
        }

        public bool CheckInRange()
        {
            if (LineList.Count > StartStronghold.Range)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsEmpty()
        {
            if (LineList.Count <= 0)
            {
                return true;
            }
            return false;
        }

        public ChainInfo Clone()
        {
            ChainInfo info = new ChainInfo();
            info.StartStronghold = StartStronghold;
            info.EndStronghold = EndStronghold;
            info.LineList = new List<Line>();
            for (int i = 0; i < LineList.Count; i++)
            {
                info.LineList.Add(LineList[i]);
            }
            return info;
        }

        public bool CheckIsLastOne(GameObject obj)
        {
            if (LineList.Count > 0)
            {
                if (obj == LineList[LineList.Count - 1].Cell1.gameObject)
                {
                    return true;
                }
            }
            return false;
        }

    }
    public enum InputStatus
    {
        None,
        Down,
        Running,
        Up
    }
    public class Player : MonoBehaviour, IAction
    {
        public bool IsDebug;
        public Action<int> ValidLineCountChange;
        public Action OnGuideEnd;
        public AudioClip ClickCell;
        public AudioClip ClickStronghold;
        public AudioClip ClickError;
        private ChainInfo m_ChainInfo;
        private GameObject m_LastErrorObj;
        private List<ChainInfo> m_AllChainInfo;
        private Line m_Line;
        private Cell m_LineOrigin;
        private InputStatus m_InputStatus;
        private InputStatus m_PreInputStatus;
        private int m_LineCount;
        private bool m_Action;
        public bool IsAction
        {
            get
            {
                return m_Action;
            }

            set
            {
                m_Action = value;
            }
        }

        private void Update()
        {
            if (!IsDebug)
            {
                if (m_Action)
                {
                    if (Input.touches.Length == 0)
                    {
                        if (m_InputStatus == InputStatus.Down || m_InputStatus == InputStatus.Running)
                        {
                            m_InputStatus = InputStatus.Up;
                            PointUp();
                        }
                        m_InputStatus = InputStatus.None;
                    }
                    if (Input.touches.Length == 1)
                    {
                        if (m_InputStatus == InputStatus.None)
                        {
                            m_InputStatus = InputStatus.Down;
                            PointDown();
                        }
                        if (m_InputStatus == InputStatus.Down)
                        {
                            m_InputStatus = InputStatus.Running;
                        }
                        else if (m_InputStatus == InputStatus.Running)
                        {
                            Point();
                        }
                    }
                    if (Input.touches.Length == 2)
                    {
                        if (m_InputStatus == InputStatus.Down || m_InputStatus == InputStatus.Running)
                        {
                            m_InputStatus = InputStatus.Up;
                            PointUp();
                        }
                        m_InputStatus = InputStatus.None;
                    }

                }
            }
            else
            {
                if (m_Action)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PointDown();
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        Point();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        PointUp();
                    }
                }
            }
        }

        public void SetAction(bool isAction)
        {
            m_Action = isAction;
            if (m_AllChainInfo == null)
            {
                m_AllChainInfo = new List<ChainInfo>();
            }
        }

        private void PointDown()
        {
            m_ChainInfo.StartStronghold = null;
            m_ChainInfo.EndStronghold = null;
            if (m_ChainInfo.LineList == null)
            {
                m_ChainInfo.LineList = new List<Line>();
            }
            m_ChainInfo.LineList.Clear();
            //第一个点
            m_Line = null;
            m_LineOrigin = null;
            Cell c = GetCell(Input.mousePosition);
            if (c != null)
            {
                Stronghold s = c.GetStronghold();
                if (s != null)
                {
                    if (!s.HasConnectStronghold())
                    {
                        bool isOk = true;
                        if (GameLevelSceneCtrl.Instance.IsGuide)
                        {
                            if (GameLevelSceneCtrl.Instance.ContainsGuideObj(c.gameObject))
                            {
                                isOk = true;
                            }
                            else
                            {
                                isOk = false;
                            }
                        }
                        if (isOk)
                        {
                            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/Line", isCache: true, isClone: true);
                            m_Line = go.GetComponent<Line>();
                            m_Line.AddCell(c);
                            m_LineOrigin = c;
                            m_Line.Parent = null;
                            m_ChainInfo.Start(s, OnValidLineCountChange);
                            m_ChainInfo.AddLine(m_Line);
                            EazySoundManager.PlaySound(ClickStronghold);
                        }

                    }
                    else
                    {
                        ChainInfo info = m_AllChainInfo.Find(x => x.StartStronghold == s || x.EndStronghold == s);
                        if (!info.IsEmpty())
                        {
                            m_AllChainInfo.Remove(info);
                            info.Destory();
                        }
                    }

                }
            }
        }

        private void OnValidLineCountChange(int obj)
        {
            if (ValidLineCountChange != null)
            {
                ValidLineCountChange(obj);
            }
        }

        private void Point()
        {

            if (m_LineOrigin != null)
            {
                Cell c = GetCell(Input.mousePosition);
                if (c != null)
                {
                    bool isNeighbours = GridManager.Instance.IsNeighbours(m_LineOrigin.Node, c.Node);
                    //有效点
                    if (isNeighbours)
                    {
                        //判断是否回退
                        if (m_Line.Parent != null && c == m_Line.Parent.Cell1)
                        {
                            Line line1 = m_Line;
                            Line line2 = m_Line.Parent;
                            if (line2.Parent == null)
                            {
                                m_Line = line2;
                            }
                            else
                            {
                                m_Line = line2.Parent;
                            }
                            if (line1 != null)
                            {
                                m_ChainInfo.RemoveLine(line1);
                                Destroy(line1.gameObject);

                            }
                            if (line2 != null)
                            {
                                m_ChainInfo.RemoveLine(line2);
                                Destroy(line2.gameObject);
                            }
                        }

                        //判断是否和其他点相交
                        if (!CheckLineIntersect(c, m_Line))
                        {
                            //判断是否在范围内
                            if (m_ChainInfo.CheckInRange())
                            {
                                //线段完成
                                m_Line.AddCell(c);
                                m_LineOrigin = null;
                                //判断是否是终点
                                Stronghold s = c.GetStronghold();
                                if (s != null && !s.HasConnectStronghold())
                                {
                                    //链条成功
                                    if (s != m_ChainInfo.StartStronghold)
                                    {
                                        m_ChainInfo.EndStronghold = s;
                                        m_ChainInfo.Scueess();
                                        //复制
                                        m_AllChainInfo.Add(m_ChainInfo.Clone());
                                        m_ChainInfo.Clear();
                                        GameLevelSceneCtrl.Instance.Turn(false);
                                        EazySoundManager.PlaySound(ClickStronghold);
                                        OnValidLineCountChange(-1);
                                        if (OnGuideEnd != null)
                                        {
                                            OnGuideEnd();
                                        }
                                    }

                                }
                                else
                                {
                                    //下一条线的起点           
                                    //第一点
                                    m_LineOrigin = c;
                                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/Line", isCache: true, isClone: true);
                                    Line line = go.GetComponent<Line>();
                                    line.Parent = m_Line;
                                    m_Line = line;
                                    m_Line.AddCell(m_LineOrigin);
                                    m_ChainInfo.AddLine(m_Line);
                                    EazySoundManager.PlaySound(ClickCell);

                                }
                            }
                            else
                            {
                                OnInvalidPoint(true);
                            }


                        }
                        else
                        {
                            OnInvalidPoint(true);
                        }

                    }
                    else
                    {
                        OnInvalidPoint();
                    }
                }

                else
                {
                    OnInvalidPoint();

                }
            }
        }

        private void PointUp()
        {
            if (!m_ChainInfo.IsEmpty())
            {
                if (m_ChainInfo.EndStronghold == null)
                {
                    m_ChainInfo.Destory();
                }
            }
            OnValidLineCountChange(-1);
        }


        /// <summary>
        /// 判断相交
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        private bool CheckLineIntersect(Cell c, Line l)
        {
            //判断本条线段
            for (int i = 0; i < m_ChainInfo.LineList.Count; i++)
            {
                if (l == m_ChainInfo.LineList[i]) continue;
                if (m_ChainInfo.LineList[i].Cell1 == c || m_ChainInfo.LineList[i].Cell2 == c)
                {
                    return true;
                }
            }
            for (int i = 0; i < m_AllChainInfo.Count; i++)
            {
                for (int j = 0; j < m_AllChainInfo[i].LineList.Count; j++)
                {
                    if (m_AllChainInfo[i].LineList[j].Cell1 == c || m_AllChainInfo[i].LineList[j].Cell2 == c)
                    {
                        return true;
                    }
                }
            }
            if (c == GameLevelSceneCtrl.Instance.Monster.Cell)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取网格
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        private Cell GetCell(Vector3 mousePos)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.up, 0.05f);
            if (hit.collider != null)
            {
                Cell c = hit.collider.GetComponent<Cell>();
                return c;
            }
            return null;

        }
        /// <summary>
        /// 无效点操作
        /// </summary>
        private void OnInvalidPoint(bool isError = false)
        {
            if (m_Line == null) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.up, 0.05f);
            if (hit.collider != null)
            {
                m_Line.UpdateLine(hit.point);
                if (m_LastErrorObj != null && m_LastErrorObj != hit.collider.gameObject)
                {
                    if (isError)
                    {
                        EazySoundManager.PlaySound(ClickError);
                        OnValidLineCountChange(0);
                    }
                    else
                    {
                        if (m_ChainInfo.CheckIsLastOne(hit.collider.gameObject))
                        {
                            OnValidLineCountChange(m_ChainInfo.ValidLineCount);
                        }
                    }
                }
                m_LastErrorObj = hit.collider.gameObject;
            }

        }
    }
}
