using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle, 
    walk,
    attack,
    stagger,
    defend
}
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] public int health;
    [SerializeField] string enemyName;
    [SerializeField] int baseAttack;
    [SerializeField] public float moveSpeed;
    [SerializeField] int XP;
    [SerializeField] EnemySoundManager enemySoundManager;

    public PlayerStats player;

    public EnemyState currentState;

    void Awake()
    {
        health = enemyHealth.baseHealth;
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        enemySoundManager.EnemyHitSound();
        CheckForDeath();
    }

    public void CheckForDeath()
    {
        if (health <= 0)
        {
            enemySoundManager.EnemyDeathSound();
            EnemyDead();
        }
    }

    public void EnemyDead()
    {
        player.AddXP(XP);
        this.gameObject.SetActive(false);
        
    }
    public void Knockback(Rigidbody2D rb, float knockbackDuration, int damage)
    {
        StartCoroutine(KnockbackWait(rb, knockbackDuration));
        TakeDamage(damage);
    }

    IEnumerator KnockbackWait(Rigidbody2D rb, float knockbackDuration)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }


}
