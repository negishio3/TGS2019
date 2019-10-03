using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_ObjectMove : MonoBehaviour
{
    float fallTime = 0.3f;// 落下速度


    /// <summary>
    /// RESULTで使用するオブジェクトの動き(アニメーションでやればよかった．．．)
    /// </summary>
    /// <param name="startPos">移動開始地点</param>
    /// <param name="endPos">目的地</param>
    /// <returns></returns>
    public IEnumerator MoveObj(Vector3 startPos, Vector3 endPos)
    {
        float t = 0;// 経過時間の初期化
        while (t < 1)
        {
            if (ResultSystem.Instance.GetState == ResultSystem.ResultState.RESULT && ResultSystem.Instance.Transition >= 1
                || ResultSystem.Instance.GetState == ResultSystem.ResultState.RANKING && ResultSystem.Instance.Transition >= 3)
                Destroy(gameObject);// 処理の途中で遷移状況が進んだらObjectを削除
            transform.position = Vector3.Lerp(startPos, endPos, t);// Lerpで移動
            t += Time.deltaTime / fallTime;// 経過時間

            yield return null;
        }

        transform.position = endPos;// 終了座標に合わせる

        if (ResultSystem.Instance.GetState == ResultSystem.ResultState.RESULT)// ステートがRESULTなら
        {
            ResultSystem.Instance.EfectInstance(transform.position);// UIを生成
            Destroy(gameObject);// 自信を削除
        }
    }
}
