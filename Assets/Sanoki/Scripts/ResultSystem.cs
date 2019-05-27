using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ResultSystem : SingletonMonoBehaviour<ResultSystem>
{

    public GameObject efectPre;// 隕石が壊れるときのエフェクト
    public GameObject scoreUI;// スコア表示のプレハブ
    public GameObject fallMeteo;// 隕石のプレハブ
    public GameObject resultBullet;// 弾丸プレハブ

    public Transform[] fallMeteoPos = new Transform[3];// 隕石の開始地点
    public Transform[] scorePos = new Transform[3];// scoreの表示位置

    public Text[] rankingText = new Text[3];// ランキング表示
    int[] ranking = new int[3];// ランキングの保存用

    int rankingCount = 0;// ランキングがどこまで表示されたかカウント

    // Start is called before the first frame update
    void Start()
    {
        string[] rankingData = PlayerPrefs.GetString("rankingData", "0,0,0").Split(',');// 保存されているデータを,毎にランキング配列に保存

        // ランキング配列分回す
        for(int i=0; i < ranking.Length; i++)
        {
            ranking[i] = int.Parse(rankingData[i]);// string型をintに変換
        }
        Debug.Log("[0]" + ranking[0] + "[1]" + ranking[1] + "[2]" + ranking[2]);// 確認用

        StartCoroutine(RankingCoroutine());

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
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
        Instantiate(efectPre, instancePos, Quaternion.identity);// エフェクトの生成
        Instantiate(scoreUI, instancePos, Quaternion.identity);// UIの生成

    }


    /// <summary>
    /// 隕石の生成
    /// </summary>
    public void InstanceMeteo()
    {
        Instantiate(fallMeteo, fallMeteoPos[rankingCount].position, Quaternion.identity);
        Instantiate(resultBullet, new Vector3(
            GetMeteoBreakPos().x,
            GetMeteoBreakPos().y - 8.0f,
            GetMeteoBreakPos().z
            ), Quaternion.identity);
    }

    /// <summary>
    ///  ランキング用のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator RankingCoroutine()
    {
        for(int i = 0; i < 3; i++)
        {
            InstanceMeteo();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
