using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float touchDamage;
    private Rigidbody2D rb2D;

    protected SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void addForce(Vector2 direction)
    {
        Debug.Log(direction);
        rb2D.AddForce(direction * 10, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.loseHealth(touchDamage);
        }
    }
}
