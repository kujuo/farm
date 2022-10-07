using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public float speed = 0.5f;
    public float spellSpeed;
    public float spellDuration;
    public float spellCoolDown = 0.5f; 
    public Vector3 castPosition;
    private SpriteRenderer sr;
    private Rigidbody2D rb2D;

    public GameObject spell;
    private bool casting = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        spellDuration = 1f;
        spellCoolDown = 0.5f;
        castPosition = new Vector3(this.transform.position.x + 5, this.transform.position.y + 5, this.transform.position.z);
    }

    void Update()
    {
        if (spellCoolDown <= 0)
        {
            //Cast spell when hit Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CastSpell();
            }
        } else
        {
            Debug.Log("Skill cool down");
            spellCoolDown -= Time.deltaTime;
        }
        //if (player == null)
        //{
        //    player = GameObject.Find("Player");
        //}
        //if (attacking) return;
        //bool wasActive = active;
        //CheckDistance();
        //if (active && !wasActive) Attack();
        //ChangeAnimationDirection();

    }

    public void CastSpell()
    {
        //Find enemy to hit within radius 10

        //RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 10f, transform.position);
        //Enemy enemy = hits[0].collider.gameObject.GetComponent<Enemy>();
        //foreach (var hit in hits)
        //{
        //    if (hit.collider && hit.collider.tag == "Enemy")
        //    {
        //        enemy = hit.collider.gameObject.GetComponent<Enemy>();
        //        break;
        //    }
        //}
        //spellCoolDown = 0.5f;
        StartCoroutine(SpellCoroutine());

        GameObject.Destroy(spell);
    }

    IEnumerator SpellCoroutine()
    {
        GameObject spell = Instantiate(this.spell, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spellDuration);

        //Vector2 spellDir = enemy.transform.position - this.transform.position;
        //spellDir.Normalize();
        //spell.gameObject.GetComponent<Projectile>().addForce(spellDir);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy en = other.gameObject.GetComponent<Enemy>();
            en.OnHurt(10);
        }
    }
}
