﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour,i_Objects
{
    public float movespeed;//UFOの移動速度
    public int HP_UFO;//UFOのHP
    float UFO_rotspeed=10;
    float SwingRange = 15;
    Vector3 rot_UFO;
    float UFO_pos;
    public int pos;
    public GameObject Item;//生成するObject

        // Start is called before the first frame update
    void Start()
    {
        //UFOのオブジェクトの位置情報を代入
        UFO_pos = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //右に移動
        transform.Translate(movespeed, -0.01f, 0, Space.World);
        rot_UFO = new Vector3(Mathf.Sin(Time.time*UFO_rotspeed)*SwingRange,UFO_rotspeed*Time.time,0);
        transform.eulerAngles = rot_UFO;
        //上下移動
        transform.position = new Vector3(transform.position.x, UFO_pos+Mathf.PingPong(Time.time, 1), transform.position.z);
        if (HP_UFO<=0)
        {
            Instantiate(Item, this.transform.position, Quaternion.identity);//生成する
            Destroy(gameObject);//㏋が0になったら消す
        }
        if (UFO_pos >= 3 )
        {
            //StartCoroutine()
        }
        //this.gameObject.transform.position = new Vector3(UFO_pos.x,(UFO_pos.y + Mathf.PingPong(Time.time, 2)), UFO_pos.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {

        }
    }

    public void IDamage()
    {
        Debug.Log("fooo");
        HP_UFO -= 1;//弾が当たったらHPを1減らす

    }
}
