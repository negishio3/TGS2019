using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionMove : MonoBehaviour
{
    public float Speed;
    Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Pos.x += Speed * Time.deltaTime;
        transform.position = Pos;
    }
}