using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model_scene_1
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
    }

}



