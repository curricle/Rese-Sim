using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryDisplayHandler : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public GameObject inventorySlotsContainer;
    public GameObject inventorySlot;
    public Sprite emptyBG;
    public Sprite occupiedBG;
    private Transform inventorySlotTransform;
    private Transform inventorySlotsContainerTransform;

    private PlayerInput playerInput;
    private InputAction openInventoryAction;

    //Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public List<GameObject> listOfInventorySlots = new List<GameObject>();

    public static Action<Item, GameObject> onUpdateInventorySlot;

    private int maxInventorySlots;

    private void Awake() {
        inventorySlotTransform = inventorySlot.GetComponent<Transform>();
        inventorySlotsContainerTransform = inventorySlotsContainer.GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();
        openInventoryAction = playerInput.actions["Inventory"];
    }

    private void OnEnable() {
        InventoryHandler.onAddItemToInventory += AddInventoryItemSlot;
        InventoryHandler.onChangeMaxInventorySize += SetMaxInventorySlots;
        
        
    }

    private void OnDisable() {
        InventoryHandler.onAddItemToInventory -= AddInventoryItemSlot;
        InventoryHandler.onChangeMaxInventorySize -= SetMaxInventorySlots;
    }

    private void Start() {
        InitializeInventory();
    }

    public void ToggleInventory() {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    public void AddInventoryItemSlot(Item item) {

        Debug.Log("this is outside the forloop so it should only fire once");

        //loop through current slots
        for(int i = 0; i < listOfInventorySlots.Count; i++) {

            var occupied = listOfInventorySlots[i].GetComponent<UI_InventorySlot>().isOccupied;
            Debug.Log("Entered the for loop");

            Debug.Log(occupied);

            if(!occupied){

                //the issue happens with the below line -- i think within UI_InventorySlot. but why?
                onUpdateInventorySlot?.Invoke(item, listOfInventorySlots[i]);
                Debug.Log("onUpdateInventorySlot fired (inside for loop)");

                return;
            } 

            else if(occupied) {
                Debug.Log($"Inventory slot {i} is occupied");
            }

            Debug.Log("Outside the for loop");
        }
    }

    private void SetMaxInventorySlots(int max) {
        maxInventorySlots = max;
    }

    public void InitializeInventory() {
        for(int i = 0; i < maxInventorySlots; i++) {

            inventorySlot = Instantiate(inventorySlot, inventorySlotsContainer.GetComponent<Transform>());
            inventorySlot.GetComponent<UI_InventorySlot>().id = i;
            inventorySlot.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);

            //set children to inactive
            inventorySlot.GetComponent<UI_InventorySlot>().SetChildrenVisibility(false);

            listOfInventorySlots.Add(inventorySlot);
        }
    }
}
