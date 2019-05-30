using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    float DeleteTime;//消す時間

    // Start is called before the first frame update
    void Start()
    {
        DeleteTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        DeleteTime -= Time.deltaTime;
        if (DeleteTime <= 0f)
        {
            Debug.Log("消したお");
            Destroy(gameObject);
        }
    }
}
