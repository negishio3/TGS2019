using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : SingletonMonoBehaviour<MeteorGenerator>
{
    // Start is called before the first frame update

    public GameObject[] MeteorPrefab;//生成するObjectの保管場所
    bool instanceFlg;
    float TimeLeft = 1;
    public bool ufoFlg = false;

    void Start()
    {
        //InvokeRepeating("Meteorpos", 1, 1);
        Data.pauseFlg = false;
    }

     void Update()
    {
        //if (Data.pauseFlg) return;
        //MeteorPrefabの生成
        //Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
        if (!ufoFlg)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0f)
            {
                TimeLeft = Random.Range(0.8f, 1.2f);//ランダムで生成する時間の範囲
                //Debug.Log(TimeLeft);
                int Meteors = Random.Range(0, MeteorPrefab.Length);//ランダムで選択するよ
                Instantiate(MeteorPrefab[Meteors], new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);//生成する
                Debug.Log("でた");
            }
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
