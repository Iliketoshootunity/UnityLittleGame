using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    /// <summary>
    /// 掉落的金币
    /// </summary>
	public class Coin1 : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_GetCoinClip;
        [SerializeField]
        private float m_DestoryDelay;
        /// <summary>
        /// 重力加速度
        /// </summary>
        [SerializeField]
        private float m_Gravity = 9.8f;
        /// <summary>
        /// 上抛初速度
        /// </summary>
        [SerializeField]
        private float m_InitVeloctity;
        /// <summary>
        /// 位移时间
        /// </summary>
        private float m_Time;
        /// <summary>
        /// 速度
        /// </summary>
        private float m_Veloctity;

        private float m_Timer;
        private SpriteRenderer m_Renderer;
        // Use this for initialization
        IEnumerator Start()
        {
            if (DelegateDefine.Instance.OnGetCoin != null)
            {
                DelegateDefine.Instance.OnGetCoin();
            }
            EazySoundManager.PlaySound(m_GetCoinClip);
            m_Renderer = GetComponentInChildren<SpriteRenderer>();
            yield return new WaitForSeconds(m_DestoryDelay);
            Destroy(this.gameObject);
        }

        private void Update()
        {
            m_Timer += Time.deltaTime;
            float process = m_Timer / m_DestoryDelay;
            process = Mathf.Clamp01(process);
            m_Renderer.color = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, 1 - process);
        }
        private void FixedUpdate()
        {
            m_Time += Time.fixedDeltaTime;
            m_Veloctity = m_InitVeloctity - m_Gravity * m_Time;
            float displacement = m_Veloctity * Time.fixedDeltaTime;
            transform.position += new Vector3(0, displacement, 0);
        }
    }
}
