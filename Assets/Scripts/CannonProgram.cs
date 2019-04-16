using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProgram : MonoBehaviour
{
    Vector3 playerDirection;//大砲の移動方向情報
    float speed = 3;//移動速度

    public GameObject bulletInstancePos;//弾の生成位置
    public GameObject bulletPre;//弾のプレハブ

    float bulletTime;//弾が生成されてからの時間
    float bulletInterval = 0.2f;//弾の生成間隔

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();//プレイヤーのInput取得
        Bullet();//弾の生成
    }

    void PlayerInput()
    {
        playerDirection.x = Input.GetAxis("Horizontal");//とりあえず左右キーで移動

        transform.position += playerDirection * Time.deltaTime * speed;//移動方向＊時間＊速度
    }

    void Bullet()
    {
       //// float bulletTime = 0;
        
       if (bulletTime != 0)return;//インターバル以下なら生成せずに終了

        Instantiate(bulletPre, bulletInstancePos.transform.position, Quaternion.identity);//弾の生成
        StartCoroutine(IntervalCounter());
    }

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
