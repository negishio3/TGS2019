using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_UI : MonoBehaviour
{
    Text scoreText;// score表示テキスト
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION05);
        scoreText = GetComponent<Text>();// 自身のTextコンポーネント取得

        SetText();// 表示するテキストを設定

        //scoreText.text = ResultSystem.Instance.GetRankingScore();
    }

    void SetText()
    {
        // 遷移状況を取得
        switch (ResultSystem.Instance.GetState)
        {
            case ResultSystem.ResultState.RESULT:// リザルト中
                scoreText.text = ResultSystem.Instance.GetResultText();// 取得
                break;
            case ResultSystem.ResultState.RANKING:// ランキング中
                if (ResultSystem.Instance.RankingCount == ResultSystem.Instance.Rank_No)
                    scoreText.color = Color.red;
                scoreText.text = ResultSystem.Instance.GetRankingScore();// 取得
                break;
            default:
                break;
        }
    }
}
