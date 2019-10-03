using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Title_UFO : MonoBehaviour
{
    Vector3 ufoRotate;// UFOの回転
    float swingRange = 15.0f;// 振れ幅
    public float swingSpeed = 5.0f;// 揺らす速度
    public float rotSpeed = 100.0f;// 回転速度
    public bool IsSwing;// X軸の揺れを許可する

    // Update is called once per frame
    void Update()
    {
        ufoRotate = new Vector3(
            (Mathf.Sin(Time.time * swingSpeed)) * swingRange * Convert.ToInt32(IsSwing) - 90.0f,
            Time.time * rotSpeed,
            0
            );
        transform.eulerAngles=ufoRotate;
    }
}
