using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject bulletInstancePos;//弾の生成位置
    public GameObject bulletPre;//弾のプレハブ
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Quaternion rote = new Quaternion(-90.0f, 0.0f, 0.0f, 1.0f);

            Instantiate(bulletPre, bulletInstancePos.transform.position, Quaternion.identity);//弾の生成
        }
    }
}
