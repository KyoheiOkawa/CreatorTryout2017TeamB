using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　メインシーン管理クラス
/// </summary>
public class MainManager : MonoBehaviour
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
    }

    private void SceneInit()
    {
    }

    private void SceneClear()
    {
        game.ResultSceneLoad();
    }

    private void SceneOver()
    {
        game.MainSceneLoad();
    }
}
