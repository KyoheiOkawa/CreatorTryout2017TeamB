using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirplaneDown : MonoBehaviour {

    // プレイヤーからどのくらい離れたら位置のリセットを行うか
    public float ResetDistance = 6.0f;

    // プレイヤーからどのくらい先に場所の設定をするか
    private float PosX = 22.0f;

    // プレイヤーからどのくらい上に場所の設定をするか
    private float PosY = 20.0f;

    private Vector3 Speed;

    // 最低速度
    public float MinSpeed = 1f;

    // 最大速度
    public float MaxSpeed = 5f;

    // 最低落下速度
    public float MinDownSpeed = 10f;

    // 最大落下速度
    public float MaxDownSpeed = 16f;

    // 最低何秒後にポジションリセットするか
    public float ResetMinTime = 3;

    // 最大何秒後にポジションリセットするか
    public float ResetMaxTime = 5;

    // 最低の高さ
    public float MinHeight = -7.0f;

    // 最高の高さ
    public float MaxHeight = -3.0f;

    // カウント用変数
    private float Cnt;

    // 何秒後にリセットするか
    private float ResetCnt = 5;

    // 移動フラグ
    private bool IsMove = false;

    // スタート秒数
    public float StartTime = 5;

    // スタートフラグ
    private bool StartFlag = false;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight) + PosY, 0.0f) + PlayerController.Instance.transform.position;

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), Random.Range(MinDownSpeed, MaxDownSpeed), 0.0f);

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

                this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight) + PosY, 0.0f) + PlayerController.Instance.transform.position;

                StartFlag = true;
            }
        }
        else
        {
            if (IsMove)
            {
                Move();

                ResetCnt -= Time.deltaTime;

                if (ResetCnt < 0 && PlayerController.Instance.Feed > 30.0f)
                    NewValueSet();
            }
            else
            {
                //1秒に1ずつ減らしていく
                Cnt -= Time.deltaTime;

                if (Cnt < 0)
                {
                    IsMove = true;

                    // MinHeight ～ MaxHeight内のランダム数を高さに設定する
                    // Random.Range(MinHeight, MaxHeight), 0.0f)
                    this.transform.position = new Vector3(PosX, Random.Range(MinHeight, MaxHeight) + PosY, 0.0f) + PlayerController.Instance.transform.position;
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

        Speed = new Vector3(Random.Range(MinSpeed, MaxSpeed), Random.Range(MinDownSpeed, MaxDownSpeed), 0.0f);

        Cnt = Random.Range(ResetMinTime, ResetMaxTime);

        IsMove = false;

        ResetCnt = 3;
    }

    //void OnBecameInvisible()
    //{
    //    if (this != null || PlayerController.Instance.Feed > 15.0f)
    //        NewValueSet();
    //}
}
