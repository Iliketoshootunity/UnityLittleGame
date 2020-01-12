
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class BuffEntity : AbstractEntity
 {
	/// <summary>
	/// Buff类型
	/// </summary>
	public int BuffType { get; set; }
	/// <summary>
	/// 叠加类型 0表示叠加时间 1表示叠加层数 2表示重置时间
	/// </summary>
	public int BuffOverlap { get; set; }
	/// <summary>
	/// 执行次数 0表示只执行一次 1表示每次调用都执行
	/// </summary>
	public int BuffCalculateType { get; set; }
	/// <summary>
	/// 关闭类型 0表示关闭所有 1表示单层关闭
	/// </summary>
	public int BuffShutDownType { get; set; }
	/// <summary>
	/// 最大限制（如果是堆叠层数，表示最大层数，如果是时间，表示最大时间）
	/// </summary>
	public int MaxLimit { get; set; }
	/// <summary>
	/// 执行时间
	/// </summary>
	public float Time { get; set; }
	/// <summary>
	/// 执行间隔
	/// </summary>
	public float CallFrequency { get; set; }
	/// <summary>
	/// 执行数值（比如说加血多少  减血多少）
	/// </summary>
	public float Value { get; set; }
  }
