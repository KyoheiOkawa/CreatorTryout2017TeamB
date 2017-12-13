using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　タイトルシーン管理クラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    private GameManager     game;
    private AudioManager    sound;

	bool isChanged = false;
    bool isFade = false;

	private void Start()
	{
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        sound = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        sound.PlayBGM("title",0.6f,true);
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

            sound.PlaySE("fade",0.3f);
            sound.PlaySE("enter",0.1f);
        }

        if (isFade)
        {
            sound.VolumeDownBGM(0.00065f);
        }
    }

    private void SceneNext()
    {
		    FadeManager.Instance.Transition (3.0f, "Main");
        isFade = true;
    }

    private void GameEnd()
    {
        Application.Quit();
    }
}
