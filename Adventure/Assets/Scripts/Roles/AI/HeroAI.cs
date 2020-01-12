using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class HeroAI : IRoleAI
    {
        public RoleCtrl CurRoleCtrl { get; set; }
        public HeroInfo RoleInfo;

        public HeroAI(RoleCtrl roleCtrl, HeroInfo info)
        {
            CurRoleCtrl = roleCtrl;
            RoleInfo = info;
        }

        public void DoAI()
        {

        }
    }
}
