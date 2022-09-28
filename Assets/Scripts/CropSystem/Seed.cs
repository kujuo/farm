using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will extend the Item class in inventory
public class Seed : MonoBehaviour
{
    private Sprite seedImage;
    private Sprite plantImage;
    private float harvestMoney;
    private float harvestTime;

    public Sprite getSeedImage()
    {
        return seedImage;
    }

    public float getHarvestMoney()
    {
        return harvestMoney;
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
