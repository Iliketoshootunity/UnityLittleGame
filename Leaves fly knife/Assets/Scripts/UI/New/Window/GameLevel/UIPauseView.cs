using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UIPauseView : UIWindowViewBase
    {
        [SerializeField]
        private Image m_MusicImage;
        [SerializeField]
        private Sprite m_MusicOnSprite;
        [SerializeField]
        private Sprite m_MusicOffSprite;

        protected override void OnStart()
        {
            base.OnStart();
            SetMusic();
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            switch (go.name)
            {
                case "btnReturn":
                    object[] obj1 = new object[1];
                    obj1[0] = false;
                    UIDispatcher.Instance.Dispatc(ConstDefine.Pause, obj1);
                    object[] obj = new object[1];
                    obj[0] = "Level";
                    UIDispatcher.Instance.Dispatc(ConstDefine.NextScene, obj);
                    break;
                case "btnMusic":
                    UIDispatcher.Instance.Dispatc(ConstDefine.Music, null);
                    SetMusic();
                    break;
                case "btnPause":
                    object[] obj2 = new object[1];
                    obj2[0] = false;
                    UIDispatcher.Instance.Dispatc(ConstDefine.Pause, obj2);
                    Close();
                    break;
            }
        }
        public void SetUI()
        {
            SetMusic();
        }

        private void SetMusic()
        {
            if (Global.IsPlaySound)
            {
                m_MusicImage.sprite = m_MusicOnSprite;
            }
            else
            {
                m_MusicImage.sprite = m_MusicOffSprite;
            }
        }

    }
}
