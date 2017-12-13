using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　PlayerIconクラス
/// </summary>
public class PlayerIcon : MonoBehaviour
{
    private GameObject player;
    Vector3 spherePos;

	void Start ()
    {
        player = GameObject.Find("Airplane");

        spherePos = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = spherePos;
	}

	void Update ()
    {
        spherePos.x = player.transform.position.x-9.0f;
        this.transform.position = spherePos;
    }
}
