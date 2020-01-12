using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public class CameraCtrl : MonoBehaviour
    {

        public CameraCtrl()
        {
            TweenManager.Instance.StartCoroutine(TT());
        }
        public IEnumerator TT()
        {
            yield return null;
        }
    }
}
