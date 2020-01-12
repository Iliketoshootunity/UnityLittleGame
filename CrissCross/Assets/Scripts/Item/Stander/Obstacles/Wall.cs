using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    /// <summary>
    /// 墙
    /// </summary>
    public class Wall : Obstacles
    {
        public override ObstaclesType ObstaclesType
        {
            get
            {
                return ObstaclesType.Wall;
            }
        }
    }
}
