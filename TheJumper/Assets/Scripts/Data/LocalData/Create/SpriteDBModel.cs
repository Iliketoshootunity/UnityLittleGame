
 //===================================================
//作    者：肖海林
//创建时间：2018-11-29 15:29:16
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Sprite数据管理
/// </summary>
public partial class SpriteDBModel : AbstractDBModel<SpriteDBModel, SpriteEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Sprite"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override SpriteEntity MakeEntity(GameDataTableParser parse)
	{
		SpriteEntity entity = new SpriteEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.SpriteName = parse.GetFieldValue("SpriteName");
		entity.MoveSpeed = parse.GetFieldValue("MoveSpeed").ToFloat();
		entity.HP = parse.GetFieldValue("HP").ToInt();
		entity.Score = parse.GetFieldValue("Score").ToInt();
		entity.PrefabName = parse.GetFieldValue("PrefabName");
		return entity;
 	}
}
