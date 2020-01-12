using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 将Multiply格式的Sprite分割为单张图片,保存为png格式,保证Sprite的锚点处于输出的PNG的中央位置
/// </summary>
public class MiniTools_SplitMultiplySprites
{
    private static string savingFolderPath;
    private static int texIndex;
    private static float intervalProgress = 0;
    private static int pngCounting = 0;

    [MenuItem("Assets/MiniTools/图片分割、合并/导出选中及选中的文件夹中的Multiply Sprite中每张Sprite")]
    public static void SplitMultiplySprites()
    {
        try
        {
            Object[] textureObjects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
            if (textureObjects.Length > 0)
            {
                savingFolderPath = EditorUtility.SaveFolderPanel("将PNG保存到", "", "");
                pngCounting = 0;
                if (!string.IsNullOrEmpty(savingFolderPath))
                {
                    intervalProgress = 1f / textureObjects.Length;
                    for (texIndex = 0; texIndex < textureObjects.Length; texIndex++)
                    {
                        SplitSprite(textureObjects[texIndex] as Texture2D);
                    }
                }
            }
        }
        finally
        {
            if (!string.IsNullOrEmpty(savingFolderPath))
            {
                //UnityEngine.Debug.Log(string.Format("导出 {0} 张png图片至 {1} 中", pngCounting, savingFolderPath.Replace("/", "\\")));
                Process.Start("Explorer", savingFolderPath.Replace("/", "\\"));
            }
            AssetDatabase.Refresh();
            HideProgress();
        }
    }

    /// <summary>
    /// 将Multiply精灵格式图片分割并保存
    /// </summary>
    /// <param name="tex"></param>
    private static void SplitSprite(Texture2D tex)
    {
        if (tex == null)
        {
            return;
        }
        string assetPath = AssetDatabase.GetAssetPath(tex);
        tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(assetPath);
        if (!textureImporter.isReadable)
        {
            textureImporter.isReadable = true;
            AssetDatabase.ImportAsset(assetPath);
        }
        if (textureImporter.textureType == TextureImporterType.Sprite && textureImporter.spriteImportMode == SpriteImportMode.Multiple)
        {
            string spritename = assetPath.Remove(0, 7).Replace('/', '_');
            if (spritename.Contains("."))
            {
                int indexOfDot = spritename.LastIndexOf('.');
                spritename = spritename.Remove(indexOfDot, spritename.Length - indexOfDot);
            }
            spritename += "  ";
            string finalFilePath;
            int summaryLength = textureImporter.spritesheet.Length;
            for (int i = 0; i < summaryLength; i++)
            {
                SpriteMetaData data = textureImporter.spritesheet[i];
                int pivotPosX = (int)(data.pivot.x * data.rect.width + data.rect.xMin);
                int pivotPosY = (int)(data.pivot.y * data.rect.height + data.rect.yMin);
                int realWidth = (int)(2 * (Mathf.Max(Mathf.Abs(data.rect.xMax - pivotPosX), Mathf.Abs(data.rect.xMin - pivotPosX))));
                int realHeight = (int)(2 * (Mathf.Max(Mathf.Abs(data.rect.yMax - pivotPosY), Mathf.Abs(data.rect.yMin - pivotPosY))));
                Texture2D texture2d = new Texture2D(realWidth, realHeight);
                texture2d.name = spritename + data.name;
                finalFilePath = savingFolderPath + "/" + texture2d.name + ".png";
                ShowProgress(finalFilePath, i / (float)summaryLength);
                Color colorTemp;
                int texInSpriteX;
                int texInSpriteY;
                for (int x = 0; x < realWidth; x++)
                {
                    for (int y = 0; y < realHeight; y++)
                    {
                        colorTemp = Color.clear;
                        texInSpriteX = x - realWidth / 2 + pivotPosX;
                        texInSpriteY = y - realHeight / 2 + pivotPosY;
                        if (texInSpriteX >= data.rect.xMin && texInSpriteX <= data.rect.xMax && texInSpriteY >= data.rect.yMin && texInSpriteY <= data.rect.yMax)
                        {
                            colorTemp = tex.GetPixel(texInSpriteX, texInSpriteY);
                        }
                        texture2d.SetPixel(x, y, colorTemp);
                    }
                }
                texture2d.Apply();
                if (File.Exists(finalFilePath))
                {
                    //UnityEngine.Debug.Log(finalFilePath + "  替换");
                    File.Delete(finalFilePath);
                }
                using (FileStream fs = new FileStream(finalFilePath, FileMode.Create))
                {
                    byte[] datas = texture2d.EncodeToPNG();
                    fs.Write(datas, 0, datas.Length);
                }
                pngCounting++;
            }
        }
    }

    private static void ShowProgress(string pngpath, float progress)
    {
        EditorUtility.DisplayProgressBar("保存精灵为单个PNG文件", "正在保存 " + pngpath, intervalProgress * (progress + texIndex));
    }

    private static void HideProgress()
    {
        EditorUtility.ClearProgressBar();
    }
}
