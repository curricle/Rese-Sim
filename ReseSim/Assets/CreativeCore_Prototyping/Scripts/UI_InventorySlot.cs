using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventorySlot : MonoBehaviour
{
    public Item currentItem;
    public bool isOccupied = false;
    public int id;

    public Transform slot;

    public Sprite occupiedBG;
    public Sprite emptyBG;
    [SerializeField]
    private Sprite currentBG;
    [SerializeField]
    private Sprite currentItemSprite;

    private string currentItemAmount;

    private void Awake() {
        currentBG = gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Image>().sprite;
        SetItemSlotBackground();
    }

    private void OnEnable() {
        InventoryDisplayHandler.onUpdateInventorySlot += SetCurrentItem;
    }

    private void OnDisable() {
        InventoryDisplayHandler.onUpdateInventorySlot -= SetCurrentItem;
    }

    public void SetCurrentItem(Item _item, GameObject currentSlot) {

        if(currentSlot.GetComponent<UI_InventorySlot>().id == id) {
            Debug.Log("SetCurrentItem fired");

            if(_item) {
                currentItem = _item;
                isOccupied = true;
            }
            else {
                currentItem = null;
                isOccupied = false;
            }

            UpdateInventorySlot();
        }
  

    }

    public void UpdateInventorySlot() {

        Debug.Log("UpdateInventorySlot fired");

        if(isOccupied) {
            currentItemSprite = currentItem.itemSprite;
            currentItemAmount = currentItem.amount.ToString();
        }
        else if(!isOccupied) {
            currentItemSprite = null;
            currentItemAmount = "";
        }

        slot.GetChild(1).gameObject.GetComponent<Image>().sprite = currentItemSprite;
        slot.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = currentItem.amount.ToString();

        SetItemSlotBackground();
        SetChildrenVisibility(isOccupied);
    }

    public void SetItemSlotBackground() {
        if(isOccupied) {
            currentBG = occupiedBG;
        }
        else {
            currentBG = emptyBG;
        }

        slot.GetChild(0).gameObject.GetComponent<Image>().sprite = currentBG;
    }

    public void SetChildrenVisibility(bool isActive) {
        for(int i = 1; i < gameObject.GetComponent<Transform>().childCount; i++) {
            Debug.Log("SetChildrenVisibility fired (for loop running)");
            gameObject.GetComponent<Transform>().GetChild(i).gameObject.SetActive(isActive);
        }
    }
}
