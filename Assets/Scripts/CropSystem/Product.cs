using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Product : MonoBehaviour
{
    InventoryManagerNew instance;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = Color.red;
    }

    public void onClicK()
    {
        Debug.Log("product has been clicked");
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
