﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startbottun : MonoBehaviour {

    [SerializeField]
    Vector3 m_MaxSize = Vector3.one;
    [SerializeField]
    Vector3 m_MinSize = Vector3.zero;
    [SerializeField]
    float m_AngularFrequency = 1.0f;
    float m_Time;

    // Use this for initialization
    void Awake () {

        m_Time = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {

        m_Time += m_AngularFrequency * Time.deltaTime;
        transform.localScale = Vector3.Lerp(m_MinSize, m_MaxSize, 0.5f * Mathf.Sin(m_Time) + 0.5f);

    }
}
