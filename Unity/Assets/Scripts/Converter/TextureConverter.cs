using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// プ：東　PngConverterクラス
/// </summary>
/// <remarks>
/// RenderTextureをPng画像として変換保存するtool
/// </remarks>
public class TextureConverter : MonoBehaviour
{
    //画像タイプ
    public enum ImageType
    {
        PNG,
        JPEG
    }
    public ImageType imageType;

    // ここに対象オブジェクトをドラック＆ドロップ
    public RenderTexture RenderTextureRef;

    private void Update()
    {
        if (imageType == ImageType.PNG)
        {
            SavePng();
        }
        else if (imageType == ImageType.JPEG)
        {
            SaveJpeg();
        }
    }

    // RenderTextureをPngに変換するクラス
    private void SavePng()
    {
        //RenderTextureの情報を取得
        Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.ARGB32, false);
        RenderTexture.active = RenderTextureRef;
        tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
        tex.Apply();

        // Pngにエンコード
        var bytes = tex.EncodeToPNG();
        //破棄
        Destroy(tex);

        //Pngとして書き出し
        File.WriteAllBytes(Application.dataPath + "/Textures/PngConvert.png", bytes);
    }

    // RenderTextureをJpegに変換するクラス
    private void SaveJpeg()
    {
        //RenderTextureの情報を取得
        Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.RGB24, false);
        RenderTexture.active = RenderTextureRef;
        tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
        tex.Apply();

        // Jpegにエンコード
        byte[] bytes = tex.EncodeToJPG();
        Destroy(tex);

        //Jpegとして書き出し
        File.WriteAllBytes(Application.dataPath + "/Textures/JpegConvert.jpeg",bytes);
    }
}