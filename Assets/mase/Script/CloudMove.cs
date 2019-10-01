using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMove : MonoBehaviour
{
    //float MoveSpeed;
    float rnd;
    float LifeTime;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        //MoveSpeed = 0.015f + 0.03f * rnd;
        LifeTime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(-MoveSpeed, 0,0, Space.World);
        transform.Translate(-Speed, 0, 0, Space.World);
        LifeTime -= Time.deltaTime;
        if (LifeTime<=0)
        {
            Destroy(gameObject);
        }
    }
}
