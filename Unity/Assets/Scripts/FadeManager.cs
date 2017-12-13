using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : Graphic
{
	[SerializeField, Range(0, 1)]
	float fadeRange = 0;

	[SerializeField]
	Texture maskTexture = null;

	[SerializeField]
	Texture fadeTexture = null;

	static FadeManager instance;

	static public FadeManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

				if (instance == null)
				{
					GameObject obj = Instantiate(Resources.Load("FadeCanvas") as GameObject);
					instance = obj.GetComponent<FadeManager>();
				}
			}

			return instance;
		}
	}

	public override Texture mainTexture
	{
		get
		{
			return fadeTexture;
		}
	}

	void Awake()
	{
		if (this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		UpdateUniform();
	}


	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();

		var rTrans = GetComponent<RectTransform>();

		// 左上
		UIVertex lt = UIVertex.simpleVert;
		lt.position = new Vector3(rTrans.rect.x, rTrans.rect.yMax, 0);
		lt.uv0 = new Vector2(0, 1);

		// 右上
		UIVertex rt = UIVertex.simpleVert;
		rt.position = new Vector3(rTrans.rect.xMax, rTrans.rect.yMax, 0);
		rt.uv0 = new Vector2(1, 1);

		// 右下
		UIVertex rb = UIVertex.simpleVert;
		rb.position = new Vector3(rTrans.rect.xMax, rTrans.rect.y, 0);
		rb.uv0 = new Vector2(1, 0);

		// 左下
		UIVertex lb = UIVertex.simpleVert;
		lb.position = new Vector3(rTrans.rect.x, rTrans.rect.y, 0);
		lb.uv0 = new Vector2(0, 0);


		vh.AddUIVertexQuad(new UIVertex[]
			{
				lb, rb, rt, lt
			});
	}

	public void Transition(float time, string transSceneName,float fadeBeginTime = 0.0f)
	{
		StartCoroutine(FadeWithSceneChange(time, transSceneName,fadeBeginTime));
	}

	IEnumerator FadeWithSceneChange(float time, string nextSceneName,float fadeBeginTime = 0.0f)
	{
		SetRayCastBlock(false);

		yield return new WaitForSeconds (fadeBeginTime);

		while (true)
		{
			fadeRange += Time.deltaTime / time;
			UpdateUniform();

			if (fadeRange >= 1)
			{
				GameManager.Instance.LoadScene (nextSceneName);

				while (GameManager.Instance.IsLoadingNextScene)
				{
					yield return new WaitForEndOfFrame();
				}

				SetRayCastBlock(false);

				fadeRange = 1.0f;

				break;
			}

			yield return null;
		}

		while (true)
		{
			fadeRange -= Time.deltaTime / time;
			UpdateUniform();

			if (fadeRange <= 0)
			{
				fadeRange = 0;

				SetRayCastBlock(true);

				break;
			}

			yield return null;
		}
	}

	/// <summary>
	/// シーン上のCanvasGroupのraycastBlockの値をセットする
	/// </summary>
	/// <param name="b">If set to <c>true</c> b.キャンバス上のボタンなどが反応する</param>
	public void SetRayCastBlock(bool b)
	{
		CanvasGroup[] group = GameObject.FindObjectsOfType<CanvasGroup>() as CanvasGroup[];
		foreach (CanvasGroup obj in group)
			obj.blocksRaycasts = b;
	}

	public void UpdateUniform()
	{
		material.SetTexture("_MainTex", fadeTexture);
		material.SetTexture("_MaskTex", maskTexture);
		material.SetFloat("_Fade", fadeRange);
		material.SetColor("_Color", color);
	}

	#if UNITY_EDITOR
	protected override void OnValidate()
	{
		base.OnValidate();

		UpdateUniform();
	}
	#endif
}