using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheckRigid : MonoBehaviour
{
	[SerializeField]
	CapsuleCollider capusule;

	[SerializeField]
	MainManager mainManager;

	[SerializeField]
	float successAngle = 30.0f;

	void Start()
	{
		if (mainManager == null)
			mainManager = GameObject.Find ("MainManager").GetComponent<MainManager> ();
	}

	void Update()
	{
		if(mainManager == null)
			mainManager = GameObject.Find ("MainManager").GetComponent<MainManager> ();


		Vector3 start = transform.position + capusule.center + transform.up * (capusule.height / 2.0f);
		Vector3 end = transform.position + capusule.center + transform.up * -(capusule.height / 2.0f);

		var cols = Physics.OverlapCapsule (start, end, capusule.radius);

		foreach (var col in cols) 
		{
			if (col.gameObject.CompareTag ("CrazyZone"))
			{
				mainManager.FailedGame (transform.position);
				gameObject.SetActive (false);
			}
		}
	}
}
