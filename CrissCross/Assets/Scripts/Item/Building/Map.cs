using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 地图生成器
    /// 关卡地图 1-墙壁 0-空地 2-玩家 3-传送门 4-钥匙 5-雕像（红色）6-树木（蓝色）（70-隐藏的冰块 71-显示的冰块） （81-灰色数字1 到 86-灰色数字6）
    /// </summary>
	public class Map : MonoSingleton<Map>
    {
        public GameObject Cell;
        /// <summary>
        /// 间隔
        /// </summary>
        public float RowInterval;
        public float ColumnInterval;
        private List<List<int>> m_MapData;
        private List<List<Cell>> m_MapCell;
        private Stander m_Player;
        private Stander m_Key;
        private Stander m_GateWay;
        private List<Stander> m_ObstaclesList;
        private List<Stander> m_PropList;
        public void Init(int level)
        {
            m_MapData = GameLevelDBModel.Instance.GetMapData(level);
            m_MapCell = new List<List<Cell>>();
            m_ObstaclesList = new List<Stander>();
            m_PropList = new List<Stander>();
            RowInterval = Global.Instance.RowInterval;
            ColumnInterval = Global.Instance.ColumnInterval;
            CreateMap();
            GameLevelSceneCtrl.Instance.Init(m_Player, m_Key, m_GateWay, m_ObstaclesList, m_PropList);

        }
        /// <summary>
        /// 创建地图
        /// </summary>
        private void CreateMap()
        {
            //地面
            //行
            for (int i = 0; i < m_MapData.Count; i++)
            {
                List<int> data = m_MapData[i];
                List<Cell> cellList = new List<Cell>();
                //列
                for (int j = 0; j < data.Count; j++)
                {
                    //Cell 构建方块
                    Vector2 pos = new Vector2(j * ColumnInterval + ColumnInterval / 2f, -(i * RowInterval + RowInterval / 2f));
                    GameObject go = null;
                    go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/Ground", isCache: true);
                    go.transform.position = pos;
                    go.transform.rotation = Quaternion.identity;
                    go.transform.localScale = new Vector3(1, 1, 1);
                    Cell cell = go.GetComponent<Cell>();
                    cell.Row = i;
                    cell.Column = j;
                    cellList.Add(cell);

                    //-------------生成站在方块上的物体--------------//

                    Stander stander = null;

                    //玩家
                    go = null;
                    if (data[j] == 2)
                    {
                        go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Role, "Player", isCache: true);
                        stander = go.GetComponent<Stander>();
                        m_Player = (RoleCtrl)stander;
                    }
                    //传送门
                    if (data[j] == 3)
                    {
                        go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Prop/GateWay", isCache: true);
                        m_GateWay = go.GetComponent<Stander>();
                    }

                    //障碍物
                    Obstacles obstacles = null;
                    RevocableObstaclesType revocableObstaclesType = RevocableObstaclesType.Red;
                    RevocableObstacles ro = null;
                    switch (data[j])
                    {
                        case 1: //墙
                            string walllName = "";
                            int r = Random.Range(0, 2);
                            if (r == 0)
                            {
                                walllName = "Wall01";
                            }
                            else
                            {
                                walllName = "Wall02";
                            }
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/" + walllName, isCache: true);
                            break;
                        case 5: //红色可被解除的障碍物
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacles/RedObstacles", isCache: true);
                            revocableObstaclesType = RevocableObstaclesType.Red;
                            break;
                        case 6: //蓝色可被解除的障碍物
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacles/BlueObstacles", isCache: true);
                            revocableObstaclesType = RevocableObstaclesType.Blue;
                            break;
                        case 11://忽隐忽现
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacles/FlickeringObstacles", isCache: true);
                            break;
                        case 70://随着玩家移动轮流隐藏出现的的障碍物
                        case 71:
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacles/StepObstacles", isCache: true);
                            StepObstacles so = go.GetComponent<StepObstacles>();
                            if (data[j] == 70)
                            {
                                so.Init(false);
                            }
                            else
                            {
                                so.Init(true);
                            }
                            break;
                        case 8://数字障碍物
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacles/NumberObstacles", isCache: true);
                            break;
                        case 12://数字锁障碍物
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Obstacles/NumberLockObstacles", isCache: true);
                            break;
                    }

                    //道具
                    Prop p = null;
                    PropType propType = PropType.Key;
                    switch (data[j])
                    {
                        case 4: //钥匙
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Prop/Key", isCache: true);
                            propType = PropType.Key;
                            m_Key = go.GetComponent<Stander>();
                            break;
                        case 9://红色道具
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Prop/RedRelieveProp", isCache: true);
                            propType = PropType.RedRelieve;
                            break;
                        case 10://蓝色道具
                            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Prop/BlueRelieveProp", isCache: true);
                            propType = PropType.BlueRelieve;
                            break;
                    }
                    //
                    if (go != null)
                    {
                        go.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -2);
                        go.transform.rotation = Quaternion.identity;
                        go.transform.localScale = new Vector3(1, 1, 1);

                        p = go.GetComponent<Prop>();
                        obstacles = go.GetComponent<Obstacles>();

                        stander = go.GetComponent<Stander>();

                        ro = go.GetComponent<RevocableObstacles>();
                    }

                    //障碍物
                    if (obstacles != null)
                    {
                        m_ObstaclesList.Add(obstacles);

                        if (ro != null)
                        {
                            ro.Init(revocableObstaclesType);
                        }
                    }
                    //道具
                    if (p != null)
                    {
                        m_PropList.Add(p);
                        p.Init(propType);
                    }
                    //stander
                    if (stander != null)
                    {
                        stander.RefreshCell(cell);
                        cell.AddRefreshStander(stander);
                    }


                }
                m_MapCell.Add(cellList);
            }
            //站立物
            //行
            for (int i = 0; i < m_MapCell.Count; i++)
            {
                List<Cell> cellList = m_MapCell[i];
                //列
                for (int j = 0; j < cellList.Count; j++)
                {
                    Cell upCell = null;
                    Cell downCell = null;
                    Cell rightCell = null;
                    Cell leftCell = null;

                    Cell cell = cellList[j];
                    int row = i;
                    int column = j;

                    int nextRow = -1;
                    int nextColumn = -1;
                    //上
                    nextRow = row - 1;
                    nextColumn = column;
                    if (CheckRangge(nextRow, nextColumn))
                    {
                        upCell = m_MapCell[nextRow][nextColumn];
                    }
                    //下
                    nextRow = row + 1;
                    nextColumn = column;
                    if (CheckRangge(nextRow, nextColumn))
                    {
                        downCell = m_MapCell[nextRow][nextColumn];
                    }

                    //左
                    nextRow = row;
                    nextColumn = column - 1;
                    if (CheckRangge(nextRow, nextColumn))
                    {
                        leftCell = m_MapCell[nextRow][nextColumn];
                    }

                    //右
                    nextRow = row;
                    nextColumn = column + 1;
                    if (CheckRangge(nextRow, nextColumn))
                    {
                        rightCell = m_MapCell[nextRow][nextColumn];
                    }

                    cell.Init(leftCell, rightCell, upCell, downCell);
                }
            }

            //填充背景
            //向上填充10行 向下填充10行
            for (int i = -5; i < m_MapCell.Count + 5; i++)
            {
                Vector2 p1 = new Vector2(ColumnInterval / 2f, -(i * RowInterval + RowInterval / 2f));
                GameObject g = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Building/FillBG", isCache: true);
                g.transform.position = p1;
                g.transform.rotation = Quaternion.identity;
                g.transform.localScale = new Vector3(1, 1, 1);
                if (i >= 0 && i < m_MapCell.Count)
                {
                    FillBG fillBg = g.GetComponent<FillBG>();
                    fillBg.ToBottom();
                }
            }
        }

        /// <summary>
        /// 检测边界
        /// </summary>
        private bool CheckRangge(int row, int column)
        {
            if (m_MapCell == null || m_MapCell.Count <= 0) return false;
            if (row >= 0 && row < m_MapCell.Count && column >= 0 && column < m_MapCell[0].Count)
            {
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            if (m_MapData == null) return;
            //for (int i = 0; i < m_MapData.Count; i++)
            //{
            //    List<int> data = m_MapData[i];
            //    for (int j = 0; j < data.Count; j++)
            //    {
            //        Gizmos.DrawSphere(new Vector2(j * RowInterval + RowInterval / 2f, -(i * RowInterval + RowInterval / 2f)), 0.1f);

            //        Gizmos.DrawWireCube(new Vector2(j * RowInterval + RowInterval / 2f, -(i * RowInterval + RowInterval / 2f)), Vector3.one * RowInterval);
            //    }
            //}
        }


    }
}
