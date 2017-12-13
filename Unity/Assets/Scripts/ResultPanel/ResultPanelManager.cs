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
		var canvas = GameObject.Find ("Canvas");

		GameObject panel = Instantiate (Resources.Load (panelName) as GameObject);
		panel.transform.parent = canvas.transform;
		panel.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, 0);
	}

	public void ShowClearPanel()
	{
		ShowPanel ("ClearPanel");
	}

	public void ShowFailedPanel()
	{
		ShowPanel ("FailedPanel");
	}
}
