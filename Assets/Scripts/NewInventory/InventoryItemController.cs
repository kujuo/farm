using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    //public Button RemoveButton;

    //public void RemoveItem()
    //{
    //    InventoryManagerNew.Instance.Remove(item);
    //}

    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
