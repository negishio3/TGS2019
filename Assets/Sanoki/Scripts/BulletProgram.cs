using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProgram : MonoBehaviour
{
    Vector3 bulletPos;//弾の座標
    float bulletInstancePosY;//生成地点

    float destroyDistance = 10.0f;//弾が消える距離

    float speed = 10;//弾のスピード

    // Start is called before the first frame update
    void Start()
    {
        //生成位置の保存
        bulletInstancePosY = transform.position.y;
        bulletPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bulletPos.y += Time.deltaTime * speed;//弾の移動の計算

        transform.position = bulletPos;//移動
        if ((transform.position.y-bulletInstancePosY) >= destroyDistance) Destroy(gameObject);//一定距離に達したら削除
    }
}
