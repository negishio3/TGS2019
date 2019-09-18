using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMove : MonoBehaviour
{
    float MoveSpeed;
    float rnd;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        MoveSpeed = 0.03f + 0.05f * rnd;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-MoveSpeed, 0,0, Space.World);

    }
}
