using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 工具类
    /// </summary>
	public class PlotTool : MonoSingleton<PlotTool> {

        private List<PlotTick> m_PlotTickList = new List<PlotTick>();
        private List<PlotTick> m_RemovePlotTickList = new List<PlotTick>();

        void Update()
        {
            m_RemovePlotTickList.Clear();
            for (int i = 0; i < m_PlotTickList.Count; i++)
            {
                if (!m_PlotTickList[i].IsFinsh)
                {
                    m_PlotTickList[i].OnUpdate();
                    continue;
                }
                m_RemovePlotTickList.Add(m_PlotTickList[i]);
            }
            for (int i = 0; i < m_RemovePlotTickList.Count; i++)
            {
                RemovePlotTick(m_RemovePlotTickList[i]);
            }
        }

        public void AddPlotTick(PlotTick tick)
        {
            m_PlotTickList.Add(tick);
        }

        public void RemovePlotTick(PlotTick tick)
        {
            if (m_PlotTickList.Contains(tick))
                m_PlotTickList.Remove(tick);
        }

        /// <summary>
        /// 延时执行某个方法
        /// </summary>
        /// <param name="time"></param>
        /// <param name="OnFinsh"></param>
        public void Delay(float time, System.Action OnFinsh)
        {
            StartCoroutine(IeDelay(time, OnFinsh));
        }

        IEnumerator IeDelay(float time, System.Action OnFinsh)
        {
            yield return new WaitForSeconds(time);
            if (OnFinsh != null)
                OnFinsh();
        }

        public static Vector3 ParseVector3(string content)
        {
            string[] data = content.Split('|');
            return new Vector3(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
        }

        public static Vector2 ParseVector2(string content)
        {
            string[] data = content.Split('|');
            return new Vector2(float.Parse(data[0]), float.Parse(data[1]));
        }

	}
}
