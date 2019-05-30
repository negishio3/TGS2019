using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_ObjectMove : MonoBehaviour
{
    float fallTime = 0.3f;// 落下速度

    public IEnumerator MoveObj(Vector3 startPos, Vector3 endPos)
    {
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime / fallTime;

            yield return null;
        }

        transform.position = endPos;
    }
}
