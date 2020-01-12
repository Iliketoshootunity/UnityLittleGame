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
        /// ��һ������������
        /// </summary>
        [SerializeField]
        private float m_FirstImpulse;
        /// <summary>
        /// �ڶ�������������
        /// </summary>
        [SerializeField]
        private float m_SecondImpuse;
        /// <summary>
        /// ͷ��������
        /// </summary>
        [SerializeField]
        private float m_TopAttackImpuse;
        /// <summary>
        /// �ŲȺ�����
        /// </summary>
        [SerializeField]
        private float m_BottomAttackImpuse;
        /// <summary>
        /// ����
        /// </summary>
        [SerializeField]
        private float m_HurtImpuse;
        /// <summary>
        /// ˮƽ�ƶ��ٶ�
        /// </summary>
        public float HorizontalVelocity = 4.42f;

        /// <summary>
        /// ����ֵ
        /// </summary>
        private int m_Hp;
        /// <summary>
        /// ����״̬��
        /// </summary>
        [HideInInspector]
        public Animator Animator;
        /// <summary>
        /// ����
        /// </summary>
        [HideInInspector]
        public Rigidbody2D Rigidbody;
        /// <summary>
        /// ��ɫAI
        /// </summary>
        public IRoleAI CurRoleAI;
        /// <summary>
        /// ����ί��
        /// </summary>
        public Action<int> OnRoleHurt;
        /// <summary>
        /// ��Ѫί��
        /// </summary>
        public Action<int> OnRoleAddHp;
        /// <summary>
        /// ����ί��
        /// </summary>
        public Action<RoleCtrl> OnRoleDie;
        /// <summary>
        /// ��ɫ������һ������
        /// </summary>
        public Action OnRoleJumpToNextFloor;
        /// <summary>
        /// �Ƿ�ֵ
        /// </summary>
        //[HideInInspector]
        public bool IsRigibody;
        /// <summary>
        /// �Ƿ��ŵ�
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
        /// ��ɫ��������ʼ��
        /// </summary>
        /// <param name="roleType"></param>
        /// <param name="roleInfo"></param>
        /// <param name="ai"></param>
        public void Init(IRoleAI ai, int hp)
        {
            CurRoleAI = ai;
            m_Hp = hp;

        }
        #region Mono ����
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
        /// ����
        /// </summary>
        private void CheckSide()
        {
            for (int i = 0; i < m_PhyCtroller.ForwardHitsStorage.Length; i++)
            {
                if (m_PhyCtroller.ForwardHitsStorage[i].distance > 0)
                {
                    if (!IsGround)
                    {
                        //������������
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

                    //�����ϰ�����ķ���
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
        /// ��
        /// </summary>
        private void CheckBelow()
        {
            bool isGround = false;
            for (int i = 0; i < m_PhyCtroller.BelowHitsStorge.Length; i++)
            {
                if (m_PhyCtroller.BelowHitsStorge[i].distance > 0)
                {
                    //���õ���
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
                    //��������
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
        /// ��
        /// </summary>
        private void CheckAbove()
        {
            for (int i = 0; i < m_PhyCtroller.AboveHitsStorge.Length; i++)
            {
                if (m_PhyCtroller.AboveHitsStorge[i].distance > 0)
                {
                    //���治���ж�
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
        #region ״̬�л�
        /// <summary>
        ///�л�����Ծ
        /// </summary>
        public void ToJump()
        {
            if (IsRigibody)
            {
                return;
            }
            //һ����
            if (IsGround)
            {
                Jump(m_FirstImpulse);
            }
            //������
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
        /// ����״̬
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
        /// ����״̬
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

