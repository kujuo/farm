using UnityEngine;

public class Seed : MonoBehaviour
{
    public Sprite seedImage;
    public Sprite plantImage;
    public Sprite[] images; // All image for this plant from start to end
    public GameObject harvestProduct;
    public Seed seed;
    public float harvestTime;
    private SpriteRenderer spriteRenderer;
    private bool isPlanted;
    private bool canPlant;
    private Item item;
    private GameObject soil;
    private BoxCollider2D seedCollider;
    
    private void Awake()
    {
        isPlanted = false;
        canPlant = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        seedImage = images[0];
        spriteRenderer.sprite = seedImage;
    }

    public Sprite getSeedImage()
    {
        return seedImage;
    }

    public Sprite[] getImages()
    {
        return images;
    }

    public GameObject getHarvestProduct()
    {
        return harvestProduct;
    }

    public Sprite getPlantImage()
    {
        return plantImage;
    }

    public float getHarvestingTime()
    {
        return harvestTime;
    }

    private void Update()
    {
        if (!isPlanted)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
            // check for collision
            if (Input.GetKeyDown(KeyCode.Mouse0) && canPlant) PlantSeed();
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                InventoryManagerNew.Instance.Add(item);
                InventoryManagerNew.Instance.ListItems();
                Destroy(gameObject);
            }
        }
    }

    public void initItem(Item i)
    {
        item = i;
    }

    private void PlantSeed()
    {
        if (soil.GetComponentInParent<Crop>().Plant(gameObject)){
            isPlanted = true;
        }
        else
        {
            InventoryManagerNew.Instance.Add(item);
            InventoryManagerNew.Instance.ListItems();
        }
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "SeedCollider")
        {
            soil = other.gameObject;
            soil.GetComponentInParent<SpriteRenderer>().color = Color.green;
            canPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (soil) soil.GetComponentInParent<SpriteRenderer>().color = Color.white;
        canPlant = false;
    }
}
