using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public enum ObstaclesType
    {
        Wall,
        Revocable,
        Flickering,
        Step,
        Number,
        NumberLock
    }
    public abstract class Obstacles : Stander
    {
        public bool IsShow = true;
        public abstract ObstaclesType ObstaclesType
        {
            get;
        }

        public override StanderType StanderType
        {
            get
            {
                return StanderType.Obstacles;
            }
        }

    }
}
