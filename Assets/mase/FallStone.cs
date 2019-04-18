using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallStone : MonoBehaviour
{
    float fallSpeed;
    float rotSpeed;
    float rnd;
    float hp = 5;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        this.fallSpeed = 0.01f + 0.06f * rnd;
        this.rotSpeed = 5f + 3 * rnd;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fallSpeed, 0, Space.World);
        transform.Rotate(rotSpeed, -rotSpeed, 0);

        if (transform.position.y<-3.0f)
        {
            Destroy(gameObject);
            Debug.Log("きえええええええええええ");
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (hp >= 5 && hp<=5)
        {
            this.gameObject.transform.localScale = new Vector3(2, 2, 2);
        }
        if (hp >= 4 && hp <= 4)
        {
            this.gameObject.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        if (hp >= 3 && hp <= 3)
        {
            this.gameObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        }
        if (hp >= 2 && hp <= 2)
        {
            this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        if (hp >= 1 && hp <= 1)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {

            Debug.Log("ヒット");
            hp -= 1f;
            Debug.Log(hp);
            Destroy(other.gameObject);

            //Destroy(gameObject);
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "bullet")
    //    {

    //        Debug.Log("ヒット");

    //        Destroy(this);
    //    }
    //}
}
