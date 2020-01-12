using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public class RoleAttack
    {
        private RoleCtrl m_Role;
        private List<RoleCtrl> m_EnemyList = new List<RoleCtrl>();
        private List<Vector2> m_AttackPosList = new List<Vector2>();

        public RoleAttack(RoleCtrl roleCtrl)
        {
            m_Role = roleCtrl;
        }
        public bool ToAttackBySkillId(AttackType attackType, int skillId)
        {
            if (m_Role.StateMachine.CurrentRoleStateType == RoleStateType.Die) return false;
            if (m_EnemyList == null)
            {
                m_EnemyList = new List<RoleCtrl>();
            }
            m_Role.StateMachine.IsNormalAtk = false;
            m_EnemyList.Clear();
            m_AttackPosList.Clear();
            if (m_Role.CurRoleType == RoleType.Hero || m_Role.CurRoleType == RoleType.Monster)
            {
                SkillLevelEntity sklillLevleEntity = null;
                SkillEntity skillEntity = null;
                if (attackType == AttackType.PhyAttack)
                {
                    m_Role.StateMachine.IsNormalAtk = true;
                }
                else
                {
                    skillEntity = SkillDBModel.Instance.Get(skillId);
                    m_Role.StateMachine.SkillIndex = 0;
                    if (skillEntity == null) return false;
                    if (m_Role.LockEnemy != null && m_Role.CurRoleType == RoleType.Monster)
                    {
                        //这里重新做，暂时只打锁定目标
                        //TODO
                        sklillLevleEntity = SkillLevelDBModel.Instance.GetSkillLevelBySkillIdAndLevel(skillEntity.Id, 1);
                        m_EnemyList.Add(m_Role.LockEnemy);
                    }
                    else if (m_Role.CurRoleType == RoleType.Hero)
                    {
                        int skillLevel = ((HeroInfo)m_Role.CurRoleInfo).GetSkillLevel(skillEntity.Id);
                        sklillLevleEntity = SkillLevelDBModel.Instance.GetSkillLevelBySkillIdAndLevel(skillEntity.Id, skillLevel);
                        SearchEnemys(m_Role.AttackOrigin, skillEntity.AttackArea, skillEntity.AttackRange);
                    }
                }

                m_Role.StateMachine.ChangeState(RoleStateType.Attack);
                for (int i = 0; i < m_EnemyList.Count; i++)
                {
                    RoleTransferAttackInfo info = CalculationHurtValue(attackType, m_EnemyList[i], skillEntity, sklillLevleEntity);
                    m_EnemyList[i].ToHurt(info);
                }
                //生成攻击特效
                if (!string.IsNullOrEmpty(skillEntity.AttackEffect))
                {
                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, "RoleEffect/" + skillEntity.AttackEffect, isCache: true);
                    go.transform.SetParent(m_Role.transform);
                    go.transform.localPosition = Vector3.zero;
                    AttackEffectBase effect = go.GetComponent<AttackEffectBase>();
                    effect.Init(m_Role, "RoleEffect/" + skillEntity.AttackEffect, m_EnemyList, m_AttackPosList);
                }
            }
            return false;
        }
        private RoleTransferAttackInfo CalculationHurtValue(AttackType attackType, RoleCtrl enemy, SkillEntity skillEntity, SkillLevelEntity skillLevelEntity)
        {
            RoleTransferAttackInfo attackInfo = new RoleTransferAttackInfo();
            attackInfo.AttackRoleID = m_Role.CurRoleInfo.RoleId;
            attackInfo.BeAttaclRoleID = enemy.CurRoleInfo.RoleId;
            attackInfo.IsCri = false;
            float damage = 0;
            if (attackType == AttackType.PhyAttack)
            {
                damage = m_Role.CurRoleInfo.PhyAtk - enemy.CurRoleInfo.PhyDef;
            }
            else
            {
                attackInfo.SkillId = skillEntity.Id;
                attackInfo.SkillLevel = skillLevelEntity.Level;
                //物理系攻击
                if (skillEntity.SkillType == 0)
                {
                    damage = m_Role.CurRoleInfo.PhyAtk * skillLevelEntity.HurtValueRate - enemy.CurRoleInfo.PhyDef;
                }
                else
                {
                    //魔法系攻击
                    damage = m_Role.CurRoleInfo.MgicAtk * skillLevelEntity.HurtValueRate - enemy.CurRoleInfo.MgicDef;
                }
                damage /= 10;
                bool isCri = Random.Range(1, 101) < m_Role.CurRoleInfo.Cri;
                attackInfo.IsCri = isCri;
                if (isCri)
                {
                    damage *= m_Role.CurRoleInfo.CriValue;
                }
                if (damage <= 0)
                {
                    damage = 1;
                }
            }
            attackInfo.HurtValue = (int)damage;
            return attackInfo;

        }

        private List<RoleCtrl> SearchHeros(int number)
        {
            //先找前排
            return m_EnemyList;
        }

        //origin 是根据Node的position值
        public List<RoleCtrl> SearchEnemys(Vector2 origin, int skillType, int range)
        {
            m_EnemyList.Clear();
            m_AttackPosList.Clear();
            m_AttackPosList = GameSceneCtrl.Instance.GetPositionsByShape(skillType, origin, range);
            for (int i = 0; i < m_AttackPosList.Count; i++)
            {
                RoleCtrl role = RayCastRoleCtr(m_AttackPosList[i]);
                if (role != null)
                {
                    m_EnemyList.Add(role);
                }
            }
            return m_EnemyList;
        }

        private RoleCtrl RayCastRoleCtr(Vector2 pos)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.05f);
            if (hit.collider != null)
            {
                RoleCtrl ctrl = hit.collider.GetComponent<RoleCtrl>();
                if (ctrl != null)
                {
                    return ctrl;
                }
            }
            return null;
        }
    }
}
