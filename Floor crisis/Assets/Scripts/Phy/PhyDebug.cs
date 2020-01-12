using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class PhyDebug
    {
        public static RaycastHit2D Raycast(Vector2 rayOriginPoint, Vector2 rayDirction, float rayDistacne, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Debug.DrawRay(rayOriginPoint, rayDirction * rayDistacne, color);
            }
            return Physics2D.Raycast(rayOriginPoint, rayDirction, rayDistacne, mask);
        }
    }
}
