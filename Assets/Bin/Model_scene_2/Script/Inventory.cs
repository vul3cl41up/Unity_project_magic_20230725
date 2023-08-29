using System.Collections.Generic;
using UnityEngine;

namespace Model_scene_2
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
        public List<int> itemHold = new List<int>();
    }
}

