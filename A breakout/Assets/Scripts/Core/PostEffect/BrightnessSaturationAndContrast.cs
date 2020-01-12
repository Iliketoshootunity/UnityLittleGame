using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{

    public class BrightnessSaturationAndContrast : PostEffectsBase
    {
        //绑定的shader
        public Shader BriSatConShader;
        private Material m_BriSatConMaterial;
        public Material material
        {
            get
            {
                //调用基类的CheckShaderAndCreateMaterial方法绑定shader与Material
                m_BriSatConMaterial = CheckShaderAndCreateMaterial(BriSatConShader, m_BriSatConMaterial);
                return m_BriSatConMaterial;
            }
        }
        //亮度值
        [Range(0.0f, 3.0f)]
        public float Brightness = 1.0f;

        //饱和度
        [Range(0.0f, 3.0f)]
        public float Saturation = 1.0f;
        //对比度
        [Range(0.0f, 3.0f)]
        public float Contrast = 1.0f;
        //重写OnRenderImage方法
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (material != null)
            {
                //设置shader中的各个值
                material.SetFloat("_Brightness", Brightness);
                material.SetFloat("_Saturation", Saturation);
                material.SetFloat("_Contrast", Contrast);
                //将源纹理通过material处理，复制到目标纹理中
                Graphics.Blit(src, dest, material);
            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }
    }
}
