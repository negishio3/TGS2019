using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProgram : MonoBehaviour
{
    Vector3 bulletPos;//弾の座標
    float bulletInstancePosY;//生成地点

    float destroyDistance = 20.0f;//弾が消える距離

    float speed = 15;//弾のスピード

    int HP_Bullet = 2;

    GameObject damageEfect;

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
        bulletPos.y += Time.deltaTime * speed;//弾の移動の計算

        transform.position = bulletPos;//移動
        if ((transform.position.y-bulletInstancePosY) >= destroyDistance) Destroy(gameObject);//一定距離に達したら削除
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteo")
        {
            HP_Bullet--;
            Instantiate(damageEfect, transform.position, Quaternion.identity);
            other.GetComponent<i_Objects>().IDamage();
            AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION03);
            if (HP_Bullet == 0) Destroy(gameObject);
        }
        if (other.tag == "TitleLogo")
        {
            Instantiate(damageEfect, transform.position, Quaternion.identity);
            other.GetComponent<i_Objects>().IDamage();
            AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION03);
            Destroy(gameObject);
        }
        if (other.tag == "UFO")
        {
            Instantiate(damageEfect, transform.position, Quaternion.identity);
            other.GetComponent<i_Objects>().IDamage();
            AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION03);
            Destroy(gameObject);
        }
    }
}
