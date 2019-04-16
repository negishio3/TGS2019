using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject meteorPrefab;

    void Start()
    {
        InvokeRepeating("meteorpos", 1, 1);
    }

    // Update is called once per frame
    void meteorpos()
    {
        Instantiate(meteorPrefab, new Vector3(-2.5f + 5 * Random.value, 9, 0), Quaternion.identity);
    }
}
