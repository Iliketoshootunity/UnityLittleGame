using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    /// <summary>
    /// 随着玩家移动轮流隐藏出现的的障碍物
    /// </summary>
	public class StepObstacles : Obstacles
    {
        public override ObstaclesType ObstaclesType
        {
            get
            {
                return ObstaclesType.Step;
            }
        }
        private SpriteRenderer m_Renderer;

        public void Init(bool isShow)
        {
            m_Renderer = GetComponentInChildren<SpriteRenderer>();
            if (isShow)
            {
                Show();
            }
            else
            {
                Hide();
            }
            IsShow = isShow;
        }

        public void Change()
        {
            IsShow = !IsShow;
            if (IsShow)
            {
                Show();
            }
            else
            {
                Hide();
            }         
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}
