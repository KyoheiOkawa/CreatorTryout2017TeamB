using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondum : MonoBehaviour {

    public GameObject[] Train;

    // Use this for initialization
    void Start () {
        var number = Random.Range(0, Train.Length);
        Instantiate(Train[number], transform.position, transform.rotation);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
