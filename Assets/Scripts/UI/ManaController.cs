#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    public Text valueText;
    public Slider valueSlider;
    public Image valueBar;
    public Color low;
    public Color mid;
    public Color high;

    public PlayerStats player;

    void Start()
    {
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
        player.playerManaSignal.Raise();
    }
    public void UpdateMana()
    {
        if (player.CurrentMana >= 0)
        {

            valueSlider.maxValue = player.MaxMana;
            valueSlider.value = player.CurrentMana;
            valueText.text = player.CurrentMana + " / " + player.MaxMana;
        }

        UpdateColorBar(player.CurrentMana, player.MaxMana);
    }

    void UpdateColorBar(int currentValue, int maxValue)
    {
        float currentPercent = ((float)currentValue / (float)maxValue) * 100;

        if (currentPercent >= 75)
        {
            valueBar.color = high;
        }
        else if (currentPercent < 75 && currentPercent >= 25)
        {
            valueBar.color = mid;
        }
        else
        {
            valueBar.color = low;
        }
    }
}
