using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerNew : MonoBehaviour
{
    public static InventoryManagerNew Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent; //item slot icon
    public GameObject InventoryItem;
    public GameObject Content;
    public GameObject inventoryUi;

    public Item.itemType filterType;

    private void setFilterType(Item.itemType type)
    {
        filterType = type;
        ListItems();
    }

    //Some quick lambdas for button event handling
    public void setCropFilter() => setFilterType(Item.itemType.Crop);
    public void setSeedFilter() => setFilterType(Item.itemType.Seed);
    public void setBuildingFilter() => setFilterType(Item.itemType.Building);
    public void setAllFilter() => setFilterType(Item.itemType.All);

    private void Awake()
    {
        Instance = this;
        filterType = Item.itemType.All;
        ListItems();
    }

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
        ListItems();
    }


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
        ListItems();
    }


    //show items on ui window
    //Render item from the this items list or (List<Ittem> Items)
    public void ListItems()
    {
        //clean inside of content before instantiating
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        if (Items.Count == 0 && inventoryUi) inventoryUi.SetActive(false);
        foreach (var item in this.Items)
        {
            if (item.type == filterType || filterType == Item.itemType.All)
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
            }

        }

    }
}
