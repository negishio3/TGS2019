using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        TouchInfo info = AppUtil.GetTouch();
        if (info == TouchInfo.Began)
        {
            // タッチ開始
            Debug.Log("画面がタッチされた");
            animator.SetTrigger("Tap");
        }
    }
}
