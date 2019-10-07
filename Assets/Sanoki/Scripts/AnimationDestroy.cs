using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationDestroy : MonoBehaviour
{
    /// <summary>
    /// 表示するスコアを設定する
    /// </summary>
    /// <param name="addScore"></param>
    public void setText(int addScore)
    {
        GetComponentInChildren<Text>().text="+"+addScore;
    }
    /// <summary>
    /// アニメーションを削除する
    /// </summary>
    public void AniDestroy()
    {
        Destroy(gameObject);
    }
}
