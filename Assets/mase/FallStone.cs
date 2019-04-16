using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallStone : MonoBehaviour
{
    float fallSpeed;
    float rotSpeed;
    float rnd;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.value;
        this.fallSpeed = 0.01f + 0.06f * rnd;
        this.rotSpeed = 5f + 3 * rnd;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -fallSpeed, 0, Space.World);
        transform.Rotate(rotSpeed, -rotSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {

            Debug.Log("ヒット");

            Destroy(gameObject);
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "bullet")
    //    {

    //        Debug.Log("ヒット");

    //        Destroy(this);
    //    }
    //}
}
