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
    ParticleSystem Damage;//体力が削られたとき
    ParticleSystem Delete;//デストロイするとき

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        //this.fallSpeed = Fall_Min + Fall_Max * rnd;
        this.fallSpeed = 0.05f + 0.1f * rnd;
        Damage = this.GetComponent<ParticleSystem>();
        Damage.Stop();

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
            Destroy(gameObject);
            //hpが0になったら消える
        }
    }


    public void IDamage()
    {
        Debug.Log("ヒット");
        HP_fallstone -= 1;
        Damage.Play();
        this.gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log(HP_fallstone);
        //Destroy(other.gameObject);
    }
}
