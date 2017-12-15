using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounmove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float scroll = Mathf.Repeat(Time.time * 0.2f, 1);
        Vector2 offset = new Vector2(scroll, 0);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

}

