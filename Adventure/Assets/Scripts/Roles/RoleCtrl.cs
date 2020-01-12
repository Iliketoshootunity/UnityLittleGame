using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using LFramework.Plugins.Astart;
using System.Collections.Generic;
using UnityEngine.EventSystems;
namespace EasyFrameWork
{

    /// <summary>
    /// 角色操作状态
    /// </summary>
    public enum RoleOperationsStatus
    {
        None,
        NoOperations,
        DragTheRole,
        DragTheSkill,
        ShowIntro,
        HideTntro
    }

    public class RoleCtrl : MonoBehaviour, INodeOwner, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler

    {
        //-------------------操纵相关----------------------------------//
        public InputManager m_InputManager;
        private StateMachine<RoleOperationsStatus> m_RoleOperationsStatus;

        [HideInInspector]
        public RoleType CurRoleType;
        [HideInInspector]
        public RoleCtrl LockEnemy;//敌人专属

        public RoleStateMachine StateMachine;
        public RoleInfoBase CurRoleInfo;
        public IRoleAI CurRoleAI;
        public HeroPlatform CurHeroPlatform;
        public RoleHeadBar HeadBar;
        public Transform HeadBarPos;
        public int AttackRange;
        public int MoveStep;
        public bool MyRound;
        public bool IsRage;
        //-------------------事件----------------------------------//

        public Action OnRoundEnd;
        public Action OnRoleHurt;
        public Action<RoleCtrl> OnRoleDie;
        public Action<Transform> OnRoleDestory;
        public delegate void OnValueChange(ValueChangeType type);
        public OnValueChange OnHPChange;
        public OnValueChange OnMPChange;


        //-------------------寻路相关----------------------------------//
        public Node Node { get; set; }

        public List<Vector2> Path = new List<Vector2>();
        //------------------------战斗相关----------------------------------//
        /// <summary>
        /// 攻击逻辑
        /// </summary>
        public RoleAttack Attack;
        /// <summary>
        /// 受伤逻辑
        /// </summary>
        private RoleHurt m_Hurt;
        /// <summary>
        /// 当前的攻击信息
        /// </summary>
        public RoleAttackInfo CurAttackInfo;
        /// <summary>
        /// 是否僵值
        /// </summary>
        [HideInInspector]
        public bool IsRigibody;
        /// <summary>
        /// 攻击原点，根据原点和技能的形状 确定打击的范围
        /// </summary>
        [HideInInspector]
        public Vector2 AttackOrigin;

        private Vector3 m_PreMouscePos;
        private float m_PointDownTimer;
        private bool m_Enter;
        //private RoleHeadBar headBar;
        private bool m_IsInit = false;

        public RoleStateType Test;

        /// <summary>
        /// 角色控制器初始化
        /// </summary>
        /// <param name="roleType"></param>
        /// <param name="roleInfo"></param>
        /// <param name="ai"></param>
        public void Init(RoleType roleType, RoleInfoBase roleInfo, IRoleAI ai)
        {
            CurRoleType = roleType;
            CurRoleInfo = roleInfo;
            CurRoleAI = ai;
            m_IsInit = true;
            StateMachine = new RoleStateMachine(this.gameObject, true);
            RoleStateAbstract[] states = GetComponents<RoleStateAbstract>();
            for (int i = 0; i < states.Length; i++)
            {
                StateMachine.AddState(states[i]);
            }
            Attack = new RoleAttack(this);
            m_Hurt = new RoleHurt(StateMachine, this);
            m_Hurt.OnHurt = OnHurt;
            ToIdle();
            m_RoleOperationsStatus = new StateMachine<RoleOperationsStatus>(this.gameObject, true, CurRoleInfo.RoleId + ":" + "RoleOperationsStatus");
            StateMachineDispatcher.Instance.AddEventListen(CurRoleInfo.RoleId + ":" + "RoleOperationsStatus", OnRoleOperationsStatusCallBack);
            m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.NoOperations);
            //初始化血条
            InitRoleHeadBar();

        }

        /// <summary>
        /// 初始化角色的血条
        /// </summary>
        public void InitRoleHeadBar()
        {
            if (RoleHeadBarCtrl.Instance == null) return;
            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIOther, "RoleHeadBar", isCache: true);
            go.transform.parent = RoleHeadBarCtrl.Instance.transform;
            go.transform.localScale = Vector3.one;
            HeadBar = go.GetComponent<RoleHeadBar>();
            HeadBar.Init(CurRoleType == RoleType.Hero ? true : false, HeadBarPos.gameObject, CurRoleInfo.RoleNickName, hpSliderValue: CurRoleInfo.CurrentHP / (float)CurRoleInfo.MaxHP);
            if (HeadBar != null)
            {
                HeadBar.SetHpSlider(hpSliderValue: CurRoleInfo.CurrentHP / (float)CurRoleInfo.MaxHP);
            }
        }
        public void RefreshPlatfrom(HeroPlatform platform)
        {
            CurHeroPlatform = platform;
        }
        public void StandOnPlatfrom()
        {
            Vector3 pos = CurHeroPlatform.transform.TransformPoint(CurHeroPlatform.StandPos);
            transform.position = pos;
        }
        /// <summary>
        /// 刷新节点
        /// </summary>
        public void RefreshNode()
        {
            if (Node != null)
            {
                Node.IsObstacle = false;
            }
            Node = GridManager.Instance.GetNode(transform.position);
            Node.IsObstacle = true;
        }

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }
        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {

        }
        private void Update()
        {
            OnUpdate();
        }
        protected virtual void OnUpdate()
        {
            if (CurRoleAI != null)
            {
                CurRoleAI.DoAI();
            }
            if (StateMachine.CurrentRoleState != null)
            {
                StateMachine.CurrentRoleState.OnUpdate();
            }
            Test = StateMachine.CurrentRoleStateType;

            if (GameSceneCtrl.Instance.GetCurLevelProcedure() == LevelProcedure.HeroEditor)
            {
                //是否显示简介
                if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.NoOperations)
                {
                    if (m_Enter)
                    {
                        m_PointDownTimer += Time.deltaTime;
                        if (m_PointDownTimer > 1.5f)
                        {
                            m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.ShowIntro);
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            OnDestroyCallBack();
            StateMachineDispatcher.Instance.RemoveEventListen(CurRoleInfo.RoleId + ":" + "RoleOperationsStatus", OnRoleOperationsStatusCallBack);
            OnRoundEnd = null;
            OnRoleHurt = null;
            OnRoleDie = null;
            OnRoleDestory = null;
            OnHPChange = null;
            OnMPChange = null;
        }
        #region 操纵
        private void OnRoleOperationsStatusCallBack(object p)
        {
            StateMachineEvents<RoleOperationsStatus> events = (StateMachineEvents<RoleOperationsStatus>)p;
            switch (events.CurState)
            {
                case RoleOperationsStatus.NoOperations:
                    EnterNoOperations(events.PreState);
                    break;
                case RoleOperationsStatus.DragTheRole:
                    EnterDragTheRole();
                    break;
                case RoleOperationsStatus.DragTheSkill:
                    EnterDragTheSkill();
                    break;
                case RoleOperationsStatus.ShowIntro:
                    EnterShowIntro();
                    break;
                case RoleOperationsStatus.HideTntro:
                    EnterHideIntrol();
                    break;
            }
        }

        private void EnterNoOperations(RoleOperationsStatus preState)
        {
            m_PointDownTimer = 0;
            m_Enter = false;
            m_PreMouscePos = Vector3.zero;
            if (preState == RoleOperationsStatus.DragTheRole)
            {
                //移除英雄
                object[] objs = new object[2];
                objs[0] = this;
                objs[1] = Input.mousePosition;
                RoleDispatcher.Instance.Dispatc(ConstDefine.SceneHero_LevelFight, objs);
            }
            else if (preState == RoleOperationsStatus.DragTheSkill)
            {
                //释放技能
                object[] objs = new object[1];
                objs[0] = this;
                RoleDispatcher.Instance.Dispatc(ConstDefine.SceneHero_HeroAttack, objs);
            }
        }

        private void EnterDragTheRole()
        {
            object[] objs = new object[1];
            objs[0] = this.gameObject;
            RoleDispatcher.Instance.Dispatc(ConstDefine.SceneHero_BeginDragTheHeroItem, objs);
        }

        private void EnterDragTheSkill()
        {
            //发送技能模板
            object[] objs = new object[2];
            objs[0] = CurRoleInfo.RoleId;
            objs[1] = transform.position;

            RoleDispatcher.Instance.Dispatc(ConstDefine.SceneHero_BeginDragTheSkillItem, objs);
        }
        private void EnterShowIntro()
        {
            object[] objs = new object[1];
            objs[0] = CurRoleInfo.RoleId;
            RoleDispatcher.Instance.Dispatc(ConstDefine.Hero_ShowHeroIntro, objs);
        }
        private void EnterHideIntrol()
        {
            RoleDispatcher.Instance.Dispatc(ConstDefine.Hero_HideHeroIntro, null);
            m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.NoOperations);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //更新
            if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.DragTheRole)
            {
                object[] objs = new object[2];
                objs[0] = this.gameObject;
                objs[1] = Input.mousePosition;
                RoleDispatcher.Instance.Dispatc(ConstDefine.SceneHero_UpdateDragTheHeroItem, objs);
            }
            else if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.DragTheSkill)
            {
                //拖拽技能
                object[] objs = new object[1];
                objs[0] = Input.mousePosition;
                RoleDispatcher.Instance.Dispatc(ConstDefine.SceneHero_UpdateDragTheSkillItem, objs);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CurRoleType == RoleType.Hero)
            {
                if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.NoOperations)
                {
                    if (GameSceneCtrl.Instance.GetCurLevelProcedure() == LevelProcedure.HeroEditor)
                    {
                        //开始拖拽角色
                        m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.DragTheRole);
                    }
                    else if (GameSceneCtrl.Instance.GetCurLevelProcedure() == LevelProcedure.Player)
                    {
                        //开始拖拽技能
                        if (MyRound)
                        {
                            m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.DragTheSkill);
                        }

                    }
                }
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            m_Enter = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_Enter = false;
            if (CurRoleType == RoleType.Hero)
            {
                if (GameSceneCtrl.Instance.GetCurLevelProcedure() == LevelProcedure.HeroEditor)
                {
                    if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.DragTheRole)
                    {
                        m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.NoOperations);
                    }
                }
                else if (GameSceneCtrl.Instance.GetCurLevelProcedure() == LevelProcedure.Player)
                {
                    if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.DragTheSkill)
                    {
                        //释放技能
                        m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.NoOperations);
                    }
                }
            }
            if (GameSceneCtrl.Instance.GetCurLevelProcedure() == LevelProcedure.HeroEditor)
            {
                if (m_RoleOperationsStatus.CurState == RoleOperationsStatus.ShowIntro)
                {
                    //关闭简介面板
                    m_RoleOperationsStatus.ChangeState(RoleOperationsStatus.HideTntro);
                }
            }
        }



        public virtual void SetInputManager(InputManager inputManager)
        {
            m_InputManager = inputManager;
        }
        #endregion




        #region 状态切换
        /// <summary>
        /// 切穿到Idle
        /// </summary>
        public void ToIdle()
        {
            if (StateMachine.CurrentRoleStateType == RoleStateType.Die) return;
            if (CurRoleType == RoleType.Monster)
            {
                RefreshNode();
            }
            StateMachine.ChangeState(RoleStateType.Idle);
        }

        /// <summary>
        /// 移动到目标点
        /// </summary>
        /// <param name="targetPos"></param>
        public void MoveTo(ArrayList path)
        {
            if (StateMachine.CurrentRoleStateType == RoleStateType.Die) return;
            Path.Clear();
            int index = 0;
            foreach (var item in path)
            {
                index++;
                Path.Add(((Node)item).Position);
                if (index >= MoveStep)
                {
                    break;
                }
            }
            StateMachine.ChangeState(RoleStateType.Run);
        }

        public bool ToAttack(AttackType attackType, int skillId)
        {
            if (StateMachine == null) return false;
            if (StateMachine.CurrentRoleStateType == RoleStateType.Die) return false;
            bool isSucess = Attack.ToAttackBySkillId(attackType, skillId);
            if (!isSucess) return false;
            return true;
        }
        /// <summary>
        /// 切换到受伤画面
        /// </summary>
        public void ToHurt(RoleTransferAttackInfo attackInfo)
        {
            StartCoroutine(m_Hurt.ToHurt(attackInfo));
        }

        private void OnHurt()
        {
            if (HeadBar != null)
            {
                HeadBar.SetHpSlider(hpSliderValue: CurRoleInfo.CurrentHP / (float)CurRoleInfo.MaxHP);
            }
            if (OnRoleHurt != null)
            {
                OnRoleHurt();
            }
            if (OnHPChange != null)
            {
                OnHPChange(ValueChangeType.Reduce);
            }
        }

        /// <summary>
        /// 切换到死亡
        /// </summary>
        public void ToDie()
        {
            if (StateMachine.CurrentRoleStateType == RoleStateType.Die) return;
            StateMachine.ChangeState(RoleStateType.Die);
            if (OnRoleDie != null)
            {
                OnRoleDie(this);
            }


        }
        private void OnDieCallBack()
        {
            if (OnRoleDie != null)
            {
                OnRoleDie(this);
            }
        }
        private void OnDestroyCallBack()
        {
            if (OnRoleDestory != null)
            {
                OnRoleDestory(transform);
            }
            if (HeadBar != null)
            {
                Destroy(HeadBar.gameObject);
            }
            if (CurRoleType == RoleType.Monster)
            {
                Node.IsObstacle = false;
            }

        }
        #endregion

        #region 回合
        /// <summary>
        /// 回合开始,由阵营决定什么时候开始
        /// </summary>
        public void RoundStart()
        {
            MyRound = true;
        }
        /// <summary>
        /// 回合结束，有自身决定
        /// 被控制的状态，移动完成，攻击完成都将使回合完成
        /// </summary>
        public void RoundEnd()
        {
            MyRound = false;
            if (OnRoundEnd != null)
            {
                OnRoundEnd();
            }
        }
        #endregion

        /// <summary>
        /// 死亡动画
        /// </summary>
        public void DeadAni()
        {
            //暂定 直接删除
            Destroy(this.gameObject);
        }

    }
}
