using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public float maxHealth = 100;
    public Sprite[] idleFrames;
    public Sprite[] rightFrames;
    public float animateSpeed = 0.5f;
    public GameObject player;
    public float speed = 0.5f;
    public float activeDistance;

    private float currHealth;
    private bool active;
    Coroutine movementAnimation;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        movementAnimation = StartCoroutine(Animate(idleFrames));
        player = GameObject.Find("Player");
        currHealth = maxHealth;
        HealthbarCreation();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activeDistance)
            {
                active = true;
                StopCoroutine(movementAnimation);
                sr.flipX = false;
                movementAnimation = StartCoroutine(Animate(rightFrames));
            }
            else return;
        }

        var step = speed * Time.deltaTime; // calculate distance to move
        Vector3 move = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.position = move;

        Vector3 sub = player.transform.position - move;
        if (sub.x > 0 && sr.flipX != false)
        {
            sr.flipX = false;
            StopCoroutine(movementAnimation);
            movementAnimation = StartCoroutine(Animate(rightFrames));
        }
        else if (sub.x < 0 && sr.flipX == false)
        {
            sr.flipX = true;
            StopCoroutine(movementAnimation);
            movementAnimation = StartCoroutine(Animate(rightFrames));
        }
    }

    IEnumerator Animate(Sprite[] frames)
    {
        int frame = 0;
        while (true)
        {
            if (frame >= frames.Length) frame = 0;
            sr.sprite = frames[frame];
            frame++;
            yield return new WaitForSeconds(animateSpeed);
        }
    }

    public override void OnHit(float damage)
    {
                
        GameObject healthObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Healthbar healthbar = healthObject.GetComponent<Healthbar>();
        if (currHealth == maxHealth) healthbar.Activate();

        currHealth -= damage;

        if (currHealth <= 0) Destroy(this.gameObject);
        else healthbar.SetHealthBarValue(currHealth/maxHealth);
    }


    public override void Attack()
    {

    }

    public override void ItemDrop()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.loseHealth(10);
        }

    }
}


