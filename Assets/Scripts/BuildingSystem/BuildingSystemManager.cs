﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingSystemManager : MonoBehaviour
{
    private static BuildingSystemManager instance;
    public static int buildingLayer;

    public Animator animator;
    public GameObject layoutParent;
    public GameObject layout;
    public BuildingBlueprint[] blueprint; // list of buildings that can be build
    // OR have a building blueprint obj that holds the prefab info
    // generate # of blue prints based on how many blue prints there are

    public bool isOpen;

    private PlayerController player;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        isOpen = false;
        BuildingSystemManager.buildingLayer = 0;
        player = FindObjectOfType<PlayerController>();
        foreach (var bp in blueprint)
        {
            var curr = Instantiate(layout);
            var text = curr.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].text = bp.buildingName;
            text[1].text = bp.buildingDescription;
            text[2].text = bp.buildingMaterials;
            curr.GetComponent<BuildingBlueprint>().building = bp.building;
            curr.transform.SetParent(layoutParent.transform, false);
            
        }
    }

    public static BuildingSystemManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<BuildingSystemManager>();
                if (!instance) throw new UnityException("BuildingSystemManager instance not found");
            }
            return instance;
        }
    }

    public bool Build(Items materials)
    {
        return true;
    }

    public void DisplayBuildingUi()
    {
        isOpen = !isOpen;
        GameStateManager.Instance.Pause(isOpen);
        animator.SetBool("isOpen", isOpen);
    }
}