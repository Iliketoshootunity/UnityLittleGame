using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    public abstract class RoleStateAbstract
    {
        protected RoleFSMMgr CurRoleFSMMgr { get; private set; }
        protected AnimatorStateInfo CurAniamatorStateInfo;
        public RoleStateAbstract(RoleFSMMgr mgr)
        {
            CurRoleFSMMgr = mgr;
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public abstract void OnEnter();
        /// <summary>
        /// 更新状态
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// 离开状态
        /// </summary>
        public abstract void OnLevel();

        /// <summary>
        /// 判断当前动画已经播放了一次
        /// </summary>
        /// <returns></returns>
        public bool CheckPlayOneOver()
        {
            if (CurAniamatorStateInfo.normalizedTime > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

