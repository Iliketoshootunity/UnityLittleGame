using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class ObjLayer : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            float process = Mathf.Clamp01(1 - pos.y);
            transform.position = new Vector3(transform.position.x, transform.position.y,
                -process * 10 - 2);

        }
    }
}
