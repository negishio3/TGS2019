using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallStone : MonoBehaviour
{
    float fallSpeed;//落ちる速度
    float rotSpeed;//回転速度
    float rnd;//ランダムの保存先

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        //this.fallSpeed = 0.01f + 0.06f * rnd;
        this.rotSpeed = 5f + 3 * rnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.pauseFlg) return;
        //transform.Translate(0, -fallSpeed, 0, Space.World);
        transform.Rotate(rotSpeed, -rotSpeed, 0);
    }
}
