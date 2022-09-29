using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private bool isPlaced;
    private bool canPlace;
    private bool behind;
    private SpriteRenderer outline;
    // Start is called before the first frame update
    void Start()
    {
        isPlaced = false;
        behind = false;
        canPlace = true;
        outline = GetComponentsInChildren<SpriteRenderer>()[1]; //GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
            // check for collision
            if (Input.GetKeyDown(KeyCode.Mouse0) && canPlace) PlaceBuilding();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isPlaced) return;
        if (GetComponentsInChildren<BoxCollider2D>()[1].IsTouching(other) && other.tag == "Building") 
        {
            canPlace = true;
            outline.color = Color.blue;
        }
        else if (other.name == "Outline")
        {
            canPlace = true;
            behind = true;
            outline.color = Color.green;
        }
        else
        {
            canPlace = false;
            outline.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isPlaced) return;
        canPlace = true;
        behind = false;
        outline.color = Color.green;
    }

    private void PlaceBuilding()
    {
        isPlaced = true;
        canPlace = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        outline.enabled = false;
        if (behind) GetComponent<SpriteRenderer>().sortingOrder = --BuildingSystemManager.buildingLayer;
        // remove from inventory
    }
}
