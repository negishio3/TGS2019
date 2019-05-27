﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MeteorPrefab;//生成するObjectの保管場所
    bool instanceFlg;
    float TimeLeft;

    void Start()
    {
        //InvokeRepeating("Meteorpos", 1, 1);
    }

     void Update()
    {
        if (Data.pauseFlg) return;
        //MeteorPrefabの生成
        //Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0f)
        {
            TimeLeft = 1;
            Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);//生成する
            Debug.Log("でた");
        }
    }

    void TimeCounter()
    {
        instanceFlg = false;
        float timmer = 0;
        timmer += Time.deltaTime;
        instanceFlg = true;
    }

    // Update is called once per frame
    //void Meteorpos()
    //{
    //    Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
    //    Debug.Log("でた");
    //}
}
