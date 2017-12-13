using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プ：東　テクスチャの色を変換して出力するクラス
/// </summary>
/// <remarks>
/// UIのImageに付与する
/// </remarks>
public class ColorConverter : MonoBehaviour
{
    // InspectorでSizeと色を指定
    public List<Color> list = new List<Color>();

	void Start ()
    {
        // SpriteTextureの取得
        Texture2D tex = (Texture2D)GetComponent<Image>().sprite.texture;
    
        // 全てのpixcelを取得して入れる配列
        Color[] pixels = tex.GetPixels();

        // 変換後のpixcelを格納する配列
        Color[] draw_Pixcels = new Color[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            Color pixel = list[1];

            if (pixels[i].a == 0.0f)
            {
                Color tmp = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                draw_Pixcels.SetValue(tmp, i);
            }
            else if (pixel == pixels[i])
            {
                // 書き換え用テクスチャのピクセル色を指定
                Color tmp = list[0];
                draw_Pixcels.SetValue(tmp, i);
            }
            else
            {
                Color tmp = list[2];
                draw_Pixcels.SetValue(tmp, i);
            }
        }

        // 書き換え用テクスチャの生成
        Texture2D change_Texture = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
        change_Texture.filterMode = FilterMode.Point;
        change_Texture.SetPixels(draw_Pixcels);
        change_Texture.Apply();

        // テクスチャを貼り替える
        GetComponent<Image>().sprite = Sprite.Create(change_Texture,new Rect(0,0,change_Texture.width,change_Texture.height),Vector2.zero);
    }
	
	void Update ()
    {
		
	}
}
