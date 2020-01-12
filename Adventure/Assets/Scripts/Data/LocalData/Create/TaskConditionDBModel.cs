
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:44
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// TaskCondition数据管理
/// </summary>
public partial class TaskConditionDBModel : AbstractDBModel<TaskConditionDBModel, TaskConditionEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "TaskCondition"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override TaskConditionEntity MakeEntity(GameDataTableParser parse)
	{
		TaskConditionEntity entity = new TaskConditionEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.TargetCount = parse.GetFieldValue("TargetCount").ToInt();
		entity.Des = parse.GetFieldValue("Des");
		entity.ConditionType = parse.GetFieldValue("ConditionType");
		entity.TargetId = parse.GetFieldValue("TargetId");
		return entity;
 	}
}
