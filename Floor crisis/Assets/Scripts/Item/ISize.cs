using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public interface ISize
    {
        Vector2 GetSize();

    }

    public class DefaultSize : ISize
    {
        public float CurX;

        public Vector2 GetSize()
        {
            return Vector2.one;
        }
    }
}
