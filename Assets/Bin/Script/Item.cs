using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item",menuName ="Inventory/New item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemhold;
    [TextArea]
    public string itemInfo;

    public bool equip;
    
}
