using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Transform target;
    void Update()
    {
        //GameObject player = GameObject.Find("Player");
        //Transform target = Transform.Find("Player");
        float step = Time.deltaTime * speed;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.LookAt(target);
    }
}