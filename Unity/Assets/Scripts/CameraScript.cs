using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	void Update () 
	{
        transform.position = PlayerController.Instance.transform.position - new Vector3(-4, 0, 10);
    }
}
