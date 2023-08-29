using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model_scene_1
{
    public class ItemOnWorld : MonoBehaviour
    {
        public Item thisItem;
        public Inventory playInventory;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                AddNewItem();
                Destroy(this.gameObject);
            }

        }
        public void AddNewItem()
        {
            if (!playInventory.itemList.Contains(thisItem))
            {
                playInventory.itemList.Add(thisItem);

            }
            else
            {
                thisItem.itemhold++;

            }
            InventoryManager.RefreshItem();
        }
    }

}
