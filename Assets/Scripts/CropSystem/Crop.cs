using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

enum CropState
{
    EMPTY,
    SEED,
    PLANT
}

public class Crop : MonoBehaviour
{
    /**
     * Loosely coupling, player will access this crop and call it.
     * 
     * Cycle: Empty -> Seed -> Plant -> Empty -> ...
     * Player plant seed and harvest money
     * 
     * Methods:
     * 
     *  setSeed() : put a seed into the crop
     *  harvest() : collect money from the plant
     * 
     */

    // Start is called before the first frame update
    private GameObject readyIcon;
    private CropState cropState;
    private SpriteRenderer spriteRenderer;
    private Seed seed;
    private float totalDuration;
    private GameObject harvestProduct, seedObject;
    public Sprite defaultSprite;
    public Sprite[] seedImages;
    public float dropOffsetX, dropOffsetY;

    private Vector3 botRight;
    // private UIManager uIManager; import UI Manager later
    void Awake()
    {
        cropState = CropState.EMPTY;
        spriteRenderer = GetComponent<SpriteRenderer>();
        readyIcon = transform.GetChild(1).gameObject;
        botRight = spriteRenderer.transform.TransformPoint(new Vector3(spriteRenderer.sprite.bounds.min.x, spriteRenderer.sprite.bounds.max.y, 0));
        botRight.x += spriteRenderer.sprite.bounds.extents.x*2;
        botRight.y -= spriteRenderer.sprite.bounds.extents.y*2;
    }

    private IEnumerator Grow()
    {
        foreach (var image in seedImages)
        {
            spriteRenderer.sprite = image;
            yield return new WaitForSeconds(totalDuration / seedImages.Length);
        }

        this.cropState = CropState.PLANT;
    }

    // Update is called once per frame
    void Update()
    {
        if (cropState == CropState.EMPTY)
        {
            spriteRenderer.sprite = defaultSprite;
            readyIcon.SetActive(false);
        }
        else if (cropState == CropState.PLANT)
        {
            readyIcon.SetActive(true);
        }
    }

    public bool Plant(GameObject seed)
    {
        if (cropState == CropState.EMPTY)
        {
            seedObject = Instantiate(seed, transform.position, Quaternion.identity);
            this.setSeed(seedObject);
            GetComponent<BoxCollider2D>().isTrigger = false;
            seedObject.SetActive(false);
            return true;
        }

        return false;
    }

    private void OnMouseDown()
    {
        if (cropState == CropState.PLANT)
        {
            harvest();
        }
    }

    
    private void OnMouseOver()
    {
        if (cropState == CropState.PLANT) spriteRenderer.color = Color.green;
        else spriteRenderer.color = Color.red;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    /**
     * Plant the crop
     * Player need to choose from the invenroty to give this crop a seed
     * It expects type SEED, otherwise, it throws an error.
     * 
     */
    public void setSeed(GameObject seedObj) 
    {
        var seed = seedObj.GetComponent<Seed>();
        if (cropState == CropState.EMPTY)
        {
            this.seed = seed;
            this.totalDuration = seed.getHarvestingTime();
            this.cropState = CropState.SEED;
            this.harvestProduct = seed.getHarvestProduct();
            this.seedImages = seed.getImages();
            this.spriteRenderer.sprite = seedImages[0];
            StartCoroutine(Grow());

        }
    }

    /**
     * Harvest the crop
     * The function will check if the crop is ready for harvesting, if yes, it will
     * give the player some amount of money, and update the crop. Otherwise, the 
     * money will be 0.
     */
    public GameObject harvest()
    {
        if (cropState == CropState.PLANT)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            cropState = CropState.EMPTY;

            Instantiate(harvestProduct, botRight, Quaternion.identity);

            harvestProduct.GetComponent<BoxCollider2D>().isTrigger = true;
            harvestProduct.SetActive(true);
            Destroy(seedObject);

        }
        return null;
    }
}
