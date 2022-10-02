using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    //public float maxHealth = 100;
    //public Sprite[] idleFrames;
    //public Sprite[] rightFrames;
    //public Sprite[] upFrames;
    //public Sprite[] downFrames;
    //public float animateSpeed = 0.5f;
    //public GameObject player;
    public float speed = 0.5f;
    public float activeDistance;


    //private Vector2 direction;
    //private CharacterAnimationController charAnimationController;
    //private float currHealth;
    //private bool active;
    //Coroutine movementAnimation;

    //private SpriteRenderer sr;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    sr = GetComponent<SpriteRenderer>();
    //    charAnimationController = GetComponent<CharacterAnimationController>();
    //    direction = new Vector2(0, -1);
    //    animationController.direction = enemyDir;

    //    movementAnimation = StartCoroutine(Animate(idleFrames));
    //    player = GameObject.Find("Player");
    //    currHealth = maxHealth;
    //    HealthbarCreation();
    //}

    // Update is called once per frame

    void Update()
    {
        if (!active)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activeDistance)
            {
                animationController.moving = true;
                active = true;
                animationController.StopCoroutine("MoveAnimation");
                animationController.StartCoroutine("MoveAnimation");
            }
            else return;
        }
        //var step = speed * Time.deltaTime; // calculate distance to move
        //Vector3 move = Vector3.MoveTowards(transform.position, player.transform.position, step);
        //Vector3 movement = move - transform.position;
        //transform.position = move;

        ////Vector3 movement = move - player.transform.position;
        //Vector2 direction = new Vector2(movement.x, movement.y);
        //Vector2 currentDir = animationController.getDirection(direction);

        //if (currentDir != enemyDir)
        //{
        //    animationController.direction = currentDir;
        //    enemyDir = currentDir;

        //    animationController.StopCoroutine("MoveAnimation");
        //    animationController.StartCoroutine("MoveAnimation");
        //}
    }

    //IEnumerator Animate(Sprite[] frames)
    //{
    //    int frame = 0;
    //    while (true)
    //    {
    //        if (frame >= frames.Length) frame = 0;
    //        sr.sprite = frames[frame];
    //        frame++;
    //        yield return new WaitForSeconds(animateSpeed);
    //    }
    //}

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
            player.loseHealth(touchDamage);
        }

    }
}


