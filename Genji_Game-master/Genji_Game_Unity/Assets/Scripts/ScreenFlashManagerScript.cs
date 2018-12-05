using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashManagerScript : MonoBehaviour {
    public Color damageFlashColor;
    public Color healthFlashColor;

    public float damageFlashSpeed;
    public float healthFlashSpeed;

    public Image damageImage;
    public Image healthImage;

    public bool damaged;
    public bool healing;

    void Start()
    {

    }

    void Update()
    {
        DamageFlash();
        HealFlash();
    }

    public void DamageFlash()
    {
        if(damaged)
        {
            damageImage.color = damageFlashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void HealFlash()
    {
        if (healing)
        {
            healthImage.color = healthFlashColor;
        }
        else
        {
            healthImage.color = Color.Lerp(healthImage.color, Color.clear, healthFlashSpeed * Time.deltaTime);
        }

        healing = false;
    }
}
