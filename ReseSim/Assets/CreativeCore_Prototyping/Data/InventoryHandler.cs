using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public Inventory inventory;
    public static Action<int> onChangeMaxInventorySize;
    public static Action<Item> onAddItemToInventory;
    public bool hasItem;

    private void Awake() {
        ChangeInventoryMaxSize(8);
    }

    private void OnEnable() {
        ItemHandler.onItemPickup += AddItemToInventory;
    }

    private void OnDisable() {
        ItemHandler.onItemPickup -= AddItemToInventory;
    }

    private void AddItemToInventory(Item item) {
        inventory.AddItem(item, item.amount);
        onAddItemToInventory?.Invoke(item);
        Debug.Log($"{item.itemName} added to inventory!");
    }

    public bool CheckInventory(Item item) {

        for(int i = 0; i < inventory.inventorySlotsContainer.Count; i++) {
            if(item == inventory.inventorySlotsContainer[i].item) {
                hasItem = true;
            }
            else {
                hasItem = false;
            }
        }
        return hasItem;
    }

    private void ChangeInventoryMaxSize(int size) {
        onChangeMaxInventorySize?.Invoke(size);
    }

    private void OnApplicationQuit() {
        inventory.inventorySlotsContainer.Clear();
    }
}

