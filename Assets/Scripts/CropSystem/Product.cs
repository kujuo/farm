using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Product : MonoBehaviour
{
    InventoryManagerNew instance;
    private SpriteRenderer spriteRenderer;
    private Item item;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void InitItem(Item item)
    {
        this.item = item;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = Color.green;
    }

    public void onClicK()
    {
        
        Debug.Log("About to delete");
        //Do sth to player, not sure yet
        var player = GameObject.FindGameObjectsWithTag("Player")[0];

        player.GetComponent<PlayerController>();
        InventoryManagerNew.Instance.RemoveItem(item);
        InventoryManagerNew.Instance.ListItems();
        //InventoryManagerNew.Instance.CheckCropType();
    }

    //private void OnMouseDown()
    //{
    //    var inventoryProduct = new Item
    //    {
    //        id = 01234,
    //        itemName = "prc",
    //        value = 0,
    //        amount = 1,
    //        icon = spriteRenderer.sprite,
    //        type = Item.itemType.Crop,
    //    };

    //    InventoryManagerNew.Instance.Add(inventoryProduct);
    //    Destroy(this.transform.gameObject);
    //}
}
