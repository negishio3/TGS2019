using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Title_System : MonoBehaviour,i_Objects
{
    int HP_title = 5;
    public GameObject BreakEfect;
    public GameObject gameCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IDamage()
    {
        HP_title--;
        if (HP_title <= 0)
        {
            TitleBreak();

        }
    }
    
    void TitleBreak()
    {
        Instantiate(BreakEfect, transform.position, Quaternion.identity);
        gameCanvas.SetActive(true);
        Destroy(gameObject);
    }
}
