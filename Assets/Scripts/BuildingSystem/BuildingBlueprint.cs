using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingBlueprint : MonoBehaviour
{
    // Start is called before the first frame update
    public string buildingName;
    public string buildingDescription;
    public string buildingMaterials; // change to item array when item has been created
    public Building building;

    public void onClick()
    {
        // checks if player has required materials 
        // todo
        var s = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        s.z = 0;
        var created = Instantiate(building, s, Quaternion.identity);
        BuildingSystemManager.Instance.DisplayBuildingUi();
    }
}
