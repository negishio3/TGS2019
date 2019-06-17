using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudGenerator : MonoBehaviour
{
    //public GameObject[] Cloud;
    public GameObject obj;
    float CreateTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //int Clouds = Random.Range(0, Cloud.Length);
        // Instantiate(Cloud[Clouds],)
        CreateTime -= Time.deltaTime;
        if (CreateTime<=0f)
        {
            Instantiate(obj, this.transform.position, Quaternion.identity);
            Debug.Log("生成");
            CreateTime = 1;
        }
    }
}
