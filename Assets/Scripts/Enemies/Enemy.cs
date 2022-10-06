using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject player;
    public float maxHealth;
    public float touchDamage;
    public float activeDistance;

    protected bool active;
    protected SpriteRenderer sr;
    protected CharacterAnimationController animationController;
    protected Vector2 enemyDir;
    protected Rigidbody2D rb2D;
    protected float currHealth;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animationController = GetComponent<CharacterAnimationController>();
        enemyDir = new Vector3(0, -1,0);
        animationController.direction = enemyDir;

        Physics2D.queriesStartInColliders = false;
        player = GameObject.Find("Player");
        currHealth = maxHealth;
        HealthbarCreation();
        animationController.moving = false;
    }

    public void CheckDistance()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            if (player == null) return;
        }

        if (!active)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activeDistance)
            {
                gameObject.GetComponent<SAP2D.SAP2DAgent>().enabled = true;
                animationController.moving = true;
                active = true;
                animationController.StopCoroutine("MoveAnimation");
                animationController.StartCoroutine("MoveAnimation");
            }
            else gameObject.GetComponent<SAP2D.SAP2DAgent>().enabled = false;
        }
    }

    public void ChangeAnimationDirection()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            if (player == null) return;
        }
        Vector3 movement = player.transform.position - transform.position;
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

    public GameObject healthCanvas;

    public void HealthbarCreation()
    {
        var canvas = Instantiate(healthCanvas, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        canvas.transform.SetParent(gameObject.transform, true);
    }

    public abstract void Attack();

    public virtual void OnHurt(float damage, bool percentDamage = false)
    {
        GameObject healthObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Healthbar healthbar = healthObject.GetComponent<Healthbar>();
        if (currHealth == maxHealth) healthbar.Activate();

        if (percentDamage) currHealth -= damage * maxHealth;
        else currHealth -= damage;
        gameObject.GetComponent<SAP2D.SAP2DAgent>().enabled = false;
        rb2D.AddForce(enemyDir * -5, ForceMode2D.Impulse);
        gameObject.GetComponent<SAP2D.SAP2DAgent>().enabled = true;
        if (currHealth <= 0) OnDeath();
        else healthbar.SetHealthBarValue(currHealth / maxHealth);
    }

    public virtual void OnDeath()
    {
        Destroy(this.gameObject);
    }

    public abstract void ItemDrop();

    }
