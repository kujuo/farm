using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManagerNew : MonoBehaviour
{
    public static InventoryManagerNew Instance;
    public List<Item> Items = new List<Item>();
   
    public Transform ItemContent; //item slot icon
    public GameObject InventoryItem;
    public GameObject Content;

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

    //show items on ui window
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
            if (!item.prefab) return;
            if (item.prefab.GetComponent<BuildingBlueprint>())
            {
                item.prefab.GetComponent<BuildingBlueprint>().initItem(item);
                btn.onClick.AddListener(item.prefab.GetComponent<BuildingBlueprint>().onClick);
            }
            else if (item.prefab.GetComponent<SeedInventory>())
            {
                item.prefab.GetComponent<SeedInventory>().initItem(item);
                btn.onClick.AddListener(item.prefab.GetComponent<SeedInventory>().onClick);
            }
            else if (item.prefab.GetComponent<Product>())
            {
                item.prefab.GetComponent<Product>().InitItem(item);
                btn.onClick.AddListener(item.prefab.GetComponent<Product>().onClicK);
            }
            //TODO, binds clicking to crops/ etc
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
        List<Item> Seeds = new List<Item>();
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].type == Item.itemType.Seed)
            {
                Seeds.Add(Items[i]);
            }
        }
        ListItems(Seeds);
        
    }

    public void CheckBuildingType()
    {
        List<Item> Buildings = new List<Item>();
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].type == Item.itemType.Building)
            {
                Buildings.Add(Items[i]);
            }
        }
        ListItems(Buildings);
    }

    public void CheckCropType()
    {
        List<Item> Crops = new List<Item>();
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].type == Item.itemType.Crop)
            {
                Crops.Add(Items[i]);
            }
        }
        ListItems(Crops);
    }

    public void checkListEmpty()
    {
        if (Items.Count == 0)
        {
            Content.SetActive(false);
        }
        else
        {
            Content.SetActive(true);
        }
    }


}
