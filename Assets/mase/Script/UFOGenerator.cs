using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOGenerator : MonoBehaviour
{
    public GameObject CreatePos_Left;//生成場所（左）
    public GameObject CreatePos_Right;//生成場所（右）
    float CreateTime = 15;//生成間隔
    GameObject Pos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateTime -= Time.deltaTime;
        if (CreateTime <= 0f)
        {
             Pos = Random.Range();
            if ()
            {

            }
        }
    }
}
