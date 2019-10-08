using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static int score = 0;//スコア

    public const int EarthMaxHP = 1000;//地球の初期耐久値
    public static int earthHP;//耐久値

    public static float timmer;//制限時間
    public static int breakMeteoCount;// 壊した隕石カウンター
    public static int breakUFOCount;// 壊したUFOカウンター

    public static bool gamestartFlg = false;// ゲーム開始用フラグ
    public static bool gyroFlg = false;// ジャイロ操作を実行するフラグ
    public static bool pauseFlg = false;//ポーズ用フラグ

    public static bool debugFlg = false;// デバッグモード用フラグ

    public static int combo = 0;//コンボ数

    // ゲームモードの種類
    public enum ModeType
    {
        Endless,// エンドレスモード
        TimeAttack// タイムアタックモード
    }

    public static ModeType GameMode;// タイプの宣言
}
