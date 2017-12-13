using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;

    // プレイヤーからどのくらい離れたら位置のリセットを行うか
    public float ResetDistance = 5.0f;

    // プレイヤーからどのくらい先に場所の設定をするか
    private float PosX = 15.0f;

    private Vector3 Speed ;

    // 最低速度
    public float MinSpeed = 0.1f;

    // 最大速度
    public float MaxSpeed = 0.5f;

    // 最低何秒後にリセットするか
    public float ResetMinTime = 3;

    // 最大何秒後にリセットするか
    public float ResetMaxTime = 5;

    // 最低の高さ
    public float MinHeight = -6.0f;

    // 最高の高さ
    public float MaxHeight = 0.0f;

    // カウント用変数
    public float Cnt;

    // 移動フラグ
    private bool IsMove;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight), 0.0f) + Player.transform.position;

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), 0.0f, 0.0f);

        Cnt = Random.Range(ResetMinTime, ResetMaxTime);

        IsMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMove)
            Move();
        else
        {
            //1秒に1ずつ減らしていく
            Cnt -= Time.deltaTime;

            if (Cnt < 0)
            {
                IsMove = true;

                this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight), 0.0f) + Player.transform.position;
            }
        }
    }

    // 移動処理
    void Move()
    {
        // ポジションに速度を足す
        this.transform.position -= Speed;
    }

    // ランダムで出した新しい値を変数に設定する
    void NewValueSet()
    {
        // プレイヤーがnullの場合は返す
        if (Player == null) return;

        // プレイヤーより手前に自分がいたら返す
        if (this.transform.position.x > Player.transform.position.x + ResetDistance) return;

        // MinHeight ～ MaxHeight内のランダム数を高さに設定する
        // Random.Range(MinHeight, MaxHeight), 0.0f)
        //this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight), 0.0f) + Player.transform.position;

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), 0.0f, 0.0f);

        Cnt = Random.Range(ResetMinTime, ResetMaxTime);

        IsMove = false;
    }

    void OnBecameInvisible()
    {
        if (this != null )
            NewValueSet();
    }
}
