using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　タイトルシーン管理クラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    private GameManager game;

	private void Start()
	{
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
        SceneUpdate();
    }

	private void SceneUpdate()
	{
        if (Input.GetMouseButtonDown(0))
            SceneNext();
    }

    private void SceneNext()
    {
        game.LoadScene("Result");
    }

    private void GameEnd()
    {
        Application.Quit();
    }
}
