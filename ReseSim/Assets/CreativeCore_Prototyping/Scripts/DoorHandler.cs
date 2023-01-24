using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public bool locked;
    private GameObject door;

    private void Awake() {
        door = this.gameObject;
    }

    private void OnEnable() {
        KeypadHandler.keypadSuccess += OpenDoor;
    }

    private void OnDisable() {
        KeypadHandler.keypadSuccess -= OpenDoor;
    }

    public void OpenDoor(bool isUnlocked) {
        if(isUnlocked){
            locked = false;
            door.SetActive(false);
        }
    }
}
