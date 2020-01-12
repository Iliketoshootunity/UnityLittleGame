using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class RoleCtrl : Stander
    {

        /// <summary>
        /// 到达目的地 当前格子 目标格子  自身
        /// </summary>
        public Action<Cell, Cell, Stander> OnArriveTarget;
        /// <summary>
        /// 攻击目标 Cell 目标格子 Stander 自身
        /// </summary>
        public Action<Prop> OnAttackTarget;
        [SerializeField]
        private float m_MoveSpeed;

        /// <summary>
        /// 抛曲线
        /// </summary>
        [SerializeField]
        private AnimationCurve m_CastCurve;
        /// <summary>
        /// 回拉曲线
        /// </summary>
        [SerializeField]
        private AnimationCurve m_ResilienceCurve;
        [SerializeField]
        private SpriteRenderer m_BodySpriteRenderer;
        [SerializeField]
        private SpriteRenderer m_ShadowSpriteRenderer;
        private float m_AttackTimer;
        [SerializeField]
        private float m_CastTime;
        [SerializeField]
        private float m_ResilienceTime;
        [SerializeField]
        private float m_TransferTime = 0.5f;
        private float m_MoveTime;
        private float m_MoveTimer;
        public bool IsRigibody;

        [SerializeField]
        private AudioClip m_GetKeySound;
        [SerializeField]
        private AudioClip m_GetPropSound;
        [SerializeField]
        private AudioClip m_GateWaySound;

        private AfterImageManager m_AfterImageManager;
        private Animator m_Animator;
        private Direction m_Dir;
        private LineRenderer m_AttackLine;
        private bool hasKey;
        private void Start()
        {
            m_AttackLine = GetComponentInChildren<LineRenderer>();
            m_AfterImageManager = GetComponent<AfterImageManager>();
            m_Animator = transform.Find("Body").GetComponent<Animator>();
            m_AttackLine.SetPosition(0, transform.position);
            m_AttackLine.SetPosition(1, transform.position);
            m_PrePos = transform.position;
        }
        public int AttackType;
        public bool HasKey
        {
            get
            {
                return hasKey;
            }
            set
            {
                hasKey = value;
            }
        }

        public override StanderType StanderType
        {
            get
            {
                return StanderType.Player;
            }
        }

        private float m_Speed;
        private float m_Time;
        private Vector3 m_PrePos;

        public void ToMove(Cell cell, Direction dir)
        {
            if (IsRigibody) return;
            m_Dir = dir;
            m_Animator.SetTrigger("TriggerRun");
            switch (dir)
            {
                case Direction.Up:
                    m_Animator.SetInteger("Run", 0);
                    break;
                case Direction.Down:
                    m_Animator.SetInteger("Run", 1);
                    break;
                case Direction.Left:
                    m_Animator.SetInteger("Run", 2);
                    break;
                case Direction.Right:
                    m_Animator.SetInteger("Run", 3);
                    break;
            }
            m_AfterImageManager.AfterImageActive = true;
            IsRigibody = true;
            m_MoveTimer = 0;
            //移动到目标格子
            float dis = Vector2.Distance(transform.position, cell.transform.position);
            m_MoveTime = dis / m_MoveSpeed;
            StartCoroutine(MoveIE(cell));
        }
        private IEnumerator MoveIE(Cell cell)
        {
            bool isRun = true;
            Vector3 startPos = transform.position;
            float process = 0;
            while (isRun)
            {
                if (process >= 1)
                {
                    isRun = false;
                    ToIdle();
                }
                m_MoveTimer += Time.deltaTime;
                process = m_MoveTimer / m_MoveTime;
                Vector3 pos = Vector3.Lerp(startPos, cell.transform.position, process);
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
                yield return null;
            }
            //移动完成，改变地图状态
            if (OnArriveTarget != null)
            {
                OnArriveTarget(Cell, cell, this);
            }
            IsRigibody = false;
        }
        /// <summary>
        /// 拿道具
        /// </summary>
        /// <param name="prop"></param>
        public void ToAttack(Prop prop, Direction dir)
        {
            if (IsRigibody) return;
            IsRigibody = true;
            m_Dir = dir;
            m_Animator.SetTrigger("TriggerAttack");
            switch (m_Dir)
            {
                case Direction.Up:
                    m_Animator.SetInteger("Attack", 0);
                    break;
                case Direction.Down:
                    m_Animator.SetInteger("Attack", 1);
                    break;
                case Direction.Left:
                    m_Animator.SetInteger("Attack", 2);
                    break;
                case Direction.Right:
                    m_Animator.SetInteger("Attack", 3);
                    break;
            }

            StartCoroutine(ToAttackIE(prop));
        }


        private IEnumerator ToAttackIE(Prop prop)
        {
            bool IsRun = true;
            m_AttackTimer = 0;
            Vector3 startPos = transform.position;
            Vector3 endPos = prop.transform.position;
            m_AttackLine.SetPosition(0, startPos);
            //抛
            while (IsRun)
            {
                m_AttackTimer += Time.deltaTime;
                float process = m_AttackTimer / m_CastTime;
                if (process >= 1)
                {
                    process = 1;
                    IsRun = false;
                }
                float value = m_CastCurve.Evaluate(process);
                Vector2 pos = Vector3.Lerp(startPos, endPos, value);

                m_AttackLine.SetPosition(1, pos);
                yield return null;
            }
            if (prop.PropType == PropType.Key)
            {
                EazySoundManager.PlaySound(m_GetKeySound);
            }
            else
            {
                EazySoundManager.PlaySound(m_GetPropSound);
            }
            bool IsRun2 = true;
            m_AttackTimer = 0;
            m_Animator.SetTrigger("Resilience");
            //拉
            while (IsRun2)
            {
                m_AttackTimer += Time.deltaTime;
                float p = m_AttackTimer / m_ResilienceTime;
                if (p >= 1)
                {
                    p = 1;
                    IsRun2 = false;
                }
                float value = m_ResilienceCurve.Evaluate(p);
                Vector2 pos = Vector3.Lerp(endPos, startPos, value);
                m_AttackLine.SetPosition(1, pos);
                prop.transform.position = pos;
                yield return null;
            }

            //攻击完成，改变地图状态
            if (OnAttackTarget != null)
            {
                OnAttackTarget(prop);
            }
            Destroy(prop.gameObject);
            IsRigibody = false;
            ToIdle();
        }

        public void ToIdle()
        {
            m_Animator.SetTrigger("TriggerIdle");
            switch (m_Dir)
            {
                case Direction.Up:
                    m_Animator.SetInteger("Idle", 0);
                    break;
                case Direction.Down:
                    m_Animator.SetInteger("Idle", 1);
                    break;
                case Direction.Left:
                    m_Animator.SetInteger("Idle", 2);
                    break;
                case Direction.Right:
                    m_Animator.SetInteger("Idle", 3);
                    break;
            }
            m_AfterImageManager.AfterImageActive = false;
        }


        public void ToWin()
        {
            IsRigibody = true;
            StartCoroutine("ToWinIE");
            EazySoundManager.PlaySound(m_GateWaySound);
        }
        private void CreateTransferEffect()
        {
            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, "RoleTransferEffect", isCache: true);
            go.transform.position = transform.position;
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            Destroy(go, 3);
        }
        private IEnumerator ToWinIE()
        {
            Color startColor = new Color(1, 1, 1, 1);
            Color endColor = new Color(1, 1, 1, 0);
            bool isRun = true;
            float timer = 0;
            //消失
            CreateTransferEffect();
            while (isRun)
            {
                timer += Time.deltaTime;
                float process = timer / m_TransferTime;
                if (process >= 1)
                {
                    process = 1;
                    isRun = false;
                }

                m_BodySpriteRenderer.color = Color.Lerp(startColor, endColor, process);
                m_ShadowSpriteRenderer.color = Color.Lerp(startColor, endColor, process);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        /// <summary>
        /// 获取前方的平台
        /// </summary>
        /// <returns></returns>
        public Cell GetForwardCell()
        {
            if (Cell == null)
            {
                Debug.LogError("Error");
                return null;
            }
            Cell c = null;
            switch (m_Dir)
            {
                case Direction.Up:
                    c = Cell.UpCell;
                    break;
                case Direction.Down:
                    c = Cell.DownCell;
                    break;
                case Direction.Left:
                    c = Cell.LeftCell;
                    break;
                case Direction.Right:
                    c = Cell.RightCell;
                    break;
            }
            return c;
        }
        private void OnDrawGizmos()
        {
            if (Cell != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(Cell.transform.position, 0.1f);
            }
        }
    }
}
