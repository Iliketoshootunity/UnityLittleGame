
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// SkillLevel数据管理
/// </summary>
public partial class SkillLevelDBModel : AbstractDBModel<SkillLevelDBModel, SkillLevelEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "SkillLevel"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override SkillLevelEntity MakeEntity(GameDataTableParser parse)
	{
		SkillLevelEntity entity = new SkillLevelEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.SkillId = parse.GetFieldValue("SkillId").ToInt();
		entity.Level = parse.GetFieldValue("Level").ToInt();
		entity.HurtValueRate = parse.GetFieldValue("HurtValueRate").ToFloat();
		entity.SkillCD = parse.GetFieldValue("SkillCD").ToFloat();
		entity.BuffChance = parse.GetFieldValue("BuffChance").ToFloat();
		entity.BuffValueMultiplier = parse.GetFieldValue("BuffValueMultiplier").ToFloat();
		entity.BuffTimeMultiplier = parse.GetFieldValue("BuffTimeMultiplier").ToFloat();
		return entity;
 	}
}
