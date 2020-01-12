using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelSceneView : UISceneViewBase
    {
        [SerializeField]
        private List<Image> m_HpValue;
        [SerializeField]
        private Text m_CoinValue;


        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnPause")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelScene_Pause, null);
            }
            EazySoundManager.PlayUISound(Global.Instance.BtnClip);
        }

        public void SetHP(int hp)
        {
            for (int i = 0; i < m_HpValue.Count; i++)
            {
                if (i < hp)
                {
                    m_HpValue[i].enabled = true;
                }
                else
                {
                    m_HpValue[i].enabled = false;
                }
            }
        }

        public void SetCoin(int coin)
        {
            m_CoinValue.text = coin.ToString();
        }
    }
}
