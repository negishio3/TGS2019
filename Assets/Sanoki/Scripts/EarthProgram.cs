using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProgram : MonoBehaviour
{
    SceneFader sf;
    private void Start()
    {
        sf = FindObjectOfType<SceneFader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteo")
        {
            Destroy(other.gameObject);
            GameSystem.Instance.EarthDamage(100);
            if (Data.earthHP <= 0)
            {
                // ここにゲームオーバーの演出
                sf.SceneChange("Result");
            }
        }
    }
}
