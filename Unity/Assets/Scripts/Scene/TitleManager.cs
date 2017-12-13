using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　タイトルシーン管理クラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    private GameManager     game;
    private AudioManager    audio;

	bool isChanged = false;

	private void Start()
	{
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audio.PlayBGM("title",0.1f,true);
	}

	private void Update()
	{
        SceneUpdate();
    }

	private void SceneUpdate()
	{
		if (Input.GetMouseButtonDown (0) && !isChanged)
		{
			SceneNext ();
			isChanged = true;
		}
    }

    private void SceneNext()
    {
		FadeManager.Instance.Transition (0.5f, "Result");
    }

    private void GameEnd()
    {
        Application.Quit();
    }
}
