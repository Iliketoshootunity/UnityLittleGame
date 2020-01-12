using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

/// <summary>
/// 依据fnt文件和位图文件创建字体,鼠标需选中fnt文件
/// </summary>
public class MiniTools_CreateCustomFont
{
    private static Object[] selectedObjs;
    private static TextAsset fntTextAsset;
    private static List<CharacterInfo> characterInfoList;

    private static string commonPattern = @"common lineHeight=(?<lineHeight>\d+)\s+base=(?<base>\d+)\s+scaleW=(?<scaleW>\d+)\s+scaleH=(?<scaleH>\d+)\s+pages=(?<pages>\d+)\s+packed=(?<packed>\d+)\s+alphaChnl=(?<alphaChnl>\d+)\s+redChnl=(?<redChnl>\d+)\s+greenChnl=(?<greenChnl>\d+)\s+blueChnl=(?<blueChnl>\d+)\s+";
    private static string charPattern = @"char id=(?<id>\d+)\s+x=(?<x>\d+)\s+y=(?<y>\d+)\s+width=(?<width>\d+)\s+height=(?<height>\d+)\s+xoffset=(?<xoffset>\d+)\s+yoffset=(?<yoffset>\d+)\s+xadvance=(?<xadvance>\d+)\s+page=(?<page>\d+)\s+chnl=(?<chnl>\d+)\s+";
    private static string pagefilePattern = @"page id=(?<id>\d+)\s+file=""(?<file>\S+)""";

    [MenuItem("Assets/MiniTools/字体相关/从fnt文件创建艺术字体")]
    private static void CreateCustomFont()
    {
        selectedObjs = Selection.GetFiltered(typeof(TextAsset), SelectionMode.Assets);
        if (selectedObjs != null)
        {
            for (int i = 0; i < selectedObjs.Length; i++)
            {
                if (selectedObjs[i] != null && AssetDatabase.GetAssetPath(selectedObjs[i]).Contains(".fnt"))
                {
                    CreateCustomFontFromFNT(AssetDatabase.GetAssetPath(selectedObjs[i]));
                }
            }
        }
    }

    private static void CreateCustomFontFromFNT(string assetpath)
    {
        fntTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(assetpath);
        if (fntTextAsset != null)
        {
            Font font = new Font(fntTextAsset.name);
            Match commonMatch = Regex.Match(fntTextAsset.text, commonPattern);
            Match pageFileMatch = Regex.Match(fntTextAsset.text, pagefilePattern);
            MatchCollection charMatches = Regex.Matches(fntTextAsset.text, charPattern);
            if (commonMatch == null || charMatches == null || pageFileMatch == null)
            {
                return;
            }
            //保存字符信息
            int summaryWidth = int.Parse(commonMatch.Groups["scaleW"].Value);
            int summaryHeight = int.Parse(commonMatch.Groups["scaleH"].Value);
            int originIndex = int.Parse(pageFileMatch.Groups["id"].Value);
            characterInfoList = new List<CharacterInfo>(charMatches.Count);
            foreach (Match match in charMatches)
            {
                CharacterInfo charInfo = new CharacterInfo();
                charInfo.index = originIndex + int.Parse(match.Groups["id"].Value);
                float uvX = int.Parse(match.Groups["x"].Value) / (float)summaryWidth;
                float uvY = 1 - (int.Parse(match.Groups["y"].Value) + int.Parse(match.Groups["height"].Value)) / (float)summaryHeight;
                float uvW = int.Parse(match.Groups["width"].Value) / ((float)summaryWidth);
                float uvH = int.Parse(match.Groups["height"].Value) / ((float)summaryHeight);
                charInfo.uvBottomLeft = new Vector2(uvX, uvY);
                charInfo.uvBottomRight = new Vector2(uvX + uvW, uvY);
                charInfo.uvTopLeft = new Vector2(uvX, uvY + uvH);
                charInfo.uvTopRight = new Vector2(uvX + uvW, uvY + uvH);
                charInfo.minX = 0;
                charInfo.maxX = int.Parse(match.Groups["width"].Value);
                charInfo.minY = -int.Parse(match.Groups["height"].Value) / 2;
                charInfo.maxY = int.Parse(match.Groups["height"].Value) / 2;
                charInfo.advance = int.Parse(match.Groups["width"].Value);
                charInfo.glyphHeight = int.Parse(match.Groups["height"].Value);
                charInfo.glyphWidth = int.Parse(match.Groups["width"].Value);
                charInfo.style = FontStyle.Normal;
                charInfo.bearing = int.Parse(match.Groups["width"].Value) / 2;
                characterInfoList.Add(charInfo);
            };
            font.characterInfo = characterInfoList.ToArray();
            //修改行间距
            SerializedObject serializedObj = new SerializedObject(font);
            SerializedProperty property = serializedObj.GetIterator();
            while (property.Next(true))
            {
                if (property.displayName == "Line Spacing")
                {
                    property.floatValue = int.Parse(commonMatch.Groups["lineHeight"].Value);
                }
            }
            serializedObj.ApplyModifiedProperties();
            //新建字体使用的材质
            Material material = new Material(Shader.Find("GUI/Text Shader"));
            string folderPath = assetpath.Substring(0, assetpath.LastIndexOf('/'));
            string texturePath = folderPath + '/' + pageFileMatch.Groups["file"];
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
            if (texture == null)
            {
                Debug.LogError(texturePath + "  图片不存在");
            }
            else
            {
                material.mainTexture = texture;
            }
            font.material = material;
            string materialPath = folderPath + '/' + fntTextAsset.name + ".mat";
            AssetDatabase.CreateAsset(material, materialPath);//保存材质
            AssetDatabase.CreateAsset(font, folderPath + '/' + fntTextAsset.name + ".fontsettings");//保存字体
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
