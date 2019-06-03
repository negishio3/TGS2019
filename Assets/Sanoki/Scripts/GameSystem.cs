using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : SingletonMonoBehaviour<GameSystem>
{
    public Text scoreText;//スコア用テキスト
    public Text timmerText;//制限時間用テキスト

    public Image damageImage;// 体力が少ないときに表示する用

    SceneFader sf;// フェードプログラム

    public GameObject cautionCanvas;// カンバス
    public GameObject textCanvas;// テキスト用のカンバス

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(AUDIO.BGM_THEEXPENDABLES);
        sf = FindObjectOfType<SceneFader>();
        Data.timmer = 120;
        scoreText.text = "Score:"+Data.score;//表示をリセット
        Data.earthHP = Data.EarthMaxHP;

        StartCoroutine(InputWait());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Data.pauseFlg) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //AddTime(5);
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
        if (Data.earthHP == 300)
        {
            StartCoroutine(DamageAnimation());
        }
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
    
    void AddTime(int addTime)
    {
        Data.timmer += addTime;
        // ここにタイムの加算アニメーション
    }

    public void GameStart()
    {
        Data.pauseFlg = false;
        TimmerStart();
    }

    /// <summary>
    /// タップの入力待機
    /// </summary>
    /// <returns></returns>
    IEnumerator InputWait()
    {
        yield return new WaitUntil(Touch);
        yield return new WaitWhile(Touch);
        cautionCanvas.SetActive(false);
        textCanvas.SetActive(false);
    }
    bool Touch()
    {
        return Input.GetMouseButtonDown(0);
    }

    IEnumerator DamageAnimation()
    {
        Color startColor=new Color(1,1,1,0);
        Color endColor=new Color(1,1,1,0.7f);

        float t = 0;
        while (t < 1)
        {

            damageImage.color = Color.Lerp(startColor, endColor, t);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
