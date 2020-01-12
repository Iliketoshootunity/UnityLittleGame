
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// GameLevelGrade数据管理
/// </summary>
public partial class GameLevelGradeDBModel : AbstractDBModel<GameLevelGradeDBModel, GameLevelGradeEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "GameLevelGrade"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override GameLevelGradeEntity MakeEntity(GameDataTableParser parse)
	{
		GameLevelGradeEntity entity = new GameLevelGradeEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.LevelId = parse.GetFieldValue("LevelId").ToInt();
		entity.Grade = parse.GetFieldValue("Grade").ToInt();
		entity.Desc = parse.GetFieldValue("Desc");
		entity.ConditionDesc = parse.GetFieldValue("ConditionDesc");
		entity.Exp = parse.GetFieldValue("Exp").ToInt();
		entity.Gold = parse.GetFieldValue("Gold").ToInt();
		entity.TimeLimit = parse.GetFieldValue("TimeLimit").ToInt();
		entity.Energy = parse.GetFieldValue("Energy").ToInt();
		return entity;
 	}
}
