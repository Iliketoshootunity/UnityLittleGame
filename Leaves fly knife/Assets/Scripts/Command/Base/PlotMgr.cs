using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

public interface IPlotMrg
{
    //创建坐标
    void CreateActor(string name, string assetName);


    //设置坐标
    void SetActorPos(string name, Vector3 pos);
    void SetActorPos(string name, Vector3 pos, float time);
    void SetActorPos(int index, Vector3 pos);
    void SetActorPos(int index, Vector3 pos, float time);

    //设置旋转
    void SetActorRot(string name, Vector3 Angle, float time);
    void SetActorRot(int index, Vector3 Angle, float time);

    //开始剧情
    void Play(string plotName);
}

namespace EasyFrameWork
{
    /// <summary>
    /// 剧情管理器
    /// </summary>
	public class PlotMgr : MonoSingleton<PlotMgr>, IPlotMrg
    {
        public PlotInfo PI;


        public void Init()
        {
            PlotInfo pi = new PlotInfo(PlotMgr.Instance);
            PI = pi;
        }
        /// <summary>
        /// 解析剧情(主要是比较复杂的项目使用，小项目直接new)
        /// </summary>
        /// <returns></returns>
        public PlotInfo ParsePlot(string command)
        {
            command = command.Replace("\r", "");
            string[] commandAry = command.Split('\n');
            Init();
            for (int i = 0; i < commandAry.Length; i++)
            {
                if (commandAry[i].Equals("")) continue;
                string[] commandStruct = commandAry[i].Split('_');
                //命令
                PlotCommand pc = PlotFactory.CreatePlotCommand(commandStruct[0]);
                if (pc != null)
                {
                    pc.OnParse(commandStruct[1]);
                    PI.AddCommand(pc);
                }
                else
                {
                    Debug.Log("创建命令失败");
                }
            }
            return PI;
        }
        /// <summary>
        /// 剧情主角
        /// </summary>
        private Dictionary<string, GameObject> m_PlotActors = new Dictionary<string, GameObject>();
        public void ClearPlotActor()
        {
            List<GameObject> obj = new List<GameObject>(m_PlotActors.Values);
            for (int i = 0; i < obj.Count; i++)
            {
                Destroy(obj[i]);
            }
            m_PlotActors.Clear();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        public GameObject GetActor(string name)
        {
            if (m_PlotActors.ContainsKey(name))
            {
                return m_PlotActors[name];
            }
            return null;
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assetName"></param>
        public void CreateActor(string name, string assetName)
        {
            GameObject go = Resources.Load<GameObject>(assetName);
            go = GameObject.Instantiate(go);
            go.name = name;
            m_PlotActors.Add(go.name, go);
        }

        /// <summary>
        /// 设置角色位置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public void SetActorPos(string name, Vector3 pos)
        {
            GameObject actor = GetActor(name);
            if (actor != null)
            {
                actor.transform.localPosition = pos;
            }
        }

        public void SetActorPos(string name, Vector3 pos, float time)
        {
            MoveTick tick = new MoveTick();
            tick.MoveActor = GetActor(name).transform;
            tick.MoveEndPos = pos;
            float dis = Vector3.Distance(tick.MoveActor.localPosition, pos);
            tick.MoveSpeed = dis / time;
            PlotTool.Instance.AddPlotTick(tick);
        }

        public void SetActorPos(int index, Vector3 pos)
        {

        }

        public void SetActorPos(int index, Vector3 pos, float time)
        {

        }

        public void SetActorRot(string name, Vector3 Angle, float time)
        {

        }

        public void SetActorRot(int index, Vector3 Angle, float speed)
        {
            RotTick tick = new RotTick();
            tick.RotActor = GetActor(name).transform;
            tick.EndAngle = Angle;
            tick.RotSpeed = speed;
            PlotTool.Instance.AddPlotTick(tick);
        }

        public void Play()
        {
            PI.Play();
        }
        public void Play(string plotName)
        {

        }
    }
}
