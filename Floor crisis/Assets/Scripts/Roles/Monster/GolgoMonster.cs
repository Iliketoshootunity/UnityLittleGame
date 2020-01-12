using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 骷髅怪物
    /// </summary>
	public class GolgoMonster : Attacker, ITopWeakness, IBottomWeakness
    {

        /// <summary>
        /// 水平移动速度
        /// </summary>
        [SerializeField]
        private float m_HorizontalVelocity = 3;

        protected override void Move()
        {
            for (int i = 0; i < m_PhyCtroller.ForwardHitsStorage.Length; i++)
            {
                if (m_PhyCtroller.ForwardHitsStorage[i].distance > 0)
                {

                    Wall o = m_PhyCtroller.ForwardHitsStorage[i].collider.GetComponent<Wall>();
                    if (o != null)
                    {
                        m_HorizontalVelocity = -m_HorizontalVelocity;
                        break;
                    }
                }
            }
            m_Rigibody.velocity = new Vector2(m_HorizontalVelocity, 0);
            if (m_HorizontalVelocity < 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        protected override void Attack()
        {
            m_AttackTimer -= Time.deltaTime;
            for (int i = 0; i < m_PhyCtroller.ForwardHitsStorage.Length; i++)
            {
                if (m_PhyCtroller.ForwardHitsStorage[i].distance > 0)
                {
                    if (m_AttackTimer < 0)
                    {
                        RoleCtrl player = m_PhyCtroller.ForwardHitsStorage[i].collider.GetComponent<RoleCtrl>();
                        if (player != null)
                        {
                            player.ToHurt(m_AttackImpuse, true);
                            m_AttackTimer = m_AttackInterval;
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < m_PhyCtroller.BackwardHitsStorage.Length; i++)
            {

                if (m_PhyCtroller.BackwardHitsStorage[i].distance > 0)
                {
                    if (m_AttackTimer < 0)
                    {
                        RoleCtrl player = m_PhyCtroller.BackwardHitsStorage[i].collider.GetComponent<RoleCtrl>();
                        if (player != null)
                        {
                            player.ToHurt(m_AttackImpuse, true);
                            m_AttackTimer = m_AttackInterval;
                            return;
                        }

                    }

                }

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
