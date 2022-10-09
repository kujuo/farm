using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float touchDamage;
    public float destroyTime;
    private Rigidbody2D rb2D;

    protected SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
    }

    public void addForce(Vector2 direction)
    {
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
