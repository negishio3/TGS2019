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

    // Start is called before the first frame update
    void Start()
    {
        minCameraWidth = Camera.main.ViewportToWorldPoint(Vector2.zero);
        maxCameraWidth = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Data.pauseFlg) return;
        PlayerInput();//プレイヤーのInput取得
        
    }

    /// <summary>
    /// プレイヤーのインプットを取得する
    /// </summary>
    void PlayerInput()
    {
        //playerDirection.x = Input.GetAxis("Horizontal");//とりあえず左右キーで移動

        //transform.position += playerDirection * Time.deltaTime * speed;//移動方向＊時間＊速度

        //マウスの左クリックが押されている間
        if (Input.GetMouseButton(0))
        {
            //マウスの座標まで移動
            if(transform.position.x + transform.localScale.x/2 <= Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                //Debug.Log("右");
                playerDirection.x += Time.deltaTime * speed;//移動方向＊時間＊速度
            }
            else if(transform.position.x - transform.localScale.x/2 >= Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                //Debug.Log("左");
                playerDirection.x -= Time.deltaTime * speed;//移動方向＊時間＊速度
            }
            Bullet();//弾の生成
        }

        transform.position = playerDirection;

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
    /// 弾の生成
    /// </summary>
    void Bullet()
    {

       if (bulletTime != 0)return;//インターバル中なら生成せずに終了
        Instantiate(bulletPre, bulletInstancePos.transform.position, Quaternion.identity);//弾の生成
        StartCoroutine(IntervalCounter());
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
