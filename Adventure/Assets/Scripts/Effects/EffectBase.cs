using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class EffectBase : MonoBehaviour
    {
        public float DelayPlay;
        public float Duration = 1; //播放持续时间
        public bool DestoryOnPlayOver = true;


        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitForSeconds(DelayPlay);
            Play();
            yield return new WaitForSeconds(Duration);
            PlayOver();
        }


        public virtual void Play()
        {

        }
        public virtual void PlayOver()
        {
            if (DestoryOnPlayOver)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
