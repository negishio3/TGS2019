using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MeteorPrefab;

    void Start()
    {
        //InvokeRepeating("Meteorpos", 1, 1);
    }

     void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
            Debug.Log("でた");
        }
    }

    // Update is called once per frame
    //void Meteorpos()
    //{
    //    Instantiate(MeteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
    //    Debug.Log("でた");
    //}
}
