
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:44
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class TaskConditionEntity : AbstractEntity
 {
	/// <summary>
	/// 目标数量
	/// </summary>
	public int TargetCount { get; set; }
	/// <summary>
	/// 条件描述
	/// </summary>
	public string Des { get; set; }
	/// <summary>
	/// 条件类型 1日常副本 2人物升级 3英雄升阶 4召唤 5杀怪
	/// </summary>
	public string ConditionType { get; set; }
	/// <summary>
	/// 条件类型 1日常副本 2人物升级 3英雄升阶 4召唤 5杀怪
	/// </summary>
	public string TargetId { get; set; }
  }
