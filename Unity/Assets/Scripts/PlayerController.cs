using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float downSpeed = -0.1f;
	private Quaternion deviceRotation;

	public static PlayerController Instance;
	void Awake()
	{
		if (null != Instance) {
			Destroy (gameObject);
		}
		else
		{
			Instance = this;
		}

		//DontDestroyOnLoad (gameObject);
	}

	void Start () 
	{
		// ジャイロを有効にする
		Input.gyro.enabled = true;
	}

	void Update () 
	{
		Move ();
	}



	void Move()
	{
		// デバイスの傾きを取得
		deviceRotation = Input.gyro.attitude;

		// デバイスの傾きを反映
		transform.rotation = new Quaternion(0,0,deviceRotation.z,deviceRotation.w);

		//Debug.Log (transform.rotation);

		//Debug.Log ((0.5f - deviceRotation.z));

		/*
		if ((0.5f - deviceRotation.z) >= 0 && (0.5f - deviceRotation.z) < 0.1f) 
		{
			downSpeed = -1;
		}
		else if ((0.5f - deviceRotation.z) <= 0 && (0.5f - deviceRotation.z) > -0.1f) 
		{
			downSpeed = -1;
		}
		else if ((0.5f - deviceRotation.z) >= 0.1f && (0.5f - deviceRotation.z) < 0.2f) 
		{
			downSpeed = -0.75f;
		}
		else if ((0.5f - deviceRotation.z) >= 0.2f && (0.5f - deviceRotation.z) < 0.3f) 
		{
			downSpeed = -0.5f;
		}
		else if ((0.5f - deviceRotation.z) <= -0.1f && (0.5f - deviceRotation.z) > -0.2f) 
		{
			downSpeed = -1.25f;
		}
		else if ((0.5f - deviceRotation.z) <= -0.2f && (0.5f - deviceRotation.z) > -0.3f) 
		{
			downSpeed = -1.5f;
		}
		*/

		//-------------------------------------------------------------------------
		// 機体の傾きに応じて落ちるスピードを変化
		if (transform.rotation.z <= 0.7f && transform.rotation.z > 0.6f) 
		{
			downSpeed = -0.1f;
		}
		else if (transform.rotation.z >= 0.7f && transform.rotation.z < 0.8f) 
		{
			downSpeed = -0.1f;
		}
		else if (transform.rotation.z <= 0.6f && transform.rotation.z > 0.5f) 
		{
			downSpeed = -0.075f;
		}
		else if (transform.rotation.z <= 0.5f && transform.rotation.z > 0.4f) 
		{
			downSpeed = -0.05f;
		}
		else if (transform.rotation.z >= 0.8f && transform.rotation.z < 0.9f) 
		{
			downSpeed = -0.125f;
		}
		else if (transform.rotation.z >= 0.9f && transform.rotation.z < 1) 
		{
			downSpeed = -0.15f;
		}
		//-------------------------------------------------------------------------

		// 機体の移動
		transform.position += new Vector3 (0.1f, downSpeed, 0);


	}


}
