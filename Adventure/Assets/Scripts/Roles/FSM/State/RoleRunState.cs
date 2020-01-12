using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public class RoleRunState : RoleStateAbstract
    {
        public float Speed = 1;
        private float m_TimeOfCell;
        private float m_Timer;
        private bool m_HorizontalMove;
        private Vector2 wayStartPoint;
        private Vector2 wayEndPoint;
        public override RoleStateType RoleStateType
        {
            get
            {
                return RoleStateType.Run;
            }
        }

        private void Start()
        {

        }

        public override void HandleInput()
        {

        }

        public override void OnEnter()
        {
            index = 0;
            m_TimeOfCell = 0;
            m_Timer = 0;
            wayStartPoint = Vector2.zero;
            wayEndPoint = Vector2.zero;
            PrepareOnWayPoint();
            m_Ani.SetTrigger("IsRun");
        }

        public override void OnLevel()
        {

        }

        int index = 0;
        public override void OnUpdate()
        {
            m_Timer += Time.deltaTime;
            Vector2 pos = Vector2.Lerp(wayStartPoint, wayEndPoint, Mathf.Clamp01(m_Timer / m_TimeOfCell));
            m_Role.transform.position = pos;
            if (m_Timer >= m_TimeOfCell)
            {
                index++;
                if (index >= m_Role.Path.Count)
                {
                    m_Role.RoundEnd();
                    m_Role.ToIdle();     
                    return;
                }
                PrepareOnWayPoint();
            }
        }

        private void PrepareOnWayPoint()
        {
            float dis = Vector3.Distance(m_Role.Path[index], m_Role.transform.position);
            m_TimeOfCell = dis / Speed;
            if (index == 0)
            {
                wayStartPoint = transform.position;
                wayEndPoint = m_Role.Path[index];
            }
            else
            {

                wayStartPoint = m_Role.Path[index - 1];
                wayEndPoint = m_Role.Path[index];
            }
        }

    }
}