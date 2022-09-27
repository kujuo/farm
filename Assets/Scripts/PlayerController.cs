using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float frameDuration = 0.5f;
    public Sprite[] downIdle;
    public Sprite[] upIdle;
    public Sprite[] rightIdle;
    public Sprite[] leftIdle;
    public Sprite[] downMove;
    public Sprite[] upMove;
    public Sprite[] rightMove;
    public Sprite[] leftMove;

    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    private Vector2 playerDir;
    private Sprite[] frames;

    private bool canInteract;

    private NpcController interactableTarget;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerDir = new Vector2(0, -1);
        canInteract = false;
        sr = GetComponent<SpriteRenderer>();
        Physics2D.queriesStartInColliders = false;

        StartCoroutine(MoveAnimation());
    }

    private void Move()
    {
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
            movement.Normalize();
            StopCoroutine(MoveAnimation());
        }

        rb2D.velocity = movement * speed;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        InteractionCheck();
        if (Input.GetKeyDown(KeyCode.E) && interactableTarget) interactableTarget.Interact();

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
        if (rb2D.velocity.x == 0 && rb2D.velocity.y == 0) // idle animations
        {
            if (playerDir.x > 0) frames = rightIdle; // idle right
            else if (playerDir.x < 0) frames = leftIdle;
            else if (playerDir.y > 0) frames = upIdle;
            else if (playerDir.y < 0) frames = downIdle;
        }
        while (i < frames.Length)
        {
            sr.sprite = frames[i];
            i++;
            yield return new WaitForSeconds(frameDuration);
                
        }
        StartCoroutine(MoveAnimation());
    }
}
