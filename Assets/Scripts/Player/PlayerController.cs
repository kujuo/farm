using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    enum States
    {
        Normal,
        Invulnerable,
        Attacking,
    }

    public float speed = 5;
    public float attackDuration = 0.5f;
    //public float frameDuration = 2f;
    public float attackRange = 0.5f;
    public float health = 100;
    public InventoryManager inventory;

    public Sprite[] upAttack;
    public Sprite[] downAttack;
    public Sprite[] rightAttack;
    public Sprite[] leftAttack;

    public LayerMask enemyLayer;
    public CharacterAnimationController animationController;
    public GameObject statusManagerPrefab;

    // PUT BUILDINGS HERE
    public List<Building> buildings = new List<Building>();


    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    private Vector2 playerDir;
    private Sprite[] frames;

    private float maxHealth = 100;
    private bool healthRegen;
    private float shieldHealth = 0;
    public PlayerStatusManager statusManager;

    private States state;
    private bool canInteract;
    private NpcController interactableTarget;


    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryManager();
        rb2D = GetComponent<Rigidbody2D>();
        animationController = GetComponent<CharacterAnimationController>();
        playerDir = new Vector2(0, -1);

        statusManager = Instantiate(statusManagerPrefab).GetComponent<PlayerStatusManager>();
        canInteract = false;
        sr = GetComponent<SpriteRenderer>();
        Physics2D.queriesStartInColliders = false;
        animationController.direction = playerDir;
        state = States.Normal;
        ApplyEffects();
    }

    private void Move()
    {
        Vector2 oldPlayerDir = new Vector2(playerDir.x, playerDir.y);
        float inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            playerDir = new Vector2(1, 0);
            animationController.direction = playerDir;
        }
        else if (inputX < 0)
        {
            playerDir = new Vector2(-1, 0);
            animationController.direction = playerDir;
        }
        float inputY = Input.GetAxis("Vertical");
        if (inputY > 0)
        {
            playerDir = new Vector2(0, 1);
            animationController.direction = playerDir;
        }
        else if (inputY < 0)
        {
            playerDir = new Vector2(0, -1);
            animationController.direction = playerDir;
        }
        Vector3 movement = new Vector3(inputX, inputY, 0);

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            if (playerDir != oldPlayerDir || !animationController.moving)
            {
                animationController.moving = true;
                animationController.StopCoroutine("MoveAnimation");
                animationController.StartCoroutine("MoveAnimation");
            }
        }
        else // magnitude is nothing
        {
            if (animationController.moving)
            {
                animationController.moving = false;
                animationController.StopCoroutine("MoveAnimation");
                animationController.StartCoroutine("MoveAnimation");
            }
        }
        rb2D.velocity = movement * speed;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (state == States.Attacking) return;
        Move();
        InteractionCheck();
        if (Input.GetKeyDown(KeyCode.E) && interactableTarget) interactableTarget.Interact();
        //if (Input.GetKeyDown(KeyCode.Z)) StartCoroutine("Attack");
        if (Input.GetKeyDown(KeyCode.B)) BuildingSystemManager.Instance.DisplayBuildingUi();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.velocity = new Vector2(0, 0);
            StartCoroutine("Attack");
            HitInteraction();
        }
    }

    private IEnumerator Attack()
    {
        state = States.Attacking;
        enabled = false;
        rb2D.velocity = new Vector2(0, 0);
        animationController.StopCoroutine("MoveAnimation");

        Sprite[][] frameSet = { rightAttack, leftAttack, upAttack, downAttack };
        Sprite[] attackFrames = animationController.GetFramesFromDirection(frameSet);

        foreach (Sprite frame in attackFrames)
        {
            sr.sprite = frame;
            HitInteraction();
            yield return new WaitForSeconds(attackDuration);
        }
        enabled = true;
        animationController.StartCoroutine("MoveAnimation");
        state = States.Normal;
    }

    private void HitInteraction()
    {
        Physics2D.queriesStartInColliders = true;
        
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, .5f, playerDir * 0.5f, attackRange, enemyLayer);

        if (hit.collider && hit.collider.tag == "Enemy")
        {
            Enemy en = hit.collider.gameObject.GetComponent<Enemy>();
            en.OnHit(10);
        }
    }

    public void loseHealth(float healthLost)
    {
        if (shieldHealth > 0)
        {
            shieldHealth -= health;
            if (shieldHealth <= 0) shieldHealth = 0;
            return;
        }
        if (state == States.Invulnerable) return;
        
        health -= healthLost;
        statusManager.updateHealth(health, maxHealth);
        if (state == States.Normal) StartCoroutine(DamageTaken());
    }

    private void InteractionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir, playerDir.magnitude);
        if (hit.collider) interactableTarget = hit.collider.GetComponent<NpcController>();
        else interactableTarget = null;
    }

    IEnumerator DamageTaken()
    {
        state = States.Invulnerable;
        sr.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(1.5f);
        sr.color = new Color(1, 1, 1, 1);
        state = States.Normal;
    }

    // EFFECTS
    // CALL THIS FUNCTION ON SCENE LOAD
    public void ApplyEffects()
    {
        foreach (Building building in buildings)
        {
            building.UseEffect();
        }
    }

    // adds a building for effects to use
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    public void Regen(float amount, float rate)
    {
        StartCoroutine(SetHealthRegenEffect(amount, rate));
    }

    public void Shield(float amount, float rate)
    {
        StartCoroutine(SetShieldEffect(amount, rate));
    }

    public void Poison(float amount, float rate)
    {
        StartCoroutine(SetPoisonEffect(amount, rate));
    }

    private IEnumerator SetHealthRegenEffect(float amount, float rate)
    {
        while (true)
        {
            if (health <= maxHealth) health += amount;
            if (health > maxHealth) health = maxHealth;
            statusManager.updateHealth(health, maxHealth);
            yield return new WaitForSeconds(rate);
        }
    }

    private IEnumerator SetShieldEffect(float amount, float rate)
    {
        while (true)
        {
            shieldHealth = amount;
            yield return new WaitForSeconds(rate);
        }
    }

    public void SetAttackRangeEffect(float amount)
    {
        attackRange = amount;
    }

    private IEnumerator SetPoisonEffect(float amount, float rate)
    {
        while (true)
        {
            Physics2D.queriesStartInColliders = true;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5, enemyLayer);

            foreach (Collider2D hit in hits)
            {
                if (hit.tag == "Enemy")
                {
                    Enemy en = hit.gameObject.GetComponent<Enemy>();
                    en.OnHit(amount);
                }
            }
            yield return new WaitForSeconds(rate);
        }
    }
}
