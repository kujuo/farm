using UnityEngine;

public class Building : MonoBehaviour
{
    private bool isPlaced;
    private bool canPlace;
    private bool front;
    private bool behind;
    private SpriteRenderer buildingSr;
    private SpriteRenderer outline;
    private bool isHealthRegenEffect;
    private bool isShieldEffect;
    private bool isAttackRangeEffect;
    private bool isPoisonEffect;

    // Start is called before the first frame update
    void Start()
    {
        isPlaced = false;
        behind = false;
        front = false;
        canPlace = true;
        outline = GetComponentsInChildren<SpriteRenderer>()[1]; //GetComponentInChildren<SpriteRenderer>();
        buildingSr = GetComponent<SpriteRenderer>();
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

    public void Init(bool health, bool shield, bool attack, bool poision)
    {
        isHealthRegenEffect = health;
        isShieldEffect = shield;
        isAttackRangeEffect = attack;
        isPoisonEffect = poision;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isPlaced) return;
        if (other.name == "Player" || other.CompareTag("Soil"))
        {
            canPlace = false;
            outline.color = Color.red;
        }
        else if (GetComponentsInChildren<BoxCollider2D>()[1].IsTouching(other) && other.tag == "Building") 
        {
            canPlace = true;
            outline.color = Color.blue;
            front = true;
        }
        else if (other.name == "Outline")
        {
            canPlace = true;
            behind = true;
            outline.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isPlaced) return;
        canPlace = true;
        behind = false;
        front = false;
        outline.color = Color.green;
    }

    private void PlaceBuilding()
    {
        isPlaced = true;
        BuildingSystemManager.Instance.placeMode = false;
        canPlace = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        outline.enabled = false;
        if (behind) GetComponent<SpriteRenderer>().sortingOrder = --BuildingSystemManager.buildingLayer;
        else if (front) GetComponent<SpriteRenderer>().sortingOrder = ++BuildingSystemManager.buildingLayerFront;
        
        BuildingSystemManager.Instance.player.AddBuilding(gameObject.GetComponent<Building>());
    }

    public void UseEffect()
    {
        PlayerController player = BuildingSystemManager.Instance.player;
        if (isHealthRegenEffect)
        {
            player.Regen(0.015f, 1f);
        }
        else if (isShieldEffect)
        {
            player.Shield(50, 5);
            
        }
        else if (isAttackRangeEffect)
        {
            player.SetAttackRangeEffect(2);
        }
        else if (isPoisonEffect)
        {
            player.Poison(0.05f, 1);
        }
    }

    private void OnMouseDown()
    {
        if (isPlaced && !BuildingSystemManager.Instance.placeMode)
        {
            isPlaced = false;
            BuildingSystemManager.Instance.placeMode = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            outline.enabled = true;
            behind = false;
            front = false;
        }
    }

    private void OnMouseOver()
    {
        if (isPlaced && !BuildingSystemManager.Instance.placeMode)
        {
            buildingSr.color = Color.green;
        }
        else buildingSr.color = Color.white;
    }

    private void OnMouseExit()
    {
        if (isPlaced && !BuildingSystemManager.Instance.placeMode)
        {
            buildingSr.color = Color.white;
        }
    }
}
