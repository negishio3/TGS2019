using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_Bullet : Result_ObjectMove
{

    void Start()
    {
        StartCoroutine(MoveObj(transform.position, ResultSystem.Instance.GetMeteoBreakPos()));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteo")
        {
            Destroy(other.gameObject);
            ResultSystem.Instance.EfectInstance(transform.position);
            Destroy(gameObject);
        }
    }
}
