
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:44
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// TaskReward数据管理
/// </summary>
public partial class TaskRewardDBModel : AbstractDBModel<TaskRewardDBModel, TaskRewardEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "TaskReward"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override TaskRewardEntity MakeEntity(GameDataTableParser parse)
	{
		TaskRewardEntity entity = new TaskRewardEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.Count = parse.GetFieldValue("Count").ToInt();
		entity.RewardType = parse.GetFieldValue("RewardType").ToInt();
		return entity;
 	}
}
