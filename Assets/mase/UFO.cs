using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float movespeed;
    public int HP_UFO;
    float UFO_rotspeed=10;
    float SwingRange = 15;
    Vector3 rot_UFO;
    Vector3 UFO_pos;
        // Start is called before the first frame update
    void Start()
    {
        //UFOのオブジェクトの位置情報を代入
        UFO_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movespeed, 0, 0, Space.World);
        rot_UFO =new Vector3(Mathf.Sin(Time.time*UFO_rotspeed)*SwingRange,UFO_rotspeed*Time.time,0);
        transform.eulerAngles = rot_UFO;
        //Debug.Log(Mathf.Sin(Time.time));
        transform.Rotate(0.1f, 0.1f, 0);
        //if (transform.position.x<)
        //{

        //}
        if (HP_UFO<=0)
        {
            Destroy(gameObject);
        }
        this.gameObject.transform.position = new Vector3(UFO_pos.x, UFO_pos.y + Mathf.PingPong(Time.time, 5), UFO_pos.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Debug.Log("fooo");
            HP_UFO -= 1;
            Destroy(other.gameObject);
        }
    }
}
