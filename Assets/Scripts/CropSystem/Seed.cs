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
    public float harvestTime;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
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
}
