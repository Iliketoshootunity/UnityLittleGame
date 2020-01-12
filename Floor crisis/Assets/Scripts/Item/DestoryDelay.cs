using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
	public class DestoryDelay : MonoBehaviour {

        public float Delay;
		// Use this for initialization
		IEnumerator Start () {
            yield return new WaitForSeconds(Delay);
            Destroy(this.gameObject);
	
		}
	
		// Update is called once per frame
		void Update () {
	
		}
	}
}
