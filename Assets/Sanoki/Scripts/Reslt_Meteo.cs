using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reslt_Meteo : Result_ObjectMove
{

    // Start is called before the first frame update
    void Start()
    {
        switch(ResultSystem.Instance.GetState)
        {
            case ResultSystem.ResultState.RESULT:
                StartCoroutine(MoveObj(ResultSystem.Instance.GetResultFallPos, ResultSystem.Instance.GetResultPos));
                break;
            case ResultSystem.ResultState.RANKING:
                StartCoroutine(MoveObj(ResultSystem.Instance.GetFallMeteoPos(), ResultSystem.Instance.GetMeteoBreakPos()));
                break;
        }
        
    } 
}
