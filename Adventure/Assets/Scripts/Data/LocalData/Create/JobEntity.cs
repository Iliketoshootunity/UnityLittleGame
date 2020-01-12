
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class JobEntity : AbstractEntity
 {
	/// <summary>
	/// 职业名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 图标
	/// </summary>
	public string Icon { get; set; }
	/// <summary>
	/// 职业描述
	/// </summary>
	public string Desc { get; set; }
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
  }
