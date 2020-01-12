using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 陷阱
    /// </summary>
    public class TrapItem : Attacker, IBottomWeakness, IObstacle
    {
        public bool IsAboveAttack = true;
        protected override void OnStart()
        {
            base.OnStart();
            if (!IsAboveAttack)
            {
                m_PhyCtroller.IsShootAboveRay = true;
                m_PhyCtroller.IsShootForwardRay = true;
                m_PhyCtroller.IsShootBackwardRay = true;
                m_PhyCtroller.IsShootBelowRay = false;
            }
            else
            {
                m_PhyCtroller.IsShootAboveRay = true;
                m_PhyCtroller.IsShootForwardRay = false;
                m_PhyCtroller.IsShootBackwardRay = false;
                m_PhyCtroller.IsShootBelowRay = false;
            }

        }
        protected override void Attack()
        {
            m_AttackTimer -= Time.deltaTime;
            for (int i = 0; i < m_PhyCtroller.AboveHitsStorge.Length; i++)
            {
                if (m_PhyCtroller.AboveHitsStorge[i].distance > 0)
                {
                    if (m_AttackTimer < 0)
                    {
                        RoleCtrl player = m_PhyCtroller.AboveHitsStorge[i].collider.GetComponent<RoleCtrl>();
                        if (player != null)
                        {
                            player.ToHurt(m_AttackImpuse);
                            m_AttackTimer = m_AttackInterval;
                            return;
                        }
                    }
                }
            }
            if (!IsAboveAttack)
            {
                for (int i = 0; i < m_PhyCtroller.ForwardHitsStorage.Length; i++)
                {
                    if (m_PhyCtroller.ForwardHitsStorage[i].distance > 0)
                    {
                        if (m_AttackTimer < 0)
                        {
                            RoleCtrl player = m_PhyCtroller.ForwardHitsStorage[i].collider.GetComponent<RoleCtrl>();
                            if (player != null)
                            {
                                player.ToHurt(m_AttackImpuse);
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
                                player.ToHurt(m_AttackImpuse);
                                m_AttackTimer = m_AttackInterval;
                                return;
                            }
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
            Destroy(gameObject);
            return false;
        }




    }
}
