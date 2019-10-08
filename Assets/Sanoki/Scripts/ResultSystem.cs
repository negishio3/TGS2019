using System.Collections;
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

    public Transform resultFallPos;// リザルトで使用する隕石の落下開始地点
    public Transform[] resultPos = new Transform[3];// リザルトで使用するTransform

    public GameObject highScore_Obj;// ハイスコアのときに表示するやつ
    bool highScoreFlg;// ↑を表示・非表示する用

    int[] ranking = new int[3];// ランキングの保存用

    int rankingCount = 0;// ランキングがどこまで表示されたかカウント

    SceneFader sf;// シーンフェーダー

    int transition;// 遷移状況

    int rank_No = -1;// 今回の順位(順位外なら-1位)

    //-----------------------
    List<GameObject> _scoreUI = new List<GameObject>();// スコアUIの一時保存用List
    //-----------------------

    // リザルトシーンのステート
    public enum ResultState{
        RESULT,// リザルト
        RANKING// ランキング
    }

    public ResultState state = ResultState.RESULT;// 初期はリザルトから


    // Start is called before the first frame update
    void Start()
    {
        rank_No = -1;
        AudioManager.Instance.PlayBGM(AUDIO.BGM_DISTANTFUTURE);// BGM再生
        sf = FindObjectOfType<SceneFader>();// SceneFaderの読み込み
        RankingUpdate();// ランキングデータの更新
        StartCoroutine(InputWait());// タップの待機処理

        Invoke("ResultStart", 1.0f);// リザルトコルーチン
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) sf.SceneChange("Game");// Gキーでゲームシーン
        if (Input.GetKeyDown(KeyCode.R)) sf.SceneChange("Result");// Rキーでリザルトを再呼び出し
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
                if (rank_No == -1) rank_No = i;// データの更新があったときのみ順位を保存
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
        //rankingCount++;// カウンターをプラス
        switch (rankingCount)
        {
            case 0:
                sData = "1st:" + ranking[rankingCount];
                break;
            case 1:
                sData = "2nd:" + ranking[rankingCount];
                break;
            case 2:
                sData = "3rd:" + ranking[rankingCount];
                break;
        }
        rankingCount++;// カウンターをプラス
        return sData;// 表示する文字列を返す
    }

    /// <summary>
    /// 演出のスキップ
    /// </summary>
    void ResultSkip()
    {
        if (state == ResultState.RESULT) highScore_Obj.SetActive(highScoreFlg);
        StopCoroutine(state == ResultState.RESULT ? ResultCoroutine() : RankingCoroutine());// 遷移状況によってコルーチンを止める
        for (int i = rankingCount; i < resultPos.Length; i++)
        {
            EfectInstance(state == ResultState.RESULT ? resultPos[i].position : scorePos[i].position);// 遷移状況に応じてUIの表示位置を調整
        }

    }

    /// <summary>
    /// エフェクトを生成
    /// </summary>
    /// <param name="instancePos">生成する座標</param>
    public void EfectInstance(Vector3 instancePos)
    {
        //audiomanager.instance.playse(audio.se_se_maoudamashii_explosion05);// seの再生
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
            Camera.main.ViewportToWorldPoint(Vector3.zero).y-resultBullet.transform.localScale.y,
            GetMeteoBreakPos().z
            ), Quaternion.identity);
    }

    /// <summary>
    /// リザルトコルーチンの呼び出し
    /// </summary>
    void ResultStart()
    {
        StartCoroutine(ResultCoroutine());// コルーチンの再生
    }

    /// <summary>
    /// リザルト用コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ResultCoroutine()
    {
        highScore_Obj.SetActive(highScoreFlg);// ハイスコアの表示・非表示
        for (int i = 0; i < resultPos.Length; i++)
        {
            if (transition >= 1) yield break;// 遷移状況が1以上になったら終了
            Instantiate(fallMeteo[Random.Range(0, 2)], resultFallPos.position, Quaternion.identity);// 隕石の生成
            yield return new WaitForSeconds(1.0f);// 1秒待つ
        }
        transition = 1;// 遷移状況を1に

    }
    /// <summary>
    ///  ランキング用のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator RankingCoroutine()
    {
        for(int i = 0; i < ranking.Length; i++)
        {
            if (transition >= 3) yield break;// 遷移状況が3以上ならスキップ
            InstanceMeteo();// 隕石の生成
            yield return new WaitForSeconds(1.0f);// 1秒待つ
        }
        transition = 3 ;// 遷移状況を3に
    }

    

    /// <summary>
    /// ランキングの更新
    /// </summary>
    void RankingUpdate()
    {
        string[] rankingData = (Data.GameMode == Data.ModeType.Endless) ?
            PlayerPrefs.GetString("EndlessMode_Data", "0,0,0").Split(',') :// 保存されているデータを
            PlayerPrefs.GetString("TimeAttackMode_Data", "0,0,0").Split(',');// ゲームモード毎にランキング配列に保存

        // ランキング配列分回す
        for (int i = 0; i < ranking.Length; i++)
        {
            ranking[i] = int.Parse(rankingData[i]);// string型をintに変換
        }
        //Debug.Log("[0]" + ranking[0] + "[1]" + ranking[1] + "[2]" + ranking[2]);// 確認用

        if (Data.score > ranking[0]) highScoreFlg = true;

        RankingSort(Data.score);// ランキングのソート

        switch (Data.GameMode)// ランキングデータの保存
        {
            case Data.ModeType.Endless:
                PlayerPrefs.SetString("EndlessMode_Data", ranking[0] + "," + ranking[1] + "," + ranking[2]);
                break;
            case Data.ModeType.TimeAttack:
                PlayerPrefs.SetString("TimeAttackMode_Data", ranking[0] + "," + ranking[1] + "," + ranking[2]);
                break;
            default:
                Debug.LogError(Data.GameMode + "用の処理が存在しません");
                break;
        }
    }

    /// <summary>
    /// タップの入力待機
    /// </summary>
    /// <returns></returns>
    IEnumerator InputWait()
    {
        transition = 0;// 遷移状況を初期化
        while (true)
        {
            if (Input.GetMouseButtonDown(0))// タップされたとき
            {
                switch (transition)
                {
                    case 0:
                        ResultSkip();// 演出のスキップ
                        transition++;// 遷移状況を1つ進める
                        break;
                    case 1:
                        transition++;// 遷移状況を1つ進める
                        DeleteScoreUI();// 生成したUIを削除
                        rankingCount = 0;// ランキングカウントのリセット
                        label[0].SetActive(false);// リザルトCanvasを非表示
                        label[1].SetActive(true);// ランキングCanvasを表示
                        state = ResultState.RANKING;// ステートを更新
                        StartCoroutine(RankingCoroutine());// コルーチンの再生
                        //SetFlg();
                        
                        break;
                    case 2:
                        ResultSkip();// 演出のスキップ
                        transition++;// 遷移状況を1つ進める
                        break;
                    case 3:
                        sf.SceneChange("Game");// Gameシーンへ
                        break;
                    default:
                        Debug.LogError("以降の処理がありません");// 例外処理
                        break;
                }
               
            }
            yield return null;// 1フレーム待つ
        }
    }

    /// <summary>
    /// ランキングデータのリセット
    /// </summary>
    void DataReset()
    {
        PlayerPrefs.DeleteKey("EndlessMode_Data");
        PlayerPrefs.DeleteKey("TimeAttackMode_Data");
    }

    public Vector3 GetResultFallPos
    {
        get
        {
            return resultFallPos.position;
        }
    }

    public Vector3 GetResultPos
    {
        get
        {
            return resultPos[rankingCount].position;
        }
    }

    public ResultState GetState
    {
        get
        {
            return state;
        }
    }

    /// <summary>
    ///  リザルトで使用するTextデータ
    /// </summary>
    /// <returns></returns>
    public string GetResultText()
    {
        string returnText = "";

        switch (rankingCount)
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

        rankingCount++;// カウンターをプラス

        return returnText;
    }

    /// <summary>
    /// UIの削除
    /// </summary>
    void DeleteScoreUI()
    {
        for (int i = 0; i < _scoreUI.Count; i++)// 生成されているUI分回す
        {
            Destroy(_scoreUI[i]);// 削除
        }

        _scoreUI.Clear();// UIデータクリア
    }
    
    // 遷移状況
    public int Transition
    {
        get { return transition; }
    }
    // 今回の順位
    public int Rank_No
    {
        get { return rank_No; }
    }

    public int RankingCount
    {
        get { return rankingCount; }
    }
}
