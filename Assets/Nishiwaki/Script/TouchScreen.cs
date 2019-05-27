using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TouchInfo info = AppUtil.GetTouch();
        if (info == TouchInfo.Began)
        {
            // タッチ開始
            Debug.Log("画面がタッチされた");
        }
    }
}
