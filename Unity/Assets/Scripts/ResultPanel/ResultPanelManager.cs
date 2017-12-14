using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanelManager : MonoBehaviour 
{
	static ResultPanelManager instance;

	static public ResultPanelManager Instance
	{
		get 
		{
			if (instance == null) 
			{
				instance = (ResultPanelManager)GameObject.FindObjectOfType<ResultPanelManager> ();

				if (instance == null) 
				{
					GameObject obj = Instantiate (Resources.Load ("ResultPanelManager") as GameObject);
					instance = obj.GetComponent<ResultPanelManager> ();
				}
			}

			return instance;
		}
	}

	void ShowPanel(string panelName)
	{
		if (GameObject.FindObjectOfType<ResultPanel> ())
			return;

		var canvas = GameObject.Find ("Canvas");

		GameObject panel = Instantiate (Resources.Load (panelName) as GameObject);
		panel.transform.parent = canvas.transform;
		panel.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, 0);
		panel.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
	}

	public void ShowClearPanel()
	{
		var audio = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
		audio.StopBGM ();
		StartCoroutine (WaitShowClearPanel (3.0f));
		audio.PlaySE ("clear", 0.5f);
	}

	public void ShowFailedPanel()
	{
		StartCoroutine (WaitShowFailedPanel (0.5f));
    }

	IEnumerator WaitShowClearPanel(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		GameObject.Find ("AudioManager").GetComponent<AudioManager> ().PlayBGM("gameclear", 0.5f, true);
		ShowPanel ("ClearPanel");
	}

	IEnumerator WaitShowFailedPanel(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		ShowPanel ("FailedPanel");
		GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayBGM("gameover", 1.0f, true);
	}
}