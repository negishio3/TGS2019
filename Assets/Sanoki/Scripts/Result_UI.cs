using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_UI : MonoBehaviour
{
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InputWait());
        scoreText = GetComponent<Text>();

        SetText();

        //scoreText.text = ResultSystem.Instance.GetRankingScore();
    }

    void SetText()
    {

        switch (ResultSystem.Instance.GetState())
        {
            case ResultSystem.ResultState.RESULT:
                scoreText.text = ResultSystem.Instance.GetResultText();
                break;
            case ResultSystem.ResultState.RANKING:
                scoreText.text = ResultSystem.Instance.GetRankingScore();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// タップの入力待機
    /// </summary>
    /// <returns></returns>
    IEnumerator InputWait()
    {
        stay:
        yield return new WaitUntil(Touch);
        yield return new WaitWhile(Touch);
        if (ResultSystem.Instance.GetStayFlg()) goto stay;
        switch (ResultSystem.Instance.GetState())
        {
            case ResultSystem.ResultState.RESULT:
                Destroy(transform.root.gameObject);
                break;
            default:
                break;
        }
        
    }

    bool Touch()
    {
        return Input.GetMouseButtonDown(0);
    }
}
