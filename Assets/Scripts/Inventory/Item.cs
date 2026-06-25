using UnityEngine;

public class Item
{
    public GameObject itemObject;
    public string itemName;

    public Item(GameObject go)
    { 
        itemObject = go;
        itemName = itemObject.name;
    }
}
