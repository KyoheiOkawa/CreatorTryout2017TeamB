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

		FadeManager.Instance.Transition (0.5f, scene.name,0.2f);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySE("enter",0.1f);
    }

	public void OnTitleButton()
	{
		FadeManager.Instance.Transition(0.5f,titleSceneName,0.2f);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySE("enter", 0.1f);
    }
}
