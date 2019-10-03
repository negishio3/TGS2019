using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameSystem : SingletonMonoBehaviour<GameSystem>
{
    public Text scoreText;//スコア用テキスト
    public GameObject timmerText_Obj;//制限時間用テキスト
    Text timmerText;

    public Image damageImage;// 体力が少ないときに表示する用

    SceneFader sf;// フェードプログラム

    public GameObject cautionCanvas;// カンバス
    public GameObject textCanvas;// テキスト用のカンバス

    bool settingsFlg = false;// 設定画面の表示フラグ
    bool moveFlg = false;// 設定画面が動いているか

    public GameObject settingsBackImage;// 設定画面の背景画像
    public Image buttonImage;// 設定画面を表示するためのボタン
    public Sprite[] SettingsButtonSprits;// ↑のボタン用スプライト
    public Toggle gyroToggle;// 操作モードを変更するToggle

    public GameObject mainCamera;// 画面を揺らす用

    int tapCount = 0;// タップした回数

    public GameObject[] debugButtons;// デバッグモード用の隠しボタン

    //// ゲームモードのタイプ
    //public enum ModeType
    //{
    //    Endless,
    //    TimeAttack
    //}

    //public ModeType GameMode;// タイプの宣言

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(AUDIO.BGM_THEEXPENDABLES);// BGMを再生
        sf = FindObjectOfType<SceneFader>();// SceneFaderを取得
        timmerText = timmerText_Obj.GetComponent<Text>();// テキストコンポーネントを取得
        gyroToggle.isOn = Data.gyroFlg;// 操作方法を取得して繁栄
        for (int i = 0; i < debugButtons.Length; i++)
        {
            debugButtons[i].SetActive(Data.debugFlg);// 隠しボタンを表示・非表示
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))// Rキーが押されたら
            GameFinish();
        if(Input.GetKeyDown(KeyCode.G)) sf.SceneChange("Game");// Gキーでゲームリスタート
        if (!Data.gamestartFlg||Data.pauseFlg) return;// ゲーム中でないなら以下の処理を飛ばす
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    /// <param name="addScore">加算するスコア</param>
    public void AddScore(int addScore)
    {
        Data.score += addScore;// 引数分スコアを加算
        scoreText.text ="Score:"+Data.score;// スコアを反映
    }

    /// <summary>
    /// 地球がダメージを受ける
    /// </summary>
    /// <param name="damage">受けるダメージ値</param>
    public void EarthDamage(int damage)
    {
        Data.earthHP -= damage;// 引数分耐久値を減らす
        if (Data.earthHP <= Data.EarthMaxHP*0.3)// 耐久値が残り３割に達したら
        {
            StartCoroutine(DamageAnimation());// 瀕死のイメージを表示
        }
    }

    /// <summary>
    /// 地球を耐久値を回復
    /// </summary>
    /// <param name="healValue">回復する値</param>
    public void EarthHeal(int healValue)
    {
        Data.earthHP += healValue;// 引数分耐久値を回復
        // ここに耐久値を回復するアニメーション
        if (Data.earthHP > Data.EarthMaxHP * 0.3)// 耐久値が残り３割に達したら
        {
            StartCoroutine(DamageAnimation());// 瀕死のイメージを表示
        }

    }

    /// <summary>
    /// タイマーを開始
    /// </summary>
    public void TimmerStart()
    {
        StartCoroutine(TimeCounter());// コルーチンを開始
    }

    /// <summary>
    /// タイムをカウントする
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeCounter()
    {
        //ゲームモードに応じて処理を変更
        switch (Data.GameMode) {
            case Data.ModeType.Endless:// エンドレス
                while (!Data.pauseFlg)
                {
                    Data.timmer += Time.deltaTime;// タイムを加算
                    timmerText.text = "TIME:" + (int)Data.timmer;// タイムを反映
                    yield return null;
                }
                break;
            case Data.ModeType.TimeAttack:// タイムアタック
                while (Data.timmer >= 0)
                {
                    if (Data.pauseFlg) yield break;// ポーズ中なら終了
                    Data.timmer -= Time.deltaTime;// タイムを減算
                    timmerText.text = "TIME:" + (int)Data.timmer;// タイムを反映
                    if (Data.timmer <= 0)// 制限時間が0以下になったら
                    {
                        GameFinish();// ゲーム終了
                    }
                    yield return null;
                }
                
                break;
            default:
                Debug.LogError("現在選択中のモード用の処理が存在しません");// 上記モード以外ならエラーメッセージ
                break;
        }
    }
    
    /// <summary>
    /// タイムを加算
    /// </summary>
    /// <param name="addTime">加算するタイム</param>
    void AddTime(int addTime)
    {
        Data.timmer += addTime;// 加算
        // ここにタイムの加算アニメーション
    }

    /// <summary>
    /// ゲームスタート時に呼び出す
    /// </summary>
    public void GameStart()
    {
        Data.gamestartFlg = true;// ゲーム開始に設定
        TimmerStart();// タイマーを開始
        cautionCanvas.SetActive(false); // 注意の表示を消す
        textCanvas.SetActive(false);// 操作の表示を消す
    }

    /// <summary>
    /// 地球の耐久値が低くなった時の表示
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageAnimation()
    {
        Color startColor=new Color(1,1,1,0);// 不透明度 0％
        Color endColor=new Color(1,1,1,0.7f);// 不透明度 70%
        float t = 0;// 経過時間のリセット

        //if()
        if (damageImage.color == endColor) yield break;

        while (t < 1)
        {
            damageImage.color = Color.Lerp(startColor, endColor, t);// 徐々に瀕死用背景を表示
            t += Time.deltaTime;// 時間経過
            yield return null;
        }
        damageImage.color = endColor;// エンドカラーに合わせる
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public void GameFinish()
    {
        Data.debugFlg = false;
        settingsFlg = false;// セッティングを中止
        Data.gamestartFlg = false;// ゲーム終了に設定
        sf.SceneChange("Result");// リザルトへ遷移
    }

    /// <summary>
    /// タイトルに戻る
    /// </summary>
    public void ReStart()
    {
        Data.debugFlg = false ;
        settingsFlg = false;// セッティングを中止
        Data.gamestartFlg = false;// ゲーム終了に設定
        sf.SceneChange("Game");// タイトルへ遷移
    }

    /// <summary>
    /// 操作モードを切り替える
    /// </summary>
    public void FlgChange()
    {
        Data.gyroFlg = gyroToggle.isOn;// 操作モードを変更
    }

    /// <summary>
    /// 設定画面を呼び出す
    /// </summary>
    public void SettingsButton()
    {
        if (moveFlg) return;// 設定画面表示中なら
        moveFlg = true;// 表示中に設定
        settingsFlg = !settingsFlg;// フラグの切り替え
        buttonImage.sprite = settingsFlg ? SettingsButtonSprits[1] : SettingsButtonSprits[0];// ボタンのイメージの変更
        StartCoroutine(SettingsImageMove());// 設定画面を動かす
    }


    /// <summary>
    /// 設定画面を表示する
    /// </summary>
    /// <returns></returns>
    IEnumerator SettingsImageMove()
    {
        float t = 0;// 時間をリセット
        Vector2 offPos = new Vector2(0, Screen.height*2);// 設定画面がオフのときの座標
        Vector2 onPos = new Vector2(0, 0.0f);// 設定画面がオンのときの座標

        if (settingsFlg) Data.pauseFlg = true;// 設定画面が表示中になるときポーズフラグをtrueに

        while (t < 1)
        {
            t += Time.deltaTime;// 時間経過を計算

            if (settingsFlg)// フラグがtureのとき
            {
                settingsBackImage.transform.localPosition = Vector2.Lerp(settingsBackImage.transform.localPosition, onPos, t);// 指定座標まで移動
            }
            else// フラグがfalseのとき
            {
                settingsBackImage.transform.localPosition = Vector2.Lerp(settingsBackImage.transform.localPosition, offPos, t);// 指定座標まで移動
            }
            yield return null;
        }

        settingsBackImage.transform.localPosition = settingsFlg ? onPos : offPos;// 座標を微調整

        moveFlg = false;

        if (!settingsFlg)
        {
            Data.pauseFlg = false;// 設定画面が非表示ならポーズフラグをオフに
            TimmerStart();
        }
    }

    public void DataReSet()
    {
        Data.timmer = Data.GameMode == Data.ModeType.Endless ? 0 : 120;// タイマーをリセット
        timmerText.text = "TIME:" + (int)Data.timmer;// タイマーの表示をリセット
        timmerText_Obj.SetActive(Data.GameMode == Data.ModeType.TimeAttack);// モードによってタイマーを表示非表示
        Data.breakMeteoCount = 0;// 壊した隕石の数をリセット
        Data.breakUFOCount = 0;// 壊したUFOの数をリセット
        Data.score = 0;// スコアをリセット
        scoreText.text = "Score:" + Data.score;//表示をリセット
        Data.earthHP = Data.EarthMaxHP;// 耐久値をリセット
    }

    /// <summary>
    /// 画面を揺らす
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));// 揺らすコルーチンを呼ぶ
    }

    // ネットから拾ってきたやつだからよく理解してないが、
    //                        動く‼
    /// <summary>
    /// 画面を揺らす
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <returns></returns>
    private IEnumerator DoShake(float duration, float magnitude)
    {
        Vector3 pos = mainCamera.transform.localPosition;// カメラの座標を取得

        float elapsed = 0f;// 

        while (elapsed < duration)
        {
            float x = pos.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = pos.y + UnityEngine.Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = pos;
    }

    /// <summary>
    /// ゲームモードを変更(デバッグ用)
    /// </summary>
    public void GameModeCange()
    {
        // 最後のモードなら最初のモードに戻る
        if (Data.GameMode == (Data.ModeType)Enum.GetNames(typeof(Data.ModeType)).Length-1)
        {
            Data.GameMode = 0;// 先頭のモードに設定
        }
        else
        {
            Data.GameMode++;// 次のモードに設定
        }
        DataReSet();// データリセット
        Debug.Log(Data.GameMode);
    }

    /// <summary>
    /// デバッグモードを起動するやつ
    /// </summary>
    public void DebugMode()
    {
        tapCount++;// 呼ばれるたびtapCountを加算
        if (tapCount == 5)// 5になったら
        {
            Data.debugFlg = !Data.debugFlg;// フラグ切り替え
            Debug.Log("debugMode：" + Data.debugFlg);// フラグの確認
            tapCount = 0;// カウントをリセット
        }
        for(int i = 0; i < debugButtons.Length; i++)
        {
            debugButtons[i].SetActive(Data.debugFlg);// 隠しボタンの表示非表示
        }
       
    }

    /// <summary>
    /// 保存されているランキングデータの初期化
    /// </summary>
    public void DataClear()
    {
        PlayerPrefs.DeleteKey("EndlessMode_Data");
        PlayerPrefs.DeleteKey("TimeAttackMode_Data");
    }

}
