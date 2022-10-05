using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float touchDamage;
    public float destroyTime;
    public float castRange;
    public float time;

    private Vector3 originalPosition;
    private Vector3 castDirection;

    private Rigidbody2D rb2D;
    public float speed = 10f;
    public float force = 10f;
    private bool collideWithEnemy = false;

    public Transform target;
    public GameObject explosion;

    protected SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

        //Select enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Get
        if (enemies.Length == 0)
        {
            castDirection = Vector3.right;
        } else
        {
            target = findclosestEnemy(enemies).transform;


            castDirection = (target.position - this.transform.position).normalized;
        }

        rb2D.velocity = castDirection * speed;
    }

    private GameObject findclosestEnemy(GameObject[] enemies)
    {
        //find closest enemy
        float minDist = 99999f;
        var closest = enemies[0]; //Target nearest enemy

        foreach (var enemy in enemies)
        {
            float currDist = (enemy.transform.position - transform.position).magnitude;

            if (currDist < minDist)
            {
                minDist = currDist;
                closest = enemy;
            }
        }

        return closest;
    }

    private void Update()
    {
        float castDistance = (rb2D.transform.position - originalPosition).magnitude;
        //Explode
        if (castDistance >= castRange || collideWithEnemy)
        {
            Instantiate(explosion, rb2D.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public void addForce(Vector2 direction)
    {
        rb2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            collideWithEnemy = true;
        }
        
    }
}
