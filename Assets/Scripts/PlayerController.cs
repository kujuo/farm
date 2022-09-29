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
    public float attackDuration = 0.25f;
    public float frameDuration = 2f;
    public float hitDistance = 3f;
    public float health = 100;
    public Sprite[] downIdle;
    public Sprite[] upIdle;
    public Sprite[] rightIdle;
    public Sprite[] leftIdle;
    public Sprite[] downMove;
    public Sprite[] upMove;
    public Sprite[] rightMove;
    public Sprite[] leftMove;

    public Sprite[] upAttack;
    public Sprite[] downAttack;
    public Sprite[] rightAttack;
    public Sprite[] leftAttack;

    public CharacterAnimationController animationController;
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    private Vector2 playerDir;
    private Sprite[] frames;

    private States state;
    private bool moving = true;
    private bool canInteract;
    private NpcController interactableTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animationController = GetComponent<CharacterAnimationController>();
        playerDir = new Vector2(0, -1);

        canInteract = false;
        sr = GetComponent<SpriteRenderer>();
        Physics2D.queriesStartInColliders = false;
        frames = downMove;
        StartCoroutine("MoveAnimation");
        state = States.Normal;
    }

    private void Move()
    {
        Vector2 oldPlayerDir = new Vector2(playerDir.x, playerDir.y);
        float inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            playerDir = new Vector2(1, 0);
            frames = rightMove;
        }
        else if (inputX < 0)
        {
            playerDir = new Vector2(-1, 0);
            frames = leftMove;
        }
        float inputY = Input.GetAxis("Vertical");
        if (inputY > 0)
        {
            playerDir = new Vector2(0, 1);
            frames = upMove;
        }
        else if (inputY < 0)
        {
            playerDir = new Vector2(0, -1);
            frames = downMove;
        }
        Vector3 movement = new Vector3(inputX, inputY, 0);

        if (movement.magnitude > 0)
        {
            moving = true;
            movement.Normalize();
            if (playerDir != oldPlayerDir)
            {
                StopCoroutine("MoveAnimation");
                StartCoroutine("MoveAnimation");
            }
        }
        else // magnitude is nothing
        {
            if (moving)
            {
                moving = false;
                StopCoroutine(MoveAnimation());
                StartCoroutine("MoveAnimation");
            }
        }
        rb2D.velocity = movement * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        InteractionCheck();
        if (Input.GetKeyDown(KeyCode.E) && interactableTarget) interactableTarget.Interact();
        if (Input.GetKeyDown(KeyCode.Z)) StartCoroutine("Attack");
        if (Input.GetKeyDown(KeyCode.B)) BuildingSystemManager.Instance.DisplayBuildingUi();
    }

    private IEnumerator Attack()
    {
        state = States.Attacking;
        enabled = false;
        rb2D.velocity = new Vector2(0, 0);
        StopCoroutine("MoveAnimation");
        Sprite[] attackFrames;
        if (playerDir.Equals(new Vector2(1, 0)))
        {
            attackFrames = rightAttack;
        }
        else if (playerDir.Equals(new Vector2(-1, 0)))
        {
            //playerDir = new Vector2(-1, 0);
            attackFrames = leftAttack;
        }
        else if (playerDir.Equals(new Vector2(0, 1)))
        {
            //playerDir = new Vector2(0, 1);
            attackFrames = upAttack;
        }
        else
        {
            //playerDir = new Vector2(0, -1);
            attackFrames = downAttack;
        }

        foreach (Sprite frame in attackFrames)
        {
            sr.sprite = frame;
            HitInteraction();
            yield return new WaitForSeconds(attackDuration);
        }
        enabled = true;
        StartCoroutine("MoveAnimation");
        state = States.Normal;
    }

    private void HitInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir, hitDistance);
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
        Debug.Log(health);
    }

    private void InteractionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir, playerDir.magnitude);
        if (hit.collider) interactableTarget = hit.collider.GetComponent<NpcController>();
        else interactableTarget = null;
    }

    IEnumerator MoveAnimation()
    {
        int i;
        i = 0;
        if (!moving) // idle animations
        {
            if (playerDir.x > 0) frames = rightIdle; // idle right
            else if (playerDir.x < 0) frames = leftIdle;
            else if (playerDir.y > 0) frames = upIdle;
            else if (playerDir.y < 0) frames = downIdle;
        }
        while (true)
        {
            sr.sprite = frames[i];
            i++;
            if (i >= frames.Length)
            {
                i = 0;
            }
            yield return new WaitForSeconds(frameDuration);
        }
    }

    IEnumerator DamageTaken()
    {
        state = States.Invulnerable;
        sr.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        state = States.Normal;
    }
}
