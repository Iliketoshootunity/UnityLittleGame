
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Buff数据管理
/// </summary>
public partial class BuffDBModel : AbstractDBModel<BuffDBModel, BuffEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Buff"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override BuffEntity MakeEntity(GameDataTableParser parse)
	{
		BuffEntity entity = new BuffEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.BuffType = parse.GetFieldValue("BuffType").ToInt();
		entity.BuffOverlap = parse.GetFieldValue("BuffOverlap").ToInt();
		entity.BuffCalculateType = parse.GetFieldValue("BuffCalculateType").ToInt();
		entity.BuffShutDownType = parse.GetFieldValue("BuffShutDownType").ToInt();
		entity.MaxLimit = parse.GetFieldValue("MaxLimit").ToInt();
		entity.Time = parse.GetFieldValue("Time").ToFloat();
		entity.CallFrequency = parse.GetFieldValue("CallFrequency").ToFloat();
		entity.Value = parse.GetFieldValue("Value").ToFloat();
		return entity;
 	}
}
