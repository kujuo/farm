using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerNew : MonoBehaviour
{
    public static InventoryManagerNew Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent; //item slot icon
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;

    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    //list item icons in inventory ui
    public void ListItems()
    {
        //clean inside of content before instantiating
        //foreach (Transform item in ItemContent)
        //{
        //    Destroy(item.gameObject);
        //}
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
            Debug.Log(itemIcon);
            itemIcon.sprite = item.icon;
        }
    }
        

}
