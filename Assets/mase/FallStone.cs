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
            this.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (hp >= 4 && hp <= 4)
        {
            this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        if (hp >= 3 && hp <= 3)
        {
            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (hp >= 2 && hp <= 2)
        {
            this.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (hp >= 1 && hp <= 1)
        {
            this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
