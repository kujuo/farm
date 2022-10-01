using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //this script must be added to all item prefabs
    //right now problem is the house prefab is already moved around because of
    //the building script -- so in building script, start should be changed. Building should
    //be able to move around once building button in inventory is clicked.!!

    public Item Item;

    void Pickup()
    {
        InventoryManagerNew.Instance.Add(Item);
        Destroy(gameObject);
    }

    //pick up objects when clicked on it. Need collider in the prefab gameobjects
    private void OnMouseDown()
    {
        Pickup();
    }
}
