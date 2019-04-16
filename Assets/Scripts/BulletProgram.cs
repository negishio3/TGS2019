using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProgram : MonoBehaviour
{
    Vector3 bulletPos;
    float bulletInstancePosY;

    float destroyDistance = 10.0f;

    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        bulletInstancePosY = transform.position.y;
        bulletPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bulletPos.y += Time.deltaTime * speed;

        transform.position = bulletPos;
        if ((transform.position.y-bulletInstancePosY) >= destroyDistance) Destroy(gameObject);
    }
}
