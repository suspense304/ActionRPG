#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{

    [SerializeField] Text valueText;
    [SerializeField] Slider valueSlider;
    [SerializeField] Image valueBar;
    [SerializeField] Color low;
    [SerializeField] Color mid;
    [SerializeField] Color high;

    public PlayerStats player;
    public void UpdateHealth()
    {
        if(player.CurrentHealth >= 0)
        {
            valueSlider.maxValue = player.MaxHealth;
            valueSlider.value = player.CurrentHealth;
            valueText.text = player.CurrentHealth + " / " + player.MaxHealth;
        }
        
        UpdateColorBar(player.CurrentHealth, player.MaxHealth);
    }

    public void UpdateMana()
    {
        Debug.Log("Updating Mana Values");
        if (player.CurrentMana >= 0)
        {
            
            valueSlider.maxValue = player.MaxMana;
            valueSlider.value = player.CurrentMana;
            valueText.text = player.CurrentMana + " / " + player.MaxMana;
        }

        UpdateColorBar(player.CurrentHealth, player.MaxHealth);
    }

    void UpdateColorBar(int currentValue, int maxValue)
    {
        float currentPercent = ((float)currentValue / (float)maxValue) * 100;

        if (currentPercent >= 75)
        {
            valueBar.color = high;
        } else if(currentPercent < 75 && currentPercent >= 25)
        {
            valueBar.color = mid;
        } else
        {
            valueBar.color = low;
        }
    }
}
