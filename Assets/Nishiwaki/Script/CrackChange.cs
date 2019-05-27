using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackChange : MonoBehaviour
{
    public Sprite a;
    public Sprite b;
    public Sprite c;
    Image image;
    int number;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // テスト用
        if (Input.GetKeyDown(KeyCode.Z))
        {
            number++;

            if (number >= 3)
            {
                number = 0;
            }
        }
        switch (number)
        {
            case 0:
                image.sprite = a;
                break;
            case 1:
                image.sprite = b;
                break;
            case 2:
                image.sprite = c;
                break;
        }
    }
}
