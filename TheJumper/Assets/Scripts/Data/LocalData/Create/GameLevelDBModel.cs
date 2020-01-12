
 //===================================================
//作    者：肖海林
//创建时间：2018-11-29 15:28:33
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// GameLevel数据管理
/// </summary>
public partial class GameLevelDBModel : AbstractDBModel<GameLevelDBModel, GameLevelEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "GameLevel"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override GameLevelEntity MakeEntity(GameDataTableParser parse)
	{
		GameLevelEntity entity = new GameLevelEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.MapTypeInfo = parse.GetFieldValue("MapTypeInfo");
		entity.RowSpacing = parse.GetFieldValue("RowSpacing").ToFloat();
		entity.ColumnSpacing = parse.GetFieldValue("ColumnSpacing").ToFloat();
		entity.Orgin = parse.GetFieldValue("Orgin");
		entity.Star2 = parse.GetFieldValue("Star2").ToInt();
		entity.Star3 = parse.GetFieldValue("Star3").ToInt();
		entity.PosInMap = parse.GetFieldValue("PosInMap");
		entity.Time = parse.GetFieldValue("Time").ToFloat();
		entity.RolePos = parse.GetFieldValue("RolePos");
		return entity;
 	}
}
