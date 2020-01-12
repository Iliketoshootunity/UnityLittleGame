using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class CameraCtrl : MonoSingleton<CameraCtrl>
    {

        public Transform Target;
        public Transform Camera;
        public float Speed;
        // Use this for initialization
        void Start()
        {
            Camera.position = new Vector3(Camera.position.x, 4, Camera.position.z);

        }

        // Update is called once per frame
        void Update()
        {
            if (Target == null) return;
            float y = Mathf.Lerp(transform.position.y, Target.transform.position.y + 1.5f, Speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        public void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}
