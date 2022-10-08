using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name == "HomeBase")
        {
            var s = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            s.z = 0;
            var created = Instantiate(building, s, Quaternion.identity);
            created.Init(isHealthRegenEffect, isShieldEffect, isAttackRangeEffect, isPoisonEffect);
            InventoryManagerNew.Instance.RemoveItem(item);
            BuildingSystemManager.Instance.placeMode = true;
            InventoryManagerNew.Instance.ListItems();
        }
    }
}
