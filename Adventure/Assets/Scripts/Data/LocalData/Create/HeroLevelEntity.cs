
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class HeroLevelEntity : AbstractEntity
 {
	/// <summary>
	/// 等级
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// 升到下一级需要的经验
	/// </summary>
	public int NeedExp { get; set; }
	/// <summary>
	/// 基础生命值
	/// </summary>
	public int Hp { get; set; }
	/// <summary>
	/// 基础物攻
	/// </summary>
	public int PhyAtk { get; set; }
	/// <summary>
	/// 基础特攻
	/// </summary>
	public int MgicAtk { get; set; }
	/// <summary>
	/// 基础暴击几率
	/// </summary>
	public int Cri { get; set; }
	/// <summary>
	/// 基础暴伤
	/// </summary>
	public float CriValue { get; set; }
	/// <summary>
	/// 基础物防
	/// </summary>
	public int PhyDef { get; set; }
	/// <summary>
	/// 基础魔防
	/// </summary>
	public int MgicDef { get; set; }
	/// <summary>
	/// 基础怒气
	/// </summary>
	public int RageValue { get; set; }
  }
