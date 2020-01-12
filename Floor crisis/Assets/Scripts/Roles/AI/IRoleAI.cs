using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public interface IRoleAI
    {

        RoleCtrl CurRoleCtrl { get; set; }

        void DoAI();
    }

}

