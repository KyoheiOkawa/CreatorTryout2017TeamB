﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour 
{
	public enum State
	{
		Playing,
		Failed,
		Clear
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

	public void FailedGame(Vector3 particlePos)
	{
		state = State.Failed;

		ResultPanelManager.Instance.ShowFailedPanel ();

		Instantiate (failedEffect, particlePos, Quaternion.identity);
	}

	public void ClearGame()
	{
		state = State.Clear;

		ResultPanelManager.Instance.ShowClearPanel ();
	}
}
