
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Skill数据管理
/// </summary>
public partial class SkillDBModel : AbstractDBModel<SkillDBModel, SkillEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Skill"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override SkillEntity MakeEntity(GameDataTableParser parse)
	{
		SkillEntity entity = new SkillEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.SkillName = parse.GetFieldValue("SkillName");
		entity.SkillDesc = parse.GetFieldValue("SkillDesc");
		entity.SkillType = parse.GetFieldValue("SkillType").ToInt();
		entity.LevelLimit = parse.GetFieldValue("LevelLimit").ToInt();
		entity.AttackArea = parse.GetFieldValue("AttackArea").ToInt();
		entity.AttackRange = parse.GetFieldValue("AttackRange").ToInt();
		entity.AttackMaxNumber = parse.GetFieldValue("AttackMaxNumber").ToInt();
		entity.AttackEffect = parse.GetFieldValue("AttackEffect");
		entity.ShowHurtEffectDelaySecond = parse.GetFieldValue("ShowHurtEffectDelaySecond").ToFloat();
		entity.BuffInfoID = parse.GetFieldValue("BuffInfoID").ToInt();
		entity.BuffConditionOfHitEnemyCount = parse.GetFieldValue("BuffConditionOfHitEnemyCount").ToInt();
		entity.BuffConditionOfKillEnemyCount = parse.GetFieldValue("BuffConditionOfKillEnemyCount").ToInt();
		entity.BuffValueMultiplier = parse.GetFieldValue("BuffValueMultiplier").ToFloat();
		return entity;
 	}
}
