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
	bool isLoadingNextScene = false;

	public bool IsLoadingNextScene 
	{
		get 
		{
			return isLoadingNextScene;
		}
	}

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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCouroutine(sceneName));
		isLoadingNextScene = true;
    }

    private IEnumerator LoadSceneCouroutine(string nextScene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;    // シーン遷移をしない

        Debug.Log("Next Scene " + nextScene);

        while (async.progress < 0.9f)
        {
            //Debug.Log(async.progress);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        async.allowSceneActivation = true;    // シーン遷移許可
		isLoadingNextScene = false;
    }
}
