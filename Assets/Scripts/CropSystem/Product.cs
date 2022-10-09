using UnityEngine;


public class Product : MonoBehaviour
{
    private InventoryManagerNew instance;
    public float healthRestore;
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
        PlayerController.instance.gainHealth(healthRestore);
        InventoryManagerNew.Instance.RemoveItem(item);
        InventoryManagerNew.Instance.ListItems();
    }
}
