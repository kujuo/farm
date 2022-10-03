using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        isPlanted = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        seedImage = images[images.Length - 1];
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
            Debug.Log("ready to be planted");
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
            // check for collision
            if (Input.GetKeyDown(KeyCode.Mouse0)) PlantSeed();
        }
    }

    private void PlantSeed()
    {
        Debug.Log("has been planted!!!!");
        isPlanted = true;
    }
}
