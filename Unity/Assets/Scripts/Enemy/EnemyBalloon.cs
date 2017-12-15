using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon
public class EnemyBalloon : MonoBehaviour
{

    // X軸の表示範囲の最小
    public float minDrawX;
    // 表示範囲の最大
    public float maxDrawX;

    // 表示範囲の最小
    public float minDrawY = -10.0f;
    // 表示範囲の最大
    public float maxDrawY = 0.0f;

    // Playerとの最小間隔
    public float minPLRange = 10.0f;
    // Playerとの最大間隔
    public float maxPLRange = 40.0f;


    //0.1の判定
    public int zeroORone;

    // 最小待機時間
    public int minWaitTime = 3;
    // 最大待機時間
    public int maxWaitTime = 5;


    // 移動ｽﾋﾟｰﾄﾞﾗﾝﾀﾞﾑ値の保存用変数
    [SerializeField]
    private float moveSpeed;

    // 待機[再生成まで]時間
    [SerializeField]
    private float WaitTime;

    // 最小ｽﾋﾟｰﾄﾞ
    [SerializeField]
    private float minSpeed = 1.0f;

    // 最大ｽﾋﾟｰﾄﾞ
    [SerializeField]
    private float maxSpeed = 8.0f;

    // 地面との高さ
    [SerializeField]
    private float groundHeight = 20.0f;

    // playerとの間隔
    private float reposX;
    private float reposY;

    // 待機　true:待機 / false: 
    private bool waitFlag;


    void Start()
    {
        // 初期化
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        // 待機時間のﾗﾝﾀﾞﾑ値取得
        WaitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    void Update()
    {
        RestartSet();
        BalloonMove();
    }

    // 可視状態
    void OnBecameVisible() { }

    // 移動
    void BalloonMove()
    {
        if (this.transform.position.y < Camera.main.transform.position.y + maxWaitTime)
        {
            waitFlag = false;
            // 座標加算[上昇]
            this.transform.position += new Vector3(0.0f, moveSpeed, 0.0f) * Time.deltaTime;

        }
        else
        {
            WaitTime -= Time.deltaTime;
            if (WaitTime < 0)
            {
                waitFlag = true;
                minDrawX = PlayerController.Instance.transform.position.x + 9;
                maxDrawX = PlayerController.Instance.transform.position.x + 10;
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
                moveSpeed = Random.Range(minSpeed, maxSpeed);

                WaitTime = Random.Range(minWaitTime, maxWaitTime);

                // 0.1の判定用
                zeroORone = Random.Range(0, 2);

                if (zeroORone == 0)
                {
                    reposX = Random.Range(minDrawX, maxDrawX);
                    // 再配置[座標]
                    this.transform.position = new Vector3(reposX, PlayerController.Instance.transform.position.y - minPLRange, 0);

                }
                else if (zeroORone == 1)
                {

                    reposY = Random.Range(minDrawY, maxDrawY);
                    // 再配置の座標
                    this.transform.position = PlayerController.Instance.transform.position + new Vector3(24, reposY, 0.0f);
                }
                else
                {// 何もなし
                }
            }
            else { }
        }
        else
        { // 何もなし
        }
        waitFlag = false;
    }
}

