using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheckRigid : MonoBehaviour
{
	[SerializeField]
	CapsuleCollider capusule;

	[SerializeField]
	MainManager mainManager;

	Rigidbody rigid;

	void Start()
	{
		if (mainManager == null)
			mainManager = GameObject.Find ("MainManager").GetComponent<MainManager> ();

		rigid = GetComponent<Rigidbody> ();
		capusule = GetComponent<CapsuleCollider> ();
	}

	void Update()
	{
		if(mainManager == null)
			mainManager = GameObject.Find ("MainManager").GetComponent<MainManager> ();

		if (rigid.velocity.magnitude < 0.1f)
			mainManager.ClearGame ();


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
