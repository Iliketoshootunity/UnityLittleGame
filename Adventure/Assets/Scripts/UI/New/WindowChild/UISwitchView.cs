using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UISwitchView : UISubViewBase
    {
        public Image RoundImage;
        public Sprite PlayerRound;
        public Sprite EnemyRound;
        public Text CurRound;
        public Text MaxRound;
        public float Duration = 1;
        public Action OnSwitchEnd;

        public void SetUI(bool playerRound, int curRound, int maxRound)
        {
            if (playerRound)
            {
                RoundImage.sprite = PlayerRound;
            }
            else
            {
                RoundImage.sprite = EnemyRound;
            }
            CurRound.text = CurRound.ToString();
            MaxRound.text = maxRound.ToString();
            transform.localScale = Vector3.zero;
            transform.DoScale(Vector3.one, Duration).SetEndAction(() =>
            {
                StartCoroutine("Stay");
            }
            );
        }

        private IEnumerator Stay()
        {
            yield return new WaitForSeconds(1);
            transform.DoScale(Vector3.zero, Duration).SetEndAction(() =>
            {
                if (OnSwitchEnd != null)
                {
                    OnSwitchEnd();
                }
            }
           );
        }
    }
}
