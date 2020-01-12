using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
	public abstract class PlotTick  {

        public System.Action OnFinsh;
        public bool IsFinsh { get; protected set; }

        public abstract void OnUpdate();
	}
}
