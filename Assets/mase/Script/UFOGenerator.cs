using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOGenerator : MonoBehaviour
{
    //public GameObject CreatePos_Left;//生成場所（左）
    //public GameObject CreatePos_Right;//生成場所（右）
    public GameObject UFOobj_Left;
    public GameObject UFOobj_Right;
    float CreateTime = 15;//生成間隔
    //public GameObject UfO_obj;
    GameObject Pos;
    int POS = 1;


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
            AudioManager.Instance.PlaySE(AUDIO.SE_UFO01);
            CreateTime = 15;
            int Pos = Random.Range(0, 2);
            //Instantiate(UfO_obj, new Vector3(-5f, 5f, 0f), Quaternion.identity);
            MeteorGenerator.Instance.ChangeUFOFlg(true);

            //Debug.Log(Pos);
            //Debug.Log(CreateTime);
            if (Pos <= 0)
            {
                Instantiate(UFOobj_Left, new Vector3(5f, 5f, 0f), Quaternion.identity);
            }
            if (Pos >= 1)
            {
                Instantiate(UFOobj_Right, new Vector3(-5f, 5f, 0f), Quaternion.identity);
                Debug.Log("北");
            }
        }
    }

}
