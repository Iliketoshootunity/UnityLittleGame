using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;

namespace EasyFrameWork
{
    //英雄的操作消息接收，攻击指令的发出
    public class HeroFaction : Faction
    {
        public Dictionary<RoleCtrl, bool> HeroToUsed;

        public override void RoundEnd()
        {
            base.RoundEnd();
        }

        public override void RoundStart()
        {
            HeroToUsed = new Dictionary<RoleCtrl, bool>();
            for (int i = 0; i < GameSceneCtrl.Instance.HeroList.Count; i++)
            {
                HeroToUsed.Add(GameSceneCtrl.Instance.HeroList[i], false);
                GameSceneCtrl.Instance.HeroList[i].RoundStart();

            }
        }



        public override void RoundUpdate()
        {

        }

        public void Used(RoleCtrl hero)
        {
            if (HeroToUsed.ContainsKey(hero))
            {
                if (HeroToUsed[hero])
                {
                    Debug.Log("错误：这个英雄已经使用过了");
                }
                else
                {
                    HeroToUsed[hero] = true;
                    bool isOver = true;
                    foreach (var item in HeroToUsed)
                    {
                        if (!item.Value)
                        {
                            isOver = false;
                        }
                    }
                    if (isOver)
                    {
                        //等1.5秒钟，等角色攻击完，伤害结算完
                        Invoke("RoundEnd", 1.5f * Time.timeScale);
                    }
                }
            }
            else
            {
                Debug.Log("错误：使用了已经移除了的英雄");
            }
        }
    }
}
