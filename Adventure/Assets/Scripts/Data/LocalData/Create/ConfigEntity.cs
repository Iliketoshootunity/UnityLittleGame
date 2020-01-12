
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class ConfigEntity : AbstractEntity
 {
	/// <summary>
	/// 一次召唤所需金钱
	/// </summary>
	public int OneSummonCoin { get; set; }
	/// <summary>
	/// 五次召唤所需金钱
	/// </summary>
	public int FiveSummonCoin { get; set; }
	/// <summary>
	/// 普通英雄抽取概率
	/// </summary>
	public float TheProbabilityOfNormalHero { get; set; }
	/// <summary>
	/// 精英英雄抽取概率
	/// </summary>
	public float TheProbabilityOfeliteHero { get; set; }
	/// <summary>
	/// 传奇英雄抽取概率
	/// </summary>
	public float TheProbabilityOfLegendHero { get; set; }
	/// <summary>
	/// 每日任务
	/// </summary>
	public string EveryOneTask { get; set; }
	/// <summary>
	/// 普通英雄分解碎片
	/// </summary>
	public int TheDebrisOfNormalHero { get; set; }
	/// <summary>
	/// 精英英雄分解碎片
	/// </summary>
	public int TheDebrisOfEliteHero { get; set; }
	/// <summary>
	/// 传奇英雄分解碎片
	/// </summary>
	public int TheDebrisOfLegendHero { get; set; }
  }
