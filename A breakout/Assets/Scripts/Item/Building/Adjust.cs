using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public class Adjust : MonoBehaviour
    {

        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            Node n = GridManager.Instance.GetNodeByPos(transform.position);
            Vector2 pos = GridManager.Instance.GetScalePos(n.Position, GridManager.Instance.YScale);
            transform.position = pos;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
