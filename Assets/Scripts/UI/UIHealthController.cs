#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{

    [SerializeField] Text health;
    [SerializeField] Slider healthSlider;
    [SerializeField] Image healthBar;
    [SerializeField] Color low;
    [SerializeField] Color mid;
    [SerializeField] Color high;

    public PlayerStats player;
    public void UpdateHealth()
    {
        if(player.CurrentHealth >= 0)
        {
            healthSlider.maxValue = player.MaxHealth;
            healthSlider.value = player.CurrentHealth;
            health.text = player.CurrentHealth + " / " + player.MaxHealth;
        }
        
        UpdateColorBar(player.CurrentHealth, player.MaxHealth);
    }
    void UpdateColorBar(int currentHealth, int maxHealth)
    {
        float currentPercent = ((float)currentHealth / (float)maxHealth) * 100;

        if (currentPercent >= 75)
        {
            healthBar.color = high;
        } else if(currentPercent < 75 && currentPercent >= 25)
        {
            healthBar.color = mid;
        } else
        {
            healthBar.color = low;
        }
    }
}
