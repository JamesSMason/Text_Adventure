using Adventure.Inventories;
using UnityEngine;

namespace Adventure.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviour, IItemHolder
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // STATE
        int index;
        Inventory inventory;

        // PUBLIC

        public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            icon.SetItem(inventory.GetItemInSlot(index));
        }

        public int MaxAcceptable(InventoryItem item)
        {
            if (inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(InventoryItem item, int number)
        {
            inventory.AddItemToSlot(index, item);
        }

        public InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public int GetNumber()
        {
            return 1;
        }

        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index);
        }
    }
}