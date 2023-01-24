using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 0)]

public class Inventory : ScriptableObject {

      public List<InventorySlot> inventorySlotsContainer = new List<InventorySlot>();
      public int maxInventorySize = 8;

      public void AddItem(Item item, int amount) {
        bool hasItem = false;
        for (int i = 0; i < inventorySlotsContainer.Count; i++) {
            if(inventorySlotsContainer[i].item == item) {
                inventorySlotsContainer[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem) {
            inventorySlotsContainer.Add(new InventorySlot(item, amount));
        }
        Debug.Log($"Inventory length: {inventorySlotsContainer.Count}");
      }

}

[System.Serializable]
public class InventorySlot {

    public Item item;
    public int amount;

    public InventorySlot(Item _item, int _amount) {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value) {
        amount += value;
    }
}