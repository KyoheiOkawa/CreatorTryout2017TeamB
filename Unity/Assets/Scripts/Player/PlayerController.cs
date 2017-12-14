using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private float downPow = 5f;
	private float moveSpeed = 5;
	private float downSpeed;
	private float upSpeed;
	private float maxSpeed;

	public float MoveSpeef
	{
		get { return moveSpeed; }
	}
	private float feed;
	public float Feed
	{
		get { return feed; }
	}
	private Quaternion deviceRotation;

	public GameObject groundObj;

	[HideInInspector]
	public bool isMove = true;
	private bool isPlayerUp = false;

	private Rigidbody r;

	public static PlayerController Instance;
	void Awake()
	{
		if (null != Instance)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	void Start()
	{
		r = GetComponent<Rigidbody> ();
		// ジャイロを有効にする
		Input.gyro.enabled = true;
	}

	void Update()
	{
		CheckGround();

		if (isMove) 
		{
			CheckGyro ();
		} 
	}

	void FixedUpdate()
	{
		if (isMove) 
		{
			Move ();
		}
	}

	// 高度をチェックする
	void CheckGround()
	{
		feed = Mathf.Abs(transform.position.y - groundObj.transform.position.y);
	}

	void CheckGyro()
	{
		// デバイスの傾きを取得
		deviceRotation = Input.gyro.attitude;

		//デバイスの傾きを反映
		transform.rotation = new Quaternion (0, 0, deviceRotation.z, deviceRotation.w );

		if (transform.localEulerAngles.z >= 320 || transform.localEulerAngles.z <= 90)
		{
			transform.localEulerAngles = new Vector3(0, 0, 320);
		}
		else if (90 > transform.localEulerAngles.z || transform.localEulerAngles.z <= 200)
		{
			transform.localEulerAngles = new Vector3(0, 0, 200);
		}
	}


	// うまくいかなかったらこっち
	//-----------------------------------------------------------------------
	/*
	void Move()
	{
		// 落下速度を設定（デバイスの傾きによって変化させる）
		downSpeed = Mathf.Abs (transform.rotation.z) / -0.7f * downPow;

		if (Mathf.Abs(downSpeed) <= downPow) 
		{
			if (!isPlayerUp) 
			{
				isPlayerUp = true;
				if (Mathf.Abs(maxSpeed) >= 5.5f)
				{
					upSpeed = Mathf.Abs (maxSpeed);
					StartCoroutine (UpMove ());
					maxSpeed = 0;
				}
			}
		}

		else 
		{
			isPlayerUp = false;
			// 機体の速度を設定する
			r.velocity = new Vector3 (Mathf.Abs(moveSpeed), downSpeed, 0);
			if (maxSpeed > downSpeed) 
			{
				maxSpeed = downSpeed;
			}
		}
	}
	*/
	//-----------------------------------------------------------------------

	void Move()
	{
		// 落下速度を設定（デバイスの傾きによって変化させる）
		downSpeed = Mathf.Abs (transform.rotation.z) / -0.7f * downPow;

		if (transform.localEulerAngles.z >= 250 && transform.localEulerAngles.z <= 290) 
		{
			r.velocity = new Vector3 (moveSpeed, downSpeed, 0);
		}

		if (Mathf.Abs(downSpeed) <= downPow) 
		{
			if (!isPlayerUp) 
			{
				isPlayerUp = true;
				if (Mathf.Abs(maxSpeed) >= 6.0f)
				{
					Debug.Log ("up");
					upSpeed = Mathf.Abs (maxSpeed);
					StartCoroutine (UpMove ());
					maxSpeed = 0;
				}
			}
		}

		else 
		{
			isPlayerUp = false;
			// 機体の速度を設定する
			LateSpeed(downSpeed);
			if (maxSpeed > downSpeed) 
			{
				maxSpeed = downSpeed;
			}
		}
	}

	IEnumerator UpMove()
	{
		yield return new WaitForSeconds (downSpeed);
		while (upSpeed >= downSpeed) 
		{
			LateSpeed (upSpeed);
			upSpeed += downSpeed * 0.1f;
			yield return null;
		}
	}

	void LateSpeed(float value_y )
	{
		r.velocity = new Vector3 (moveSpeed * 0.6f, value_y, 0);
	}

}
