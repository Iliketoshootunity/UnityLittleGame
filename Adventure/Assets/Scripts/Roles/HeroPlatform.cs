using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class HeroPlatform : MonoBehaviour
    {
        public long RoleId;

        public Vector3 StandPos;
        public void RefreshRoleId(long roleId)
        {
            RoleId = roleId;
        }

    }
}
