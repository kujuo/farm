using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public bool activated = false;
    private Image HealthBarImage;
    // Start is called before the first frame update
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
        SetHealthBarColor(new Color(1, 1, 1, 0));
        if (activated) SetHealthBarColor(new Color(0, 255, 0, 1));
    }

    public void Activate()
    {
        SetHealthBarColor(new Color(0, 255, 0, 1));
    }

    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        if (HealthBarImage.fillAmount < 0.2f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (HealthBarImage.fillAmount < 0.4f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    public void SetHealthBarAlpha(float a)
    {
        Color col = HealthBarImage.color;
        col.a = a;
        HealthBarImage.color = col;
    }
}
