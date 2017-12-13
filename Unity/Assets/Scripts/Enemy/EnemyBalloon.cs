using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon
public class EnemyBalloon : MonoBehaviour {

    // 表示範囲の最小
    public float minDrawRange;
    // 表示範囲の最大
    public float maxDrawRange;
    // Playerとの最小間隔
    public float minPLRange = 20.0f;
    // Playerとの最大間隔
    public float maxPLRange = 40.0f;

    // 最小待機時間
    public int minWaitTime = 3;
    // 最大待機時間
    public int maxWaitTime = 10;


    // 移動ｽﾋﾟｰﾄﾞｽﾋﾟｰﾄﾞ値の保存用変数
    [SerializeField]
    private float moveSpeed;

    // 再生成までの時間
    [SerializeField]
    private float resTime;

    // 地面との高さ
    [SerializeField]
    private float groundHeight = 20.0f;

    // 最小ｽﾋﾟｰﾄﾞ
    [SerializeField]
    private float minSpeed = 1.0f;

    // 最大ｽﾋﾟｰﾄﾞ
    [SerializeField]
    private float maxSpeed = 8.0f;

    // playerとの間隔
    private float rangeBalloon;
    // 時間ｶｳﾝﾄ
    private int TimeCnt = 0;
    // 待機　true:待機 / false: 
    private bool waitFlag;


    void Start()
    {
        // 初期化
        moveSpeed  = Random.Range(minSpeed, maxSpeed);
        // 待機時間のﾗﾝﾀﾞﾑ値取得
        resTime    = Random.Range(minWaitTime, maxWaitTime);
    }

    void Update()
    {
        RestartSet();
        BalloonMove();
    }

    // 可視状態
    void OnBecameVisible() {}

    // 移動
    void BalloonMove()
    {
        if (this.transform.position.y < Camera.main.transform.position.y + maxWaitTime)
        {
            this.transform.position += new Vector3(0.0f, moveSpeed, 0.0f)*Time.deltaTime;
            waitFlag = false;
        }
        else
        {
            resTime -= Time.deltaTime;
            if(resTime < 0)
            {
                waitFlag = true;
                minDrawRange = PlayerController.Instance.transform.position.x + 20;
                maxDrawRange = PlayerController.Instance.transform.position.x + 30;
            }
        }
    }

    // 再配置
    void RestartSet()
    {
        if (waitFlag)
        {
            if (PlayerController.Instance.Feed > groundHeight)
            {
                // 次の配置を行う
                Debug.Log("再配置するよ");

                // ｽﾋﾟｰﾄﾞのﾗﾝﾀﾞﾑ値の取得
                moveSpeed = Random.Range(minSpeed, maxSpeed);
                // 待機時間のﾗﾝﾀﾞﾑ値取得
                resTime = Random.Range(minWaitTime, maxWaitTime);
                // 再描画範囲のﾗﾝﾀﾞﾑ値取得
                rangeBalloon = Random.Range(minDrawRange, maxDrawRange);

                // 再配置の座標
                this.transform.position = new Vector3(rangeBalloon, PlayerController.Instance.transform.position.y - 15, 0);
            }
            else
            {
                waitFlag = false;
            }
        }
        else
        { // 何もなし
        }
        waitFlag = false;
    }

}

