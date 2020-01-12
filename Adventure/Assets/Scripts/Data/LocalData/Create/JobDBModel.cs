
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Job数据管理
/// </summary>
public partial class JobDBModel : AbstractDBModel<JobDBModel, JobEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Job"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override JobEntity MakeEntity(GameDataTableParser parse)
	{
		JobEntity entity = new JobEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.Name = parse.GetFieldValue("Name");
		entity.Icon = parse.GetFieldValue("Icon");
		entity.Desc = parse.GetFieldValue("Desc");
		entity.Hp = parse.GetFieldValue("Hp").ToFloat();
		entity.PhyAtk = parse.GetFieldValue("PhyAtk").ToFloat();
		entity.MgicAtk = parse.GetFieldValue("MgicAtk").ToFloat();
		entity.Cri = parse.GetFieldValue("Cri").ToFloat();
		entity.CriValue = parse.GetFieldValue("CriValue").ToFloat();
		entity.PhyDef = parse.GetFieldValue("PhyDef").ToFloat();
		entity.MgicDef = parse.GetFieldValue("MgicDef").ToFloat();
		return entity;
 	}
}
