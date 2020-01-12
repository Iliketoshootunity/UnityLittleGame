
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Task数据管理
/// </summary>
public partial class TaskDBModel : AbstractDBModel<TaskDBModel, TaskEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Task"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override TaskEntity MakeEntity(GameDataTableParser parse)
	{
		TaskEntity entity = new TaskEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.Name = parse.GetFieldValue("Name");
		entity.Desc = parse.GetFieldValue("Desc");
		entity.Conditions = parse.GetFieldValue("Conditions");
		entity.Rewards = parse.GetFieldValue("Rewards");
		return entity;
 	}
}
