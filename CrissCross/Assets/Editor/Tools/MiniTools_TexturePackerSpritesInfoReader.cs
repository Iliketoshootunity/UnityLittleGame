using UnityEngine;
using System.Collections;
using UnityEditor;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// 读取由TexturePacker导出的图片信息,设置图片格式为Sprite Multiply格式并切割精灵
/// TexturePacker导出图片时应关闭Allow rotation选项,因为unity不支持旋转精灵
/// 导出的图片和信息文本需在同一文件夹下,且文件名称相同
/// 调用了LitJson.dll,用以解析Json格式
/// </summary>
public class MiniTools_TexturePackerSpritesInfoReader
{
    [MenuItem("Assets/MiniTools/图片分割、合并/分割从TexturePacker中导出的图片为Multiply Sprite(最近点采样)")]
    public static void SpriteInfoRead()
    {
        try
        {
            Object[] textureObjects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
            for (int i = 0; i < textureObjects.Length; i++)
            {
                ChangeTextureFormatAccordingToFileInfo(textureObjects[i] as Texture2D);
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    [MenuItem("Assets/MiniTools/图片分割、合并/分割从TexturePacker中导出的图片为Multiply Sprite(双线性变换)")]
    public static void SpriteInfoReadWithAntiAliasing()
    {
        try
        {
            Object[] textureObjects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
            for (int i = 0; i < textureObjects.Length; i++)
            {
                ChangeTextureFormatAccordingToFileInfo(textureObjects[i] as Texture2D, FilterMode.Bilinear);
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    private static List<Tools_TPSprite_Format> sprites;//精灵列表
    private static Vector2 textureSize;
    private static float textureBorderSize;//图片的边长
    private static float count = 0;

    /// <summary>
    /// 根据信息文件更改图片的格式
    /// </summary>
    /// <param name="tex"></param>
    private static void ChangeTextureFormatAccordingToFileInfo(Texture2D tex, FilterMode filtermode = FilterMode.Point)
    {
        if (ReadTextureFile(tex))
        {
            ChangeTextureFormat(tex, filtermode);
        }
    }

    /// <summary>
    /// 读取图片的信息文件
    /// </summary>
    /// <param name="tex"></param>
    /// <returns></returns>
    private static bool ReadTextureFile(Texture2D tex)
    {
        if (tex == null)
        {
            Debug.Log("图片为空");
            return false;
        }
        JsonData data;
        try
        {
            string textPath = AssetDatabase.GetAssetPath(tex).Substring(0, AssetDatabase.GetAssetPath(tex).LastIndexOf('/') + 1) + tex.name + ".txt";
            FileStream fs = File.Open(textPath, FileMode.Open, FileAccess.Read);
            byte[] byteArray = new byte[fs.Length];
            string textStr;
            fs.Read(byteArray, 0, (int)fs.Length);
            //以默认编码格式读取文本
            textStr = Encoding.Default.GetString(byteArray, 0, (int)fs.Length);
            fs.Close();
            data = JsonMapper.ToObject(textStr);
        }
        //路径错误,或者文件的编码格式不为ASCII格式(从TexturePacker导出的txt文本均默认为ASCII格式)
        catch (System.Exception)
        {
            Debug.Log("读取" + tex.name + ".txt 信息文件出错");
            return false;
        }
        if (sprites == null)
        {
            sprites = new List<Tools_TPSprite_Format>();
        }
        else
        {
            sprites.Clear();
        }
        JsonData framesData = data["frames"];
        JsonData metaData = data["meta"];
        //精灵信息获取
        textureSize = new Vector2(int.Parse(metaData["size"]["w"].ToString()), int.Parse(metaData["size"]["h"].ToString()));
        textureBorderSize = Mathf.Max(textureSize.x, textureSize.y);
        count = 0;
        foreach (string frameName in framesData.Keys)
        {
            string spriteName = frameName.Substring(0, frameName.LastIndexOf('.'));
            JsonData dataTemp = framesData[frameName];
            int frameX = int.Parse(dataTemp["frame"]["x"].ToString());
            int frameY = int.Parse(dataTemp["frame"]["y"].ToString());
            int frameW = int.Parse(dataTemp["frame"]["w"].ToString());
            int frameH = int.Parse(dataTemp["frame"]["h"].ToString());
            int sssX = int.Parse(dataTemp["spriteSourceSize"]["x"].ToString());
            int sssY = int.Parse(dataTemp["spriteSourceSize"]["y"].ToString());
            int sssW = int.Parse(dataTemp["spriteSourceSize"]["w"].ToString());
            int sssH = int.Parse(dataTemp["spriteSourceSize"]["h"].ToString());
            int ssW = int.Parse(dataTemp["sourceSize"]["w"].ToString());
            int ssH = int.Parse(dataTemp["sourceSize"]["h"].ToString());
            //TexturePacker导出的坐标为以图片的左上为原点,精灵的左上角坐标,Unity的Multiply格式图片的精灵为以图片的左下为原点,精灵的左下角坐标,故转化时需要将图片大小减去精灵y坐标再减去精灵的高度作为精灵的y坐标
            Tools_TPSprite_Format format = new Tools_TPSprite_Format(spriteName, new Rect(frameX, textureSize.y - frameH - frameY, frameW, frameH), new Rect(sssX, ssH - sssH - sssY, sssW, sssH), new Vector2(ssW, ssH));
            sprites.Add(format);
            count++;
            EditorUtility.DisplayProgressBar("正在读取精灵数据", frameName, count / framesData.Count);
        }
        return true;
    }

    /// <summary>
    /// 改变图片格式
    /// </summary>
    /// <param name="tex"></param>
    private static void ChangeTextureFormat(Texture2D tex, FilterMode filtermode = FilterMode.Point)
    {
        //首先获取图片的原始尺寸(不超过4096)
        TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(tex));
        //设置图片格式
        textureImporter.textureType = TextureImporterType.Sprite;//图片类型设置为精灵
        textureImporter.spritePivot = new Vector2(0.5f, 0.5f);
        textureImporter.spritePixelsPerUnit = 100;
        textureImporter.mipmapEnabled = false;
        textureImporter.filterMode = filtermode;
        textureImporter.spriteImportMode = SpriteImportMode.Multiple;
        textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
        textureImporter.maxTextureSize = (int)textureBorderSize;//设置图片最大大小
        //设置精灵
        List<SpriteMetaData> metaList = new List<SpriteMetaData>();
        List<SpriteMetaData> originList;
        if (textureImporter.spritesheet == null || textureImporter.spritesheet.Length > 0)
        {
            originList = new List<SpriteMetaData>(textureImporter.spritesheet);
        }
        else
        {
            originList = new List<SpriteMetaData>();
        }
        EditorUtility.DisplayProgressBar("正在配置精灵", "", 0.3f);
        for (int i = 0; i < sprites.Count; i++)
        {
            SpriteMetaData data = sprites[i].ConvertToSpriteMetaData();
            //若原精灵列表中已经包含对应名字的精灵,则不进行替换,而是更改精灵的信息,保证替换图片前后Sprite的引用不变
            for (int j = 0; j < originList.Count; j++)
            {
                if (originList[j].name == data.name)
                {
                    SpriteMetaData dataTemp = originList[j];
                    //dataTemp.border = data.border;
                    dataTemp.alignment = data.alignment;
                    dataTemp.pivot = data.pivot;
                    dataTemp.rect = data.rect;
                    data = dataTemp;
                }
            }
            metaList.Add(data);
            count++;
        }
        EditorUtility.DisplayProgressBar("正在配置精灵", "", 0.5f);
        textureImporter.spritesheet = metaList.ToArray();
        EditorUtility.DisplayProgressBar("正在配置精灵", "", 0.8f);
        //保存格式更改
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tex));
        Debug.Log("图片  " + tex.name + "  格式处理完毕,分割出" + metaList.Count + "张精灵");
    }

    /// <summary>
    /// 单张精灵格式
    /// </summary>
    public class Tools_TPSprite_Format
    {
        public string spriteName;
        public Rect rect;
        public Rect sourceRect;
        public Vector2 sourceSize;

        public Tools_TPSprite_Format(string spriteName, Rect rect, Rect sourceRect, Vector2 sourceSize)
        {
            this.spriteName = spriteName;
            this.rect = rect;
            this.sourceRect = sourceRect;
            this.sourceSize = sourceSize;
        }

        /// <summary>
        /// 转化为精灵数据
        /// </summary>
        /// <returns></returns>
        public SpriteMetaData ConvertToSpriteMetaData()
        {
            SpriteMetaData dataTemp = new SpriteMetaData();
            dataTemp.name = spriteName;
            dataTemp.border = Vector4.zero;
            dataTemp.alignment = 9;
            dataTemp.pivot = new Vector2(0.5f - (sourceRect.x + sourceRect.width / 2f - sourceSize.x / 2) / sourceRect.width, 0.5f - (sourceRect.y + sourceRect.height / 2f - sourceSize.y / 2) / sourceRect.height);
            dataTemp.rect = rect;
            return dataTemp;
        }

        public override string ToString()
        {
            return string.Format("精灵名称: {0} , 位置: {1}", spriteName, rect.ToString());
        }
    }
}
