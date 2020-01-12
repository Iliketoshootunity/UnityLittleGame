using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using System.Collections.Generic;
using EasyFrameWork.UI;
using UnityEngine.EventSystems;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class GameLevelSceneCtrl : GameSceneCtrlBase
    {
        public static GameLevelSceneCtrl Instance;

        public List<Sprite> Number;
        [SerializeField]
        private AudioClip m_StepObstaclesSound;
        /// <summary>
        /// 玩家
        /// </summary>
        private Stander m_Player;
        /// <summary>
        /// 钥匙
        /// </summary>
        private Stander m_Key;
        /// <summary>
        /// 传送门
        /// </summary>
        private Stander m_GateWay;

        private List<Stander> m_ObstaclesList;

        private List<Stander> m_PropList;
        /// <summary>
        /// 触发的数字障碍物
        /// </summary>
        private List<NumberObstacles> m_TriggerNumberObstaclesList;

        /// <summary>
        /// 数字总和
        /// </summary>
        [SerializeField]
        private int m_NumberTotal;

        /// <summary>
        /// 数字锁开启
        /// </summary>
        private bool m_NumberLockOpen;

        private Vector2 downPos;

        private bool m_IsClickUI;

        public bool IsPause;

        public int TestLevel;

        private void Awake()
        {
            Instance = this;
        }

        #region 初始化工作
        private void Start()
        {
            if (TestLevel > 0)
            {
                Map.Instance.Init(TestLevel);
            }
            else
            {
                Map.Instance.Init(Global.Instance.CurLevel);
            }

            UIGameLevelView view = UISceneCtrl.Instance.Load(UISceneType.GameLevel).GetComponent<UIGameLevelView>();
            view.SetUI();
            UIDispatcher.Instance.AddEventListen(ConstDefine.GameLevelSceneViewClickAudioBtn, OnGameLevelSceneViewClickAudioBtn);
            UIDispatcher.Instance.AddEventListen(ConstDefine.GameLevelSceneViewClickPauseBtn, OnGameLevelSceneViewClickPauseBtn);
            m_TriggerNumberObstaclesList = new List<NumberObstacles>();
            ((RoleCtrl)m_Player).OnArriveTarget = OnPlayerArriveTarget;
            ((RoleCtrl)m_Player).OnAttackTarget = OnPlayerAttackTarget;
            NumberManager();
            Global.Instance.SetMusic();
        }



        public void Init(Stander player, Stander key, Stander gateKey, List<Stander> obstaclesList, List<Stander> propList)
        {
            m_Player = player;
            m_Key = key;
            m_GateWay = gateKey;
            m_ObstaclesList = obstaclesList;
            m_PropList = propList;
        }

        /// <summary>
        /// 关于数字锁的初始化工作
        /// </summary>
        private void NumberManager()
        {
            //初始化数字障碍物
            List<Stander> ns = m_ObstaclesList.FindAll(x => ((Obstacles)x).ObstaclesType == ObstaclesType.Number);
            List<int> numbers = new List<int>();
            int totalNumberCount = 0;
            for (int i = 0; i < ns.Count; i++)
            {
                int number = UnityEngine.Random.Range(1, 7);
                ((NumberObstacles)ns[i]).Init(number);
                numbers.Add(number);
            }
            //计算总数字
            if (ns.Count >= 3)
            {
                while (totalNumberCount < 3)
                {
                    int index = UnityEngine.Random.Range(0, numbers.Count);
                    int value = UnityEngine.Random.Range(0, 100);
                    if (value >= 50)
                    {
                        m_NumberTotal += numbers[index];
                        totalNumberCount++;
                        numbers.RemoveAt(index);
                    }
                }
            }
            //初始化所有的数字锁
            List<Stander> nls = m_ObstaclesList.FindAll(x => ((Obstacles)x).ObstaclesType == ObstaclesType.NumberLock);
            for (int i = 0; i < nls.Count; i++)
            {
                ((NumberLockObstacles)nls[i]).Init(m_NumberTotal);
            }
        }
        #endregion

        protected override void BeforeOnDestory()
        {
            base.BeforeOnDestory();
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.GameLevelSceneViewClickAudioBtn, OnGameLevelSceneViewClickAudioBtn);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.GameLevelSceneViewClickPauseBtn, OnGameLevelSceneViewClickPauseBtn);
        }

        #region 角色事件

        private void OnPlayerAttackTarget(Prop prop)
        {
            if (prop.PropType == PropType.Key)
            {
                ((RoleCtrl)m_Player).HasKey = true;
                ((GateWay)m_GateWay).ToAwake();
            }
            else if (prop.PropType == PropType.RedRelieve)
            {
                //红色障碍物隐藏
                //狼色障碍物出现
                List<Stander> ros = m_ObstaclesList.FindAll(x => (((Obstacles)x).ObstaclesType == ObstaclesType.Revocable && ((RevocableObstacles)x).RevocableObstaclesType == RevocableObstaclesType.Red));
                for (int i = 0; i < ros.Count; i++)
                {
                    ((RevocableObstacles)(ros[i])).Hide();
                }
                List<Stander> bos = m_ObstaclesList.FindAll(x => ((Obstacles)x).ObstaclesType == ObstaclesType.Revocable && ((RevocableObstacles)x).RevocableObstaclesType == RevocableObstaclesType.Blue);
                for (int i = 0; i < bos.Count; i++)
                {
                    ((RevocableObstacles)(bos[i])).Show();
                }

            }
            else if (prop.PropType == PropType.BlueRelieve)
            {
                //红色障碍物出现
                //狼色障碍物隐藏
                List<Stander> ros = m_ObstaclesList.FindAll(x => (((Obstacles)x).ObstaclesType == ObstaclesType.Revocable && ((RevocableObstacles)x).RevocableObstaclesType == RevocableObstaclesType.Red));
                for (int i = 0; i < ros.Count; i++)
                {
                    ((RevocableObstacles)(ros[i])).Show();
                }
                List<Stander> bos = m_ObstaclesList.FindAll(x => ((Obstacles)x).ObstaclesType == ObstaclesType.Revocable && ((RevocableObstacles)x).RevocableObstaclesType == RevocableObstaclesType.Blue);
                for (int i = 0; i < bos.Count; i++)
                {
                    ((RevocableObstacles)(bos[i])).Hide();
                }
            }

        }

        private void OnPlayerArriveTarget(Cell preCell, Cell targetCell, Stander player)
        {
            player.RefreshCell(targetCell);
            preCell.RemoveRefreshStander(player);
            targetCell.AddRefreshStander(player);
            //是否有钥匙
            if (((RoleCtrl)player).HasKey)
            {
                for (int i = 0; i < targetCell.StanderList.Count; i++)
                {
                    if (targetCell.StanderList[i].StanderType == StanderType.GateWay)
                    {
                        OnWin();
                        return;
                    }
                }
            }
            //StepObstacles(随着玩家移动轮流隐藏出现的的障碍物) 改变状态
            List<Stander> sos = m_ObstaclesList.FindAll(x => ((Obstacles)x).ObstaclesType == ObstaclesType.Step);
            if (sos.Count > 0)
            {
                LFrameWork.Sound.EazySoundManager.PlaySound(m_StepObstaclesSound);
            }
            for (int i = 0; i < sos.Count; i++)
            {
                ((StepObstacles)sos[i]).Change();
            }
            if (!m_NumberLockOpen)
            {
                //判断前方是否是数字障碍物
                Cell c = ((RoleCtrl)m_Player).GetForwardCell();
                if (c != null && c.StanderList.Count > 0)
                {
                    for (int i = 0; i < c.StanderList.Count; i++)
                    {
                        if (c.StanderList[i].StanderType == StanderType.Obstacles && ((Obstacles)c.StanderList[i]).ObstaclesType == ObstaclesType.Number)
                        {
                            if (m_TriggerNumberObstaclesList.Contains((NumberObstacles)c.StanderList[i])) continue;
                            m_TriggerNumberObstaclesList.Add((NumberObstacles)c.StanderList[i]);

                            if (m_TriggerNumberObstaclesList.Count >= 3)
                            {
                                //触发成功
                                if (m_NumberTotal == GetNumberTotal())
                                {
                                    for (int j = 0; j < m_TriggerNumberObstaclesList.Count; j++)
                                    {
                                        m_TriggerNumberObstaclesList[j].TriggerSucess();
                                        m_NumberLockOpen = true;
                                        //找到所有的数字锁,让其消失
                                        List<Stander> noos = m_ObstaclesList.FindAll(x => ((Obstacles)x).ObstaclesType == ObstaclesType.NumberLock);
                                        for (int t = 0; t < noos.Count; t++)
                                        {
                                            ((NumberLockObstacles)noos[t]).Hide();
                                        }
                                    }
                                }
                                else//触发失败
                                {
                                    for (int j = 0; j < m_TriggerNumberObstaclesList.Count; j++)
                                    {
                                        m_TriggerNumberObstaclesList[j].TriggerFail();
                                    }
                                    m_TriggerNumberObstaclesList.Clear();
                                }
                            }
                            else
                            {
                                ((NumberObstacles)c.StanderList[i]).Trigger();
                            }
                        }
                    }
                }
            }


        }

        #endregion
        #region UI 事件
        private void OnGameLevelSceneViewClickPauseBtn(object[] p)
        {
            UIViewMgr.Instance.OpenView(UIViewType.Pause);
            Time.timeScale = 1;
        }
        private void OnGameLevelSceneViewClickAudioBtn(object[] p)
        {
            Global.Instance.IsPlaySound = !Global.Instance.IsPlaySound;
            Global.Instance.SetMusic();
        }
        #endregion

        public void OnWin()
        {
            StartCoroutine("OnWinIE");
        }

        private IEnumerator OnWinIE()
        {
            ((RoleCtrl)m_Player).ToWin();
            yield return new WaitForSeconds(0.5f);
            UIViewMgr.Instance.OpenView(UIViewType.Win);
        }

        private static bool IsPointerOverUIObject()
        {
            if (EventSystem.current == null)
                return false;
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }


        private void Update()
        {
            if (IsPause) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUIObject())
                {
                    m_IsClickUI = true;
                    return;
                }
                downPos = Input.mousePosition;

            }
            if (m_IsClickUI && ((RoleCtrl)m_Player).IsRigibody)
            {
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (IsPointerOverUIObject())
                {
                    m_IsClickUI = true;
                    return;
                }
                Vector2 relativePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - downPos;
                Direction dir = GetMoveDir(relativePos);
                Cell targetCell = GetCell(m_Player.Cell, dir);
                //如果目标为钥匙，拿取钥匙
                for (int i = 0; i < targetCell.StanderList.Count; i++)
                {
                    //目标站台有道具
                    if (targetCell.StanderList[i].StanderType == StanderType.Prop)
                    {
                        ((RoleCtrl)m_Player).ToAttack((Prop)targetCell.StanderList[i], dir);
                        return;
                    }
                }
                if (targetCell != m_Player.Cell)
                {
                    ((RoleCtrl)m_Player).ToMove(targetCell, dir);
                }
            }
        }

        private int GetNumberTotal()
        {
            int n = 0;
            for (int i = 0; i < m_TriggerNumberObstaclesList.Count; i++)
            {
                n += m_TriggerNumberObstaclesList[i].Number;
            }
            return n;
        }

        private Vector3 GetWorldPosOnInput(Vector3 scenePos)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(scenePos);
            return pos;
        }

        private Direction GetMoveDir(Vector3 relativePos)
        {
            //横轴移动
            if (Mathf.Abs(relativePos.x) > Mathf.Abs(relativePos.y))
            {
                if (relativePos.x > 0)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
            }
            else       //纵轴移动
            {
                if (relativePos.y > 0)
                {
                    return Direction.Up;
                }
                else
                {
                    return Direction.Down;
                }
            }

        }

        private Cell GetCell(Cell cell, Direction dir)
        {
            Cell nextCell = null;
            switch (dir)
            {
                case Direction.Up:
                    nextCell = cell.UpCell;
                    break;
                case Direction.Down:
                    nextCell = cell.DownCell;
                    break;
                case Direction.Left:
                    nextCell = cell.LeftCell;
                    break;
                case Direction.Right:
                    nextCell = cell.RightCell;
                    break;
            }
            //null
            if (nextCell == null)
            {
                return cell;
            }
            else
            {
                for (int i = 0; i < nextCell.StanderList.Count; i++)
                {
                    //下一个方块上道具则返回下一个方块
                    Stander s = nextCell.StanderList[i];
                    if (s.StanderType == StanderType.Prop)
                    {
                        return nextCell;
                    }

                    else if (s.StanderType == StanderType.Obstacles)
                    {
                        //方块上有障碍物
                        Obstacles obstacles = (Obstacles)s;
                        if (obstacles.IsShow)
                        {
                            return cell;
                        }
                    }

                }
                return GetCell(nextCell, dir);
            }

        }

    }
}
