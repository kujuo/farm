using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public Healthbar playerHealth;
    public Healthbar shieldBar;

    public void initShieldBar(float shield)
    {
        shieldBar.Activate();
        shieldBar.SetHealthBarValue(shield);
        shieldBar.SetHealthBarAlpha(0.8f);
    }
    
    public void updateHealth(float health, float maxHealth)
    {
        if (shieldBar && shieldBar.GetHealthBarValue() > 0)
        {
            shieldBar.SetHealthBarValue(health / maxHealth);
            shieldBar.SetHealthBarAlpha(0.8f);
        }
        else
        {
            playerHealth.SetHealthBarValue(health / maxHealth);
            playerHealth.SetHealthBarAlpha(0.8f);
            shieldBar.activated = false;
        }
    }
    

    
}
