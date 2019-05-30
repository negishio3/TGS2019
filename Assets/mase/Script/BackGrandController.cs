using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrandController : MonoBehaviour
{
    float BackGrand_pos;

    // Start is called before the first frame update
    void Start()
    {
        BackGrand_pos = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.01f, 0, 0);
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(10f, BackGrand_pos, 4);
        }
    }
}
