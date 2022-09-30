using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private CropState cropState;
    private SpriteRenderer spriteRenderer;
    private Seed seed;
    private float waitingTime = 0;
    private GameObject harvestProduct, seedObject;
    public Sprite defaultSprite;
    // private UIManager uIManager; import UI Manager later
    void Start()
    {
        cropState = CropState.EMPTY;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cropState == CropState.SEED)
        {
            if (waitingTime > 0)
            {
                waitingTime -= Time.deltaTime;
            } else
            {
                cropState = CropState.PLANT;
                spriteRenderer.sprite = seed.getPlantImage();
            }
        } else
        {
            if (cropState == CropState.EMPTY) spriteRenderer.sprite = defaultSprite;
        }
    }

    private void OnMouseDown()
    {
        if (cropState == CropState.EMPTY)
        {
            //Redundant way of setting image to sprite :)
            //Waiting for the UI, just a temporary solution
            seedObject = Instantiate(GameObject.FindGameObjectsWithTag("Seed")[0], transform.position, Quaternion.identity);

            this.setSeed(seedObject);
            seedObject.SetActive(false);
            
        } else if (cropState == CropState.PLANT)
        {
            harvest();
        }

    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = Color.red;
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
            this.waitingTime = seed.getHarvestingTime();
            this.cropState = CropState.SEED;
            this.spriteRenderer.sprite = seed.getSeedImage();
            this.harvestProduct = seed.getHarvestProduct();

        } else
        {
            //display to the UI that this cannot be planted

            Debug.Log("Cannot plant here");
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
            cropState = CropState.EMPTY;

            //Better to instantaniate object
            Object.Instantiate(harvestProduct, this.transform.position, Quaternion.identity);
            
            harvestProduct.SetActive(true);
            Destroy(seedObject);

        } else if (cropState == CropState.SEED)
        {
            //Should access to the UI and display some info
            Debug.Log("Please come back later. Waiting time: " + this.waitingTime);
        } else
        {
            //Should access to the UI and display some info
            Debug.Log("Please plant something");
        }

        return null;
    }
}
