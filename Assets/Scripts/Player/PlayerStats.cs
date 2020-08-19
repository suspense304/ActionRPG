#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Current Values")]
    [SerializeField] int attack;
    [SerializeField] int currentMana;
    [SerializeField] int currentHealth;
    [SerializeField] int gold;
    [SerializeField] int level;
    [SerializeField] int maxMana;
    [SerializeField] int maxHealth;
    [SerializeField] int xp;
    [SerializeField] int xpToNextLevel;

    [Header("Modifiers")]
    [SerializeField] float healthModifier;
    [SerializeField] float xpModifier;

    [Header("References")]
    [SerializeField] public GameObject player;
    [SerializeField] public SignalSender playerGoldSignal;
    [SerializeField] public SignalSender playerHealthSignal;
    [SerializeField] public SignalSender playerManaSignal;

    private int baseXp = 40;
    public int MaxHealth { get => maxHealth; }
    public int Attack { get => attack; }
    public int Level { get => level; }
    public int XP { get => xp; }
    public int XPtoNextLevel1 { get => xpToNextLevel; }
    public int CurrentHealth { get => currentHealth; }
    public int CurrentMana { get => currentMana; }
    public int MaxMana { get => maxMana; }
    public int Gold { get => gold; }


    void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Manager");
        
        if(players.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (playerHealthSignal != null)
        {
            playerHealthSignal.Raise();
        }

        if (playerManaSignal != null)
        {
            playerManaSignal.Raise();
        }

        UpdateStats();
    }

    public void UpdateStats()
    {
        playerHealthSignal.Raise();
        playerManaSignal.Raise();
        playerGoldSignal.Raise();
    }

    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
    }

    public void IncreaseHealth(int addedHealth)
    {
        currentHealth += addedHealth;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        playerHealthSignal.Raise();
    }

    public void SetAttack(int newAttack)
    {
        attack = newAttack;
    }

    public void ChangeGold(int amount)
    {
        gold += amount;
        playerGoldSignal.Raise();
    }

    public void UseAbilityPower(int abilityPoints)
    {
        currentMana -= abilityPoints;
        playerManaSignal.Raise();
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
        currentMana = maxMana;
        SetAttack(attack + 1);
        SetNextLevelXP();
        xp = 0;
        UpdateStats();
    }

    public void AddXP(int xpGained)
    {
        xp += xpGained;
        CheckNewLevel();
    }

    public void CheckNewLevel()
    {
        if (xp >= xpToNextLevel)
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
