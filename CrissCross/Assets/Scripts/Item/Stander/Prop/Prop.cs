using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public enum PropType
    {
        /// <summary>
        /// 钥匙
        /// </summary>
        Key,
        /// <summary>
        /// 解除红色障碍物的道具
        /// </summary>
        RedRelieve,
        /// <summary>
        /// 解除蓝色障碍物的道具
        /// </summary>
        BlueRelieve
    }
    /// <summary>
    /// 道具
    /// </summary>
	public class Prop : Stander
    {

        /// <summary>
        /// 道具类型
        /// </summary>
        public PropType PropType;

        public override StanderType StanderType
        {
            get
            {
                return StanderType.Prop;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type"></param>
        public void Init(PropType type)
        {
            PropType = type;
        }
    }
}
