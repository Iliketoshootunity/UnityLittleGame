using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
	public class RoleMgr : Singleton<RoleMgr> {

        /// <summary>
        /// 加载角色
        /// </summary>
        /// <returns></returns>
        public GameObject LoadRole(RoleType roleType, string name)
        {
            GameObject go = null;
            string path = "";
            switch (roleType)
            {
                case RoleType.Hero:
                    path = "Hero";
                    break;
                case RoleType.Monster:
                    path = "Monster";
                    break;
            }
            return go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Role, string.Format("{0}/{1}", path, name)); ;

        }
    }
}
