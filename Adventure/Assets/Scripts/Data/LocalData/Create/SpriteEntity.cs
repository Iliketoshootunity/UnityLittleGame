
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class SpriteEntity : AbstractEntity
 {
	/// <summary>
	/// 精灵类型
	/// </summary>
	public int SpriteType { get; set; }
	/// <summary>
	/// 名字
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 等级
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// 是否是boss（0 代表不是 1 代表是）
	/// </summary>
	public int IsBoss { get; set; }
	/// <summary>
	/// 贴图名字
	/// </summary>
	public string TextureName { get; set; }
	/// <summary>
	/// 头像名字
	/// </summary>
	public string HeadPic { get; set; }
	/// <summary>
	/// 预设名字
	/// </summary>
	public string PrefabName { get; set; }
	/// <summary>
	/// 生命值
	/// </summary>
	public int Hp { get; set; }
	/// <summary>
	/// 物攻
	/// </summary>
	public int PhyAtk { get; set; }
	/// <summary>
	/// 特攻
	/// </summary>
	public int MgicAtk { get; set; }
	/// <summary>
	/// 暴击
	/// </summary>
	public int Cri { get; set; }
	/// <summary>
	/// 暴伤
	/// </summary>
	public float CriValue { get; set; }
	/// <summary>
	/// 物防
	/// </summary>
	public int PhyDef { get; set; }
	/// <summary>
	/// 魔防
	/// </summary>
	public int MgicDef { get; set; }
	/// <summary>
	/// 移动格子数
	/// </summary>
	public int MoveCellCount { get; set; }
	/// <summary>
	/// 攻击范围
	/// </summary>
	public int AttackRange { get; set; }
	/// <summary>
	/// 使用的技能
	/// </summary>
	public string UesSkill { get; set; }
  }
