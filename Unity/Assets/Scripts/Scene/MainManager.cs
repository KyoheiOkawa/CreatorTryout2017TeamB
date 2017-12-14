using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour 
{
	public enum State
	{
		Playing,
        Grounded,
		Failed,
		Clear,
	}

	State state = State.Playing;

	public State NowState 
	{
		get 
		{
			return state;
		}
	}

	[SerializeField]
	GameObject failedEffect;
	[SerializeField]
	GameObject failedEffect2;
	[SerializeField]
	Text feedText;

	void Start()
	{
		AudioManager.Instance.PlayBGM ("main", 0.5f, true);
	}

	void Update()
	{
		feedText.text = "地表まで残り約：" + PlayerController.Instance.Feed;
		if (state == State.Grounded || state == State.Failed) 
		{
			feedText.enabled = false;
		}
	}

    public void Ground()
    {
        state = State.Grounded;
    }

	public void FailedGame(Vector3 particlePos)
	{
		state = State.Failed;

		ResultPanelManager.Instance.ShowFailedPanel ();

		Instantiate (failedEffect, particlePos, Quaternion.identity);
		Instantiate (failedEffect2, particlePos, Quaternion.identity);
	}

	public void ClearGame()
	{
		state = State.Clear;

		ResultPanelManager.Instance.ShowClearPanel ();
	}
}
