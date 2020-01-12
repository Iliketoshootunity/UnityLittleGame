using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class Emitter : MonoBehaviour
    {
        [SerializeField]
        private Transform m_FixedPos;
        [SerializeField]
        private Transform m_Instructions;

        private void Awake()
        {
            ShowOrHideInstructions(false);
        }
        public bool IsWeaK
        {
            get
            {
                return false;
            }
        }


        public void Fixed(Missile n)
        {
            n.ToFixed();
            n.SetParent(m_FixedPos);
        }


        public void ShowOrHideInstructions(bool isShow)
        {
            m_Instructions.gameObject.SetActive(isShow);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BaseMissile m = collision.GetComponent<BaseMissile>();
            if (m != null)
            {
                GameSceneCtrl.Instance.RefreshEmitter(this);
            }
        }
        private void OnDrawGizmos()
        {
            Vector3 pos = m_FixedPos.transform.position;
            Gizmos.DrawWireSphere(pos, 0.1f);
        }
    }
}
