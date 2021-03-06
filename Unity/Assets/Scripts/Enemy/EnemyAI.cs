﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //public GameObject Player;

    // プレイヤーからどのくらい離れたら位置のリセットを行うか
    public float ResetDistance = 6.0f;

    // プレイヤーからどのくらい先に場所の設定をするか
    private float PosX = 35.0f;

    // 速度
    private Vector3 Speed ;

    // 最低速度
    public float MinSpeed = 3f;

    // 最大速度
    public float MaxSpeed = 8f;

    // 最低何秒後にリセットするか
    public float ResetMinTime = 0;

    // 最大何秒後にリセットするか
    public float ResetMaxTime = 3;

    // 最低の高さ
    public float MinHeight = -7.0f;

    // 最高の高さ
    public float MaxHeight = -3.0f;

    // カウント用変数
    private float Cnt;

    // 移動フラグ
    private bool IsMove = false;

    // 何秒後にリセットするか
    private float ResetCnt = 5;

    // スタート秒数
    public float StartTime = 3;

    // スタートフラグ
    private bool StartFlag = false;

    // エンドフラグ
    private bool EndFlag = false;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), 0.0f, 0.0f);

        Cnt = Random.Range(ResetMinTime, ResetMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartFlag)
        {
            if (StartTime > 0)
            {
                StartTime -= Time.deltaTime;
            }
            else if (StartTime <= 0)
            {
                IsMove = true;

                this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight), 0.0f) + PlayerController.Instance.transform.position;

                StartFlag = true;
            }
        }
        else
        {
            if (IsMove)
            {
                Move();

                ResetCnt -= Time.deltaTime;

                //if (ResetCnt < 0 && PlayerController.Instance.Feed > 15.0f)
                //    NewValueSet();
            }
            else if (!EndFlag)
            {
                //1秒に1ずつ減らしていく
                Cnt -= Time.deltaTime;

                if (Cnt < 0)
                {
                    IsMove = true;

                    if (PlayerController.Instance == null) return;
                    // MinHeight ～ MaxHeight内のランダム数を高さに設定する
                    // Random.Range(MinHeight, MaxHeight), 0.0f)
                    this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight), 0.0f) + PlayerController.Instance.transform.position;
                }
            }
        }
    }

    // 移動処理
    void Move()
    {
        // ポジションに速度を足す
        this.transform.position -= Speed * Time.deltaTime;
    }

    // ランダムで出した新しい値を変数に設定する
    void NewValueSet()
    {
        // プレイヤーがnullの場合は返す
        if (PlayerController.Instance == null) return;

        // プレイヤーより手前に自分がいたら返す
        if (this.transform.position.x > PlayerController.Instance.transform.position.x + ResetDistance) return;

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), 0.0f, 0.0f);

        Cnt = Random.Range(ResetMinTime, ResetMaxTime);

        IsMove = false;

        ResetCnt = 3;
    }

    void OnBecameInvisible()
    {
        if (this != null || PlayerController.Instance.Feed > 15.0f)
            NewValueSet();
        else
        {
            IsMove = false;

            EndFlag = true;
        }
    }
}
