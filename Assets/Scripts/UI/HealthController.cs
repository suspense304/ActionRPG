#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    public Text health;
    public Slider healthSlider;
    public Image healthBar;
    public Color low;
    public Color mid;
    public Color high;

    public PlayerStats player;

    void Start()
    {
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
        player.playerHealthSignal.Raise();
    }
    public void UpdateHealth()
    {
        if(player != null)
        {
            if (player.CurrentHealth >= 0)
            {
                healthSlider.maxValue = player.MaxHealth;
                healthSlider.value = player.CurrentHealth;
                health.text = player.CurrentHealth + " / " + player.MaxHealth;
            }

            UpdateColorBar(player.CurrentHealth, player.MaxHealth);
        }
        
    }
    void UpdateColorBar(int currentHealth, int maxHealth)
    {
        if (player != null)
        {
            float currentPercent = ((float)currentHealth / (float)maxHealth) * 100;

            if (currentPercent >= 75)
            {
                healthBar.color = high;
            }
            else if (currentPercent < 75 && currentPercent >= 25)
            {
                healthBar.color = mid;
            }
            else
            {
                healthBar.color = low;
            }
        }
    }
}
