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

    public static bool pauseFlg = true;//ポーズ用フラグ

    //public static bool stayFlg = true;
}
