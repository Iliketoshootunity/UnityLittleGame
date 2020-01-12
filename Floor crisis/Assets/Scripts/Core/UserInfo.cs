using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 用户信息
    /// </summary>
	public class UserInfo
    {

        private int m_Coin;
        private int m_CurJob;
        public int Coin
        {
            get
            {
                return m_Coin;
            }
            set
            {
                ToSava();
                m_Coin = value; ;
            }
        }
        public int CurJob
        {
            get
            {
                return m_CurJob;
            }
            set
            {
                ToSava();
                m_CurJob = value;
            }
        }
        public List<int> JobList;

        public UserInfo()
        {
            JobList = new List<int>();
        }

        public void AddJob(int value)
        {
            JobList.Add(value);
            ToSava();
        }

        public static UserInfo ToUserInfo()
        {
            string coin = PlayerPrefs.GetString("Coin", "0");
            string curjob = PlayerPrefs.GetString("CurJob", "0");
            string jobList = PlayerPrefs.GetString("JobList", "0");
            UserInfo info = new UserInfo();
            info.Coin = coin.ToInt();
            info.CurJob = curjob.ToInt();
            string[] jobArr = jobList.Split(',');
            for (int i = 0; i < jobArr.Length; i++)
            {
                info.AddJob(jobArr[i].ToInt());
            }
            return info;
        }

        public void ToSava()
        {
            PlayerPrefs.SetString("Coin", Coin.ToString());
            PlayerPrefs.SetString("CurJob", CurJob.ToString());
            string jobList = "";
            for (int i = 0; i < JobList.Count; i++)
            {
                if (i == 0)
                {
                    jobList = JobList[i].ToString();
                }
                else
                {
                    jobList += ("," + JobList[i].ToString());
                }

            }
            PlayerPrefs.SetString("JobList", jobList);
        }
    }
}
