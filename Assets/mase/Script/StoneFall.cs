using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFall : MonoBehaviour,i_Objects
{
    //public float Fall_Max;//落ちる速度の最大
    //public float Fall_Min;//落ちる速度の最小
    float fallSpeed;
    public int HP_fallstone = 3;//隕石の体力
    float rnd;//ランダムの保存先
    public ParticleSystem Damage;//弾が当たり体力が減った時
    public ParticleSystem explosion;//体力がなくなった時


    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        //this.fallSpeed = Fall_Min + Fall_Max * rnd;
        this.fallSpeed = 0.03f + 0.05f * rnd;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fallSpeed, 0, Space.World);

        if (transform.position.y < -3.0f)
        {
            Destroy(gameObject);
            Debug.Log("きえええええええええええ");
        }

        if (HP_fallstone <= 0)
        {
            GameSystem.Instance.AddScore(100*HP_fallstone);
            Instantiate(explosion, transform.position, transform.rotation);//体力が0になったら再生
            Destroy(gameObject);
            //hpが0になったら消える
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(Damage, transform.position, transform.rotation);            Instantiate(Damage, transform.position, transform.rotation);
            this.gameObject.transform.localScale -= new Vector3(transform.localScale.x / 3, transform.localScale.y / 3, transform.localScale.z / 3);

        }
    }


    public void IDamage()
    {
        Debug.Log("ヒット");
        HP_fallstone -= 1;
        Instantiate(Damage, transform.position, transform.rotation);//弾が当たったら再生
        this.gameObject.transform.localScale -= new Vector3(this.transform.localScale.x/3,transform.localScale.y/3,transform.localScale.z/3);
        Debug.Log(HP_fallstone);
        //Destroy(other.gameObject);
    }
}
