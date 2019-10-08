using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOGenerator : MonoBehaviour
{
    //public GameObject CreatePos_Left;//生成場所（左）
    //public GameObject CreatePos_Right;//生成場所（右）
    public GameObject ufo;
    float CreateTime = 15;//生成間隔
    //public GameObject UfO_obj;
    GameObject Pos;
    int POS = 1;
    public GameObject Arrow_Right;
    public GameObject Arrow_Left;
    float LifeTime;


    // Start is called before the first frame update
    void Start()
    {
        Arrow_Left.SetActive(false);
        Arrow_Right.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Data.gamestartFlg||Data.pauseFlg) return;
        CreateTime -= Time.deltaTime;
        if (CreateTime <= 0f)
        {
            CreateTime = 15;
            //AudioManager.Instance.PlaySE(AUDIO.SE_UFO01);
            int Pos = Random.Range(0, 2);
            //Instantiate(UfO_obj, new Vector3(-5f, 5f, 0f), Quaternion.identity);
            MeteorGenerator.Instance.ChangeUFOFlg(true);

            //Debug.Log(Pos);
            //Debug.Log(CreateTime);
            if (Pos <= 0)
            {

                Instantiate(ufo, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x+2.0f, 5f, 0f), Quaternion.identity);
                Arrow_Left.SetActive(true);
                LifeTime = 1.5f;
            }
            if (Pos >= 1)
            {

                Instantiate(ufo, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x-2.0f, 5f, 0f), Quaternion.identity);
                Arrow_Right.SetActive(true);
                LifeTime = 1.5f;
            }
        }
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Arrow_Left.SetActive(false);
            Arrow_Right.SetActive(false);
        }
    }

}
