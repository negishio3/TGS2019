using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProgram : MonoBehaviour
{
    Vector3 bulletPos;//弾の座標
    float bulletInstancePosY;//生成地点

    float destroyDistance = 20.0f;//弾が消える距離

    float speed = 15;//弾のスピード

    int HP_Bullet = 2;// 弾の耐久値(貫通の表現に使う予定だった)

    GameObject damageEfect;// 爆発エフェクト

    // Start is called before the first frame update
    void Start()
    {
        //生成位置の保存
        bulletInstancePosY = transform.position.y;
        bulletPos = transform.position;
        damageEfect = (GameObject)Resources.Load("Prefabs/Boro");
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.pauseFlg) return;
        bulletPos.y += Time.deltaTime * speed;//弾の移動の計算

        transform.position = bulletPos;//移動
        if ((transform.position.y-bulletInstancePosY) >= destroyDistance) Destroy(gameObject);//一定距離に達したら削除
    }
    private void OnTriggerEnter(Collider other)
    {
        // 触れた対象に応じて処理を変える
        switch (other.tag)
        {
            // 隕石
            case "Meteo":
                HP_Bullet-=2;// 耐久値をマイナス
                DestroyEffect(other);
                if (HP_Bullet == 0) Destroy(gameObject);// 耐久値が0なら削除
                break;

            // ゲームモード用のObj,UFO
            case "GameMode":
            case "UFO":
                DestroyEffect(other);
                Destroy(gameObject);// 削除
                break;
        }
    }

    /// <summary>
    /// 弾が消えるときの処理
    /// </summary>
    /// <param name="other">Collider</param>
    void DestroyEffect(Collider other)
    {
        Instantiate(damageEfect, transform.position, Quaternion.identity);// エフェクトの生成
        other.GetComponent<i_Objects>().IDamage();// 触れた対象のIDamage()を呼ぶ
        AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION03);// SEを再生
    }
}
