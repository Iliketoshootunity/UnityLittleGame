using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class FixedKnife : BaseMissile, IHealth
    {


        [SerializeField]
        private float m_Speed;


        protected override void OnStart()
        {
            base.OnStart();
            m_Status = Status.Fixed;

        }
        protected override void ShootUpdate()
        {
            base.ShootUpdate();
            transform.position += transform.right * m_Speed * Time.deltaTime;
            m_CurSpeed = m_Speed * transform.right;
        }


        protected override void OnCollision(GameObject collision)
        {
            if (m_Status == Status.Fixed)
            {
                if (collision.gameObject.tag.EndsWith("Ground")) return;
            }

            IHealth health = collision.gameObject.GetComponent<IHealth>();
            if (health != null)
            {
                health.OnHit(this.gameObject);
            }
            if (m_Status == Status.Fixed)
            {
                BaseMissile m = collision.gameObject.GetComponent<BaseMissile>();
                if (m != null)
                {
                    m_NextStatus = Status.Shoot;
                    if (m.Type == MissileType.Fixed)
                    {
                        ConnectMissile = m.ConnectMissile;
                    }
                    else
                    {
                        ConnectMissile = m;
                    }

                }
                else
                {
                    GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, "Explode", isCache: true);
                    go.transform.position = transform.position;
                    Destroy(this.gameObject);
                }
            }
            base.OnCollision(collision);
        }

        public void OnHit(GameObject attacker)
        {
            m_NextStatus = Status.Shoot;
            GameSceneCtrl.Instance.SetCanOverRime(5);
        }
    }
}
