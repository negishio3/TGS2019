﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour,i_Objects
{
    public enum UFO_Type
    {
        Left,
        Right
    }

    UFO_Type UFOType = UFO_Type.Left;

    public float movespeed;//UFOの移動速度
    public int HP_UFO;//UFOのHP
    float UFO_rotspeed = 10;
    float SwingRange = 15;
    Vector3 rot_UFO;
    float UFO_pos;

    int addScore;

        // Start is called before the first frame update
    void Start()
    {
        addScore = 150 / HP_UFO;//基本値をHPで割った値をスコアとする
        Debug.Log(Camera.main.ViewportToWorldPoint(Vector3.one).x);
        //UFOのオブジェクトの位置情報を代入
        UFO_pos = this.gameObject.transform.position.y;
        if (transform.position.x >= Vector3.zero.x)
        {
            UFOType = UFO_Type.Right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.pauseFlg) return;
        rot_UFO = new Vector3(Mathf.Sin(Time.time * UFO_rotspeed) * SwingRange, UFO_rotspeed * Time.time, Mathf.Sin(Time.time * UFO_rotspeed) * SwingRange);
        transform.eulerAngles = rot_UFO;
        //上下移動
        transform.position = new Vector3(transform.position.x, UFO_pos + Mathf.PingPong(Time.time, 1), transform.position.z);

        switch (UFOType) {

            case UFO_Type.Left:
                //右に移動
                transform.Translate(movespeed, -0.01f, 0, Space.World);
                // 画面外に出たら削除
                if (transform.position.x >= Camera.main.ViewportToWorldPoint(Vector3.one).x + transform.localScale.x)
                {
                    MeteorGenerator.Instance.ChangeUFOFlg(false);
                    //StartCoroutine()
                    Destroy(gameObject);//自分を消す
                }

                break;
            case UFO_Type.Right:
                //左に移動
                transform.Translate(-movespeed, -0.01f, 0, Space.World);
                // 画面外に出たら削除
                if (transform.position.x <= Camera.main.ViewportToWorldPoint(Vector3.zero).x - transform.localScale.x)
                {
                    MeteorGenerator.Instance.ChangeUFOFlg(false);
                    //StartCoroutine()
                    Destroy(gameObject);//自分を消す
                }
                break;
            default:
                Debug.LogError("Typeが選択されていません");
                break;
        }
        if (HP_UFO <= 0)
        {
            if (Data.GameMode == Data.ModeType.TimeAttack) GameSystem.Instance.AddTime(5);
            else GameSystem.Instance.EarthHeal(50);
            GameSystem.Instance.AddScore(150);
            MeteorGenerator.Instance.ChangeUFOFlg(false);
            Data.breakUFOCount++;
            //Instantiate(Item,transform.position, Quaternion.identity);//生成する
            Destroy(gameObject);//㏋が0になったら消す
        }
        //this.gameObject.transform.position = new Vector3(UFO_pos.x,(UFO_pos.y + Mathf.PingPong(Time.time, 2)), UFO_pos.z);
    }


    public void IDamage()
    {
        GameSystem.Instance.AddScore(addScore);// 値分のスコアを加算
        HP_UFO -= 1;//弾が当たったらHPを1減らす

    }
}
