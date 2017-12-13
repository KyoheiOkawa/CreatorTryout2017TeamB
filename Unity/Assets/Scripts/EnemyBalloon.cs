using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon
public class EnemyBalloon : MonoBehaviour {

    public GameObject player;

    // 表示範囲
    public float minRange;
    public float maxRange;
    public float bulloonRange;
    public float bulloonRange1;

    // Baloonｽﾋﾟｰﾄﾞ
    public float minSpeed;
    public float maxSpeed;


    // ｽﾋﾟｰﾄﾞ値の保存用変数
    [SerializeField]
    // 移動ｽﾋﾟｰﾄﾞ
    private float moveSpeed;
    private float rangeBalloon;

    // 待機　true:待機 / false: 
    private bool waitFlag;

    void Start ()
    {
        player = GameObject.Find("player");

        bulloonRange  = 10;
        bulloonRange1 = 25;
   

        // 初期化
        minSpeed = 0.01f;
        maxSpeed = 0.08f;
        // ｽﾋﾟｰﾄﾞのﾗﾝﾀﾞﾑ値の取得
        moveSpeed    = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        WaitCheck();
        // 移動
        BalloonMove();

        minRange = player.transform.position.x + bulloonRange;
        maxRange = player.transform.position.x + bulloonRange1;
    }

    // 可視状態
    void OnBecameVisible()
    {
        Debug.Log("ok");
        enabled = true;
    }

    // 移動
    void BalloonMove()
    {
        if(this.transform.position.y < Camera.main.transform.position.y+10)
        {
            if(waitFlag == false)
            {
                this.transform.position += new Vector3(0, moveSpeed, 0);
            }
        }
        else
        {
            waitFlag = true;
        }
    }

    // 気球の待機
    void WaitCheck()
    {
        if (waitFlag == true)
        {
            // 次の配置を行う
            Debug.Log("再配置するよ");
            // 再描画範囲のﾗﾝﾀﾞﾑ値取得
            rangeBalloon = Random.Range(minRange, maxRange);

            // 再配置の座標
            this.transform.position = new Vector3(rangeBalloon, -15, 5);
        }
        else
        {
            // 何もしない
        }
        waitFlag = false;
    }


}

