using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public bool activated = false;
    private Image HealthBarImage;

    private Color initColor;
    // Start is called before the first frame update
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
        initColor = HealthBarImage.color;
        //SetHealthBarColor(new Color(1, 1, 1, 0));
        //if (activated) SetHealthBarColor(new Color(0, 255, 0, 0.6f));
    }

    public void Activate()
    {
        SetHealthBarColor(initColor);
        HealthBarImage.fillAmount = 1;
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
            SetHealthBarColor(initColor);
        }
        SetHealthBarAlpha(0.6f);
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    public Color GetColor()
    {
        return HealthBarImage.color;
    }

    public void SetHealthBarAlpha(float a)
    {
        Color col = HealthBarImage.color;
        col.a = a;
        HealthBarImage.color = col;
    }
}
