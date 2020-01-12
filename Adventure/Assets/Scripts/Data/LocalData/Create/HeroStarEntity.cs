
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class HeroStarEntity : AbstractEntity
 {
	/// <summary>
	/// 星级
	/// </summary>
	public int Star { get; set; }
	/// <summary>
	/// 生命值系数
	/// </summary>
	public float Hp { get; set; }
	/// <summary>
	/// 物攻系数
	/// </summary>
	public float PhyAtk { get; set; }
	/// <summary>
	/// 特攻系数
	/// </summary>
	public float MgicAtk { get; set; }
	/// <summary>
	/// 暴击系数
	/// </summary>
	public float Cri { get; set; }
	/// <summary>
	/// 暴伤系数
	/// </summary>
	public float CriValue { get; set; }
	/// <summary>
	/// 物防系数
	/// </summary>
	public float PhyDef { get; set; }
	/// <summary>
	/// 魔防系数
	/// </summary>
	public float MgicDef { get; set; }
	/// <summary>
	/// x 下一个星级需要的英雄碎片
	/// </summary>
	public int NeedHeroDebris { get; set; }
  }
