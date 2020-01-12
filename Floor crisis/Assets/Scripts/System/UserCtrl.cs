using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;

namespace EasyFrameWork
{
    public class UserCtrl : SystemCtrlBase<UserCtrl>, ISystemCtrl
    {
        private UIInitShopView m_ShopView;
    
        public void OpenView(UIViewType type)
        {
            switch (type)
            {
                case UIViewType.Shop:
                    OpenShopView();
                    break;
            }
        }

        private void OpenShopView()
        {
            m_ShopView = UIViewUtil.Instance.OpenWindow(UIViewType.Shop).GetComponent<UIInitShopView>();
            SetShopUI();
        }

        private void OnClikBuyJob(object[] p)
        {
            int buyJob = (int)p[0];
            int coin = (int)p[1];
            if (Global.Instance.UserInfo.JobList.Contains(buyJob))
            {
                Global.Instance.UserInfo.CurJob = buyJob;
                MessageCtrl.Instance.Show("购买信息", "已经有了这个角色");
                return;
            }
            if (coin > Global.Instance.UserInfo.Coin)
            {
                MessageCtrl.Instance.Show("购买信息", "金币不足");
                return;
            }
            Global.Instance.UserInfo.Coin -= coin;
            Global.Instance.UserInfo.CurJob = buyJob;
            Global.Instance.UserInfo.AddJob(buyJob);
            SetShopUI();
            MessageCtrl.Instance.Show("购买信息", "已购买成功");
        }

        private void SetShopUI()
        {
            DataTransfer data = new DataTransfer();
            //拥有的英雄
            data.SetData(ConstDefine.Has_Job, Global.Instance.UserInfo.JobList);
            //当前的英雄
            data.SetData(ConstDefine.Cur_Job, Global.Instance.UserInfo.CurJob);
            //拥有的金币
            data.SetData(ConstDefine.Has_Coin, Global.Instance.UserInfo.Coin);
            m_ShopView.SetUI(data);

            AddEventListen(ConstDefine.InitScene_ShopWindow_Buy, OnClikBuyJob);
        }
    }
}
