using UnityEngine;

namespace Model_scene_2
{
    [CreateAssetMenu(fileName ="NewItem", menuName ="Inventory/New item")]
    public class Item : ScriptableObject
    {
        public string Name;
        public Sprite itemImage;
    }
}
