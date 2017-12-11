using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プ：東　ゲーム全体のシーン管理クラス
/// </summary>
/// <remarks>
/// SingletonMonoBehaviorクラス継承
/// </remarks>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private const int TITLE  = 0;
    private const int MAIN   = 1;
    private const int RESULT = 2;

    public int nowScene = 0;

	private void Awake()
	{
		//SceneManagerを登録
		if (this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

		//アプリケーションのフレームレートを固定
		Application.targetFrameRate = 60;
	}

    public void TitleSceneLoad()
    {
        SceneManager.LoadScene("Title");
    }

    public void MainSceneLoad()
    {
        SceneManager.LoadScene("MainStage");
    }

    public void ResultSceneLoad()
    {
        SceneManager.LoadScene("Result");
    }
}
