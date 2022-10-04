using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public float speed = 0.5f;

    void Update()
    {
        CheckDistance();
        ChangeAnimationDirection();
    }


    public override void OnHurt(float damage)
    {
                
        GameObject healthObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Healthbar healthbar = healthObject.GetComponent<Healthbar>();
        if (currHealth == maxHealth) healthbar.Activate();

        currHealth -= damage;

        if (currHealth <= 0) {
            Destroy(this.gameObject);
        }
        else healthbar.SetHealthBarValue(currHealth / maxHealth);

        CheckDistance();
        ChangeAnimationDirection();
    }


    public override void Attack()
    {

    }

    public override void ItemDrop()
    {
        throw new System.NotImplementedException();
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


