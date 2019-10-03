using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    /// <summary>
    /// GameStartのアニメーション開始時に呼ばれるやつ
    /// </summary>
    public void AnimationStart()
    {
        GameSystem.Instance.DataReSet();// タイマーなどを初期化
    }

    /// <summary>
    /// GameStartアニメーションの終了時に呼ばれるやつ
    /// </summary>
    public void AnimationFinish()
    {
        GameSystem.Instance.GameStart();// ゲーム開始関数を呼び出し
        gameObject.SetActive(false);// GameStartの非表示
    }
}
