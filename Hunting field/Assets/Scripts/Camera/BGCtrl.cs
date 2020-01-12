using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class BGCtrl : MonoBehaviour
    {

        // Use this for initialization
        IEnumerator Start()
        {
            yield return null;
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            float height = Camera.main.orthographicSize * 2 * 100;
            float wight = height / (Screen.height / (float)Screen.width);
            float scaleH = height / 852;
            float scaleW = wight / 1136;
            if (scaleH > scaleW)
            {
                transform.localScale = scaleH * Vector3.one;
            }
            else
            {
                transform.localScale = scaleW * Vector3.one;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
