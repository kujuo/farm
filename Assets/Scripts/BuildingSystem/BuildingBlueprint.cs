using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// use as item drops
public class BuildingBlueprint : MonoBehaviour
{
    // Start is called before the first frame update
    public string buildingName;
    public string buildingDescription;
    public Building building;
    
    public bool isHealthRegenEffect;
    public bool isShieldEffect;
    public bool isAttackRangeEffect;
    public bool isPoisonEffect;
    private Item item;

    public void initItem(Item item)
    {
        this.item = item;
    }

    public void onClick()
    {
        var s = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        s.z = 0;
        var created = Instantiate(building, s, Quaternion.identity);
        created.Init(isHealthRegenEffect, isShieldEffect, isAttackRangeEffect, isPoisonEffect);
        //BuildingSystemManager.Instance.DisplayBuildingUi(); no longer needed
        InventoryManagerNew.Instance.RemoveItem(item);
        InventoryManagerNew.Instance.ListItems(InventoryManagerNew.Instance.Items);
    }
}
