using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	[SerializeField]
	private GameObject player;

	void Start () 
	{
		
	}

	void Update () 
	{
		transform.position = player.transform.position - new Vector3 (-4, 0, 10);
	}

	public void SetTargetObject(GameObject target)
	{
		player = target;
	}
}
