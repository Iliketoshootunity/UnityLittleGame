
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
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
		entity.SpriteType = parse.GetFieldValue("SpriteType").ToInt();
		entity.Name = parse.GetFieldValue("Name");
		entity.Level = parse.GetFieldValue("Level").ToInt();
		entity.IsBoss = parse.GetFieldValue("IsBoss").ToInt();
		entity.TextureName = parse.GetFieldValue("TextureName");
		entity.HeadPic = parse.GetFieldValue("HeadPic");
		entity.PrefabName = parse.GetFieldValue("PrefabName");
		entity.Hp = parse.GetFieldValue("Hp").ToInt();
		entity.PhyAtk = parse.GetFieldValue("PhyAtk").ToInt();
		entity.MgicAtk = parse.GetFieldValue("MgicAtk").ToInt();
		entity.Cri = parse.GetFieldValue("Cri").ToInt();
		entity.CriValue = parse.GetFieldValue("CriValue").ToFloat();
		entity.PhyDef = parse.GetFieldValue("PhyDef").ToInt();
		entity.MgicDef = parse.GetFieldValue("MgicDef").ToInt();
		entity.MoveCellCount = parse.GetFieldValue("MoveCellCount").ToInt();
		entity.AttackRange = parse.GetFieldValue("AttackRange").ToInt();
		entity.UesSkill = parse.GetFieldValue("UesSkill");
		return entity;
 	}
}
