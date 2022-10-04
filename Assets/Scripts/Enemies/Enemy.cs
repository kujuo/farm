using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject player;
    public float maxHealth;
    public float touchDamage;

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

    public GameObject healthCanvas;

    public void HealthbarCreation()
    {
        var canvas = Instantiate(healthCanvas, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        canvas.transform.SetParent(gameObject.transform, true);
    }

    public abstract void Attack();

    public abstract void OnHit(float damage);

    public abstract void ItemDrop();

        //public abstract float touchDamage(); 

    }
