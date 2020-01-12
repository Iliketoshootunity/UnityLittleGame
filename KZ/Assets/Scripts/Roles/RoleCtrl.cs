using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{

    public class RoleCtrl : MonoBehaviour
    {
        public InputManager m_InputManager { get; protected set; }
        public RoleStateMachine StateMachine { get; protected set; }
        public RoleType RoleType { get; protected set; }

        protected IRoleAI m_AI;
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            StateMachine = new RoleStateMachine(this.gameObject, true);
            RoleStateAbstract[] states = GetComponents<RoleStateAbstract>();
            for (int i = 0; i < states.Length; i++)
            {
                StateMachine.AddState(states[i]);
            }
        }
        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {

        }
        private void Update()
        {
            OnUpdate();
        }
        protected virtual void OnUpdate()
        {
            if (m_AI != null)
            {
                m_AI.DoAI();
            }
            if (StateMachine.CurrentRoleState != null)
            {
                StateMachine.CurrentRoleState.OnUpdate();
            }
        }

        public void Initialize(RoleType roleType, IRoleAI ai = null)
        {
            m_AI = ai;
            RoleType = roleType;
        }

        public virtual void SetInputManager(InputManager inputManager)
        {
            m_InputManager = inputManager;
        }
        protected virtual void UpdateStateMachine()
        {

        }
    }
}
