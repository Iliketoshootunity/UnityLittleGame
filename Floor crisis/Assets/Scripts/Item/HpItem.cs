using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class HpItem : MonoBehaviour
    {
        public AudioClip Clip;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            RoleCtrl ctrl = collision.GetComponent<RoleCtrl>();
            if (ctrl != null)
            {
                ctrl.AddHp();
                Destroy(this.gameObject);
                EazySoundManager.PlaySound(Clip);
            }
        }
    }
}
