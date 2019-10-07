using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeItem : MonoBehaviour,i_item
{
    public void ItemUse()
    {
        GameSystem.Instance.AddTime(5);
    }
}
