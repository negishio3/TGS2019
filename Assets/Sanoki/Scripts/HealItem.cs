using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour,i_item
{
    public void ItemUse()
    {
        GameSystem.Instance.EarthHeal(50);
        Destroy(gameObject);
    }
}
