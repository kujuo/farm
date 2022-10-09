using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed = 5;
    public float attackDuration = 0.5f;
    public float attackRange = 0.5f; 
    public float health = 100;
    public float attackDamage = 10;
    public float spellCoolDown = 2f, spellTimer = 2f;

    public Sprite[] upAttack;
    public Sprite[] downAttack;
    public Sprite[] rightAttack;
    public Sprite[] leftAttack;

    public LayerMask enemyLayer;
    public CharacterAnimationController animationController;
    public GameObject statusManagerPrefab;
  

    // PUT BUILDINGS HERE
    public List<Building> buildings = new List<Building>();
    public GameObject pauseMenu;
    
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    private Vector2 playerDir;
    private Sprite[] frames;

    private float maxHealth = 100;
    private bool healthRegen;
    private float shieldHealth = 0;
    private float maxShieldAmount;
    private GameObject poisonAura;
    private SpriteRenderer poisonAuraSr;
    private float poisonAuraAlpha;
    public PlayerStatusManager statusManager;

    private bool attacking = false;
    private bool invulnerable = false;

    public GameObject spellObj;

    private void Awake()
    {
        instance = this;
    }

    //public static 
    // Start is called before the first frame update
    void Start()
    {
        //inventory = new InventoryManager();
        rb2D = GetComponent<Rigidbody2D>();
        animationController = GetComponent<CharacterAnimationController>();
        playerDir = new Vector2(0, -1);

        statusManager = Instantiate(statusManagerPrefab).GetComponent<PlayerStatusManager>();
        sr = GetComponent<SpriteRenderer>();
        Physics2D.queriesStartInColliders = false;
        animationController.direction = playerDir;
        poisonAura = transform.GetChild(0).gameObject;
        poisonAuraSr = poisonAura.GetComponent<SpriteRenderer>();
        poisonAuraAlpha = 1;
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
        if (attacking) return;
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.velocity = new Vector2(0, 0);
            StartCoroutine("Attack");
            HitInteraction();
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            //Testing
            if (spellTimer <= 0)
            {
                Instantiate(spellObj, this.transform.position, Quaternion.identity);
                spellTimer = spellCoolDown;
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        //Update spell cooldown time
        spellTimer -= Time.deltaTime;
    }

    public void Reset()
    {
        invulnerable = false;
        attacking = false;
        enabled = true;
        poisonAura.SetActive(false);
        sr.color = new Color(1, 1, 1, 1);
        rb2D.velocity = new Vector2(0, 0);
        health = maxHealth;
        statusManager.updateHealth(health, maxHealth);
        animationController.StopCoroutine("MoveAnimation");
        StopAllCoroutines();
        animationController.StartCoroutine("MoveAnimation");
    }


    private IEnumerator Attack()
    {
        attacking = true;
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
        attacking = false;
    }


    private void HitInteraction()
    {
        Physics2D.queriesStartInColliders = true;
        
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, .5f, playerDir * 0.5f, attackRange, enemyLayer);

        if (hit.collider && hit.collider.tag == "Enemy")
        {
            Enemy en = hit.collider.gameObject.GetComponent<Enemy>();
            en.OnHurt(attackDamage);
        }

    }

    public void gainHealth(float healthGained)
    {
        health += healthGained;
        if (health > maxHealth) health = maxHealth;
        statusManager.updateHealth(health, maxHealth);
    }

    public void loseHealth(float healthLost)
    {
        if (invulnerable) return;
        if (shieldHealth > 0)
        {
            shieldHealth -= health;
            statusManager.updateHealth(shieldHealth, maxShieldAmount);
            if (shieldHealth <= 0) shieldHealth = 0;
            StartCoroutine(DamageTaken());
            return;
        }

        health -= healthLost;
        if (health <= 0)
        {
            CombatLevelManager combatLevelManager = FindObjectOfType<CombatLevelManager>();
            combatLevelManager.playerDeath();
        }
        statusManager.updateHealth(health, maxHealth);
        if (!invulnerable)
        {
            StartCoroutine(DamageTaken());
        }
    }


    IEnumerator DamageTaken()
    {
        invulnerable = true;
        sr.color = new Color(255, 0, 0);
        yield return new WaitForSecondsRealtime(1f);
        sr.color = new Color(1, 1, 1, 1);
        invulnerable = false;
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
        bool exists = false;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].name == building.name)
            {
                exists = true;
                break;
            }
        }
        if (!exists) buildings.Add(building);
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
        poisonAura.SetActive(true);
        StartCoroutine(SetPoisonEffect(amount, rate));
    }

    private IEnumerator SetHealthRegenEffect(float amount, float rate)
    {
        while (true)
        {
            if (health <= maxHealth) health += amount*maxHealth; // % regen instead of flat regen
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
            maxShieldAmount = shieldHealth;
            statusManager.initShieldBar(shieldHealth);
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
            var color = poisonAuraSr.color;
            if (color.a > 0.6f) poisonAuraAlpha = -1;
            else if (color.a < 0.3f) poisonAuraAlpha = 1;
            color.a += poisonAuraAlpha * 0.1f;
            poisonAuraSr.color = color;
            Physics2D.queriesStartInColliders = true;

            List<Collider2D> hits = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D
            {
                useTriggers = false,
                useLayerMask = true,
                useDepth = false,
                useOutsideDepth = false,
                useNormalAngle = false,
                useOutsideNormalAngle = false,
                layerMask = enemyLayer,
                minDepth = 0,
                maxDepth = 0,
                minNormalAngle = 0,
                maxNormalAngle = 0
            };
            GetComponentInChildren<CircleCollider2D>().OverlapCollider(filter, hits);
            
            foreach (Collider2D hit in hits)
            { 
                if (hit.tag == "Enemy")
                {
                    Enemy en = hit.gameObject.GetComponent<Enemy>();
                    en.OnHurt(amount, true);
                }
            }
            yield return new WaitForSeconds(rate);
        }
    }

}
