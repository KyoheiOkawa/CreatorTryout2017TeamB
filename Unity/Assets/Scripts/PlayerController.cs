using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float downSpeed = -10f;
    [SerializeField]
    private float moveSpeed = 10f;
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
        // ジャイロを有効にする
        Input.gyro.enabled = true;
    }

    void Update()
    {
        CheckGround();
        if (isMove)
        {
            Move();
        }
    }

    // 高度をチェックする
    void CheckGround()
    {
        feed = Mathf.Abs(transform.position.y - groundObj.transform.position.y);
    }

    void Move()
    {
		// デバイスの傾きを取得
		deviceRotation = Input.gyro.attitude;

		//デバイスの傾きを反映
		transform.rotation = new Quaternion (0, 0, deviceRotation.z, deviceRotation.w);

        if (transform.localEulerAngles.z >= 320 || transform.localEulerAngles.z <= 90)
        {
            transform.localEulerAngles = new Vector3(0, 0, 320);
        }
        else if (90 > transform.localEulerAngles.z || transform.localEulerAngles.z <= 200)
        {
            transform.localEulerAngles = new Vector3(0, 0, 200);
        }

		downSpeed = Mathf.Abs (transform.rotation.z) / -0.7f * 5.0f;

        
        // 機体の移動
        transform.position += new Vector3(moveSpeed, downSpeed, 0) * Time.deltaTime;
    }
}
