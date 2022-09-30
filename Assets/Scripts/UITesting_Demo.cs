using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITesting_Demo : MonoBehaviour
{
    private InventoryManager inventory;
    [SerializeField] private UIInventory uiInventory;
    //private void Awake()
    void Start()
    {
        inventory = new InventoryManager();
        uiInventory.SetInventory(inventory);
    }
}
