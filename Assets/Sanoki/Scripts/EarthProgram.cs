using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProgram : MonoBehaviour
{
    SceneFader sf;
    public GameObject explosionEfect;// 爆発エフェクト
    private void Start()
    {
        sf = FindObjectOfType<SceneFader>();// フェーダーを取得
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteo")// 隕石に触れたら
        {
            GameSystem.Instance.Shake(0.25f, 0.1f);
            Instantiate(explosionEfect, other.transform.position, Quaternion.identity);// エフェクトを生成
            AudioManager.Instance.PlaySE(AUDIO.SE_SE_MAOUDAMASHII_EXPLOSION05);// 爆発のSEを再生
            Destroy(other.gameObject);// 隕石を削除
            GameSystem.Instance.EarthDamage(100);// 耐久値を減らす
            if (Data.earthHP <= 0)// 耐久値が0になったら
            {
                GameSystem.Instance.GameFinish();// ゲームを終了
            }
        }
    }
}
