using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTest : MonoBehaviour 
{
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.A))
			ResultPanelManager.Instance.ShowClearPanel ();

		if (Input.GetKeyDown (KeyCode.S))
			ResultPanelManager.Instance.ShowFailedPanel ();
	}
}
