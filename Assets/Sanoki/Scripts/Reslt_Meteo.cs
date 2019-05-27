using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reslt_Meteo : Result_ObjectMove
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObj(ResultSystem.Instance.GetFallMeteoPos(), ResultSystem.Instance.GetMeteoBreakPos()));
    } 
}
