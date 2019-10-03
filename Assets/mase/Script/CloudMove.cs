using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMove : MonoBehaviour
{
    //float MoveSpeed;
    float rnd;
    float lifeTime;
    float speed = 0.05f;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = speed / sr.bounds.size.x;
        rnd = Random.value;
        //MoveSpeed = 0.015f + 0.03f * rnd;
        lifeTime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(-MoveSpeed, 0,0, Space.World);
        transform.Translate(-speed, 0, 0, Space.World);
        lifeTime -= Time.deltaTime;
        if (transform.position.x <= Camera.main.ViewportToWorldPoint(Vector3.zero).x - sr.bounds.size.x/2.0f)
        {
            Destroy(gameObject);
        }
    }
}
