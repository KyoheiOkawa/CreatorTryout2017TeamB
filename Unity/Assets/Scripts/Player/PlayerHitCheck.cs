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

    private Rigidbody rigid;

	void Start()
	{
		if (mainManager == null)
			mainManager = GameObject.Find ("MainManager").GetComponent<MainManager> ();

        rigid = GetComponent<Rigidbody>();
	}

    void Update()
    {
        if (mainManager == null)
            mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        if (mainManager.NowState == MainManager.State.Grounded)
        {
            HitCheckCrazyZoneAfterGrouded();

            if (rigid.velocity.magnitude < 0.1f)
                mainManager.ClearGame();
        }

        if (mainManager.NowState != MainManager.State.Playing)
            return;

		if(transform.position.y < 5.0f)
			Camera.main.GetComponent<CameraScript> ().ClearClose ();

        Vector3 start = transform.position + capusule.center + transform.up * (capusule.height / 2.0f);
        Vector3 end = transform.position + capusule.center + transform.up * -(capusule.height / 2.0f);

        var cols = Physics.OverlapCapsule(start, end, capusule.radius);

        foreach (var col in cols)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
				mainManager.Ground ();

                float angle = Vector3.Angle(transform.up, Vector3.right);

                if (angle <= successAngle)
                {
                    PlayerController.Instance.isMove = false;
                    mainManager.Ground();

                    rigid.useGravity = true;
                    rigid.velocity = transform.up * 15;
                }
                else
                {
					AudioManager.Instance.PlaySE ("bomb", 0.5f);
                    mainManager.FailedGame(transform.position);
                    gameObject.SetActive(false);
                }
            }

            if (col.gameObject.CompareTag("CrazyZone"))
            {
				AudioManager.Instance.PlaySE ("bomb", 0.5f);
                mainManager.FailedGame(transform.position);
                gameObject.SetActive(false);
            }

            if (col.gameObject.CompareTag("Enemy"))
            {
				AudioManager.Instance.PlaySE ("bomb", 0.5f);
                mainManager.FailedGame(transform.position);
                gameObject.SetActive(false);
            }
        }
    }

    void HitCheckCrazyZoneAfterGrouded()
    {
        Vector3 start = transform.position + capusule.center + transform.up * (capusule.height / 2.0f);
        Vector3 end = transform.position + capusule.center + transform.up * -(capusule.height / 2.0f);

        var cols = Physics.OverlapCapsule(start, end, capusule.radius);

        foreach (var col in cols)
        {
            if (col.gameObject.CompareTag("CrazyZone"))
            {
				AudioManager.Instance.PlaySE ("bomb", 0.5f);
                mainManager.FailedGame(transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
