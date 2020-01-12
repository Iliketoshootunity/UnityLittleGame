using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public enum RevocableObstaclesType
    {
        Red,
        Blue
    }
    /// <summary>
    /// 可被解除的障碍物
    /// </summary>
    public class RevocableObstacles : Obstacles
    {
        public override ObstaclesType ObstaclesType
        {
            get
            {
                return ObstaclesType.Revocable;
            }
        }
        public RevocableObstaclesType RevocableObstaclesType;
        private SpriteRenderer m_Renderer;

        public void Init(RevocableObstaclesType revocableObstaclesType)
        {
            RevocableObstaclesType = revocableObstaclesType;
            m_Renderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsShow = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsShow = false;
        }
    }
}
