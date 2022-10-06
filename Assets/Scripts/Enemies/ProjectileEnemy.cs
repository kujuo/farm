using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    public float speed = 0.5f;
    public float attackSpeed;
    public float buildUpTime;

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
        bool wasActive = active;
        CheckDistance();
        if (active && !wasActive) Attack();
        ChangeAnimationDirection();
    }

    public override void OnHurt(float damage, bool percentDamage = false)
    {

        GameObject healthObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Healthbar healthbar = healthObject.GetComponent<Healthbar>();
        if (currHealth == maxHealth) healthbar.Activate();

        if (percentDamage) currHealth -= damage * maxHealth;
        else currHealth -= damage;

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
        gameObject.GetComponent<SAP2D.SAP2DAgent>().enabled = false;
        GameObject shoot = Instantiate(projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(buildUpTime);
        Vector2 shootingVector = player.transform.position - transform.position;
        shootingVector.Normalize();
        shoot.gameObject.GetComponent<Projectile>().addForce(shootingVector);
        attacking = false;
        gameObject.GetComponent<SAP2D.SAP2DAgent>().enabled = true;
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
