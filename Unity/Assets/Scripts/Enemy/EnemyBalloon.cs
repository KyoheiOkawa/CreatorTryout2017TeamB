using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon
public class EnemyBalloon : MonoBehaviour
{

    // 表示範囲の最小
    public float minDrawRangeWidth;
    // 表示範囲の最大
    public float maxDrawRangeWidth;

    // Playerとの最小間隔
    public float minPLRange = 10.0f;
    // Playerとの最大間隔
    public float maxPLRange = 40.0f;

    // 表示範囲の最小
    public float minDrawRangeHeight = -10.0f;
    // 表示範囲の最大
    public float maxDrawRangeHeight = 0.0f;

    //0.1の判定
    public int zeroORone;

    // 最小待機時間
    public int minWaitTime = 3;
    // 最大待機時間
    public int maxWaitTime = 5;



    // 移動ｽﾋﾟｰﾄﾞｽﾋﾟｰﾄﾞ値の保存用変数
    [SerializeField]
    private float moveSpeed;

    // 再生成までの時間
    [SerializeField]
    private float resTime;

    // 最小ｽﾋﾟｰﾄﾞ
    [SerializeField]
    private float minSpeed = 1.0f;

    // 最大ｽﾋﾟｰﾄﾞ
    [SerializeField]
    private float maxSpeed = 8.0f;

    // 地面との高さ
    [SerializeField]
    private float groundHeight = 20.0f;

    // 縦からか横かの判定
    private float heiORwid;

    // playerとの間隔
    private float rangeBalloonW;
    private float rangeBalloonH;

    // 待機　true:待機 / false: 
    private bool waitFlag;


    void Start()
    {
        // 初期化
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        // 待機時間のﾗﾝﾀﾞﾑ値取得
        resTime = Random.Range(minWaitTime, maxWaitTime);
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
            if (zeroORone == 0)
            {
                this.transform.position += new Vector3(0.0f, moveSpeed, 0.0f) * Time.deltaTime;
            }
            else if (zeroORone == 1)
            {
                this.transform.position += new Vector3(0.0f, moveSpeed, 0.0f) * Time.deltaTime;
            }
            waitFlag = false;
        }
        else
        {
            resTime -= Time.deltaTime;
            if (resTime < 0)
            {
                waitFlag = true;
				minDrawRangeWidth = PlayerController.Instance.transform.position.x + 6;
                maxDrawRangeWidth = PlayerController.Instance.transform.position.x + 7;
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
                // ｽﾋﾟｰﾄﾞのﾗﾝﾀﾞﾑ値の取得
                moveSpeed = Random.Range(minSpeed, maxSpeed);

                // 待機時間のﾗﾝﾀﾞﾑ値取得
                resTime = Random.Range(minWaitTime, maxWaitTime);

                // 0.1の判定用
                zeroORone = Random.Range(0, 2);

                if (zeroORone == 0)
                {
                    rangeBalloonW = Random.Range(minDrawRangeWidth, maxDrawRangeWidth);
                    heiORwid = rangeBalloonW;
                    // 再配置の座標
                    this.transform.position = new Vector3(heiORwid, PlayerController.Instance.transform.position.y - minPLRange, 0);

                }
                else if (zeroORone == 1)
                {
                    rangeBalloonH = Random.Range(minDrawRangeHeight, maxDrawRangeHeight);
                    heiORwid = rangeBalloonH;
                    // 再配置の座標
                    this.transform.position = PlayerController.Instance.transform.position + new Vector3(24, heiORwid, 0.0f);
                }
                else
                {
                    // 何もなし
                }
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

