using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    Vector3 bulletPos;
    float bulletInstancePosY;

    float destroyDistance = 10.0f;
    public float Straight;

    public float speed;
    GameObject Meteorite; //最も近い隕石
    bool flg;
    void Start()
    {
        flg = false;
        StartCoroutine(First());
        //最も近かったオブジェクトを取得
        Meteorite = serchTag(gameObject, "Cube");

        bulletInstancePosY = transform.position.y;
        bulletPos = transform.position;
    }
    void Update()
    {
        //if (!flg)
        //{
        //    bulletPos.x += Time.deltaTime * speed;

        //    transform.position = bulletPos;
        //}
        //else if (flg)
        //{
        //    float step = Time.deltaTime * speed;
        //    transform.position = Vector3.MoveTowards(transform.position, Meteorite.transform.position, step); // 隕石との距離を詰める
        //    transform.LookAt(Meteorite.transform); //隕石の方を向く
        //}
        //else if ((transform.position.y - bulletInstancePosY) >= destroyDistance) Destroy(gameObject);
    }
    //指定されたタグの中で最も近いものを取得
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "Cube";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
    IEnumerator First()
    {
        yield return new WaitForSeconds(Straight);

        flg = true;

        yield return null;
    }
}