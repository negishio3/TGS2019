using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Title_System : MonoBehaviour,i_Objects
{
    int HP_title = 5;// タイトルの耐久値
    public GameObject BreakEfect;// 壊れたときのエフェクト
    public GameObject gameCanvas;// ゲーム用Canvas

    // ゲームモードのタイプ
    public enum MadeType
    {
        Endless,// エンドレスモード
        TimeAttack// タイムアタックモード
    }

    public MadeType gameMode;// ゲームモード

    public void IDamage()
    {
        HP_title--;
        if (HP_title <= 0)
        {
            TitleBreak();

        }
    }
    
    void TitleBreak()
    {
        Data.GameMode = (Data.ModeType)gameMode;
        Instantiate(BreakEfect, transform.position, Quaternion.identity);
        gameCanvas.SetActive(true);
        Destroy(transform.parent.gameObject);
    }
}
