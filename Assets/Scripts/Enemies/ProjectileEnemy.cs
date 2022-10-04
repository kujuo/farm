using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    public float speed = 0.5f;
    public float attackSpeed;
    public float buildUpTime;
    public float activeDistance;

    public GameObject projectile;
    private bool attacking = false;

    public override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animationController = GetComponent<CharacterAnimationController>();
        enemyDir = new Vector2(0, -1);
        animationController.direction = enemyDir;

        Physics2D.queriesStartInColliders = false;
        player = GameObject.Find("Player");
        currHealth = maxHealth;
        HealthbarCreation();
        animationController.moving = false;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (attacking) return;
        if (!active)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activeDistance)
            {
                Attack();
                animationController.moving = true;
                active = true;
                animationController.StopCoroutine("MoveAnimation");
                animationController.StartCoroutine("MoveAnimation");
            }
            else return;
        }
        var step = speed * Time.deltaTime; // calculate distance to move
        Vector3 move = Vector3.MoveTowards(transform.position, player.transform.position, step);
        Vector3 movement = move - transform.position;
        transform.position = move;

        //Vector3 movement = move - player.transform.position;
        Vector2 direction = new Vector2(movement.x, movement.y);
        Vector2 currentDir = animationController.getDirection(direction);

        if (currentDir != enemyDir)
        {
            animationController.direction = currentDir;
            enemyDir = currentDir;

            animationController.StopCoroutine("MoveAnimation");
            animationController.StartCoroutine("MoveAnimation");
        }


    }

    public override void OnHit(float damage)
    {

        GameObject healthObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Healthbar healthbar = healthObject.GetComponent<Healthbar>();
        if (currHealth == maxHealth) healthbar.Activate();

        currHealth -= damage;

        if (currHealth <= 0) { Destroy(this.gameObject);
        }
        else healthbar.SetHealthBarValue(currHealth / maxHealth);
    }


    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
        
    }

    public override void ItemDrop()
    {
        throw new System.NotImplementedException();
    }

    

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            StartCoroutine(ShootingProjectile());
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    IEnumerator ShootingProjectile()
    {
        attacking = true;
        GameObject shoot = Instantiate(projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(buildUpTime);
        Vector2 shootingVector = player.transform.position - transform.position;
        shootingVector.Normalize();
        shoot.gameObject.GetComponent<Projectile>().addForce(shootingVector);
        attacking = false;

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
