using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　リザルトシーン管理クラス
/// </summary>
public class ResultManager : MonoBehaviour
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
            SceneBack();
    }

    private void SceneInit()
    {
    }

    private void SceneBack()
    {
        game.LoadScene("Title");
    }
}
