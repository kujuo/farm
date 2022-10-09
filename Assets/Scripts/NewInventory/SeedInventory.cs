using UnityEngine;
using UnityEngine.SceneManagement;

public class SeedInventory : MonoBehaviour
{

    public Seed seed;
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
            var created = Instantiate(seed, s, Quaternion.identity);
            created.initItem(item);
            InventoryManagerNew.Instance.RemoveItem(item);
            InventoryManagerNew.Instance.ListItems();
        }
    }
}
