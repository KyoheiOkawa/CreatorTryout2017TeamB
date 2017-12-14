using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour 
{
	[SerializeField]
	string titleSceneName;

	[SerializeField]
	Image panelImg;

	[SerializeField]
	Button retryButton;

	[SerializeField]
	Button titleButton;

	[SerializeField]
	float fadeTime = 0.5f;

	void Start()
	{
		retryButton.enabled = false;
		retryButton.enabled = false;
		panelImg.color = new Color(1,1,1,0);

		StartCoroutine (PanelFade ());
	}

	public void OnRetryButton()
	{
		Scene scene;
		scene = SceneManager.GetActiveScene ();

		FadeManager.Instance.Transition (0.5f, scene.name,0.2f);
		FadeManager.Instance.SetRayCastBlock (false);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySE("enter",0.1f);
    }

	public void OnTitleButton()
	{
		FadeManager.Instance.Transition(0.5f,titleSceneName,0.2f);
		FadeManager.Instance.SetRayCastBlock (false);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySE("enter", 0.1f);
    }

	IEnumerator PanelFade()
	{
		while (true)
		{
			Color color = panelImg.color;
			color.a += Time.deltaTime * fadeTime;
			panelImg.color = color;

			if (panelImg.color.a >= 1.0f) 
			{
				panelImg.color = new Color(1,1,1,1);
				break;
			}

			yield return null;
		}

		retryButton.enabled = true;
		retryButton.enabled = true;
	}
}
