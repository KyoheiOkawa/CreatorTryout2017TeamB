using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	[SerializeField]
	Vector3 offset = new Vector3(4, 0, -10);

	[SerializeField]
	Vector3 clearCloseOffset = new Vector3(0,3,-10);

	bool isClose = false;
	void Start()
	{
		transform.position = PlayerController.Instance.transform.position + offset;
	}

	void Update () 
	{
		if (isClose)
			return;

		Vector3 targetPos = PlayerController.Instance.transform.position;
		Vector3 nextPos = targetPos + offset;

		Vector3 currentVel = new Vector3 ();
		Vector3 newPos = Vector3.SmoothDamp(transform.position,nextPos,ref currentVel,0.2f);

		transform.position = newPos;
    }

	public void ClearClose()
	{
		if (isClose)
			return;

		transform.LookAt (PlayerController.Instance.transform.position);
		StartCoroutine (ClearCloseCoroutine ());

		isClose = true;

		Debug.Log ("clearClose");
	}

	IEnumerator ClearCloseCoroutine()
	{
		var player = PlayerController.Instance;
		Vector3 playerPos = player.transform.position;
		Vector3 toPos = playerPos + clearCloseOffset;
		Vector3 currentVel = new Vector3 ();

		while (true) 
		{
			playerPos = player.transform.position;
			toPos = playerPos + clearCloseOffset;

			transform.LookAt (playerPos);

			Vector3 newPos = Vector3.SmoothDamp (transform.position, toPos, ref currentVel,0.5f);

			transform.position = newPos;

			if (currentVel.magnitude < 0.1f)
				break;

			yield return null;
		}
	}
}
