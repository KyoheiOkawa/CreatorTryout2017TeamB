using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerHitCheck : MonoBehaviour 
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

		if (mainManager.NowState != MainManager.State.Playing)
			return;

		Vector3 start = transform.position + capusule.center + transform.up * (capusule.height / 2.0f);
		Vector3 end = transform.position + capusule.center + transform.up * -(capusule.height / 2.0f);

		var cols = Physics.OverlapCapsule (start, end, capusule.radius);

		foreach (var col in cols) 
		{
			if (col.gameObject.CompareTag ("Ground"))
			{
				float angle = Vector3.Angle (transform.up, Vector3.right);

				if (angle <= successAngle) 
				{
					mainManager.ClearGame ();

					var rigidAirplane = Instantiate (Resources.Load ("AirplaneRigid"),transform.position,transform.rotation) as GameObject;
					Camera.main.GetComponent<CameraScript> ().SetTargetObject (rigidAirplane.gameObject);

					rigidAirplane.GetComponent<Rigidbody> ().velocity = transform.up * 20;
				} 
				else 
				{
					mainManager.FailedGame (transform.position);
				}

				gameObject.SetActive (false);
			}

			if (col.gameObject.CompareTag ("Enemy")) 
			{
				mainManager.FailedGame (transform.position);
				gameObject.SetActive (false);
			}
		}
	}
}
