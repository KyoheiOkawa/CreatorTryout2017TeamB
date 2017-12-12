using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プ：東　ゲーム全体のシーン管理クラス
/// </summary>
/// <remarks>
/// SingletonMonoBehaviourクラス継承
/// </remarks>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
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
        Debug.Log("Next Scene Title");
        SceneManager.LoadScene("Title");
    }

    public void MainSceneLoad()
    {
        Debug.Log("Next Scene Main");
        SceneManager.LoadScene("MainStage");
    }

    public void ResultSceneLoad()
    {
        Debug.Log("Next Scene Result");
        SceneManager.LoadScene("Result");
    }
}
