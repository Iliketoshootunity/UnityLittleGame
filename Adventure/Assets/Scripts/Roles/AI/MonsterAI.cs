using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFramework.Plugins.Astart;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class MonsterAI : IRoleAI
    {
        public RoleCtrl CurRoleCtrl { get; set; }
        public MonsterInfo RoleInfo;

        public MonsterAI(RoleCtrl roleCtrl, MonsterInfo info)
        {
            CurRoleCtrl = roleCtrl;
            RoleInfo = info;
        }
        public void DoAI()
        {
            if (CurRoleCtrl.MyRound)
            {
                if (CurRoleCtrl.StateMachine.CurrentRoleStateType == RoleStateType.Idle)
                {
                    ArrayList list = SearceMinDisPathFromMeToPlayer();
                    if (list.Count <= CurRoleCtrl.AttackRange)
                    {
                        //随机选取一个英雄当做目标
                        CurRoleCtrl.LockEnemy = GameSceneCtrl.Instance.HeroList[Random.Range(0, GameSceneCtrl.Instance.HeroList.Count)];
                        List<int> lst = RoleInfo.GetSkillList();
                        if (list.Count == 0)
                        {
                            CurRoleCtrl.ToAttack(AttackType.PhyAttack, 0);
                        }
                        else
                        {
                            CurRoleCtrl.ToAttack(AttackType.SkillAttack, lst[Random.Range(0, lst.Count)]);
                        }

                    }
                    else
                    {
                        ArrayList path = SearceMinDisPathFromMeToPlayer();
                        CurRoleCtrl.MoveTo(path);
                    }
                }
                else if (CurRoleCtrl.StateMachine.CurrentRoleStateType == RoleStateType.BeCtrl)
                {
                    CurRoleCtrl.RoundEnd();
                }
                else
                {
                    //Debug.LogError("Error:行为错误");
                }
            }
        }

        public ArrayList SearceMinDisPathFromMeToPlayer()
        {
            //第一列 为玩家
            ArrayList path = null;
            int step = int.MaxValue;
            for (int i = 0; i < GridManager.Instance.NumOfRow; i++)
            {
                ArrayList tempPath = AStar.FindPath(CurRoleCtrl.Node, GridManager.Instance.GetNode(i, 0));
                if (tempPath != null)
                {
                    if (tempPath.Count < step)
                    {
                        step = tempPath.Count;
                        path = tempPath;
                    }
                }
            }
            return path;
        }
    }
}
