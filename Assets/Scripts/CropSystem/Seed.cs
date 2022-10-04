using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Will extend the Item class in inventory
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
    private Crop soil;
    private BoxCollider2D seedCollider;
    
    private void Awake()
    {
        isPlanted = false;
        canPlant = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        seedImage = images[0];
        spriteRenderer.sprite = seedImage;
        //seedCollider = GetComponentsInChildren<BoxCollider2D>()[1];
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
        }
    }

    private void PlantSeed()
    {
        isPlanted = true;
        soil.Plant(gameObject);
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "SeedCollider")
        {
            Debug.Log("can now plant");
            soil = other.GetComponentInParent<Crop>();
            canPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("can no longer plant");
        canPlant = false;
    }
}
