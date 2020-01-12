using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class Coin2 : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_GetCoinClip;
        [SerializeField]
        private Vector2 m_MoveRange;
        [SerializeField]
        private float m_FloatRange;
        [SerializeField]
        private AnimationCurve m_MoveCurve;
        [SerializeField]
        private AnimationCurve m_IdleCurve;
        [SerializeField]
        private float m_MoveTime;
        [SerializeField]
        private float m_IdleTime;

        private Vector2 m_TargetPos;
        private Vector2 m_StartPos;
        private bool m_IsMove;
        private bool m_TriggerRole;
        private float m_Timer;
        private Animator m_Ani;
        // Use this for initialization
        IEnumerator Start()
        {
            m_Ani = GetComponentInChildren<Animator>();
            m_Ani.Play("Loop", 0, Random.Range(0, 1f));
            m_TriggerRole = false;
            m_IsMove = true;
            m_StartPos = new Vector2(transform.position.x, transform.position.y);
            m_TargetPos = new Vector2(Random.Range(-m_MoveRange.x, m_MoveRange.x), Random.Range(m_MoveRange.y / 3, m_MoveRange.y)) + new Vector2(transform.position.x, transform.position.y);
            yield return new WaitForSeconds(m_MoveTime * 0.5f);
            m_TriggerRole = true;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (m_IsMove)
            {
                m_Timer += Time.deltaTime;
                float process = m_Timer / m_MoveTime;
                process = Mathf.Clamp01(process);
                if (process == 1)
                {
                    m_Timer = 0;
                    m_IsMove = false;
                }
                process = m_MoveCurve.Evaluate(process);
                Vector2 point = m_StartPos + (m_TargetPos - m_StartPos) * process;
                transform.position = new Vector3(point.x, point.y, transform.position.z);
            }
            else
            {
                m_Timer += Time.deltaTime;
                float process = m_Timer / m_IdleTime;
                process = Mathf.Clamp01(process);
                if (process == 1)
                {
                    m_Timer = 0;
                }
                float value = m_IdleCurve.Evaluate(process);
                transform.position = new Vector3(m_TargetPos.x, m_TargetPos.y, 0) + new Vector3(0, value * m_FloatRange, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Wall wall = collision.GetComponent<Wall>();
            if (wall != null)
            {
                //m_Timer = Random.Range(0, m_IdleTime);
                m_IsMove = false;
            }
            if (m_TriggerRole)
            {
                RoleCtrl ctrl = collision.GetComponent<RoleCtrl>();
                if (ctrl != null)
                {
                    if (DelegateDefine.Instance.OnGetCoin != null)
                    {
                        DelegateDefine.Instance.OnGetCoin();
                    }
                }
                EazySoundManager.PlaySound(m_GetCoinClip);
                Destroy(this.gameObject);
            }

        }
    }
}
