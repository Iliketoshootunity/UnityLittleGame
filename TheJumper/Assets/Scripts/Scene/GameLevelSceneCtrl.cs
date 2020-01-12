using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;
using UnityEngine.EventSystems;

public enum GameStatus
{
    Start,
    Playing,
    Over,
    Pause
}

public class GameLevelSceneCtrl : GameSceneCtrlBase
{

    public static GameLevelSceneCtrl Instance;
    /// <summary>
    /// 游戏状态
    /// </summary>
    public GameStatus GameStatus;
    /// <summary>
    /// 第一次点击
    /// </summary>
    private bool m_FirstInput = false;
    /// <summary>
    /// 区域数量范围
    /// </summary>
    [SerializeField]
    private Vector2 m_RegionNumberRange;
    /// <summary>
    /// 生成区域数量
    /// </summary>
    private int m_ReginNumber;
    /// <summary>
    /// 生成的区域总数量
    /// </summary>
    private int m_ReginTotalNumber;
    /// <summary>
    /// 区域的方向 L左 R 右
    /// </summary>
    private bool m_ReginLeftDir;
    /// <summary>
    /// 最后一个平台
    /// </summary>
    private Platform m_LastPlatform;
    /// <summary>
    /// 一个平台
    /// </summary>
    private Platform m_FirsrtPlasform;
    /// <summary>
    /// 平台信息列表
    /// </summary>
    public List<PlatformInfo> PlatformInfoList = new List<PlatformInfo>();
    /// <summary>
    /// 当前平台信息
    /// </summary>
    private PlatformInfo m_CurPlatformInfo;
    /// <summary>
    /// 步数
    /// </summary>
    public int Step;
    /// <summary>
    /// 改变环境的步数
    /// </summary>
    private int m_ChangeEnvStep = 100;
    /// <summary>
    /// 平台停留时间
    /// </summary>
    private float m_StandTime;
    /// <summary>
    /// 最小平台时间
    /// </summary>
    private float m_MinStandTime;

    /// <summary>
    /// 难度等级
    /// </summary>
    private int m_Level;
    /// <summary>
    /// 最高难度等级
    /// </summary>
    private int m_MaxLevel;

    private UIGameLevelSceneView m_SceneUI;

    [SerializeField]
    private List<AudioClip> m_Musics;
    #region Mono流程

    protected override void OnAwake()
    {
        Instance = this;
    }

    protected override void OnStart()
    {
        //加载UI
        m_SceneUI = UISceneCtrl.Instance.Load(UISceneType.GameLevel).GetComponent<UIGameLevelSceneView>();
        m_SceneUI.SetUI(GlobalInit.Instance.IsPlaySound);
        m_SceneUI.OnClickAudioBtn = OnClickAudioBtn;
        m_SceneUI.OnClickPauseBtn = OnClickPauseBtn;
        m_SceneUI.OnClickInputBtn = OnClickInputBtn;
        //属性初始化
        m_FirstInput = true;
        m_ReginNumber = m_ReginTotalNumber - 1;
        GameStatus = GameStatus.Start;
        Step = 0;
        m_Level = 0;
        m_MaxLevel = 5;
        m_StandTime = 0.4f;
        m_MinStandTime = m_StandTime / 10 * 4;
        RandomPlatformInfo();
        //生成第一个平台
        GameObject go = Resources.Load<GameObject>("Platform/" + m_CurPlatformInfo.NormalPlatfromName);
        go = Instantiate(go);
        m_FirsrtPlasform = go.GetComponent<Platform>();
        m_FirsrtPlasform.Init(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0.1f, 0.5f, Platform.PlatformStatus.Stand, m_CurPlatformInfo, PlatfromType.Normal);
        m_LastPlatform = m_FirsrtPlasform;
        //随机生成5个平台
        for (int i = 0; i < 5; i++)
        {
            int temp = UnityEngine.Random.Range(0, 2);
            if (temp == 0)
            {
                m_LastPlatform = CreateLeftPlatform(m_CurPlatformInfo.NormalPlatfromName, m_LastPlatform, m_CurPlatformInfo, false);
            }
            else
            {
                m_LastPlatform = CreateRightPlatform(m_CurPlatformInfo.NormalPlatfromName, m_LastPlatform, m_CurPlatformInfo, false);
            }
        }
        //生成玩家
        go = Resources.Load<GameObject>("Role/Role");
        go = Instantiate(go);
        GlobalInit.Instance.CurRoleCtrl = go.GetComponent<RoleCtrl>();
        GlobalInit.Instance.CurRoleCtrl.Init(m_FirsrtPlasform);
        GlobalInit.Instance.CurRoleCtrl.OnDead = OnRoleDead;
        GlobalInit.Instance.CurRoleCtrl.OnJumpToNext = OnRoleJumpToNext;
        //模拟点击播放声音按钮
        OnClickAudioBtn(GlobalInit.Instance.IsPlaySound);
    }



    protected override void OnUpdate()
    {
        PlayerCtrl();
        CalReginNumber();
    }

    protected override void BeforeOnDestory()
    {

    }


    #endregion

    #region UI事件

    private void OnClickInputBtn()
    {
        //同一时刻值允许一个手指操作
        //if (Input.touchCount == 1)
        //{

        //}
        //Vector3 position = Input.touches[0].position;
        ////左
        //if (position.x < Screen.width / 2)
        //{
        //    if (m_FirstInput)
        //    {
        //        GameStart();
        //        m_FirstInput = false;
        //    }
        //    GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Left);
        //    CreatePlatform();
        //}
        //else
        //{
        //    //右
        //    if (m_FirstInput)
        //    {
        //        GameStart();
        //        m_FirstInput = false;
        //    }
        //    GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Right);
        //    CreatePlatform();
        //}
    }

    private void OnClickAudioBtn(bool isPlay)
    {

        if (isPlay)
        {
            EazySoundManager.GlobalSoundsVolume = 1;
            EazySoundManager.GlobalUISoundsVolume = 1;
            EazySoundManager.PlayMusic(m_Musics[0], 1, true, false, 1, 1);
        }
        else
        {
            EazySoundManager.GlobalSoundsVolume = 0;
            EazySoundManager.GlobalUISoundsVolume = 0;
            EazySoundManager.StopAll(1);
        }

        GlobalInit.Instance.IsPlaySound = isPlay;

    }

    private void OnClickPauseBtn()
    {
        if (GameStatus == GameStatus.Playing)
        {
            Time.timeScale = 0;
            UIViewMgr.Instance.OpenView(UIViewType.GameLevelPause);
            GameStatus = GameStatus.Pause;
        }
    }

    #endregion


    #region 角色事件

    public void OnRoleDead(RoleCtrl role)
    {
        StartCoroutine(OnRoleDeadIE(role));
    }

    private IEnumerator OnRoleDeadIE(RoleCtrl role)
    {
        EazySoundManager.StopAllMusic(0.3f);
        GameStatus = GameStatus.Over;
        if (GlobalInit.Instance.CurRoleCtrl.CurPlatform == null || GlobalInit.Instance.CurRoleCtrl.CurPlatform.PlatfromType == PlatfromType.Hide)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            if (GlobalInit.Instance.CurRoleCtrl.CurPlatform.PlatfromType == PlatfromType.HideTrap || GlobalInit.Instance.CurRoleCtrl.CurPlatform.PlatfromType == PlatfromType.OtherTrap)
            {
                //撞到障碍物,爆炸
                CameraCtrl.Instance.Shake();
                Destroy(GlobalInit.Instance.CurRoleCtrl.gameObject);
                yield return new WaitForSeconds(1);
            }
        }
        int maxScore = PlayerPrefs.GetInt("MaxScore");
        if (Step > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", Step);
        }
        UIViewMgr.Instance.OpenView(UIViewType.GameLevelFail);

    }
    private void OnRoleJumpToNext()
    {
        //分数+1
        Step++;
        if (Step <= 100)
        {
            m_ChangeEnvStep = 50;
        }
        else
        {
            m_ChangeEnvStep = 100;
        }
        m_SceneUI.SetScore(Step);
        //Debug.Log(Step);
        //每一百步难度上升，场景变化
        if (Step % m_ChangeEnvStep == 0)
        {
            //难度变化
            m_Level++;
            if (m_Level >= m_MaxLevel)
            {
                m_Level = m_MaxLevel;
            }
            m_StandTime -= m_StandTime / 10;
            if (m_StandTime <= m_MinStandTime)
            {
                m_StandTime = m_MinStandTime;
            }
            Debug.Log(m_StandTime);
            //场景变化
            //int index = PlatformInfoList.FindIndex(x => x == m_CurPlatformInfo);
            //index++;
            //if (index > PlatformInfoList.Count - 1)
            //{
            //    index = 0;
            //}
            //m_CurPlatformInfo = PlatformInfoList[index];
            RandomPlatformInfo();

        }
    }

    #endregion

    public void GameStart()
    {
        GameStatus = GameStatus.Playing;
        m_FirsrtPlasform.BeginFall = true;
        GameLevelCtrl.Instance.CloseHelpView();
    }

    private void CalReginNumber()
    {
        if (m_ReginNumber == m_ReginTotalNumber)
        {
            m_ReginTotalNumber = UnityEngine.Random.Range((int)m_RegionNumberRange.x, (int)m_RegionNumberRange.y);
            m_ReginLeftDir = !m_ReginLeftDir;
            m_ReginNumber = 0;
        }
    }

    private void RandomPlatformInfo()
    {
        bool isRun = true;
        if (PlatformInfoList == null || PlatformInfoList.Count == 0)
        {
            return;
        }
        if (PlatformInfoList.Count == 1)
        {
            m_CurPlatformInfo = PlatformInfoList[0];
        }
        while (isRun)
        {
            PlatformInfo info = PlatformInfoList[UnityEngine.Random.Range(0, PlatformInfoList.Count)];
            if (m_CurPlatformInfo != info)
            {
                m_CurPlatformInfo = info;
                isRun = false;
            }

        }

        if (m_FirstInput)
        {
            BGCtrl.Instance.ToNext(m_CurPlatformInfo.BGName, 0);
        }
        else
        {
            BGCtrl.Instance.ToNext(m_CurPlatformInfo.BGName);
        }

    }

    #region 玩家控制
    public void PlayerCtrl()
    {
        if (GlobalInit.Instance.CurRoleCtrl.Status == RoleCtrl.RoleStatus.Stand)
        {
            //跳一下 生成一层平台
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (m_FirstInput)
                {
                    GameStart();
                    m_FirstInput = false;
                }
                GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Left);
                CreatePlatform();

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (m_FirstInput)
                {
                    GameStart();
                    m_FirstInput = false;
                }
                GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Right);
                CreatePlatform();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                if (m_FirstInput)
                {
                    GameStart();
                    m_FirstInput = false;
                }

                if ((GlobalInit.Instance.CurRoleCtrl.CurPlatform.LNode) != null && ((Platform)(GlobalInit.Instance.CurRoleCtrl.CurPlatform.LNode)).PlatfromType == PlatfromType.Normal)
                {
                    GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Left);
                }
                else
                {
                    GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Right);
                }
                CreatePlatform();
            }
            //同一时刻值允许一个手指操作
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (IsPointerOverUIObject(Input.GetTouch(0).position))
                {
                    return;
                }
                else
                {
                    Vector3 position = Input.touches[0].position;
                    //左
                    if (position.x < Screen.width / 2)
                    {
                        if (m_FirstInput)
                        {
                            GameStart();
                            m_FirstInput = false;
                        }
                        GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Left);
                        CreatePlatform();
                    }
                    else
                    {
                        //右
                        if (m_FirstInput)
                        {
                            GameStart();
                            m_FirstInput = false;
                        }
                        GlobalInit.Instance.CurRoleCtrl.ToJump(JumpDir.Right);
                        CreatePlatform();
                    }
                }


            }

        }


    }

    //方法二 通过UI事件发射射线
    //是 2D UI 的位置，非 3D 位置
    public bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        //实例化点击事件
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //将点击位置的屏幕坐标赋值给点击事件
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        //向点击处发射射线
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
    #endregion
    #region 平台相关
    public void CreatePlatformTest()
    {
        m_ReginNumber++;
        //生成隐藏陷阱后续
        List<Node> hideList = FindHidePlatform(GlobalInit.Instance.CurRoleCtrl.CurPlatform);
        if (hideList != null)
        {
            for (int i = 0; i < hideList.Count; i++)
            {
                if (((Platform)hideList[i]).HideTrapChildCount < 2)
                {
                    Node node = hideList[i];
                    //5步之内查看玩家是否在范围内,是的话显示隐藏陷阱
                    for (int j = 0; j < 5; j++)
                    {
                        if (node == GlobalInit.Instance.CurRoleCtrl.CurPlatform)
                        {
                            ((Platform)hideList[i]).ShowHideTrap();
                            break;
                        }
                        node = node.PNode;
                    }
                    //生成下一个平台
                    node = null;
                    //左边
                    if (hideList[i].PNode.LNode == hideList[i])
                    {
                        node = FindLeftLastNode(hideList[i]);
                        if (node == null)
                        {
                            CreateLeftPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)hideList[i], ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                        else
                        {
                            CreateLeftPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)node, ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                    }
                    else//右边
                    {
                        node = FindRightLastNode(hideList[i]);
                        if (node == null)
                        {
                            CreateRightPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)hideList[i], ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                        else
                        {
                            CreateRightPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)node, ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                    }
                    ((Platform)hideList[i]).HideTrapChildCount++;
                }
            }
        }

        //转折处生成陷阱
        if (m_ReginNumber == 1)
        {
            //在改变环境处不生成陷阱
            if (Step % m_ChangeEnvStep != 0)
            {

                //概率在相反方向生成隐藏陷阱
                int temp = UnityEngine.Random.Range(0, 100);
                Platform t = null;
                string trapName = "";
                PlatfromType tempType = PlatfromType.HideTrap;
                if (temp < 15)
                {
                    //转折处距离最小为1,避免拥挤
                    if (m_ReginTotalNumber >= 2)
                    {
                        trapName = m_CurPlatformInfo.HideTrapPlatfromName;
                        tempType = PlatfromType.HideTrap;
                    }
                }
                else if (temp < 50 && temp >= 15)
                {
                    trapName = m_CurPlatformInfo.OtherTrapPlatfromNames[0];
                    tempType = PlatfromType.OtherTrap;
                }
                else if (temp < 85 && temp >= 50)
                {
                    trapName = m_CurPlatformInfo.OtherTrapPlatfromNames[1];
                    tempType = PlatfromType.OtherTrap;
                }

                if (!string.IsNullOrEmpty(trapName))
                {
                    if (m_ReginLeftDir)
                    {

                        t = CreateRightPlatform(trapName, m_LastPlatform, m_CurPlatformInfo,
                            platfromType: tempType);
                    }
                    else
                    {
                        t = CreateLeftPlatform(trapName, m_LastPlatform, m_CurPlatformInfo, platfromType: tempType);
                    }
                }

            }

        }
        else
        {
            //中间处概率 生成陷阱或者空的物体
            float n = (m_RegionNumberRange.y - m_RegionNumberRange.x) / 2 + m_RegionNumberRange.x;
            if (m_ReginNumber == n && m_ReginTotalNumber == m_RegionNumberRange.y - 1)
            {
                bool isTrap = UnityEngine.Random.Range(0, 2) == 0 ? true : false;
                string trapName = m_CurPlatformInfo.OtherTrapPlatfromNames[UnityEngine.Random.Range(0, m_CurPlatformInfo.OtherTrapPlatfromNames.Count)];
                PlatfromType tempType = PlatfromType.HideTrap;
                Platform t = null;
                if (isTrap)
                {
                    if (m_ReginLeftDir)
                    {

                        t = CreateRightPlatform(trapName, m_LastPlatform, m_CurPlatformInfo,
                            platfromType: PlatfromType.OtherTrap);
                    }
                    else
                    {
                        t = CreateLeftPlatform(trapName, m_LastPlatform, m_CurPlatformInfo, platfromType: PlatfromType.OtherTrap);
                    }
                }
                else
                {
                    if (m_ReginLeftDir)
                    {
                        //生成空的
                        t = CreateRightPlatform("Hide", m_LastPlatform, m_CurPlatformInfo, isNoramlShow: false,
                            platfromType: PlatfromType.Hide);
                        //生成
                        t = CreateRightPlatform(trapName, t, m_CurPlatformInfo,
                            platfromType: PlatfromType.OtherTrap);
                    }
                    else
                    {
                        //生成空的
                        t = CreateLeftPlatform("Hide", m_LastPlatform, m_CurPlatformInfo, isNoramlShow: false,

                        platfromType: PlatfromType.Hide);
                        //生成
                        t = CreateLeftPlatform(trapName, t, m_CurPlatformInfo,
                            platfromType: PlatfromType.OtherTrap);
                    }
                }
            }
        }
        //生成正常的平台
        if (m_ReginLeftDir)
        {
            m_LastPlatform = CreateLeftPlatform(m_CurPlatformInfo.NormalPlatfromName, m_LastPlatform, m_CurPlatformInfo);
        }
        else
        {
            m_LastPlatform = CreateRightPlatform(m_CurPlatformInfo.NormalPlatfromName, m_LastPlatform, m_CurPlatformInfo);
        }
    }
    public void CreatePlatform()
    {
        m_ReginNumber++;
        //隐藏的陷阱
        CreateHidePlatform();
        //转折处生成陷阱
        if (m_ReginNumber == 1)
        {
            CreateTurnTrapPlatform();

        }
        else
        {
            //中间的陷阱
            CreateMidTrapPlatform();
        }
        //普通平台
        CreateNormalPlatform();
    }

    /// <summary>
    /// 隐藏的陷阱
    /// </summary>
    private void CreateHidePlatform()
    {
        //生成隐藏陷阱后续
        List<Node> hideList = FindHidePlatform(GlobalInit.Instance.CurRoleCtrl.CurPlatform);
        if (hideList != null)
        {
            for (int i = 0; i < hideList.Count; i++)
            {
                if (((Platform)hideList[i]).HideTrapChildCount < 2)
                {
                    Node node = hideList[i];
                    //5步之内查看玩家是否在范围内,是的话显示隐藏陷阱
                    for (int j = 0; j < 5; j++)
                    {
                        if (node == GlobalInit.Instance.CurRoleCtrl.CurPlatform)
                        {
                            ((Platform)hideList[i]).ShowHideTrap();
                            break;
                        }
                        node = node.PNode;
                    }
                    //生成下一个平台
                    node = null;
                    //左边
                    if (hideList[i].PNode.LNode == hideList[i])
                    {
                        node = FindLeftLastNode(hideList[i]);
                        if (node == null)
                        {
                            CreateLeftPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)hideList[i], ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                        else
                        {
                            CreateLeftPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)node, ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                    }
                    else//右边
                    {
                        node = FindRightLastNode(hideList[i]);
                        if (node == null)
                        {
                            CreateRightPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)hideList[i], ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                        else
                        {
                            CreateRightPlatform(((Platform)hideList[i]).CurPlatformInfo.NormalPlatfromName, (Platform)node, ((Platform)hideList[i]).CurPlatformInfo, platfromType: PlatfromType.Normal);
                        }
                    }
                    ((Platform)hideList[i]).HideTrapChildCount++;
                }
            }
        }
    }

    /// <summary>
    /// 转折处陷阱
    /// </summary>
    private void CreateTurnTrapPlatform()
    {
        //在改变环境处不生成陷阱
        if (Step % m_ChangeEnvStep != 0)
        {

            //概率在相反方向生成隐藏陷阱
            int temp = UnityEngine.Random.Range(0, 100);
            Platform t = null;
            string trapName = "";
            PlatfromType tempType = PlatfromType.HideTrap;
            if (temp < 15)
            {
                //转折处距离最小为1,避免拥挤
                if (m_ReginTotalNumber >= 2)
                {
                    trapName = m_CurPlatformInfo.HideTrapPlatfromName;
                    tempType = PlatfromType.HideTrap;
                }
            }
            else if (temp < 50 && temp >= 15)
            {
                trapName = m_CurPlatformInfo.OtherTrapPlatfromNames[0];
                tempType = PlatfromType.OtherTrap;
            }
            else if (temp < 85 && temp >= 50)
            {
                trapName = m_CurPlatformInfo.OtherTrapPlatfromNames[1];
                tempType = PlatfromType.OtherTrap;
            }

            if (!string.IsNullOrEmpty(trapName))
            {
                if (m_ReginLeftDir)
                {

                    t = CreateRightPlatform(trapName, m_LastPlatform, m_CurPlatformInfo,
                        platfromType: tempType);
                }
                else
                {
                    t = CreateLeftPlatform(trapName, m_LastPlatform, m_CurPlatformInfo, platfromType: tempType);
                }
            }

        }
    }

    /// <summary>
    /// 中间处的陷阱
    /// </summary>
    private void CreateMidTrapPlatform()
    {
        //中间处概率 生成陷阱或者空的物体
        float n = (m_RegionNumberRange.y - m_RegionNumberRange.x) / 2 + m_RegionNumberRange.x;
        if (m_ReginNumber == n && m_ReginTotalNumber == m_RegionNumberRange.y - 1)
        {
            bool isTrap = UnityEngine.Random.Range(0, 2) == 0 ? true : false;
            string trapName = m_CurPlatformInfo.OtherTrapPlatfromNames[UnityEngine.Random.Range(0, m_CurPlatformInfo.OtherTrapPlatfromNames.Count)];
            PlatfromType tempType = PlatfromType.HideTrap;
            Platform t = null;
            if (isTrap)
            {
                if (m_ReginLeftDir)
                {

                    t = CreateRightPlatform(trapName, m_LastPlatform, m_CurPlatformInfo,
                        platfromType: PlatfromType.OtherTrap);
                }
                else
                {
                    t = CreateLeftPlatform(trapName, m_LastPlatform, m_CurPlatformInfo, platfromType: PlatfromType.OtherTrap);
                }
            }
            else
            {
                if (m_ReginLeftDir)
                {
                    //生成空的
                    t = CreateRightPlatform("Hide", m_LastPlatform, m_CurPlatformInfo, isNoramlShow: false,
                        platfromType: PlatfromType.Hide);
                    //生成
                    t = CreateRightPlatform(trapName, t, m_CurPlatformInfo,
                        platfromType: PlatfromType.OtherTrap);
                }
                else
                {
                    //生成空的
                    t = CreateLeftPlatform("Hide", m_LastPlatform, m_CurPlatformInfo, isNoramlShow: false,

                    platfromType: PlatfromType.Hide);
                    //生成
                    t = CreateLeftPlatform(trapName, t, m_CurPlatformInfo,
                        platfromType: PlatfromType.OtherTrap);
                }
            }
        }
    }

    /// <summary>
    /// 生成正常的平台
    /// </summary>
    private void CreateNormalPlatform()
    {
        //生成正常的平台
        if (m_ReginLeftDir)
        {
            m_LastPlatform = CreateLeftPlatform(m_CurPlatformInfo.NormalPlatfromName, m_LastPlatform, m_CurPlatformInfo);
        }
        else
        {
            m_LastPlatform = CreateRightPlatform(m_CurPlatformInfo.NormalPlatfromName, m_LastPlatform, m_CurPlatformInfo);
        }

    }

    public Platform CreateLeftPlatform(string name, Platform parnet, PlatformInfo platformInfo, bool isNoramlShow = true, PlatfromType platfromType = PlatfromType.Normal)
    {
        GameObject go = Resources.Load<GameObject>("Platform/" + name);
        go = Instantiate(go);
        go.name = go.name + Step.ToString();
        Platform platform = go.GetComponent<Platform>();
        parnet.LNode = platform;
        platform.PNode = parnet;
        Vector3 startPos = parnet.transform.TransformPoint(new Vector3(-0.65f, 3f, 0));
        Vector3 endPos = parnet.transform.TransformPoint(new Vector3(-0.65f, 0.65f, 0));
        Platform.PlatformStatus status = Platform.PlatformStatus.Show;
        if (!isNoramlShow)
        {
            status = Platform.PlatformStatus.Stand;
        }
        platform.Init(startPos, endPos, 0.1f, m_StandTime, status, platformInfo, platfromType); ;
        return platform;
    }
    public Platform CreateRightPlatform(string name, Platform parnet, PlatformInfo platformInfo, bool isNoramlShow = true, PlatfromType platfromType = PlatfromType.Normal)
    {
        GameObject go = Resources.Load<GameObject>("Platform/" + name);
        go = Instantiate(go);
        Platform platform = go.GetComponent<Platform>();
        parnet.RNode = platform;
        platform.PNode = parnet;
        Vector3 startPos = parnet.transform.TransformPoint(new Vector3(0.65f, 3f, 0));
        Vector3 endPos = parnet.transform.TransformPoint(new Vector3(0.65f, 0.65f, 0));
        Platform.PlatformStatus status = Platform.PlatformStatus.Show;
        if (!isNoramlShow)
        {
            status = Platform.PlatformStatus.Stand;
        }
        platform.Init(startPos, endPos, 0.1f, m_StandTime, status, platformInfo, platfromType);
        return platform;
    }



    private Node m_LastLeftNode;

    private Node FindLeftLastNode(Node root)
    {
        m_LastLeftNode = null;
        FindLeftLastNodeErgodic(root);
        return m_LastLeftNode;
    }

    private void FindLeftLastNodeErgodic(Node root)
    {
        if (root != null)
        {
            m_LastLeftNode = root;
            FindLeftLastNodeErgodic(root.LNode);
        }
    }

    private Node m_LastRightNode;

    private Node FindRightLastNode(Node root)
    {
        m_LastRightNode = null;
        FindRightLastNodeErgodic(root);
        return m_LastRightNode;
    }

    private void FindRightLastNodeErgodic(Node root)
    {
        if (root != null)
        {
            m_LastRightNode = root;
            FindRightLastNodeErgodic(root.RNode);
        }
    }

    private List<Node> FindHidePlatform(Node root)
    {
        if (m_FindHidePlatformResultList == null)
        {
            m_FindHidePlatformResultList = new List<Node>();
        }
        m_FindHidePlatformResultList.Clear();
        FindHidePlatformErgodic(root);
        return m_FindHidePlatformResultList;
    }

    private List<Node> m_FindHidePlatformResultList;

    private void FindHidePlatformErgodic(Node root)
    {
        if (root != null)
        {
            if (((Platform)root).PlatfromType == PlatfromType.HideTrap)
            {
                if (m_FindHidePlatformResultList == null)
                {
                    m_FindHidePlatformResultList = new List<Node>();
                }
                m_FindHidePlatformResultList.Add(root);
            }
            FindHidePlatformErgodic(root.LNode);
            FindHidePlatformErgodic(root.RNode);
        }
    }

    #endregion


}
