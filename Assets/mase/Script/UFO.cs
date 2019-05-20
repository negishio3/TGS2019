using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float movespeed;//UFOの移動速度
    public int HP_UFO;//UFOのHP
    float UFO_rotspeed=10;
    float SwingRange = 15;
    Vector3 rot_UFO;
    Vector3 UFO_pos;
    public int pos;
        // Start is called before the first frame update
    void Start()
    {
        //UFOのオブジェクトの位置情報を代入
        UFO_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movespeed, -0.01f, 0, Space.World);

        rot_UFO = new Vector3(Mathf.Sin(Time.time*UFO_rotspeed)*SwingRange,UFO_rotspeed*Time.time,0);
        transform.eulerAngles = rot_UFO;
        transform.position = new Vector3(transform.position.x, 5+Mathf.PingPong(Time.time, 1), transform.position.z);
        //float sin = Mathf.Sin(Time.time);
        //this.transform.position = new Vector3(UFO_pos.x, sin, UFO_pos.z);
        //Debug.Log(Mathf.Sin(Time.time));
        //transform.Rotate(0.1f, 0.1f, 0);
        //transform.position = new Vector3(transform.position.x, 5 + Mathf.Sin(Time.frameCount * 1f), transform.position.z);
        //if (transform.position.x<)
        //{

        //}
        if (HP_UFO<=0)
        {
            Destroy(gameObject);//㏋が0になったら消す
        }
        //this.gameObject.transform.position = new Vector3(UFO_pos.x,(UFO_pos.y + Mathf.PingPong(Time.time, 2)), UFO_pos.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Debug.Log("fooo");
            HP_UFO -= 1;//弾が当たったらHPを1減らす
            Destroy(other.gameObject);
        }
    }
}
