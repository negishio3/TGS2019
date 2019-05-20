using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MeteorPrefab;//生成するObjectの保管場所
    bool instanceFlg;

    void Start()
    {
        //InvokeRepeating("Meteorpos", 1, 1);
    }

     void Update()
    {
            //MeteorPrefabの生成
            //Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
            
            Debug.Log("でた");
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
