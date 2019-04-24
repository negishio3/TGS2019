using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : SingletonMonoBehaviour<GameSystem>
{
    // Start is called before the first frame update
    void Start()
    {
        Data.earthHP = Data.EarthMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddScore(100);
            //Debug.Log("SCORE : "+Data.score);
        }
    }

    public void AddScore(int addScore)
    {
        Data.score += addScore;
    }

    public void EarthDamage(int damage)
    {
        Data.earthHP -= damage;
    }

    public void EarthHeal(int healValue)
    {
        Data.earthHP += healValue;
    }
}
