using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProgram : MonoBehaviour
{
    Vector3 playerDirection;//大砲の移動方向情報
    float speed = 20;//移動速度

    public GameObject bulletInstancePos;//弾の生成位置
    public GameObject bulletPre;//弾のプレハブ

    float bulletTime;//弾が生成されてからの時間
    float bulletInterval = 0.1f;//弾の生成間隔

    Vector2 minCameraWidth;//カメラの左端座標
    Vector2 maxCameraWidth;//カメラの右端座標

    Vector3 acacceleration;// 端末の傾き具合

    bool longTap = false;// 長押しフラグ
    float tapTime = 0.0f;// 長押し時間
    const float LONG_TAP_TIME = 0.1f;// 長押しの判定(時間)

    // Start is called before the first frame update
    void Start()
    {
        // 画面両端の座標を取得
        minCameraWidth = Camera.main.ViewportToWorldPoint(Vector2.zero);
        maxCameraWidth = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) LongTapCheck();// 長押しのチェック開始
        if (Input.GetMouseButtonUp(0))// 指を話したとき
        {
            longTap = false;// 長押しフラグをオフに
            tapTime = 0.0f;// 長押し時間をリセット
        }

        if (Data.pauseFlg) return;
        PlayerInput();//プレイヤーのInput取得
    }

    /// <summary>
    /// プレイヤーのインプットを取得する
    /// </summary>
    void PlayerInput()
    {
        if (Data.gyroFlg)
        {
            acacceleration = Input.acceleration;// 端末の傾き具合を取得
            playerDirection.x += Time.deltaTime * speed * acacceleration.x;// 傾き具合に応じて砲台を移動
        }
        else if (!Data.gyroFlg)
        {
            //マウスの左クリックが押されている間
            if (Input.GetMouseButton(0))
            {
                // 移動する座標を計算
                playerDirection.x += Time.deltaTime * speed * (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x);
            }
        }

        if(longTap)Bullet();//弾の生成
        transform.position = playerDirection;// 計算後の座標に移動

        //画面外に行かないようにする
        if(transform.position.x >= maxCameraWidth.x - transform.localScale.x/2)
        {
            transform.position = new Vector3(maxCameraWidth.x - transform.localScale.x/2, transform.position.y);
        }
        else if (transform.position.x <= minCameraWidth.x + transform.localScale.x/2)
        {
            transform.position = new Vector3(minCameraWidth.x + transform.localScale.x/2, transform.position.y);
        }
    }

    /// <summary>
    /// 長押しの判定
    /// </summary>
    void LongTapCheck()
    {
        // 長押し中でないなら
        if (!longTap)
        {
            tapTime += Time.deltaTime;// 経過時間
            if (tapTime >= LONG_TAP_TIME)// 長押しの判定
            {
                longTap = true;// 長押しフラグ
            }
        }
    }


    /// <summary>
    /// 弾の生成
    /// </summary>
    void Bullet()
    {

       if (bulletTime != 0)return;//インターバル中なら生成せずに終了
        if (Input.GetMouseButton(0))
        {
            Instantiate(bulletPre, bulletInstancePos.transform.position, Quaternion.identity);//弾の生成
            StartCoroutine(IntervalCounter());// クールタイムを計算
        }
    }

    /// <summary>
    /// 生成時間の計算
    /// </summary>
    /// <returns></returns>
    IEnumerator IntervalCounter()
    {

        while (bulletTime <= bulletInterval)
        {
            bulletTime += Time.deltaTime;//生成時間計算
            yield return null;
        }

        bulletTime = 0;//経過時間のリセット

    }
}
