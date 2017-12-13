using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour 
{
	[SerializeField]
	string titleSceneName;

	public void OnRetryButton()
	{
		Scene scene;
		scene = SceneManager.GetActiveScene ();

		FadeManager.Instance.Transition (0.5f, scene.name);
	}

	public void OnTitleButton()
	{
		FadeManager.Instance.Transition(0.5f,titleSceneName);
	}
}
