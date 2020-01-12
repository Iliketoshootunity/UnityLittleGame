using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    public class EnemyFaction : Faction
    {
        public List<RoleCtrl> MonsterList;
        public int m_Index;
        public override void RoundStart()
        {
            if (GameSceneCtrl.Instance.GetLevelProcedure.CurState == LevelProcedure.Enemy)
            {
                m_Index = 0;
                MonsterList = GameSceneCtrl.Instance.MonsterList;
                for (int i = 0; i < MonsterList.Count; i++)
                {
                    MonsterList[i].OnRoundEnd = OnRoundEndCallBack;
                }
                Next();
            }

        }

        private void OnRoundEndCallBack()
        {
            if (GameSceneCtrl.Instance.GetLevelProcedure.CurState == LevelProcedure.Enemy)
            {
                MonsterList[m_Index].OnRoundEnd = null;
                m_Index++;
                if (m_Index < MonsterList.Count)
                {
                    Next();
                }
                else
                {
                    RoundEnd();
                }
            }

        }

        private void Next()
        {
            MonsterList[m_Index].MyRound = true;
        }
        public override void RoundUpdate()
        {

        }
        public override void RoundEnd()
        {
            base.RoundEnd();
        }


    }
}
