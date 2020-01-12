
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class SkillLevelEntity : AbstractEntity
 {
	/// <summary>
	/// 技能ID
	/// </summary>
	public int SkillId { get; set; }
	/// <summary>
	/// 技能等级
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// 技能伤害率
	/// </summary>
	public float HurtValueRate { get; set; }
	/// <summary>
	/// 技能CD（不填）
	/// </summary>
	public float SkillCD { get; set; }
	/// <summary>
	/// Buff使用几率
	/// </summary>
	public float BuffChance { get; set; }
	/// <summary>
	/// Buff值乘法器
	/// </summary>
	public float BuffValueMultiplier { get; set; }
	/// <summary>
	/// Buff时间（回合）乘法器
	/// </summary>
	public float BuffTimeMultiplier { get; set; }
  }
