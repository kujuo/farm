using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellExplosion : MonoBehaviour
{
    public float explosionTime = 2f;

    //Manually add all sprite sheet in. Which is not that efficient, but they are in small size 
    public Sprite[] firstFormSprites;
    public Sprite[] secondFormSprites;
    public Sprite[] thirdFormSprites;
    public Sprite[] finalFormSprites;

    private Sprite[] explodeSprites;

    public float timePassed;

    private SpriteRenderer sr;
    private Rigidbody2D rb2D;


    private void selectExplodeSprites()
    {
        //PlayerController.instance.downAttack;
        explodeSprites = firstFormSprites;
    }

    private void Awake()
    {
        //explodeSprites = Resources.LoadAll<Sprite>("Sprites/MidnightTrail");

        selectExplodeSprites();

        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(explodeCoroutine());
        Destroy(gameObject, explosionTime);
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        print(timePassed);
        if (other.gameObject.tag == "Enemy")
        {

//            Enemy en = other.gameObject.GetComponent<Enemy>();
//            en.OnHurt(0.1f);
//=======
            Enemy en = other.gameObject.GetComponent<Enemy>();
            en.OnHurt(0.1f * Mathf.Exp(timePassed * 1000));

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
