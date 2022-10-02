using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerNew : MonoBehaviour
{
    public static InventoryManagerNew Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent; //item slot icon
    public GameObject InventoryItem;

    public InventoryItemController[] InventoryItems;
    private void Awake()
    {
        Instance = this;

    }

    //public void Add(Item item)
    //{
    //    Items.Add(item);
    //}

    public void Add(Item item)
    {
        bool itemInInventory = false;
        for (int i = 0; i < Items.Count; i++)
            {
            if (item.name == Items[i].name)
            {
                Items[i].amount += 1;
                itemInInventory = true;
            }
        }
        if (itemInInventory == false)
        {
            Items.Add(item);
            item.amount = 1;
        }
    }

    //public void Remove(Item item)
    //{
    //    Items.Remove(item);
    //}
    //

    public void RemoveItem(Item item)
    {
        Item needRemovedItem = null;
        for (int i = 0; i < Items.Count; i++)
        {
            if (item.name == Items[i].name)
            {
                Items[i].amount -= 1;
                if (Items[i].amount == 0)
                {
                    needRemovedItem = Items[i];
                }
            }
        }
        if (needRemovedItem != null)
        {
            Items.Remove(needRemovedItem);
        }
    }

    //list item icons in inventory ui
    //public void ListItems()
    //{
    //    //clean inside of content before instantiating
    //    foreach (Transform item in ItemContent)
    //    {
    //        Destroy(item.gameObject);
    //    }

    //    foreach (var item in Items)
    //    {
    //        GameObject obj = Instantiate(InventoryItem, ItemContent);
    //        var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
    //        itemIcon.sprite = item.icon;
    //        var itemAmount = obj.transform.Find("ItemAmount").GetComponent<Text>();
    //        itemAmount.text = item.amount.ToString();
    //    }

    //    SetInventoryItems();
    //}

    public void ListItems(List<Item> ItemList)
    {
        //clean inside of content before instantiating
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in ItemList)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
            itemIcon.sprite = item.icon;
            var itemAmount = obj.transform.Find("ItemAmount").GetComponent<Text>();
            itemAmount.text = item.amount.ToString();
            Button btn = obj.GetComponent<Button>();
            // binds clicking to building
            if (item.prefab.GetComponent<BuildingBlueprint>())
            {
                item.prefab.GetComponent<BuildingBlueprint>().initItem(item);
                btn.onClick.AddListener(item.prefab.GetComponent<BuildingBlueprint>().onClick);
            }
            // TODO, binds clicking to crops/etc
        }

        SetInventoryItems();
    }



    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }

    }

    //check if item in the inventory is of seed type
    public void CheckSeedType()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].type == Item.itemType.Seed)
            {

            }
        }
    }


}
