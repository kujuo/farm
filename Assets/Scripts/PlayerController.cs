using System;
using System.Collections;
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
    public float hitDistance = 0.5f;
    public float health = 100;
    public InventoryManager inventory;

    public Sprite[] upAttack;
    public Sprite[] downAttack;
    public Sprite[] rightAttack;
    public Sprite[] leftAttack;

    public CharacterAnimationController animationController;
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    private Vector2 playerDir;
    private Sprite[] frames;
    private float maxHealth;
    private bool healthRegen;

    private States state;
    private bool canInteract;
    private NpcController interactableTarget;

    private float healthRegenAmount;
    private bool startedHealthRegen;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthRegen = true;
        startedHealthRegen = false;
        inventory = new InventoryManager();
        rb2D = GetComponent<Rigidbody2D>();
        animationController = GetComponent<CharacterAnimationController>();
        playerDir = new Vector2(0, -1);

        canInteract = false;
        sr = GetComponent<SpriteRenderer>();
        Physics2D.queriesStartInColliders = false;
        animationController.direction = playerDir;
        state = States.Normal;
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
        //Debug.Log(rb2D.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.Attacking) return;
        Move();
        InteractionCheck();
        if (Input.GetKeyDown(KeyCode.E) && interactableTarget) interactableTarget.Interact();
        if (Input.GetKeyDown(KeyCode.Z)) StartCoroutine("Attack");
        if (Input.GetKeyDown(KeyCode.B)) BuildingSystemManager.Instance.DisplayBuildingUi();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb2D.velocity = new Vector2(0, 0);
            StartCoroutine("Attack");
            HitInteraction();
        }

        if (healthRegen && !startedHealthRegen)
        {
            StartCoroutine(ApplyEffects());
            startedHealthRegen = true;
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
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, .5f, playerDir * 0.5f, hitDistance);
        if (hit.collider && hit.collider.tag == "Enemy")
        {
            Enemy en = hit.collider.gameObject.GetComponent<Enemy>();
            en.OnHit(10);
        }
    }

    public void loseHealth(float healthLost)
    {
        if (state == States.Invulnerable) return;
        health -= healthLost;
        StartCoroutine(DamageTaken());
        //Debug.Log(health);
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
        yield return new WaitForSeconds(1f);
        sr.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        state = States.Normal;
    }
    
    // EFFECTS
    private IEnumerator ApplyEffects()
    {
        yield break;
    }
    public void SetHealthRegenEffect(float amount)
    {
        
    }

    public void SetShieldEffect(float amount)
    {
        
    }

    public void SetAttackRangeEffect(float amount)
    {
        
    }

    public void SetPoisonEffect(float amount)
    {
        
    }
}
