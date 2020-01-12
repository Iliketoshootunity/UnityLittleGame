using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using LFramework.Plugins.Astart;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public enum Dirction
    {
        Left,
        LeftUp,
        RightUp,
        Right,
        RightDown,
        LeftDown
    }

    public class RoleCtrl : MonoBehaviour
    {
        public AudioClip MoveClip;
        public AudioClip AtkClip;
        public SpriteRenderer BodyRenderer;
        public bool IsPlayer;
        public bool IsRigibody;
        public int Range;
        public float Speed;
        public float AttackRange = 0.35f;
        public GameObject Target;
        public Action<bool> OnWin;

        [SerializeField]
        private List<Hex> m_HexRange;
        private Hex m_StanderHex;
        private Animator m_Aniamtor;
        private Dirction m_Dirction;


        public void Init(int range, GameObject target)
        {
            Range = range;
            m_HexRange = new List<Hex>();
            Target = target;
        }
        private void Start()
        {
            RefreshRange(GridManager.Instance.GetNodeByPos(transform.position));
            ShowRange();
            m_Aniamtor = GetComponentInChildren<Animator>();
            ToStand();
        }
        public void ShowRange()
        {
            for (int i = 0; i < m_HexRange.Count; i++)
            {
                m_HexRange[i].Show();
            }

        }
        /// <summary>
        /// 刷新自己的范围
        /// </summary>
        public void RefreshRange(Node n)
        {
            for (int i = 0; i < m_HexRange.Count; i++)
            {
                m_HexRange[i].Level(IsPlayer);
            }
            m_HexRange.Clear();
            if (n != null)
            {
                ArrayList list = AStar.FindArea(n, Range);
                for (int i = 0; i < list.Count; i++)
                {
                    Node na = (Node)list[i];
                    Hex hex = GridManager.Instance.GetHex(na.Row, na.Column);
                    if (hex != null)
                    {
                        m_HexRange.Add(hex);
                    }
                }
            }
            for (int i = 0; i < m_HexRange.Count; i++)
            {
                m_HexRange[i].Enter(IsPlayer);
            }
        }
        public void HideRange()
        {
            for (int i = 0; i < m_HexRange.Count; i++)
            {
                m_HexRange[i].Wipe();
            }
        }

        public bool CheckTargetInRange(Node node)
        {
            Hex hex = GridManager.Instance.GetHex(node.Row, node.Column);
            if (m_HexRange.Contains(hex))
            {
                Node n = GridManager.Instance.GetNodeByPos(transform.position);
                if (hex.Row == n.Row && hex.Column == n.Column)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckFail()
        {
            if (m_HexRange.Count <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="path"></param>
        /// <param name="endCallBack"></param>
        public void ToMove(List<Node> path, Action endCallBack)
        {
            StartCoroutine(ToMoveIE(path, endCallBack));
        }
        private IEnumerator ToMoveIE(List<Node> path, Action endCallBack)
        {
            IsRigibody = true;
            for (int i = 0; i < path.Count; i++)
            {
                bool isRun = true;
                Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 endPos = GridManager.Instance.GetScalePos(path[i].Position, GridManager.Instance.YScale);
                float distance = Vector2.Distance(transform.position, endPos);
                float time = distance / Speed;
                float process = 0;
                float timer = 0;
                //六个方向
                Node selfNode = GridManager.Instance.GetNodeByPos(transform.position);
                if (selfNode == path[i]) continue;
                float row = path[i].Row - selfNode.Row;
                float colum = path[i].Column - selfNode.Column;
                m_Aniamtor.SetTrigger("ToRun");
                if (row == 0 && colum == -1)
                {
                    m_Aniamtor.SetInteger("RunType", 0);
                    m_Dirction = Dirction.Left;
                    //Debug.Log("左");
                }
                else if (row == 1 && colum == -1)
                {
                    m_Aniamtor.SetInteger("RunType", 1);
                    m_Dirction = Dirction.LeftUp;
                    //Debug.Log("左上");
                }
                else if (row == 1 && colum == 0)
                {
                    m_Aniamtor.SetInteger("RunType", 2);
                    m_Dirction = Dirction.RightUp;
                    //Debug.Log("右上");
                }
                else if (row == 0 && colum == 1)
                {
                    m_Aniamtor.SetInteger("RunType", 3);
                    m_Dirction = Dirction.Right;
                    //Debug.Log("右");
                }
                else if (row == -1 && colum == 1)
                {
                    m_Aniamtor.SetInteger("RunType", 4);
                    m_Dirction = Dirction.RightDown;
                    //Debug.Log("右下");
                }
                else if (row == -1 && colum == 0)
                {
                    m_Aniamtor.SetInteger("RunType", 5);
                    m_Dirction = Dirction.LeftDown;
                    //Debug.Log("左下");
                }

                while (isRun)
                {
                    timer += Time.deltaTime;
                    process = timer / time;
                    if (process >= 1)
                    {
                        process = 1;
                        isRun = false;
                        //EazySoundManager.PlaySound(MoveClip);
                    }
                    Vector2 pos = Vector2.Lerp(startPos, endPos, process);
                    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
                    if (Vector2.Distance(transform.position, Target.transform.position) < AttackRange)
                    {
                        if (!IsPlayer)
                        {
                            ToAttack();
                            yield return new WaitForSeconds(0.4f);
                            RoleCtrl ctrl = Target.GetComponent<RoleCtrl>();
                            ctrl.ToDie();
                            yield return new WaitForSeconds(0.25f);
                            ToStand();
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            ToWin();
                            transform.position = new Vector3(endPos.x, endPos.y, transform.position.z);
                            yield return new WaitForSeconds(0.5f);
                        }
                        if (OnWin != null)
                        {
                            OnWin(IsPlayer);
                        }

                        yield break;
                    }
                    yield return null;
                }
            }
            if (endCallBack != null)
            {
                endCallBack();
            }
            IsRigibody = false;
            ToStand();
        }

        public void ToAttack()
        {
            EazySoundManager.PlaySound(AtkClip);
            m_Aniamtor.SetTrigger("ToAtk");
            switch (m_Dirction)
            {
                case Dirction.Left:
                    m_Aniamtor.SetInteger("AtkType", 0);
                    break;
                case Dirction.LeftUp:
                    m_Aniamtor.SetInteger("AtkType", 1);
                    break;
                case Dirction.RightUp:
                    m_Aniamtor.SetInteger("AtkType", 2);
                    break;
                case Dirction.Right:
                    m_Aniamtor.SetInteger("AtkType", 3);
                    break;
                case Dirction.RightDown:
                    m_Aniamtor.SetInteger("AtkType", 4);
                    break;
                case Dirction.LeftDown:
                    m_Aniamtor.SetInteger("AtkType", 5);
                    break;
                default:
                    break;
            }
        }

        public void ToStand()
        {
            m_Aniamtor.SetTrigger("ToStand");
            switch (m_Dirction)
            {
                case Dirction.Left:
                    m_Aniamtor.SetInteger("StandType", 0);
                    break;
                case Dirction.LeftUp:
                    m_Aniamtor.SetInteger("StandType", 1);
                    break;
                case Dirction.RightUp:
                    m_Aniamtor.SetInteger("StandType", 2);
                    break;
                case Dirction.Right:
                    m_Aniamtor.SetInteger("StandType", 3);
                    break;
                case Dirction.RightDown:
                    m_Aniamtor.SetInteger("StandType", 4);
                    break;
                case Dirction.LeftDown:
                    m_Aniamtor.SetInteger("StandType", 5);
                    break;
                default:
                    break;
            }
        }
        public void ToDie()
        {
            m_Aniamtor.SetTrigger("ToDie");
            switch (m_Dirction)
            {
                case Dirction.Left:
                    m_Aniamtor.SetInteger("DieType", 0);
                    break;
                case Dirction.LeftUp:
                    m_Aniamtor.SetInteger("DieType", 1);
                    break;
                case Dirction.RightUp:
                    m_Aniamtor.SetInteger("DieType", 2);
                    break;
                case Dirction.Right:
                    m_Aniamtor.SetInteger("DieType", 3);
                    break;
                case Dirction.RightDown:
                    m_Aniamtor.SetInteger("DieType", 4);
                    break;
                case Dirction.LeftDown:
                    m_Aniamtor.SetInteger("DieType", 5);
                    break;
                default:
                    break;
            }
        }

        public void ToWin()
        {
            m_Aniamtor.SetTrigger("ToStand");
            m_Aniamtor.SetInteger("StandType", 6);
            transform.DoScale(Vector3.one * 0.3f, 0.5f).SetDelay(0.1f);
            BodyRenderer.DoColor(new Color(1, 1, 1, 0), 0.5f).SetDelay(0.1f);
        }

    }
}
