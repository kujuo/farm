using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{

    public Seed seed;
    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void initItem(Item item)
    {
        this.item = item;
    }

    public void onClick()
    {
        var s = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        s.z = 0;
        var created = Instantiate(seed, s, Quaternion.identity);
        created.initItem(item);
        InventoryManagerNew.Instance.RemoveItem(item);
        InventoryManagerNew.Instance.ListItems();
        //InventoryManagerNew.Instance.CheckSeedType();
    }
}
