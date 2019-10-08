using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    public void SetComboText()
    {
        GetComponentInChildren<Text>().text = Data.combo + "combo!";
    }

    public void AnimationFinish()
    {
        Destroy(gameObject);
    }
}
