
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class HeroEntity : AbstractEntity
 {
	/// <summary>
	/// 英雄名
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 英雄半身像
	/// </summary>
	public string RolePic { get; set; }
	/// <summary>
	/// 英雄头像
	/// </summary>
	public string HeadPic { get; set; }
	/// <summary>
	/// 预设名字
	/// </summary>
	public string PrefabName { get; set; }
	/// <summary>
	/// 英雄描述
	/// </summary>
	public string Desc { get; set; }
	/// <summary>
	/// 英雄职业
	/// </summary>
	public int Job { get; set; }
	/// <summary>
	/// 英雄品质 0普通 1精英 2 传奇
	/// </summary>
	public int Quality { get; set; }
	/// <summary>
	/// 使用的技能
	/// </summary>
	public string UesSkill { get; set; }
	/// <summary>
	/// 系数-生命值
	/// </summary>
	public float Hp { get; set; }
	/// <summary>
	/// 系数-物攻
	/// </summary>
	public float PhyAtk { get; set; }
	/// <summary>
	/// 系数-特攻
	/// </summary>
	public float MgicAtk { get; set; }
	/// <summary>
	/// 系数-暴击
	/// </summary>
	public float Cri { get; set; }
	/// <summary>
	/// 系数-暴伤
	/// </summary>
	public float CriValue { get; set; }
	/// <summary>
	/// 系数-物防
	/// </summary>
	public float PhyDef { get; set; }
	/// <summary>
	/// 系数-魔防
	/// </summary>
	public float MgicDef { get; set; }
	/// <summary>
	/// 系数-怒气
	/// </summary>
	public float Rage { get; set; }
  }
