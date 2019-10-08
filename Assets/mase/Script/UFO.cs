using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour,i_Objects
{
    public enum UFO_Type
    {
        Left,
        Right
    }

    UFO_Type UFOType = UFO_Type.Left;

    public float movespeed;//UFOの移動速度
    public int HP_UFO;//UFOのHP
    float UFO_rotspeed = 10;
    float SwingRange = 15;
    Vector3 rot_UFO;
    float UFO_pos;

    public GameObject[] item;
    int addScore;

    public GameObject addScoreCanvas;
    public GameObject comboCanvas;

        // Start is called before the first frame update
    void Start()
    {
        addScore = (150 / HP_UFO) * (1 + Data.combo / 10);//基本値をHPで割った値をスコアとする

        //UFOのオブジェクトの位置情報を代入
        UFO_pos = this.gameObject.transform.position.y;
        if (transform.position.x >= Vector3.zero.x)
        {
            UFOType = UFO_Type.Right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.pauseFlg) return;
        rot_UFO = new Vector3(Mathf.Sin(Time.time * UFO_rotspeed) * SwingRange, UFO_rotspeed * Time.time, Mathf.Sin(Time.time * UFO_rotspeed) * SwingRange);
        transform.eulerAngles = rot_UFO;
        //上下移動
        transform.position = new Vector3(transform.position.x, UFO_pos + Mathf.PingPong(Time.time, 1), transform.position.z);

        switch (UFOType) {

            case UFO_Type.Left:
                //右に移動
                transform.Translate(movespeed, -0.01f, 0, Space.World);
                // 画面外に出たら削除
                if (transform.position.x >= Camera.main.ViewportToWorldPoint(Vector3.one).x + transform.localScale.x)
                {
                    MeteorGenerator.Instance.ChangeUFOFlg(false);
                    Data.combo = 0;
                    DestroyUFO();//自分を消す
                }

                break;
            case UFO_Type.Right:
                //左に移動
                transform.Translate(-movespeed, -0.01f, 0, Space.World);
                // 画面外に出たら削除
                if (transform.position.x <= Camera.main.ViewportToWorldPoint(Vector3.zero).x - transform.localScale.x)
                {
                    MeteorGenerator.Instance.ChangeUFOFlg(false);
                    Data.combo = 0;
                    DestroyUFO();//自分を消す
                }
                break;
            default:
                Debug.LogError("Typeが選択されていません");
                break;
        }
        if (HP_UFO <= 0)
        {
            Data.combo++;

            switch (Data.GameMode)
            {
                case Data.ModeType.Endless:
                    Instantiate(item[0], transform.position, Quaternion.identity);
                    break;
                case Data.ModeType.TimeAttack:
                    Instantiate(item[1],transform.position, Quaternion.identity);//生成する
                    break;
            }
            MeteorGenerator.Instance.ChangeUFOFlg(false);
            Data.breakUFOCount++;

            Instantiate(comboCanvas, new Vector3(transform.position.x, transform.position.y, -6.0f), Quaternion.identity);
            DestroyUFO();//㏋が0になったら消す
        }
        //this.gameObject.transform.position = new Vector3(UFO_pos.x,(UFO_pos.y + Mathf.PingPong(Time.time, 2)), UFO_pos.z);
    }

    void DestroyUFO()
    {
        //ここにSEを止める処理

        Destroy(gameObject);
    }

    public void IDamage()
    {
        GameObject scoreCanvas = Instantiate(
            addScoreCanvas,
            new Vector3(transform.position.x, transform.position.y, -5.0f),
            Quaternion.identity);
        scoreCanvas.GetComponent<AnimationDestroy>().setText(addScore);
        GameSystem.Instance.AddScore(addScore);// 値分のスコアを加算
        HP_UFO -= 1;//弾が当たったらHPを1減らす

    }
}
