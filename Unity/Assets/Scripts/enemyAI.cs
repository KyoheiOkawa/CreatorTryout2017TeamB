using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject player;

    // プレイヤーからどのくらい離れたら位置のリセットを行うか
    public float Resetdistance = 5.0f;

    // プレイヤーからどのくらい先に場所の設定をするか
    private float Posx = 15.0f;

    private Vector3 Speed = new Vector3(0.1f, 0.0f, 0.0f);

    // 最低速度
    public float MinSpeed = 0.1f;

    // 最大速度
    public float MaxSpeed = 0.1f;

    // 最低何秒後にリセットするか
    public float ResetMinTime = 5;

    // 最大何秒後にリセットするか
    public float ResetMaxTime = 5;

    // 最低の高さ
    public float MinHeight = -5.0f;

    // 最高の高さ
    public float MaxHeight = 5.0f;

    // カウント用変数
    private float time;

    // 移動フラグ
    private bool IsMove;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(Posx, Random.Range(MinHeight, MaxHeight), 0.0f) + player.transform.position;

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), 0.0f, 0.0f);

        time = Random.Range(ResetMinTime, ResetMaxTime);

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
            time -= Time.deltaTime;

            if (time < 0)
            {
                IsMove = true;
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
        if (player == null) return;

        // プレイヤーより手前に自分がいたら返す
        if (this.transform.position.x > player.transform.position.x) return;

        // -5.0 ～ 5.0内のランダム数を高さに設定する
        // Random.Range(-5.0f, 5.0f), 0.0f)
        this.transform.position = new Vector3(Posx, Random.Range(MinHeight, MaxHeight), 0.0f) + player.transform.position;

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), 0.0f, 0.0f);

        time = Random.Range(ResetMinTime, ResetMaxTime);

        IsMove = false;
    }

    void OnBecameInvisible()
    {
        if (this != null )
            NewValueSet();
    }
}
