using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProgram : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteo")
        {
            Destroy(other.gameObject);
            GameSystem.Instance.EarthDamage(10);
            if (Data.earthHP <= 0) Debug.Log("Game Over");
        }
    }
}
