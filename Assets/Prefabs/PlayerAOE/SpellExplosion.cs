using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellExplosion : MonoBehaviour
{
    public float explosionTime = 2f;
    // Start is called before the first frame update
    public Sprite[] explodeSprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        //explodeSprites = Resources.LoadAll<Sprite>("Sprites/MidnightTrail");

        foreach(var sprite in explodeSprites)
        {
            Debug.Log(sprite + "ESDF");
        }

        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(explodeCoroutine());
        Destroy(gameObject, explosionTime);
    }



    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy en = other.collider.gameObject.GetComponent<Enemy>();
            en.OnHurt(0.5f);
        }


    }

    IEnumerator explodeCoroutine()
    {
        foreach (var sprite in explodeSprites)
        {
            sr.sprite = sprite;
            yield return new WaitForSeconds(explosionTime / explodeSprites.Length);
        }
    }
}
