using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public interface IHealth
    {
        void OnHit(GameObject attacker);
    }
}
