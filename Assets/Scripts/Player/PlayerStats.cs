using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] int attack;
    [SerializeField] int level;
    [SerializeField] float healthModifier;
    [SerializeField] int xp;
    [SerializeField] int xpToNextLevel;
    [SerializeField] float xpModifier;

    public GameObject player;
    public UIHealthController UI;

    private int baseXp = 40;

    public int MaxHealth { get => maxHealth; }
    public int Attack { get => attack; }
    public int Level { get => level; }
    public int XP { get => xp; }
    public int XPtoNextLevel1 { get => xpToNextLevel; }
    public int CurrentHealth { get => currentHealth; }
    public SignalSender playerHealthSignal;

    void Awake()
    {
        if(UI != null) UI.UpdateHealth();
    }

    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
    }

    public void SetAttack(int newAttack)
    {
        attack = newAttack;
    }

    public void SetNextLevelXP()
    {
        xpToNextLevel = Mathf.CeilToInt(baseXp * level * xpModifier);
    }

    public void IncrementLevel()
    {
        level++;
        SetMaxHealth(maxHealth + (Mathf.CeilToInt(Level * healthModifier)));
        currentHealth = maxHealth;
        SetAttack(attack + 1);
        SetNextLevelXP();
        xp = 0;
        playerHealthSignal.Raise();
    }

    public void AddXP(int xpGained)
    {
        xp += xpGained;
        CheckNewLevel();
    }

    public void CheckNewLevel()
    {
        if(xp >= xpToNextLevel)
        {
            IncrementLevel();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        PlayerSoundManager.instance.PlayHitSound();
        playerHealthSignal.Raise();
        CheckForDeath();
        
    }

    public void CheckForDeath()
    {
        if (currentHealth <= 0)
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {
        player.SetActive(false);
    }
}
