using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Default Item")]
public class Item : ScriptableObject {

public string itemName;
int itemID;
public GameObject itemGameObject;
public Sprite itemSprite;
public bool isItemUnique;
[TextArea(5, 5)]
public string itemDescription;
public int amount;    

}

