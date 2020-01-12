using UnityEngine;
using System.Collections;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class RoleCtrl : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_TopAttackFloorClip;
        [SerializeField]
        private AudioClip m_TopAttackOtherClip;
        [SerializeField]
        private AudioClip m_BottomAttackClip;
        [SerializeField]
        private AudioClip m_HurtClip;
        [SerializeField]
        private AudioClip m_DeadClip;
        /// <summary>
        /// 第一次起跳的推力
        /// </summary>
        [SerializeField]
        private float m_FirstImpulse;
        /// <summary>
        /// 第二次起跳的推力
        /// </summary>
        [SerializeField]
        private float m_SecondImpuse;
        /// <summary>
        /// 头顶后推力
        /// </summary>
        [SerializeField]
        private float m_TopAttackImpuse;
        /// <summary>
        /// 脚踩后推力
        /// </summary>
        [SerializeField]
        private float m_BottomAttackImpuse;
        /// <summary>
        /// 受伤
        /// </summary>
        [SerializeField]
        private float m_HurtImpuse;
        /// <summary>
        /// 水平移动速度
        /// </summary>
        public float HorizontalVelocity = 4.42f;

        /// <summary>
        /// 生命值
        /// </summary>
        private int m_Hp;
        /// <summary>
        /// 动画状态机
        /// </summary>
        [HideInInspector]
        public Animator Animator;
        /// <summary>
        /// 刚体
        /// </summary>
        [HideInInspector]
        public Rigidbody2D Rigidbody;
        /// <summary>
        /// 角色AI
        /// </summary>
        public IRoleAI CurRoleAI;
        /// <summary>
        /// 受伤委托
        /// </summary>
        public Action<int> OnRoleHurt;
        /// <summary>
        /// 加血委托
        /// </summary>
        public Action<int> OnRoleAddHp;
        /// <summary>
        /// 死亡委托
        /// </summary>
        public Action<RoleCtrl> OnRoleDie;
        /// <summary>
        /// 角色跳到下一个地面
        /// </summary>
        public Action OnRoleJumpToNextFloor;
        /// <summary>
        /// 是否僵值
        /// </summary>
        //[HideInInspector]
        public bool IsRigibody;
        /// <summary>
        /// 是否着地
        /// </summary>
        //[HideInInspector]
        public bool IsGround;

        private Floor m_CurFloor;

        public Floor CurFloor
        {
            get
            {
                return m_CurFloor;
            }
        }

        private PhyCtroller m_PhyCtroller;

        private BoxCollider2D m_Collider;



        private bool IsDead;
        /// <summary>
        /// 角色控制器初始化
        /// </summary>
        /// <param name="roleType"></param>
        /// <param name="roleInfo"></param>
        /// <param name="ai"></param>
        public void Init(IRoleAI ai, int hp)
        {
            CurRoleAI = ai;
            m_Hp = hp;

        }
        #region Mono 流程
        // Use this for initialization
        void Start()
        {
            Animator = GetComponentInChildren<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
            m_PhyCtroller = GetComponent<PhyCtroller>();
            m_Collider = GetComponent<BoxCollider2D>();
        }

        private bool m_IsPress;
        private void Update()
        {
            if (IsDead) return;
            if (Input.GetKeyDown(KeyCode.A))
            {
                ToJump();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                ToHurt(5);
            }
            if (Input.touchCount == 1)
            {
                if (!m_IsPress)
                {
                    ToJump();
                }
                m_IsPress = true;
            }
            if (Input.touchCount == 0)
            {
                m_IsPress = false;
            }

            m_PhyCtroller.OnUpdate();
            CheckSide();
            CheckAbove();
            CheckBelow();
            Rigidbody.velocity = new Vector2(HorizontalVelocity, Rigidbody.velocity.y);
            if (HorizontalVelocity < 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.5f);
            if (CurRoleAI == null) return;
            CurRoleAI.DoAI();
        }

        /// <summary>
        /// 左右
        /// </summary>
        private void CheckSide()
        {
            for (int i = 0; i < m_PhyCtroller.ForwardHitsStorage.Length; i++)
            {
                if (m_PhyCtroller.ForwardHitsStorage[i].distance > 0)
                {
                    if (!IsGround)
                    {
                        //空中遇到地面
                        Platform platform = m_PhyCtroller.ForwardHitsStorage[i].collider.GetComponent<Platform>();
                        if (platform != null)
                        {
                            HorizontalVelocity = -HorizontalVelocity;
                            Rigidbody.velocity = new Vector2(HorizontalVelocity, 0);
                            Rigidbody.AddForce(new Vector2(0, m_TopAttackImpuse), ForceMode2D.Impulse);
                            IsRigibody = true;
                            break;
                        }
                    }

                    //遇到障碍物更改方向
                    IObstacle obstacle = m_PhyCtroller.ForwardHitsStorage[i].collider.GetComponent<IObstacle>();
                    if (obstacle != null)
                    {
                        HorizontalVelocity = -HorizontalVelocity;
                        break;
                    }

                }
            }
        }

        /// <summary>
        /// 下
        /// </summary>
        private void CheckBelow()
        {
            bool isGround = false;
            for (int i = 0; i < m_PhyCtroller.BelowHitsStorge.Length; i++)
            {
                if (m_PhyCtroller.BelowHitsStorge[i].distance > 0)
                {
                    //设置地面
                    if (m_PhyCtroller.BelowHitsStorge[i].collider.transform.parent != null)
                    {
                        Floor m_floor = m_PhyCtroller.BelowHitsStorge[i].collider.transform.parent.GetComponent<Floor>();
                        if (m_floor != null)
                        {
                            if (transform.position.y - m_PhyCtroller.BelowHitsStorge[i].point.y > (m_PhyCtroller.GetHeight() / 2 - 0.2f))
                            {
                                if (m_floor != m_CurFloor)
                                {
                                    if (OnRoleJumpToNextFloor != null)
                                    {
                                        OnRoleJumpToNextFloor();
                                    }
                                }
                                if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("RoleWalk"))
                                {
                                    Animator.SetTrigger("IsWalk");
                                }
                                m_CurFloor = m_floor;
                                CameraCtrl.Instance.SetTarget(m_CurFloor.transform);
                                isGround = true;
                                IsRigibody = false;
                                m_floor.SetAllPlatformCollider(true);
                            }
                        }
                        else
                        {
                            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("RoleJump"))
                            {
                                Animator.SetTrigger("IsJump");
                            }
                        }
                    }
                    //击破弱点
                    ITopWeakness weakness = m_PhyCtroller.BelowHitsStorge[i].collider.GetComponent<ITopWeakness>();
                    if (weakness != null)
                    {
                        Jump(m_BottomAttackImpuse);
                        if (m_BottomAttackClip != null)
                        {
                            EazySoundManager.PlaySound(m_BottomAttackClip);
                        }
                        weakness.Break();
                        return;
                    }
                }
            }
            IsGround = isGround;
        }

        /// <summary>
        /// 上
        /// </summary>
        private void CheckAbove()
        {
            for (int i = 0; i < m_PhyCtroller.AboveHitsStorge.Length; i++)
            {
                if (m_PhyCtroller.AboveHitsStorge[i].distance > 0)
                {
                    //地面不做判断
                    if (IsGround) return;
                    IBottomWeakness weakness = m_PhyCtroller.AboveHitsStorge[i].collider.GetComponent<IBottomWeakness>();
                    if (weakness != null)
                    {
                        Rigidbody.velocity = new Vector2(HorizontalVelocity, 0);
                        Rigidbody.AddForce(new Vector2(0, m_TopAttackImpuse), ForceMode2D.Impulse);
                        IsRigibody = true;
                        weakness.Break();
                        Platform p = weakness as Platform;
                        if (p != null)
                        {
                            if (m_TopAttackFloorClip != null)
                            {
                                EazySoundManager.PlaySound(m_TopAttackFloorClip);
                            }
                        }
                        else
                        {
                            if (m_TopAttackOtherClip != null)
                            {
                                EazySoundManager.PlaySound(m_TopAttackOtherClip);
                            }
                        }

                        return;
                    }

                }
            }
        }

        #endregion
        #region 状态切换
        /// <summary>
        ///切换到跳跃
        /// </summary>
        public void ToJump()
        {
            if (IsRigibody)
            {
                return;
            }
            //一段跳
            if (IsGround)
            {
                Jump(m_FirstImpulse);
            }
            //二段跳
            else
            {
                Jump(m_SecondImpuse);
                IsRigibody = true;
            }

        }

        public void Jump(float impulse)
        {
            Animator.SetTrigger("IsJump");
            Rigidbody.velocity = new Vector2(HorizontalVelocity, 0);
            Rigidbody.AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);
        }
        /// <summary>
        /// 受伤状态
        /// </summary>
        public void ToHurt(float hurtImpuse, bool chandDir = false)
        {
            if (m_Hp <= 0) return;
            GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Effect, "HurtEffect", isCache: true);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            Destroy(go, 0.6f);
            if (chandDir)
            {
                HorizontalVelocity = -HorizontalVelocity;
            }
            Jump(hurtImpuse);
            m_Hp--;
            if (m_Hp <= 0)
            {
                m_Hp = 0;
                ToDead();
            }
            if (OnRoleHurt != null)
            {
                OnRoleHurt(m_Hp);
            }
            if (m_Hp == 0)
            {
                EazySoundManager.PlaySound(m_DeadClip);
            }
            else
            {
                EazySoundManager.PlaySound(m_HurtClip);
            }
        }

        /// <summary>
        /// 死亡状态
        /// </summary>
        public void ToDead()
        {
            Rigidbody.velocity = new Vector2(HorizontalVelocity / 3, 0);
            if (OnRoleDie != null)
            {
                OnRoleDie(this);
            }
            IsDead = true;
            m_Collider.isTrigger = true;
        }


        #endregion

        public void AddHp()
        {
            m_Hp++;
            if (m_Hp >= 3) m_Hp = 3;
            if (OnRoleAddHp != null)
            {
                OnRoleAddHp(m_Hp);
            }
        }



    }
}

