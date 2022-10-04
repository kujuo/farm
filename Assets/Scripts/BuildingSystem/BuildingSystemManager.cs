using UnityEngine;

public class BuildingSystemManager : MonoBehaviour
{
    private static BuildingSystemManager instance;
    public static int buildingLayer;
    public static int buildingLayerFront;
    public bool placeMode;
    

    public PlayerController player;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        placeMode = false;
        buildingLayer = 0;
        buildingLayerFront = 0;
        player = FindObjectOfType<PlayerController>();
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
}
