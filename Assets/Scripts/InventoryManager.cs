using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    //public List<Sprite> inventoryCount;
    public static List<Items> itemList;


    //This is for testing out the functions

    //public InventoryManager()
    //{
    //    itemList = new List<Items>();
    //    AddItem(new Items { itemType = Items.itemTypes.Building, amount = 1});
    //    AddItem(new Items { itemType = Items.itemTypes.Crop, amount = 1 });
    //    //Debug.Log(itemList.Count);
    //    RemoveItem(Items.itemTypes.Building);
    //    RemoveItem(Items.itemTypes.Crop);
    //    Debug.Log(itemList.Count);
    //}

    public void AddItem(Items item){
        bool itemInInventory = false;
        foreach (Items inventoryItem in itemList)
        {
            if (item.itemType == inventoryItem.itemType)
            {
                inventoryItem.amount += 1;
                itemInInventory = true;
            }
        }
        if(itemInInventory == false)
        {
            itemList.Add(item);
            item.amount = 1;
        } 
    }

    public void RemoveItem(Items.itemTypes item)
    {
        Items needRemovedItem = null;
        foreach (Items inventoryItem in itemList)
        {
            if (item == inventoryItem.itemType)
            {
                inventoryItem.amount -= 1;
                if(inventoryItem.amount == 0)
                {
                    needRemovedItem = inventoryItem;
                }
            }
        }
        if(needRemovedItem != null)
        {
            itemList.Remove(needRemovedItem);
        }
        }

        public List<Items> GetItemList()
    {
        return itemList;
    }




}
