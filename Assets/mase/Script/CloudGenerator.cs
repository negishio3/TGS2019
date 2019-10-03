using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] Cloud; //雲の保管場所
    //public GameObject obj;
    float CreateTime = 1.0f;
    float[] cloudSize = new float [3];


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Cloud.Length; i++)
        {
            cloudSize[i] = Cloud[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int Clouds = Random.Range(0, Cloud.Length);
        // Instantiate(Cloud[Clouds],)
        CreateTime -= Time.deltaTime;
        if (CreateTime<=0f)
        {
            int Clouds = Random.Range(0, Cloud.Length);
            Instantiate(Cloud[Clouds], new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x+cloudSize[Clouds]/2, Random.Range(7f, 9f), -1), Quaternion.identity);
            CreateTime = 3;
        }
    }
}
