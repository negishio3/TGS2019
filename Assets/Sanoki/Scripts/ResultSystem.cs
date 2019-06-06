﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ResultSystem : SingletonMonoBehaviour<ResultSystem>
{

    public GameObject efectPre;// 隕石が壊れるときのエフェクト
    public GameObject scoreUI;// スコア表示のプレハブ
    public GameObject[] fallMeteo;// 隕石のプレハブ
    public GameObject resultBullet;// 弾丸プレハブ
    public GameObject[] label;// リザルトのラベル

    public Transform[] fallMeteoPos = new Transform[3];// 隕石の開始地点
    public Transform[] scorePos = new Transform[3];// scoreの表示位置

    public Transform resultFallPos;
    public Transform[] resultPos = new Transform[3];// リザルトで使用するTransform

    int[] ranking = new int[3];// ランキングの保存用

    int rankingCount = 0;// ランキングがどこまで表示されたかカウント

    SceneFader sf;

    bool stayFlg = true;

    //-----------------------
    List<GameObject> _scoreUI = new List<GameObject>();
    //-----------------------

    // リザルトシーンのステート
    public enum ResultState{
        RESULT,
        RANKING
    }

    public ResultState state = ResultState.RESULT;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(AUDIO.BGM_THEEXPENDABLES);
        stayFlg = true;
        sf = FindObjectOfType<SceneFader>();// SceneFaderの読み込み
        RankingUpdate();// ランキングデータの更新
        StartCoroutine(InputWait());// タップの待機処理
        Data.pauseFlg = true;// ポーズ中に設定

        Invoke("ResultStart", 1.0f);// リザルトコルーチン
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DataReset();// データのリセット
        }
    }

    /// <summary>
    /// スコアのソート
    /// </summary>
    /// <param name="score">ソートに使用されるスコア</param>
    void RankingSort(int score)
    {
        // ランキング配列分回す
        for(int i = 0; i < ranking.Length; i++)
        {
            // scoreがranking[i]より大きかったら
            if (ranking[i] < score)
            {
                int x = ranking[i];// xに一時保存
                ranking[i] = score;// ranking[i]にscoreを更新
                score = x;// scoreを更新
            }
        }
        //Debug.Log("[0]" + ranking[0] + "[1]" + ranking[1] + "[2]" + ranking[2]);// 確認用
    }

    /// <summary>
    /// 隕石の生成位置を取得
    /// </summary>
    /// <returns></returns>
    public Vector3 GetFallMeteoPos()
    { 
           return fallMeteoPos[rankingCount].position;
    }


    /// <summary>
    /// 隕石の壊したい位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMeteoBreakPos()
    {
        return scorePos[rankingCount].position;
    }

    /// <summary>
    /// 表示するスコアの取得
    /// </summary>
    /// <returns></returns>
    public string GetRankingScore()
    {
        string sData = "";// 戻り値の初期化
        rankingCount++;// カウンターをプラス
        switch (rankingCount - 1)
        {
            case 0:
                sData = "1st:" + ranking[rankingCount - 1];
                break;
            case 1:
                sData = "2nd:" + ranking[rankingCount - 1];
                break;
            case 2:
                sData = "3rd:" + ranking[rankingCount - 1];
                break;
        }

        return sData;// 表示する文字列を返す
    }

    /// <summary>
    /// エフェクトを生成
    /// </summary>
    /// <param name="instancePos">生成する座標</param>
    public void EfectInstance(Vector3 instancePos)
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION05);
        Instantiate(efectPre, instancePos, Quaternion.identity);// エフェクトの生成
        _scoreUI.Add(Instantiate(scoreUI, instancePos, Quaternion.identity));// UIの生成

    }


    /// <summary>
    /// 隕石の生成
    /// </summary>
    public void InstanceMeteo()
    {
        Instantiate(fallMeteo[Random.Range(0,2)], fallMeteoPos[rankingCount].position, Quaternion.identity);// 隕石の生成
        Instantiate(resultBullet, new Vector3(// 弾の生成
            GetMeteoBreakPos().x,
            GetMeteoBreakPos().y - 8.0f,
            GetMeteoBreakPos().z
            ), Quaternion.identity);
    }

    /// <summary>
    /// リザルトコルーチンの呼び出し
    /// </summary>
    void ResultStart()
    {
        StartCoroutine(ResultCoroutine());
    }

    /// <summary>
    /// リザルト用コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ResultCoroutine()
    {
        for (int i = 0; i < resultPos.Length; i++)
        {
            Instantiate(fallMeteo[Random.Range(0, 2)], resultFallPos.position, Quaternion.identity);// 隕石の生成
            yield return new WaitForSeconds(1.0f);// 1秒待つ
        }
        stayFlg = false;

    }
    /// <summary>
    ///  ランキング用のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator RankingCoroutine()
    {
        for(int i = 0; i < ranking.Length; i++)
        {
            InstanceMeteo();// 隕石の生成
            yield return new WaitForSeconds(1.0f);// 1秒待つ
        }
        stayFlg = false;
    }

    

    /// <summary>
    /// ランキングの更新
    /// </summary>
    void RankingUpdate()
    {
        string[] rankingData = PlayerPrefs.GetString("RankingData", "0,0,0").Split(',');// 保存されているデータを,毎にランキング配列に保存

        // ランキング配列分回す
        for (int i = 0; i < ranking.Length; i++)
        {
            ranking[i] = int.Parse(rankingData[i]);// string型をintに変換
        }
        //Debug.Log("[0]" + ranking[0] + "[1]" + ranking[1] + "[2]" + ranking[2]);// 確認用

        RankingSort(Data.score);// ランキングのソート

        PlayerPrefs.SetString("RankingData", ranking[0] + "," + ranking[1] + "," + ranking[2]);// データの保存
    }

    /// <summary>
    /// タップの入力待機
    /// </summary>
    /// <returns></returns>
    IEnumerator InputWait()
    {
     result:
        yield return new WaitUntil(Touch);
        yield return new WaitWhile(Touch);// タップの待機
        if (stayFlg) goto result;
        DeleteScoreUI();
        rankingCount = 0;// ランキングカウントのリセット
        label[0].SetActive(false);
        label[1].SetActive(true);
        state = ResultState.RANKING;
        StartCoroutine(RankingCoroutine());
        
        SetFlg();
     ranking:
        yield return new WaitUntil(Touch);
        yield return new WaitWhile(Touch);
        if (stayFlg) goto ranking;
        sf.SceneChange("Game");
    }

    bool Touch()
    {
        return Input.GetMouseButtonDown(0);
    }

    public void SetFlg()
    {
        stayFlg = true;
    }

    /// <summary>
    /// ランキングデータのリセット
    /// </summary>
    void DataReset()
    {
        PlayerPrefs.DeleteKey("RankingData");
    }

    public Vector3 GetResultFallPos()
    {
        return resultFallPos.position;
    }

    public Vector3 GetResultPos()
    {
        return resultPos[rankingCount].position;
    }

    public ResultState GetState()
    {
        return state;

    }

    public bool GetStayFlg()
    {
        return stayFlg;
    }
    
    public string GetResultText()
    {
        string returnText = "";

        rankingCount++;// カウンターをプラス
        switch (rankingCount - 1)
        {
            case 0:
                returnText = Data.score.ToString();
                break;
            case 1:
                returnText = Data.breakMeteoCount.ToString();
                break;
            case 2:
                returnText = Data.breakUFOCount.ToString();
                break;
        }

        return returnText;
    }

    void DeleteScoreUI()
    {
        for (int i = 0; i < _scoreUI.Count; i++)
        {
            Destroy(_scoreUI[i]);
        }

        _scoreUI.Clear();
    }
}
