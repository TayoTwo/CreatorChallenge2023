using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {

    NONE,
    HAT,
    WEAPON

}

[CreateAssetMenu]
public class ShopItem : ScriptableObject
{

    public string itemName;
    public int itemPrice;
    public GameObject itemPrefab;

    public ItemType itemType;

}
