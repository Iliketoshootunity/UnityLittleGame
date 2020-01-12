using UnityEngine;
using System.Collections;
using EasyFrameWork;
using DG.Tweening;

namespace EasyFrameWork
{
    public class GunnerMonster : Attacker, IObstacle, IBottomWeakness, ITopWeakness
    {
        private LineRenderer m_LineRender;
        [SerializeField]
        private Transform m_LineStartPos;
        [SerializeField]
        private float m_StartAngle;
        [SerializeField]
        private float m_Angle;
        [SerializeField]
        private float m_AngleSpeed;

        [SerializeField]
        private LayerMask m_LineMask;
        [SerializeField]
        private bool m_Attack;
        [SerializeField]
        private SpriteRenderer m_WarmingRender;

        private bool m_AttackRole;

        private Material m_LineRenderMaterial;

        private int m_Dir;
        private Vector3 m_HitPoint;
        private bool m_IsWarming;
        private Tween m_WarmingRenderTween;
        protected override void OnStart()
        {
            base.OnStart();

            m_LineRender = GetComponentInChildren<LineRenderer>();
            m_LineRenderMaterial = new Material(m_LineRender.material);
            m_LineRender.material = m_LineRenderMaterial;
            m_LineRender.SetPosition(0, m_LineStartPos.position);
            m_LineRender.SetPosition(1, m_LineStartPos.position);
            m_Attack = true;
            m_AttackRole = true;

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x < 0.5f)
            {
                m_Dir = 1;
                transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                m_Dir = -1;
                transform.localEulerAngles = new Vector3(0, -180, 0);
            }
            m_Angle = m_StartAngle * m_Dir;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        private void RayCastFloor()
        {
            if (!m_Attack)
            {
                m_LineRender.SetPosition(1, m_LineStartPos.position);
                return;
            }
            m_LineRenderMaterial.SetTextureOffset("_MainTex", new Vector2(m_Angle * 10, 0));
            Vector3 dir = Vector3.one;
            if (m_Dir == 1)
            {
                m_Angle = m_Angle + m_AngleSpeed * Time.deltaTime;
                Quaternion rot = Quaternion.AngleAxis(m_Angle, Vector3.forward);
                dir = rot * Vector3.down;
            }
            else
            {
                m_Angle = m_Angle - m_AngleSpeed * Time.deltaTime;
                Quaternion rot = Quaternion.AngleAxis(m_Angle, Vector3.forward);
                dir = rot * Vector3.down;
            }

            RaycastHit2D hit = PhyDebug.Raycast(m_LineStartPos.position, dir, 10, m_LineMask, Color.red);
            if (hit.distance > 0)
            {
                m_LineRender.SetPosition(1, hit.point);
                m_HitPoint = hit.point;
                if (m_AttackRole)
                {
                    RoleCtrl ctrl = hit.collider.GetComponent<RoleCtrl>();
                    if (ctrl != null)
                    {
                        if (Vector2.Dot(transform.position - ctrl.transform.position, ctrl.transform.right) > 0)
                        {
                            ctrl.ToHurt(5, chandDir: true);
                        }
                        else
                        {
                            ctrl.ToHurt(5);
                        }
                        m_AttackRole = false;
                    }
                }
            }
            if (m_Dir == 1)
            {
                if (m_Angle >= 90)
                {
                    m_Attack = false;
                    //StartCoroutine("EndAttack");
                }
            }
            else
            {
                if (m_Angle <= -90)
                {
                    m_Attack = false;
                    //StartCoroutine("EndAttack");
                }
            }


        }

        private IEnumerator EndAttack()
        {
            float timer = 0;
            bool isRun = true;
            Vector3 startPos = m_LineStartPos.position;
            while (isRun)
            {
                timer += Time.deltaTime;
                float process = timer / 0.05f;
                if (process >= 1)
                {
                    process = 1;
                    isRun = false;
                }
                m_LineRender.SetPosition(0, Vector3.Lerp(startPos, m_HitPoint, process));
                yield return null;
            }
            m_LineRender.SetPosition(0, m_LineStartPos.position);
            m_LineRender.SetPosition(1, m_LineStartPos.position);
        }
        protected override void Attack()
        {
            base.Attack();
            m_AttackTimer -= Time.deltaTime;
            if (m_AttackTimer < 0)
            {
                m_Angle = m_StartAngle * m_Dir;
                StartCoroutine(TweenWarmingRendeColor(true));
                m_Attack = true;
                m_AttackRole = true;
                m_AttackTimer = m_AttackInterval;
                m_IsWarming = false;
            }
            if (m_AttackTimer < 2)
            {
                if (m_IsWarming) return;
                StartCoroutine(TweenWarmingRendeColor(false));
                m_IsWarming = true;
            }
            RayCastFloor();
        }
        private IEnumerator TweenWarmingRendeColor(bool forward)
        {
            Color endColor = new Color(0, 0, 0, 0);
            if (forward)
            {
                endColor = new Color(1, 1, 1, 0);
            }
            else
            {
                endColor = new Color(1, 1, 1, 1);
            }
            Color startColor = m_WarmingRender.color;
            float timer1 = 0;
            bool isRun = true;
            while (isRun)
            {
                timer1 += Time.deltaTime;
                float process = timer1 / 0.1f;
                if (process >= 1)
                {
                    process = 1;
                    isRun = false;
                }
                m_WarmingRender.color = Color.Lerp(startColor, endColor, process);
                yield return null;
            }
        }
        public bool Break()
        {
            GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "Coin/Coin1", isCache: true);
            go.transform.position = transform.position + new Vector3(0, 0.25f, 0);
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            GameObject go1 = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Effect, "DeadEffect", isCache: true);
            go1.transform.position = transform.position + new Vector3(0, 0.4f, 0);
            go1.transform.rotation = Quaternion.identity;
            go1.transform.localScale = Vector3.one;

            Destroy(this.gameObject);
            return true;
        }

    }
}
