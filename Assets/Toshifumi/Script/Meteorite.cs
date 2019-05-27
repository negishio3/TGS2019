using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Vector3 hitPos;
        foreach (ContactPoint point in other.contacts)
        {
            Debug.Log("エフェクト！");
            hitPos = other.transform.position;
            Instantiate(effect, hitPos, Quaternion.identity);
        }
    }
}
