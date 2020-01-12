using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class RoleHeadBarCtrl : MonoBehaviour
    {
        public static RoleHeadBarCtrl Instance;
        public RectTransform Rect;
        private void Awake()
        {
            Instance = this;
        }
        // Use this for initialization
        void Start()
        {
            Rect = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
