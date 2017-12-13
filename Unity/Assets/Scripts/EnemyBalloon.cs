using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon
public class EnemyBalloon : MonoBehaviour {

    // 表示範囲の最小
    public float minRange;
    // 表示範囲の最大
    public float maxRange;
    // Playerとの最小間隔
    public float bullRangeMin;
    // Playerとの最大間隔
    public float bullRangeMax;


    // 最小ｽﾋﾟｰﾄﾞ
    public float minSpeed;
    // 最大ｽﾋﾟｰﾄﾞ
    public float maxSpeed;


    // 移動ｽﾋﾟｰﾄﾞｽﾋﾟｰﾄﾞ値の保存用変数
    [SerializeField]
    private float moveSpeed;
    // playerとの間隔
    private float rangeBalloon;

    // 待機　true:待機 / false: 
    private bool waitFlag;

    void Start ()
    {
        // 初期化
        minSpeed = 0.01f;
        maxSpeed = 0.08f;

        bullRangeMin = 10.0f;
        bullRangeMax = 25.0f;

        moveSpeed    = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        RestartSet();
        BalloonMove();
    }

    // 可視状態
    void OnBecameVisible()
    {
        Debug.Log("ok");
    }

    // 移動
    void BalloonMove()
    {
        if(this.transform.position.y < Camera.main.transform.position.y + bullRangeMin)
        {
            this.transform.position += new Vector3(0, moveSpeed, 0);
            waitFlag = false;
        }
        else
        {
            waitFlag = true;
            minRange = PlayerController.Instance.transform.position.x + bullRangeMin;
            maxRange = PlayerController.Instance.transform.position.x + bullRangeMax;
        }
    }

    // 再配置
    void RestartSet()
    {
        if(waitFlag)
        {
            // 次の配置を行う
            Debug.Log("再配置するよ");

            // 再描画範囲のﾗﾝﾀﾞﾑ値取得
            rangeBalloon = Random.Range(minRange, maxRange);

            // ｽﾋﾟｰﾄﾞのﾗﾝﾀﾞﾑ値の取得
            moveSpeed = Random.Range(minSpeed, maxSpeed);

            // 再配置の座標
            this.transform.position = new Vector3(rangeBalloon, PlayerController.Instance.transform.position.y -15, 0);
        }
        else
        { // 何もなし
        }
        waitFlag = false;
    }

}

