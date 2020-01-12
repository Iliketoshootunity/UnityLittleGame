
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class SkillEntity : AbstractEntity
 {
	/// <summary>
	/// 技能名字
	/// </summary>
	public string SkillName { get; set; }
	/// <summary>
	/// 技能描述
	/// </summary>
	public string SkillDesc { get; set; }
	/// <summary>
	/// 技能类型 0 表示物攻 1表示特攻
	/// </summary>
	public int SkillType { get; set; }
	/// <summary>
	/// 技能最大等级
	/// </summary>
	public int LevelLimit { get; set; }
	/// <summary>
	/// 形状 0 表示横状 1表示竖状 2十字状（英雄专属）
	/// </summary>
	public int AttackArea { get; set; }
	/// <summary>
	/// 攻击范围（格子数，英雄专属，十字形状必须是技术）
	/// </summary>
	public int AttackRange { get; set; }
	/// <summary>
	/// 最大攻击数量（怪物专属）
	/// </summary>
	public int AttackMaxNumber { get; set; }
	/// <summary>
	/// 攻击特效
	/// </summary>
	public string AttackEffect { get; set; }
	/// <summary>
	/// 攻击动作发出多少秒后被攻击者才播放受伤效果
	/// </summary>
	public float ShowHurtEffectDelaySecond { get; set; }
	/// <summary>
	/// Buff Id
	/// </summary>
	public int BuffInfoID { get; set; }
	/// <summary>
	/// 特殊效果触发条件：命中敌人个数
	/// </summary>
	public int BuffConditionOfHitEnemyCount { get; set; }
	/// <summary>
	/// 特殊效果触发条件：杀死敌人个数
	/// </summary>
	public int BuffConditionOfKillEnemyCount { get; set; }
	/// <summary>
	/// Buff值乘法器
	/// </summary>
	public float BuffValueMultiplier { get; set; }
  }
