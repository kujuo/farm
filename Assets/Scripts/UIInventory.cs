using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private InventoryManager inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    //void Start()
    {
        itemSlotContainer = transform.Find("InventorySlot");
        itemSlotTemplate = itemSlotContainer.Find("ItemButton");
        //Debug.Log(itemSlotContainer);
        //Debug.Log(itemSlotTemplate);
    }

    public void SetInventory(InventoryManager inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (Items item in inventory.GetItemList())
        {
            //Transform template = Instantiate(itemSlotTemplate);
            //Debug.Log(template);
            //RectTransform itemSlotRectTransform = template.GetComponent<RectTransform>();
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            Debug.Log(itemSlotRectTransform);
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
