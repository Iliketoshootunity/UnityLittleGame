using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class PrefabInstance : MonoBehaviour
    {
        public GameObject Prefab;
        // Use this for initialization
        void Start()
        {
            GameObject go = Instantiate(Prefab);
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localEulerAngles = new Vector3(0, 0, 0);
            go.transform.localRotation = Quaternion.identity;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private Vector2 PrefabSize;
        private void OnDrawGizmos()
        {
            if (PrefabSize == Vector2.zero)
            {
                PrefabSize = Prefab.GetComponentInChildren<BoxCollider2D>().size;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector2(0, PrefabSize.y / 2) + new Vector2(transform.position.x, transform.position.y), new Vector3(PrefabSize.x, PrefabSize.y));
        }
    }
}
