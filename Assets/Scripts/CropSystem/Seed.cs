using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will extend the Item class in inventory
public class Seed : MonoBehaviour
{
    public Sprite seedImage;
    public Sprite plantImage;
    public GameObject harvestProduct;
    public float harvestTime;

    public Sprite getSeedImage()
    {
        return seedImage;
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
