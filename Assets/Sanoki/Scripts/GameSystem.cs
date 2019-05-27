using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : SingletonMonoBehaviour<GameSystem>
{
    public Text scoreText;//スコア用テキスト
    public Text timmerText;//制限時間用テキスト
    SceneFader sf;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        sf = FindObjectOfType<SceneFader>();
        Data.timmer = 120;
        scoreText.text = "Score:"+Data.score;//表示をリセット
        Data.earthHP = Data.EarthMaxHP;
        Invoke("Start_Animation", 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) TimmerStart();
        if (Data.pauseFlg) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddScore(100);
            //Debug.Log("SCORE : "+Data.score);
        }
    }

    public void AddScore(int addScore)
    {
        Data.score += addScore;
        scoreText.text ="Score:"+Data.score;
    }

    public void EarthDamage(int damage)
    {
        Data.earthHP -= damage;
    }

    public void EarthHeal(int healValue)
    {
        Data.earthHP += healValue;
    }

    public void TimmerStart()
    {
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while (Data.timmer >= 0)
        {
            Data.timmer -= Time.deltaTime;
            timmerText.text = "TIME:" + (int)Data.timmer;
            yield return null;
        }
        timmerText.text = "GameClear!";
    } 

    void Start_Animation()
    {
        ani.SetTrigger("GameStart");
    }

    public void GameStart()
    {
        Data.pauseFlg = false;
        TimmerStart();
    }
}
