using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFall : MonoBehaviour,i_Objects
{
    float fallSpeed;
    public int HP_fallstone = 3;//隕石の体力
    float rnd;//ランダムの保存先

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        this.fallSpeed = 0.01f + 0.06f * rnd;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {

            

            //Destroy(gameObject);
        }
    }

    public void IDamage()
    {
        Debug.Log("ヒット");
        HP_fallstone -= 1;
        this.gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log(HP_fallstone);
        //Destroy(other.gameObject);
    }
}
