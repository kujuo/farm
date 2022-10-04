using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public Healthbar playerHealth;

    public void updateHealth(float health, float maxHealth)
    {
        playerHealth.SetHealthBarValue(health / maxHealth);
        playerHealth.SetHealthBarAlpha(0.8f);
    }

    
}
